using System;
using System.Net;
using Polygon.Diagnostics;

namespace Polygon.Connector
{
    /// <summary>
    ///     В этом классе определяются методы для записи свойств различных объектов в лог
    /// </summary>
    internal static class LogFields
    {
        public static ILogField Message(object value) => LogMessage.Field(LogFieldNames.Message, value);
        public static ILogField Message(string value) => LogMessage.Field(LogFieldNames.Message, value);
        public static ILogField TransactionId(Guid value) => LogMessage.Field(LogFieldNames.TransactionId, value);
        public static ILogField TransactionId(object value) => LogMessage.Field(LogFieldNames.TransactionId, value);
        public static ILogField ExchangeOrderId(string value) => LogMessage.Field(LogFieldNames.ExchangeOrderId, value);
        public static ILogField ExchangeOrderId(long value) => LogMessage.Field(LogFieldNames.ExchangeOrderId, value);
     
        public static ILogField Id(string value) => LogMessage.Field(LogFieldNames.Id, value);
        public static ILogField Id(int value) => LogMessage.Field(LogFieldNames.Id, value);
        public static ILogField Id(uint value) => LogMessage.Field(LogFieldNames.Id, value);
        public static ILogField Id(object value) => LogMessage.Field(LogFieldNames.Id, value);
     
        public static ILogField State(object value) => LogMessage.Field(LogFieldNames.State, value);
        public static ILogField Login(string value) => LogMessage.Field(LogFieldNames.Login, value);
        public static ILogField Price(decimal value) => LogMessage.Field(LogFieldNames.Price, value);
        public static ILogField Price(decimal? value) => LogMessage.Field(LogFieldNames.Price, value);
        public static ILogField Quantity(int value) => LogMessage.Field(LogFieldNames.Quantity, value, format: "+0");
        public static ILogField Quantity(uint value) => LogMessage.Field(LogFieldNames.Quantity, value);
        public static ILogField Quantity(uint? value) => LogMessage.Field(LogFieldNames.Quantity, value);
        public static ILogField ActiveQuantity(int value) => LogMessage.Field(LogFieldNames.ActiveQuantity, value, format: "+0");
        public static ILogField ConnectionStatus(object value) => LogMessage.Field(LogFieldNames.ConnectionStatus, value);
        public static ILogField Result(object value) => LogMessage.Field(LogFieldNames.Result, value);
        public static ILogField URL(string value) => LogMessage.Field(LogFieldNames.URL, value);
        public static ILogField Level(object value) => LogMessage.Field(LogFieldNames.Level, value);
        public static ILogField Status(object value) => LogMessage.Field(LogFieldNames.Status, value);
        public static ILogField ContractId(uint value) => LogMessage.Field(LogFieldNames.ContractId, value);
        public static ILogField Symbol(string value) => LogMessage.Field(LogFieldNames.Symbol, value);
        public static ILogField CorrectPriceScale(double value) => LogMessage.Field(LogFieldNames.CorrectPriceScale, value);
        public static ILogField DisplayPriceScale(uint value) => LogMessage.Field(LogFieldNames.DisplayPriceScale, value);
        public static ILogField Instrument(object value) => LogMessage.Field(LogFieldNames.Instrument, value);
        public static ILogField RequestId(uint value) => LogMessage.Field(LogFieldNames.RequestId, value);
        public static ILogField IsSnapshot(bool value) => LogMessage.Field(LogFieldNames.IsSnapshot, value);
        public static ILogField ChainOrderId(string value) => LogMessage.Field(LogFieldNames.ChainOrderId, value);
        public static ILogField AccountId(int value) => LogMessage.Field(LogFieldNames.AccountId, value);
        public static ILogField Account(object value) => LogMessage.Field(LogFieldNames.Account, value);
        public static ILogField ExecOrderId(string value) => LogMessage.Field(LogFieldNames.ExecOrderId, value);
        public static ILogField ClOrderId(string value) => LogMessage.Field(LogFieldNames.ClOrderId, value);
        public static ILogField Transaction(object value) => LogMessage.Field(LogFieldNames.Transaction, value);
        public static ILogField IP(IPAddress value) => LogMessage.Field(LogFieldNames.IP, value);
        public static ILogField Port(int value) => LogMessage.Field(LogFieldNames.Port, value);
        public static ILogField Exchange(object value) => LogMessage.Field(LogFieldNames.Exchange, value);
        public static ILogField Code(string value) => LogMessage.Field(LogFieldNames.Code, value);
        public static ILogField Attempt(int value) => LogMessage.Field(LogFieldNames.Attempt, value);
        public static ILogField Span(object value) => LogMessage.Field(LogFieldNames.Span, value);
        public static ILogField RequestId(Guid value) => LogMessage.Field(LogFieldNames.RequestId, value);

        public static ILogField ExchangeId(uint value) => LogMessage.Field(LogFieldNames.ExchangeId, value);
        public static ILogField ExchangeId(string value) => LogMessage.Field(LogFieldNames.ExchangeId, value);
        public static ILogField ErrorCode(int value) => LogMessage.Field(LogFieldNames.ErrorCode, value);
        public static ILogField ErrorCode(string value) => LogMessage.Field(LogFieldNames.ErrorCode, value);
        public static ILogField PendingForCancel(bool value) => LogMessage.Field(LogFieldNames.PendingForCancel, value);
        public static ILogField Comment(string value) => LogMessage.Field(LogFieldNames.Comment, value);
        public static ILogField IsinId(int value) => LogMessage.Field(LogFieldNames.IsinId, value);
        public static ILogField ClientCode(string value) => LogMessage.Field(LogFieldNames.ClientCode, value);
        public static ILogField QueueSize(int value) => LogMessage.Field(LogFieldNames.QueueSize, value);
        public static ILogField PenaltyRemain(int value) => LogMessage.Field(LogFieldNames.PenaltyRemain, value);
        public static ILogField SessionBeginTime(DateTime value) => LogMessage.Field(LogFieldNames.SessionBeginTime, value);
        public static ILogField SessionEndTime(DateTime value) => LogMessage.Field(LogFieldNames.SessionEndTime, value);
        public static ILogField EveningSessionBeginTime(DateTime value) => LogMessage.Field(LogFieldNames.EveningSessionBeginTime, value);
        public static ILogField EveningSessionEndTime(DateTime value) => LogMessage.Field(LogFieldNames.EveningSessionEndTime, value);
    }
}