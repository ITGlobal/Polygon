-- флаг проставляется в false в колбэке OnStop - нажатие на кнопку остановки скрипта в квике
running = true
logLevel = 'debug'

function areWeOutOfQuik()
  if getScriptPath then 
    return false
  else 
    return true 
  end
end

-- определение директории, в которой выполняется скрипт
function workingDirectory()
  -- функция getScriptPath есть только в окружении квика
  if getScriptPath then 
    return getScriptPath() 
  else 
    fileNamePos, temp = string.find(arg[0],"itgQuikLuaServer.lua")
    fileDirectoryPath = string.sub(arg[0],0,fileNamePos-1)
    return fileDirectoryPath
  end
end

package.path = package.path .. ";" .. workingDirectory() .. "\\?.lua;" .. workingDirectory() .. "\\?.luac"..";"..".\\?.lua;"..".\\?.luac"
package.cpath = package.cpath .. ";" .. workingDirectory() .. '\\?.dll'..";"..'.\\?.dll' .. ";" .. workingDirectory() .. '\\?51.dll'..";"..'.\\?51.dll' .. ";" .. workingDirectory() .. '\\?5.1.dll'..";"..'.\\?5.1.dll'

local owLogsPath = ""
local appDataFolder = os.getenv('LOCALAPPDATA')
local myDocsFolder = os.getenv('HOMEPATH')
if appDataFolder ~= nil then
  owLogsPath = appDataFolder .. "\\Application\\Logs\\"
elseif myDocsFolder ~= nil then
  owLogsPath = myDocsFolder .. "\\"  
else
  owLogsPath = "c:\\temp\\"
end

-- Имя лог файла
local logFileFullPath = owLogsPath .. "itgQuikLuaServer.txt"


function initLog()
  local logFile = io.open(logFileFullPath, "w+t")	

  if logFile ~= nil then
    message("Lua log file created at " .. logFileFullPath, 1)
    logFile:close()
  else
    message("Lua log file WASN'T created at " .. logFileFullPath .. " Logging is disabled.", 3)
  end
end 

initLog()

-- функция логгирования
function trace(level, message)  
  if level=='debug' and logLevel~='debug' then
    return
  end
  local local_t = os.date("*t", os.time())
  local mess=""
  if message ~= nil then
    mess=message
  end
  
  local logFile = io.open(logFileFullPath, "a")	
  if logFile == nil then
    return
  end
  
  logFile:write(os.date("%H:%M:%S",os.time()) .. " " .. level .. ": " .. mess .. "\n")
  logFile:flush()
  logFile:close()
end

-- путь директории, в которой расположен файл скрипта
trace('debug','Working directory is ' .. workingDirectory())

if not areWeOutOfQuik() then
  package.loadlib(workingDirectory()  .. "\\lua51.dll", "main")  
  package.loadlib(workingDirectory()  .. "\\cjson.dll", "cjson")  
  package.loadlib(workingDirectory()  .. "\\socket\\core.dll", "socket.core")      
end

--local json = require "cjson"
require("quikCallbacks")
cjson = require("cjson")
require("string")

transport = require("transport")
require("clientInit")
hp = require("historyProvider")
gp = require("glassProvider")
ip = require("instrumentParamsProvider")
router = require("orderRouter")

keep = cjson.encode_keep_buffer(false)

-- sleep
function threadSleep(t)
  -- функция sleep доступна только в квике
  if sleep then
    sleep(t)
  else
    socket.select(nil, nil, t / 1000)
  end
end

-- обработчик сообщения от клиента
function clientMessageHandler(message, messageJson)  
  
  -- обработка настроек
  if message.message_type == transport.messageTypes.QuikSideSettings then    
    ip:Setup(message)
    gp:Setup(message)
  end
  
  -- обработка запроса исторических данных  
  if message.message_type == transport.messageTypes.CandlesRequest then    
    hp.SendCandlesOnce(message)
  end
  
  -- обработка подписки на исторические данные
  if message.message_type == transport.messageTypes.CandlesSubscription then    
    hp.SubscribeCandles(message)
  end
  
  -- обработка подписки на стакан
  if message.message_type == transport.messageTypes.OrderBookSubscriptionRequest then    
    gp:subscribe(message)
  end
  
  -- обработка отписки от стакана
  if message.message_type == transport.messageTypes.OrderBookUnsubscriptionRequest then    
    gp:unsubscribe(message)
  end
  
  -- подписка на параметры инструмента
  if message.message_type == transport.messageTypes.InstrumentParamsSubscriptionRequest then    
    ip:subscribe(message)
  end
  
  -- отписка от параметров инструмента
  if message.message_type == transport.messageTypes.InstrumentParamsUnsubscriptionRequest then    
    ip:unsubscribe(message)
  end
  
  -- транзакция
  if message.message_type == transport.messageTypes.Transaction then
    router:processTransaction(message, messageJson)
  end
  
end


-- обработчик события подключения клиента к серверу (мы в сервере)
function clientConnectedHandler()  
  initClient()
end

-- обработчик события отодключения клиента от сервера (мы в сервере)
function clientDisconnectedHandler()  
  deinitClient()
end

local prevTime = os.time()

local function SendHeartbeat()
  -- отправляем раз в секунду
  if os.time() - prevTime > 1 then
      prevTime = os.time()
      msg={};
      msg.message_type = transport.messageTypes.Heartbeat
            
      msg.time = getInfoParam ("SERVERTIME")
            
      --msg.isTrading = true

      -- NOTE значения STARTTIME, ENDTIME, EVNSTARTTIME, EVNENDTIME
      --      есть только на 2 ближайших фьючерса, по остальным приходят пустые строки

      par = getParamEx("SPBFUT",  "RIZ7", "STARTTIME") 
      msg.startTime = par.param_image
      
      par = getParamEx("SPBFUT",  "RIZ7", "ENDTIME") 
      msg.endTime = par.param_image
      
      par = getParamEx("SPBFUT",  "RIZ7", "EVNSTARTTIME") 
      msg.evnStartTime = par.param_image
      
      par = getParamEx("SPBFUT",  "RIZ7", "EVNENDTIME") 
      msg.evnEndTime = par.param_image
      
--      trace("info", transport.serialize(msg))
      transport.putMessageIfClientConnected(msg)
    end
end
--par = getParamEx(class,  sec, "") 
-- 
--20 TRADINGSTATUS STRING Состояние сессии 
--94 STARTTIME STRING Начало основной сессии 
--95 ENDTIME STRING Окончание основной сессии 
--96 EVNSTARTTIME STRING Начало вечерней сессии 
--97 EVNENDTIME STRING Окончание вечерней сессии 
--98 MONSTARTTIME STRING Начало утренней сессии 
--99 MONENDTIME STRING Окончание утренней сессии 

-- основной цикл работы адаптера
function workingCycle()
  transport.init(clientConnectedHandler, clientDisconnectedHandler, clientMessageHandler)
  
  while running do
    transport.cycle()
    threadSleep(10)       
    SendHeartbeat()
  end
  
  transport.dispose()
end

function stopWorkingCycle()
  running=false  
end

-- при запуске в квике вызовется кобэк main (см. QuikCallbacks.lua)
if areWeOutOfQuik() then
  trace("info", "Running out of QUIK")
  workingCycle()
end

