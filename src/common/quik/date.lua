luadate={}

function luadate.parseDateTime(dateString)
    --2016-08-21T15:42:27.4579388+03:00
    dateString = dateString:sub(1, 19)                      
    local pattern = "(%d+)%-(%d+)%-(%d+)T(%d+):(%d+):(%d+)"
    local xyear, xmonth, xday, xhour, xminute, xseconds = dateString:match(pattern)
    local convertedTimestamp = os.time(
                                    {
                                        year = xyear, 
                                        month = xmonth, 
                                        day = xday, 
                                        hour = xhour, 
                                        min = xminute, 
                                        sec = xseconds
                                    })
        
    return convertedTimestamp 
end

return luadate