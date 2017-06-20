-- набор функций для инициализации клиента, отправки ему необходимой информации после подключения

require("quikCallbacks")
transport = require("transport")
gp = require("glassProvider")
ip = require("instrumentParamsProvider")

-- отправить список инструментов и параметры
local function sendInstrumentsListAndParams()
  msg={}
  msg.message_type = transport.messageTypes.InstrumentsList
  msg.futures = getClassSecurities("SPBFUT")
  msg.options = getClassSecurities("SPBOPT")
  transport.putMessage(msg)
  
  trace("info", "SNAPSHOT of futures params")
  for code in string.gmatch(msg.futures, "%w+") do     
    OnParam("SPBFUT", code)
  end  
  
  trace("info", "SNAPSHOT of options params")
  for code in string.gmatch(msg.options, "%w+") do     
    OnParam("SPBOPT", code)
  end
end

-- отправить счета
local function sendAccounts()
  msg={}
  msg.message_type = transport.messageTypes.AccountsList
  msg.accounts = {}
  
  local n = getNumberOf("client_codes")
  trace("info", "Number of accounts is " .. tostring(n))
  
  for i=0,n-1,1 do 
    local clientCode = getItem("client_codes", i)
    trace("info", "Account = " .. clientCode)
    
    if string.len(clientCode) > 0 then    
      table.insert(msg.accounts, clientCode)
    end
  end
  if table.getn(msg.accounts) == 0 then
    return
  end
  transport.putMessage(msg)  
end

-- отправка клиенту всех его сделок
local function sendFills()  
  local n = getNumberOf('trades')
  trace("info", "Fills = " .. tostring(n))
  
  for i=0,n-1,1 do     
    router:processFill(getItem("trades", i))
  end  
end

-- отправка клиенту всех его заявок
local function sendOrders()  
  local n = getNumberOf('orders')
  trace("info", "Orders = " .. tostring(n))
  
  for i=0,n-1,1 do     
    router:processOrderStateChange(getItem("orders", i))
  end  
end

-- отправка клиенту его позиций
local function sendPositions()  
  local n = getNumberOf('futures_client_holding')
  trace("info", "Positions = " .. tostring(n))
  
  for i=0,n-1,1 do     
    router:futuresClientHolding(getItem("futures_client_holding", i))
  end  
end

-- отправка клиенту его денежных лимитов
local function sendMoney()
  local n = getNumberOf('futures_client_limits')
  trace("info", "Money positions = " .. tostring(n))
  
  for i=0,n-1,1 do     
    router:futuresLimitChange(getItem("futures_client_limits", i))
  end  
  
end

-- отправить инициализирующие сообщения клиенту
function initClient()
  trace("info", "========================== BEGIN INIT ==========================")
  
  beginInit={}
  beginInit.message_type = transport.messageTypes.InitBegin  
  transport.putMessage(beginInit)
  
  sendInstrumentsListAndParams()  
  sendAccounts()
  sendMoney()
  sendPositions()
  sendOrders()
  sendFills()
  
  endInit={}
  endInit.message_type = transport.messageTypes.InitEnd
  transport.putMessage(endInit)
  
  trace("info", "=========================== END INIT ===========================")
end

-- отписать клиента отовсего
function deinitClient()
  gp:clearSubscriptions()
  ip:clearSubscriptions()
end


