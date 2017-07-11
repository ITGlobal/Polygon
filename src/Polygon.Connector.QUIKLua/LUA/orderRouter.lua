--require("quikCallbacks")
transport = require("transport")

router = {}

-- отправка траназакции квику
function router:processTransaction(transaction, tJson)
  trace("info", "NEW TRANSACTION: " .. tJson)
  local ok = sendTransaction(transaction)
  if ok ~= "" then
    trace("error", "Error while sending transaction: " .. ok)
    err={}
    err.message_type = transport.messageTypes.TransactionReply
    err.trans_id = transaction.TRANS_ID
    err.status = 4
    err.result_msg = ok
    transport.putMessage(err)
  end  
end

-- обработка результата отправки транзакции
function router:processTransactionReply(transReply)
  trace("info", "TRANSACTION REPLY: " .. cjson.encode(transReply))
  transReply.message_type = transport.messageTypes.TransactionReply
  transport.putMessage(transReply)
end
 
-- обработка изменения состояния заявки
function router:processOrderStateChange(order)
  trace("info", "ORDER STATE CHANGE: " .. cjson.encode(order))
  order.message_type = transport.messageTypes.OrderStateChange
  transport.putMessage(order)
end

-- обработка сделки
function router:processFill(fill)  
  trace("info", "FILL: " .. cjson.encode(fill))
  fill.message_type = transport.messageTypes.Fill
  transport.putMessage(fill) 
end

-- изменение денег
function router:futuresLimitChange(moneyPosition)
  trace("info", "MONEY CHANGE: " .. cjson.encode(moneyPosition))
  moneyPosition.message_type = transport.messageTypes.MoneyPosition
  transport.putMessage(moneyPosition)
end

-- изменение позиции по инструменту
function router:futuresClientHolding(position)  
  trace("info", "POSITION CHANGE: " .. cjson.encode(position))
  position.message_type = transport.messageTypes.Position  
  transport.putMessage(position)
end

return router