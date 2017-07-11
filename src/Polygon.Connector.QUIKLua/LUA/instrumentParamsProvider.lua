transport = require("transport")

local ip={}
ip.subscriptions={}
ip.receiveMarketdata = true

-- выставление настроек, полученных от клиента
function ip:Setup(settings)
  ip.receiveMarketdata = settings.receiveMarketdata 
end

-- подписаться на инструмента
function ip:subscribe(request)
  trace("info", "Instrument params subscription requested for " .. request.instrument)  
  local classCode = "SPBFUT"
  if string.len(request.instrument) >= 5 then classCode = "SPBOPT" end      
  
  if self.subscriptions[request.instrument] ~= nil then
    trace("warn", "Instrument params for instrument " .. request.instrument .. " was subscribed earlier")
    return
  end  
  self.subscriptions[request.instrument] = request    
  ip:processParams(classCode, request.instrument) -- форсим отправку параметров
end


-- отписаться от инструмента
function ip:unsubscribe(request)
  trace("info", "Instrument params UNsubscription for " .. request.instrument)  
  local classCode = "SPBFUT"
  if string.len(request.instrument) >= 5 then classCode = "SPBOPT" end      
  
  if self.subscriptions[request.instrument] == nil then
    trace("warn", "Instrument params for instrument " .. request.instrument .. " was UNsubscribed earlier")
    return
  end
  
  self.subscriptions[request.instrument] = nil
  
end


function ip:processParams( class, sec )
  
  if ip.receiveMarketdata == false then
    return
  end
  
  -- если нет подписки, то не обрабатываем
  if self.subscriptions[sec] == nil then
    return
  end
  
  if class =="SPBFUT" or class =="SPBOPT" then 
    local p={}
    p.message_type = transport.messageTypes.InstrumentParams
    p.code = sec
    par = getParamEx(class,  sec, "CLASS_CODE") 	
    p.classCode = par.param_image    
    par = getParamEx(class,  sec, "LONGNAME") 	
    p.fullCode = par.param_image    
    par = getParamEx(class,  sec, "BID") 
    p.bid = tonumber(par.param_value)
    par = getParamEx(class,  sec, "OFFER") 
    p.offer = tonumber(par.param_value)
    par = getParamEx(class,  sec, "BIDDEPTH") 
    p.bidQuantity  = tonumber(par.param_value)
    par = getParamEx(class,  sec, "OFFERDEPTH") 
    p.offerQuantity  = tonumber(par.param_value)
    par = getParamEx(class,  sec, "LAST") 
    p.last = tonumber(par.param_value)

    par = getParamEx(class,  sec, "SETTLEPRICE") 
    p.settlement = tonumber(par.param_value)
    par = getParamEx(class,  sec, "PREVSETTLEPRICE") 
    p.previousSettlement = tonumber(par.param_value)

    par = getParamEx(class,  sec, "SEC_PRICE_STEP") 
    p.priceStep = tonumber(par.param_value)

    -- Стоимость шага цены
    par = getParamEx(class,  sec, "STEPPRICET")
    p.stepPriceT = tonumber(par.param_value)

    -- Стоимость шага цены (для новых контрактов FORTS и RTS Standard)
    par = getParamEx(class,  sec, "STEPPRICE")
    p.stepPrice = tonumber(par.param_value)

    -- Стоимость шага цены для клиринга
    par = getParamEx(class,  sec, "STEPPRICECL")
    p.stepPriceCl = tonumber(par.param_value)

    -- Стоимость шага цены для промклиринга
    par = getParamEx(class,  sec, "STEPPRICEPRCL")
    p.stepPricePrCl = tonumber(par.param_value)

	-- Окончание основной сессии
    par = getParamEx(class,  sec, "ENDTIME")
    p.endTime = par.param_image

    par = getParamEx(class,  sec, "TIME") 
    p.time = tonumber(par.param_value)
    par = getParamEx(class,  sec, "STRIKE") 
    p.strike = tonumber(par.param_value)
    par = getParamEx(class,  sec, "VOLATILITY") 
    p.volatility = tonumber(par.param_value)
    par = getParamEx(class,  sec, "LOTSIZE") 
    p.lotsize = tonumber(par.param_value)    
    par = getParamEx(class,  sec, "OPTIONTYPE") 
    p.optiontype = par.param_image
    par = getParamEx(class,  sec, "OPTIONBASE") 
    p.optionbase = par.param_image    
    par = getParamEx(class,  sec, "PRICEMAX") 
    p.pricemax = par.param_image
    par = getParamEx(class,  sec, "PRICEMIN") 
    p.pricemin = par.param_image    
	par = getParamEx(class,  sec, "NUMCONTRACTS") 
    p.openinterest = tonumber(par.param_image)
    --p.BIGFIELD = ""
    
    -- кладём обновление параметров в очередь на отправку только если клиент подключен
    transport.putMessageIfClientConnected(p)    
  end
end


-- удалить все подписки
function ip:clearSubscriptions()
  trace("info", "Unsubscribe client from all instrumens")
  self.subscriptions = {}
end

return ip


--Список возможных идентификаторов параметров:
--1 STATUS STRING Статус 
--2 LOTSIZE NUMERIC Размер лота 
--3 BID NUMERIC Лучшая цена спроса 
--4 BIDDEPTH NUMERIC Спрос по лучшей цене 
--5 BIDDEPTHT NUMERIC Суммарный спрос 
--6 NUMBIDS NUMERIC Количество заявок на покупку 
--7 OFFER NUMERIC Лучшая цена предложения 
--8 OFFERDEPTH NUMERIC Предложение по лучшей цене 
--9 OFFERDEPTHT NUMERIC Суммарное предложение 
--10 NUMOFFERS NUMERIC Количество заявок на продажу 
--11 OPEN NUMERIC Цена открытия 
--12 HIGH NUMERIC Максимальная цена сделки 
--13 LOW NUMERIC Минимальная цена сделки 
--14 LAST NUMERIC Цена последней сделки 
--15 CHANGE NUMERIC Разница цены последней к предыдущей сессии 
--16 QTY NUMERIC Количество бумаг в последней сделке 
--17 TIME STRING Время последней сделки 
--18 VOLTODAY NUMERIC Количество бумаг в обезличенных сделках 
--19 VALTODAY NUMERIC Оборот в деньгах 
--20 TRADINGSTATUS STRING Состояние сессии 
--21 VALUE NUMERIC Оборот в деньгах последней сделки 
--22 WAPRICE NUMERIC Средневзвешенная цена 
--23 HIGHBID NUMERIC Лучшая цена спроса сегодня 
--24 LOWOFFER NUMERIC Лучшая цена предложения сегодня 
--25 NUMTRADES NUMERIC Количество сделок за сегодня 
--26 PREVPRICE NUMERIC Цена закрытия 
--27 PREVWAPRICE NUMERIC Предыдущая оценка 
--28 CLOSEPRICE NUMERIC Цена периода закрытия 
--29 LASTCHANGE NUMERIC % изменения от закрытия 
--30 PRIMARYDIST STRING Размещение 
--31 ACCRUEDINT NUMERIC Накопленный купонный доход 
--32 YIELD NUMERIC Доходность последней сделки 
--33 COUPONVALUE NUMERIC Размер купона 
--34 YIELDATPREVWAPRICE NUMERIC Доходность по предыдущей оценке 
--35 YIELDATWAPRICE NUMERIC Доходность по оценке 
--36 PRICEMINUSPREVWAPRICE NUMERIC Разница цены последней к предыдущей оценке 
--37 CLOSEYIELD NUMERIC Доходность закрытия 
--38 CURRENTVALUE NUMERIC Текущее значение индексов Московской Биржи 
--39 LASTVALUE NUMERIC Значение индексов Московской Биржи на закрытие предыдущего дня 
--40 LASTTOPREVSTLPRC NUMERIC Разница цены последней к предыдущей сессии 
--41 PREVSETTLEPRICE NUMERIC Предыдущая расчетная цена 
--42 PRICEMVTLIMIT NUMERIC Лимит изменения цены 
--43 PRICEMVTLIMITT1 NUMERIC Лимит изменения цены T1 
--44 MAXOUTVOLUME NUMERIC Лимит объема активных заявок (в контрактах) 
--45 PRICEMAX NUMERIC Максимально возможная цена 
--46 PRICEMIN NUMERIC Минимально возможная цена 
--47 NEGVALTODAY NUMERIC Оборот внесистемных в деньгах 
--48 NEGNUMTRADES NUMERIC Количество внесистемных сделок за сегодня 
--49 NUMCONTRACTS NUMERIC Количество открытых позиций 
--50 CLOSETIME STRING Время закрытия предыдущих торгов (для индексов РТС) 
--51 OPENVAL NUMERIC Значение индекса РТС на момент открытия торгов 
--52 CHNGOPEN NUMERIC Изменение текущего индекса РТС по сравнению со значением открытия 
--53 CHNGCLOSE NUMERIC Изменение текущего индекса РТС по сравнению со значением закрытия 
--54 BUYDEPO NUMERIC Гарантийное обеспечение продавца 
--55 SELLDEPO NUMERIC Гарантийное обеспечение покупателя 
--56 CHANGETIME STRING Время последнего изменения 
--57 SELLPROFIT NUMERIC Доходность продажи 
--58 BUYPROFIT NUMERIC Доходность покупки 
--59 TRADECHANGE NUMERIC Разница цены последней к предыдущей сделки (FORTS, ФБ СПБ, СПВБ) 
--60 FACEVALUE NUMERIC Номинал (для бумаг СПВБ) 
--61 MARKETPRICE NUMERIC Рыночная цена вчера 
--62 MARKETPRICETODAY NUMERIC Рыночная цена 
--63 NEXTCOUPON NUMERIC Дата выплаты купона 
--64 BUYBACKPRICE NUMERIC Цена оферты 
--65 BUYBACKDATE NUMERIC Дата оферты 
--66 ISSUESIZE NUMERIC Объем обращения 
--67 PREVDATE NUMERIC Дата предыдущего торгового дня 
--68 DURATION NUMERIC Дюрация 
--69 LOPENPRICE NUMERIC Официальная цена открытия 
--70 LCURRENTPRICE NUMERIC Официальная текущая цена 
--71 LCLOSEPRICE NUMERIC Официальная цена закрытия 
--72 QUOTEBASIS STRING Тип цены 
--73 PREVADMITTEDQUOT NUMERIC Признаваемая котировка предыдущего дня 
--74 LASTBID NUMERIC Лучшая спрос на момент завершения периода торгов 
--75 LASTOFFER NUMERIC Лучшее предложение на момент завершения торгов 
--76 PREVLEGALCLOSEPR NUMERIC Цена закрытия предыдущего дня 
--77 COUPONPERIOD NUMERIC Длительность купона 
--78 MARKETPRICE2 NUMERIC Рыночная цена 2 
--79 ADMITTEDQUOTE NUMERIC Признаваемая котировка 
--80 BGOP NUMERIC БГО по покрытым позициям 
--81 BGONP NUMERIC БГО по непокрытым позициям 
--82 STRIKE NUMERIC Цена страйк 
--83 STEPPRICET NUMERIC Стоимость шага цены 
--84 STEPPRICE NUMERIC Стоимость шага цены (для новых контрактов FORTS и RTS Standard) 
--85 SETTLEPRICE NUMERIC Расчетная цена 
--86 OPTIONTYPE STRING Тип опциона 
--87 OPTIONBASE STRING Базовый актив 
--88 VOLATILITY NUMERIC Волатильность опциона 
--89 THEORPRICE NUMERIC Теоретическая цена 
--90 PERCENTRATE NUMERIC  Агрегированная ставка 
--91 ISPERCENT STRING Тип цены фьючерса 
--92 CLSTATE STRING Статус клиринга 
--93 CLPRICE NUMERIC Котировка последнего клиринга 
--94 STARTTIME STRING Начало основной сессии 
--95 ENDTIME STRING Окончание основной сессии 
--96 EVNSTARTTIME STRING Начало вечерней сессии 
--97 EVNENDTIME STRING Окончание вечерней сессии 
--98 MONSTARTTIME STRING Начало утренней сессии 
--99 MONENDTIME STRING Окончание утренней сессии 
--100 CURSTEPPRICE STRING Валюта шага цены 
--101 REALVMPRICE NUMERIC  Текущая рыночная котировка 
--102 MARG STRING Маржируемый 
--103 EXPDATE NUMERIC Дата исполнения инструмента 
--104 CROSSRATE NUMERIC Курс 
--105 BASEPRICE NUMERIC Базовый курс 
--106 HIGHVAL NUMERIC Максимальное значение (RTSIND) 
--107 LOWVAL NUMERIC Минимальное значение (RTSIND) 
--108 ICHANGE NUMERIC Изменение (RTSIND) 
--109 IOPEN NUMERIC Значение на момент открытия (RTSIND) 
--110 PCHANGE NUMERIC Процент изменения (RTSIND) 
--111 OPENPERIODPRICE NUMERIC Цена предторгового периода 
--112 MIN_CURR_LAST NUMERIC Минимальная текущая цена 
--113 SETTLECODE STRING Код расчетов по умолчанию 
--114 STEPPRICECL DOUBLE Стоимость шага цены для клиринга 
--115 STEPPRICEPRCL DOUBLE Стоимость шага цены для промклиринга 


--Список идентификаторов дополнительных параметров, доступных для функции GET_PARAM_EX:
--1 LONGNAME STRING Полное название бумаги 
--2 SHORTNAME STRING Краткое название бумаги 
--3 CODE STRING Код бумаги 
--4 CLASSNAME STRING Название класса 
--5 CLASS_CODE STRING Код класса 
--6 TRADE_DATE_CODE DOUBLE Дата торгов 
--7 MAT_DATE DOUBLE Дата погашения 
--8 DAYS_TO_MAT_DATE DOUBLE Число дней до погашения 
--9 SEC_FACE_VALUE DOUBLE Номинал бумаги 
--10 SEC_FACE_UNIT STRING Валюта номинала 
--11 SEC_SCALE DOUBLE Точность цены 
--12 SEC_PRICE_STEP DOUBLE Минимальный шаг цены 
--13 SECTYPE STRING Тип инструмента 
