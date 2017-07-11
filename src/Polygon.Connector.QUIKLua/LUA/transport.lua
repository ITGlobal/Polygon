local socket = require("socket")
local cjson = require "cjson"
local running = true
local transport={}
local server
local clientConnected = false
local waitingForClientLogged = false
local waitingForAckLogged=false
local requestHandler
local clientConnectedHandler
local clientDisconnectedHandler
local outputQueue = {} -- очередь исходящих сообщений
local sentCount=0
local lastEnvReceivedByClient=0
local maxChunkSize = 64000
-- типы сообщений
local messageTypes={}
transport.messageTypes = messageTypes

-- от квика клиенту
messageTypes.InstrumentParams               = "InstrumentParams"
messageTypes.InstrumentsList                = "InstrumentsList"
messageTypes.Heartbeat                      = "Heartbeat"
messageTypes.AccountsList                   = "AccountsList"
messageTypes.CandlesResponse                = "CandlesResponse"
messageTypes.CandlesUpdate                  = "CandlesUpdate"
messageTypes.OrderBook                      = "OrderBook"
messageTypes.TransactionReply               = "TransactionReply"
messageTypes.OrderStateChange               = "OrderStateChange"
messageTypes.Fill                           = "Fill"
messageTypes.Position                       = "Position"
messageTypes.MoneyPosition                  = "MoneyPosition"
messageTypes.InitBegin                      = "InitBegin"
messageTypes.InitEnd                        = "InitEnd"

-- от клиента квику
messageTypes.EnvelopeAcknowledgment                   = "EnvAck"
messageTypes.QuikSideSettings                         = "QuikSideSettings"
messageTypes.CandlesRequest                           = "CandlesRequest"
messageTypes.CandlesSubscription                      = "CandlesSubscription"
messageTypes.OrderBookSubscriptionRequest             = "OrderBookSubscriptionRequest"
messageTypes.OrderBookUnsubscriptionRequest           = "OrderBookUnsubscriptionRequest"
messageTypes.Transaction                              = "Transaction"
messageTypes.InstrumentParamsSubscriptionRequest      = "InstrumentParamsSubscriptionRequest"
messageTypes.InstrumentParamsUnsubscriptionRequest    = "InstrumentParamsUnsubscriptionRequest"

-- положить сообщение в очередь на отправку
function transport.putMessage(message)
  outputQueue[#outputQueue+1] = message
end

-- положить сообщение в очередь на отправку только если клиент подключен
function transport.putMessageIfClientConnected(message)
  if clientConnected then
    table.insert(outputQueue, message)
    -- outputQueue[#outputQueue+1] = message
  end
end

-- достать сообщение из очереди для обработки
local function popMessage()
  if #outputQueue == 0 then return nil end
  return table.remove(outputQueue, 1)
end

-- очистить очередь исходящих сообщений
local function clearOutputQueue()
  while #outputQueue > 0 do
    table.remove(outputQueue, 1)
  end
end

local function createServer()
  lastEnvReceivedByClient = 0
  sentCount = 0
  server = socket.bind('0.0.0.0', 1248, 0)
  server.settimeout(server, 1)
end

-- инициализация транспорта
function transport.init(clientConnectedCallback, clientDisconnectedCallback, clientMessageCallback)
  trace("info", "Initializing transport")    
  requestHandler = clientMessageCallback
  clientConnectedHandler = clientConnectedCallback
  clientDisconnectedHandler = clientDisconnectedCallback
  createServer()
end

-- остановка транспорта
function transport.dispose()
  server = nil
  client = nil
end

function transport.serialize(msg)
  local ok, jsn = pcall(cjson.encode, msg)
  if ok then
    return jsn
  else
    trace("error", jsn)
  end
end

-- формирует конверт сообщений
function transport.MakeEnvelope()
  local envelope = {}        
    envelope.body={}
    envelope.count=0
    
    if #outputQueue > 0 then trace('debug', 'Messages in queue: ' .. #outputQueue) end
    
    -- выбираем 500 (или сколько есть) сообщений и формируем конверт из них
    local outMessage = popMessage()
    while outMessage ~= nil do
      envelope.count = envelope.count + 1
      table.insert(envelope.body, outMessage)
      if envelope.count >= 500 then break end
      outMessage = popMessage()
    end
    
    return envelope
end

-- увеличивает счётчик отправленных конвертов и возвращает обновлённое значение счётчика
function transport.IncrementSentEnvelopesCounter()
  sentCount = sentCount + 1
  return sentCount
end

-- прогнать цикл транспорта
function transport.cycle()
  
  -- если нет подключенного клиента, продожаем ждать подключения
  if not clientConnected then    
    client = server.accept(server)
    if client==nil then
      if waitingForClientLogged==false then trace("Info", "Waiting for client"); waitingForClientLogged=true end
      return
    else
      clientConnected = true      
      server.close(server) -- если этого не сделать, то accept продолжает действовать и другие клиенты могут соединиться
      client.settimeout(client, 0.001)            
      trace("info", "Client connected, Sent count = " .. sentCount .. ", LastEnvReceivedByClient = " .. lastEnvReceivedByClient)     
      if clientConnectedHandler then
        clientConnectedHandler()
      end      
    end
  else       
  
  -- отправляем клиенту если:
  -- 1. что-то есть в очереди на отправку 
  -- 2. клиент прислал подтверждение о получении предыдущего конверта  
  if lastEnvReceivedByClient == sentCount then
    waitingForAckLogged = false
    local envelope = transport.MakeEnvelope()

    -- если в конверт что-то набралось, отправляем его
    if envelope.count > 0 then      
      envelope.id = transport.IncrementSentEnvelopesCounter()

      local envelopeJson = transport.serialize(envelope)
      if envelopeJson ~= nil then
        
        -- нарезаем пакет на куски разамером не более maxChunkSize (больше не пролазит через сокет)
        envelopeJson = envelopeJson .. '\n' -- закончить строку переносом нужно, чтобы на клиенте сработал stream.ReadLine
        local envelopeSize = string.len(envelopeJson)
        local sentSize = 0
        
        -- проталкиваем в сокет, сразу может не пролезть, поэтому 
        while sentSize < envelopeSize do
          
          -- Сигнатура send кое как описана тут: http://goo.gl/4GdaRh, но нужно вчитываться в текст, чтобы понять
          local ok, count, err, count2 = pcall(client.send, client, envelopeJson, sentSize+1, envelopeSize)
                    
          if err ~= nil then 
            trace("error", "Error while pcall(client.send...): " .. err) 
            count = count2
          
            -- если отправка не прошла по таймауту, то немного засыпаем, чтобы клиент прочухался
            if err == "timeout" then
              trace("warn", "Sleep 100ms after socket send timeout")
              threadSleep(100)
            end            
          end          
          
          sentSize = count          
          trace ('debug', 'Sending envelope ' .. sentCount .. '. Sent ' .. sentSize .. ' of ' .. envelopeSize)          
        end               
      
      -- ошибка сериализации конверта
      else
        trace('error', 'Envelope wasnt serialized: ' .. envelope.count .. " messages in envelope.")
      end

    end
    envelope = nil
  
  -- если клиент ещё не прислал подтверждение получения предыдущего пакета, джём его
  else
    if waitingForAckLogged == false then
      trace('debug', "Waiting for ack " .. sentCount .. " from client. Last ack received is " .. lastEnvReceivedByClient)
      waitingForAckLogged = true
    end  
  end


  -- пытаемся получить что-то от клиента    
  local status, requestString, err = pcall(client.receive, client)    

  if not status then
    trace("error", "Client receive failed: " .. err)      
  end

  -- если клиент отвалился, заново начинаем слушать входящее подключение
    if err == "closed" then
    trace("error", "Client disconnected!")
    clientConnected = false
    waitingForClientLogged = false
    clientDisconnectedHandler()      
    createServer()
    return
  end

  -- если всё нормально, то отдаём полученное сообщение подписчику
  if status and requestString then
    local ok, clientMessage = pcall(cjson.decode, requestString)
    if not ok then trace("error", clientMessage); return end     

    if clientMessage.message_type == nil then
        trace("error","Client message doesn't have a 'message_type' property")
      return            
    end

    -- если это подтверждение получения конверта
    if clientMessage.message_type == messageTypes.EnvelopeAcknowledgment then
      lastEnvReceivedByClient = clientMessage.id
      --trace("debug", "env ack " .. tostring(lastEnvReceivedByClient))
      return
    end

    -- остальное передаём подписчику, если он есть   
    if requestHandler then        
      requestHandler(clientMessage, requestString)
    end    
  end    
end  
end

return transport
