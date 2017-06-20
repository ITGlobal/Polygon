transport = require("transport")
gp = require("glassProvider")
router = require("orderRouter")
--local cjson = require "cjson"

function OnInit( script_path)
end

-- точка входа в скрипт в квике
function main()
  trace('info', 'quik main callback start')  
  workingCycle()    
end

-- остановка скрипта
function OnStop(signal)  
  stopWorkingCycle()  
end

-- ответ на отправку транзакции
function OnTransReply(trans_reply)
  if trans_reply.status > 16 then
    return
  end  
  router:processTransactionReply(trans_reply)
end

-- изменение статуса заявки
function OnOrder(orderStateChange)
  router:processOrderStateChange(orderStateChange)
end

-- новая сделка
function OnTrade(fill)
  router:processFill(fill)
end

-- изменение денег
function OnFuturesLimitChange(futLimit)
  router:futuresLimitChange(futLimit)
end

-- изменение позиции
function OnFuturesClientHolding(futOptPosition)
  router:futuresClientHolding(futOptPosition)
end

-- изменение стакана
function OnQuote(class, sec)
  gp:processGlass(class, sec)
end

-- изменение параметров инструментов
function OnParam( class, sec )
  ip:processParams( class, sec )
end

