--require("quikCallbacks")
transport = require("transport")

local gp = {}
gp.subscriptions={}
gp.receiveMarketdata = true

-- выставление настроек, полученных от клиента
function gp:Setup(settings)
  gp.receiveMarketdata = settings.receiveMarketdata 
end

function gp:processGlass(class, sec)  
  
  if ip.receiveMarketdata == false then
    return
  end
    
  -- если нет подписки, то не обрабатываем
  if self.subscriptions[sec] == nil then
    return
  end
  
  ob = getQuoteLevel2(class, sec)
  g = {}
  g.message_type = transport.messageTypes.OrderBook
  g.instrument = sec
  
  local bidCount=tonumber(ob.bid_count)
  if bidCount > 0 then
  g.bids={}
    for i = bidCount, 1, -1 do    
      if ob.bid[i].quantity ~= nil then   -- На некоторых ценах могут отсутствовать заявки
        table.insert(g.bids, {q=tonumber(ob.bid[i].quantity), p=tonumber(ob.bid[i].price)})
      end
    end
  end
  
  local offerCount=tonumber(ob.offer_count)
  if offerCount > 0 then
    g.offers={}
    for i = offerCount, 1, -1 do
      if ob.offer[i].quantity ~= nil then   -- На некоторых ценах могут отсутствовать заявки
        table.insert(g.offers, {q=tonumber(ob.offer[i].quantity), p=tonumber(ob.offer[i].price)})
      end
    end
  end
  
  transport.putMessageIfClientConnected(g)
end

-- подписаться на стакан
function gp:subscribe(request)
  trace("info", "Order book subscription requested for " .. request.instrument .. ". Request id = " .. request.id)  
  local classCode = "SPBFUT"
  if string.len(request.instrument) >= 5 then classCode = "SPBOPT" end      
  
  if self.subscriptions[request.instrument] ~= nil then
    trace("warn", "Order book for instrument " .. request.instrument .. " was subscribed earlier")
    return
  end  
  self.subscriptions[request.instrument] = request  
  Subscribe_Level_II_Quotes(classCode, request.instrument)
  gp:processGlass(classCode, request.instrument) -- формим отправку стакана
end

-- отписаться от стакана
function gp:unsubscribe(request)
  trace("info", "Order book UNsubscription for " .. request.instrument .. ". Request id = " .. request.id)  
  local classCode = "SPBFUT"
  if string.len(request.instrument) >= 5 then classCode = "SPBOPT" end      
  
  if self.subscriptions[request.instrument] == nil then
    trace("warn", "Order book for instrument " .. request.instrument .. " was UNsubscribed earlier")
    return
  end
  
  self.subscriptions[request.instrument] = nil
  
  Unsubscribe_Level_II_Quotes(classCode, request.instrument)
end

-- удалить все подписки
function gp:clearSubscriptions()
  trace("info", "Unsubscribe client from all order books")
  self.subscriptions = {}
end


return gp