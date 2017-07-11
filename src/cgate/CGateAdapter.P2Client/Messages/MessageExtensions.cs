

using System;  
using System.Diagnostics;  
using System.Text;
using JetBrains.Annotations;
using ru.micexrts.cgate.message;


namespace CGateAdapter.Messages.Clr
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "money_clearing":
                    return CGateMessageFactory.CreateCgmMoneyClearing(source);
                case "money_clearing_sa":
                    return CGateMessageFactory.CreateCgmMoneyClearingSa(source);
                case "clr_rate":
                    return CGateMessageFactory.CreateCgmClrRate(source);
                case "fut_pos":
                    return CGateMessageFactory.CreateCgmFutPos(source);
                case "opt_pos":
                    return CGateMessageFactory.CreateCgmOptPos(source);
                case "fut_pos_sa":
                    return CGateMessageFactory.CreateCgmFutPosSa(source);
                case "opt_pos_sa":
                    return CGateMessageFactory.CreateCgmOptPosSa(source);
                case "fut_sess_settl":
                    return CGateMessageFactory.CreateCgmFutSessSettl(source);
                case "opt_sess_settl":
                    return CGateMessageFactory.CreateCgmOptSessSettl(source);
                case "pledge_details":
                    return CGateMessageFactory.CreateCgmPledgeDetails(source);
                case "sys_events":
                    return CGateMessageFactory.CreateCgmSysEvents(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmMoneyClearing CreateCgmMoneyClearing(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmMoneyClearing();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_share = streamDataMessage["share"];
            if (value_share != null)
            {
                message.Share = value_share.asSByte();
            }

            var value_amount_beg = streamDataMessage["amount_beg"];
            if (value_amount_beg != null)
            {
                message.AmountBeg = value_amount_beg.asDouble();
            }

            var value_vm = streamDataMessage["vm"];
            if (value_vm != null)
            {
                message.Vm = value_vm.asDouble();
            }

            var value_premium = streamDataMessage["premium"];
            if (value_premium != null)
            {
                message.Premium = value_premium.asDouble();
            }

            var value_pay = streamDataMessage["pay"];
            if (value_pay != null)
            {
                message.Pay = value_pay.asDouble();
            }

            var value_fee_fut = streamDataMessage["fee_fut"];
            if (value_fee_fut != null)
            {
                message.FeeFut = value_fee_fut.asDouble();
            }

            var value_fee_opt = streamDataMessage["fee_opt"];
            if (value_fee_opt != null)
            {
                message.FeeOpt = value_fee_opt.asDouble();
            }

            var value_go = streamDataMessage["go"];
            if (value_go != null)
            {
                message.Go = value_go.asDouble();
            }

            var value_amount_end = streamDataMessage["amount_end"];
            if (value_amount_end != null)
            {
                message.AmountEnd = value_amount_end.asDouble();
            }

            var value_free = streamDataMessage["free"];
            if (value_free != null)
            {
                message.Free = value_free.asDouble();
            }

            var value_ext_reserve = streamDataMessage["ext_reserve"];
            if (value_ext_reserve != null)
            {
                message.ExtReserve = value_ext_reserve.asDouble();
            }

                        return message;
        } // CreateCgmMoneyClearing
        
        public static CgmMoneyClearingSa CreateCgmMoneyClearingSa(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmMoneyClearingSa();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_settlement_account = streamDataMessage["settlement_account"];
            if (value_settlement_account != null)
            {
                message.SettlementAccount = value_settlement_account.asString();
            }

            var value_share = streamDataMessage["share"];
            if (value_share != null)
            {
                message.Share = value_share.asSByte();
            }

            var value_amount_beg = streamDataMessage["amount_beg"];
            if (value_amount_beg != null)
            {
                message.AmountBeg = value_amount_beg.asDouble();
            }

            var value_vm = streamDataMessage["vm"];
            if (value_vm != null)
            {
                message.Vm = value_vm.asDouble();
            }

            var value_premium = streamDataMessage["premium"];
            if (value_premium != null)
            {
                message.Premium = value_premium.asDouble();
            }

            var value_pay = streamDataMessage["pay"];
            if (value_pay != null)
            {
                message.Pay = value_pay.asDouble();
            }

            var value_fee_fut = streamDataMessage["fee_fut"];
            if (value_fee_fut != null)
            {
                message.FeeFut = value_fee_fut.asDouble();
            }

            var value_fee_opt = streamDataMessage["fee_opt"];
            if (value_fee_opt != null)
            {
                message.FeeOpt = value_fee_opt.asDouble();
            }

            var value_go = streamDataMessage["go"];
            if (value_go != null)
            {
                message.Go = value_go.asDouble();
            }

            var value_amount_end = streamDataMessage["amount_end"];
            if (value_amount_end != null)
            {
                message.AmountEnd = value_amount_end.asDouble();
            }

            var value_free = streamDataMessage["free"];
            if (value_free != null)
            {
                message.Free = value_free.asDouble();
            }

                        return message;
        } // CreateCgmMoneyClearingSa
        
        public static CgmClrRate CreateCgmClrRate(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmClrRate();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_rate = streamDataMessage["rate"];
            if (value_rate != null)
            {
                message.Rate = value_rate.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_signs = streamDataMessage["signs"];
            if (value_signs != null)
            {
                message.Signs = value_signs.asSByte();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_rate_id = streamDataMessage["rate_id"];
            if (value_rate_id != null)
            {
                message.RateId = value_rate_id.asInt();
            }

                        return message;
        } // CreateCgmClrRate
        
        public static CgmFutPos CreateCgmFutPos(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutPos();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_account = streamDataMessage["account"];
            if (value_account != null)
            {
                message.Account = value_account.asSByte();
            }

            var value_pos_beg = streamDataMessage["pos_beg"];
            if (value_pos_beg != null)
            {
                message.PosBeg = value_pos_beg.asInt();
            }

            var value_pos_end = streamDataMessage["pos_end"];
            if (value_pos_end != null)
            {
                message.PosEnd = value_pos_end.asInt();
            }

            var value_vm = streamDataMessage["vm"];
            if (value_vm != null)
            {
                message.Vm = value_vm.asDouble();
            }

            var value_fee = streamDataMessage["fee"];
            if (value_fee != null)
            {
                message.Fee = value_fee.asDouble();
            }

            var value_accum_go = streamDataMessage["accum_go"];
            if (value_accum_go != null)
            {
                message.AccumGo = value_accum_go.asDouble();
            }

            var value_fee_ex = streamDataMessage["fee_ex"];
            if (value_fee_ex != null)
            {
                message.FeeEx = value_fee_ex.asDouble();
            }

            var value_vat_ex = streamDataMessage["vat_ex"];
            if (value_vat_ex != null)
            {
                message.VatEx = value_vat_ex.asDouble();
            }

            var value_fee_cc = streamDataMessage["fee_cc"];
            if (value_fee_cc != null)
            {
                message.FeeCc = value_fee_cc.asDouble();
            }

            var value_vat_cc = streamDataMessage["vat_cc"];
            if (value_vat_cc != null)
            {
                message.VatCc = value_vat_cc.asDouble();
            }

                        return message;
        } // CreateCgmFutPos
        
        public static CgmOptPos CreateCgmOptPos(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptPos();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_account = streamDataMessage["account"];
            if (value_account != null)
            {
                message.Account = value_account.asSByte();
            }

            var value_pos_beg = streamDataMessage["pos_beg"];
            if (value_pos_beg != null)
            {
                message.PosBeg = value_pos_beg.asInt();
            }

            var value_pos_end = streamDataMessage["pos_end"];
            if (value_pos_end != null)
            {
                message.PosEnd = value_pos_end.asInt();
            }

            var value_vm = streamDataMessage["vm"];
            if (value_vm != null)
            {
                message.Vm = value_vm.asDouble();
            }

            var value_fee = streamDataMessage["fee"];
            if (value_fee != null)
            {
                message.Fee = value_fee.asDouble();
            }

            var value_fee_ex = streamDataMessage["fee_ex"];
            if (value_fee_ex != null)
            {
                message.FeeEx = value_fee_ex.asDouble();
            }

            var value_vat_ex = streamDataMessage["vat_ex"];
            if (value_vat_ex != null)
            {
                message.VatEx = value_vat_ex.asDouble();
            }

            var value_fee_cc = streamDataMessage["fee_cc"];
            if (value_fee_cc != null)
            {
                message.FeeCc = value_fee_cc.asDouble();
            }

            var value_vat_cc = streamDataMessage["vat_cc"];
            if (value_vat_cc != null)
            {
                message.VatCc = value_vat_cc.asDouble();
            }

                        return message;
        } // CreateCgmOptPos
        
        public static CgmFutPosSa CreateCgmFutPosSa(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutPosSa();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_settlement_account = streamDataMessage["settlement_account"];
            if (value_settlement_account != null)
            {
                message.SettlementAccount = value_settlement_account.asString();
            }

            var value_pos_beg = streamDataMessage["pos_beg"];
            if (value_pos_beg != null)
            {
                message.PosBeg = value_pos_beg.asInt();
            }

            var value_pos_end = streamDataMessage["pos_end"];
            if (value_pos_end != null)
            {
                message.PosEnd = value_pos_end.asInt();
            }

            var value_vm = streamDataMessage["vm"];
            if (value_vm != null)
            {
                message.Vm = value_vm.asDouble();
            }

            var value_fee = streamDataMessage["fee"];
            if (value_fee != null)
            {
                message.Fee = value_fee.asDouble();
            }

            var value_fee_ex = streamDataMessage["fee_ex"];
            if (value_fee_ex != null)
            {
                message.FeeEx = value_fee_ex.asDouble();
            }

            var value_vat_ex = streamDataMessage["vat_ex"];
            if (value_vat_ex != null)
            {
                message.VatEx = value_vat_ex.asDouble();
            }

            var value_fee_cc = streamDataMessage["fee_cc"];
            if (value_fee_cc != null)
            {
                message.FeeCc = value_fee_cc.asDouble();
            }

            var value_vat_cc = streamDataMessage["vat_cc"];
            if (value_vat_cc != null)
            {
                message.VatCc = value_vat_cc.asDouble();
            }

                        return message;
        } // CreateCgmFutPosSa
        
        public static CgmOptPosSa CreateCgmOptPosSa(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptPosSa();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_settlement_account = streamDataMessage["settlement_account"];
            if (value_settlement_account != null)
            {
                message.SettlementAccount = value_settlement_account.asString();
            }

            var value_pos_beg = streamDataMessage["pos_beg"];
            if (value_pos_beg != null)
            {
                message.PosBeg = value_pos_beg.asInt();
            }

            var value_pos_end = streamDataMessage["pos_end"];
            if (value_pos_end != null)
            {
                message.PosEnd = value_pos_end.asInt();
            }

            var value_vm = streamDataMessage["vm"];
            if (value_vm != null)
            {
                message.Vm = value_vm.asDouble();
            }

            var value_fee = streamDataMessage["fee"];
            if (value_fee != null)
            {
                message.Fee = value_fee.asDouble();
            }

            var value_fee_ex = streamDataMessage["fee_ex"];
            if (value_fee_ex != null)
            {
                message.FeeEx = value_fee_ex.asDouble();
            }

            var value_vat_ex = streamDataMessage["vat_ex"];
            if (value_vat_ex != null)
            {
                message.VatEx = value_vat_ex.asDouble();
            }

            var value_fee_cc = streamDataMessage["fee_cc"];
            if (value_fee_cc != null)
            {
                message.FeeCc = value_fee_cc.asDouble();
            }

            var value_vat_cc = streamDataMessage["vat_cc"];
            if (value_vat_cc != null)
            {
                message.VatCc = value_vat_cc.asDouble();
            }

                        return message;
        } // CreateCgmOptPosSa
        
        public static CgmFutSessSettl CreateCgmFutSessSettl(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutSessSettl();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_date_clr = streamDataMessage["date_clr"];
            if (value_date_clr != null)
            {
                message.DateClr = value_date_clr.asDateTime();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_settl_price = streamDataMessage["settl_price"];
            if (value_settl_price != null)
            {
                message.SettlPrice = value_settl_price.asDouble();
            }

                        return message;
        } // CreateCgmFutSessSettl
        
        public static CgmOptSessSettl CreateCgmOptSessSettl(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptSessSettl();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_date_clr = streamDataMessage["date_clr"];
            if (value_date_clr != null)
            {
                message.DateClr = value_date_clr.asDateTime();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_volat = streamDataMessage["volat"];
            if (value_volat != null)
            {
                message.Volat = value_volat.asDouble();
            }

            var value_theor_price = streamDataMessage["theor_price"];
            if (value_theor_price != null)
            {
                message.TheorPrice = value_theor_price.asDouble();
            }

                        return message;
        } // CreateCgmOptSessSettl
        
        public static CgmPledgeDetails CreateCgmPledgeDetails(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmPledgeDetails();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_pledge_name = streamDataMessage["pledge_name"];
            if (value_pledge_name != null)
            {
                message.PledgeName = value_pledge_name.asString();
            }

            var value_amount_beg = streamDataMessage["amount_beg"];
            if (value_amount_beg != null)
            {
                message.AmountBeg = value_amount_beg.asDouble();
            }

            var value_pay = streamDataMessage["pay"];
            if (value_pay != null)
            {
                message.Pay = value_pay.asDouble();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asDouble();
            }

            var value_rate = streamDataMessage["rate"];
            if (value_rate != null)
            {
                message.Rate = value_rate.asDouble();
            }

            var value_amount_beg_money = streamDataMessage["amount_beg_money"];
            if (value_amount_beg_money != null)
            {
                message.AmountBegMoney = value_amount_beg_money.asDouble();
            }

            var value_pay_money = streamDataMessage["pay_money"];
            if (value_pay_money != null)
            {
                message.PayMoney = value_pay_money.asDouble();
            }

            var value_amount_money = streamDataMessage["amount_money"];
            if (value_amount_money != null)
            {
                message.AmountMoney = value_amount_money.asDouble();
            }

            var value_com_ensure = streamDataMessage["com_ensure"];
            if (value_com_ensure != null)
            {
                message.ComEnsure = value_com_ensure.asSByte();
            }

                        return message;
        } // CreateCgmPledgeDetails
        
        public static CgmSysEvents CreateCgmSysEvents(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmSysEvents();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_event_id = streamDataMessage["event_id"];
            if (value_event_id != null)
            {
                message.EventId = value_event_id.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_event_type = streamDataMessage["event_type"];
            if (value_event_type != null)
            {
                message.EventType = value_event_type.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmSysEvents
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.FortsMessages
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "FutAddOrder":
                    return CGateMessageFactory.CreateCgmFutAddOrder(source);
                case "FORTS_MSG101":
                    return CGateMessageFactory.CreateCgmFortsMsg101(source);
                case "FutAddMultiLegOrder":
                    return CGateMessageFactory.CreateCgmFutAddMultiLegOrder(source);
                case "FORTS_MSG129":
                    return CGateMessageFactory.CreateCgmFortsMsg129(source);
                case "FutDelOrder":
                    return CGateMessageFactory.CreateCgmFutDelOrder(source);
                case "FORTS_MSG102":
                    return CGateMessageFactory.CreateCgmFortsMsg102(source);
                case "FutDelUserOrders":
                    return CGateMessageFactory.CreateCgmFutDelUserOrders(source);
                case "FORTS_MSG103":
                    return CGateMessageFactory.CreateCgmFortsMsg103(source);
                case "FutMoveOrder":
                    return CGateMessageFactory.CreateCgmFutMoveOrder(source);
                case "FORTS_MSG105":
                    return CGateMessageFactory.CreateCgmFortsMsg105(source);
                case "OptAddOrder":
                    return CGateMessageFactory.CreateCgmOptAddOrder(source);
                case "FORTS_MSG109":
                    return CGateMessageFactory.CreateCgmFortsMsg109(source);
                case "OptDelOrder":
                    return CGateMessageFactory.CreateCgmOptDelOrder(source);
                case "FORTS_MSG110":
                    return CGateMessageFactory.CreateCgmFortsMsg110(source);
                case "OptDelUserOrders":
                    return CGateMessageFactory.CreateCgmOptDelUserOrders(source);
                case "FORTS_MSG111":
                    return CGateMessageFactory.CreateCgmFortsMsg111(source);
                case "OptMoveOrder":
                    return CGateMessageFactory.CreateCgmOptMoveOrder(source);
                case "FORTS_MSG113":
                    return CGateMessageFactory.CreateCgmFortsMsg113(source);
                case "FutChangeClientMoney":
                    return CGateMessageFactory.CreateCgmFutChangeClientMoney(source);
                case "FORTS_MSG104":
                    return CGateMessageFactory.CreateCgmFortsMsg104(source);
                case "FutChangeBFMoney":
                    return CGateMessageFactory.CreateCgmFutChangeBfmoney(source);
                case "FORTS_MSG107":
                    return CGateMessageFactory.CreateCgmFortsMsg107(source);
                case "OptChangeExpiration":
                    return CGateMessageFactory.CreateCgmOptChangeExpiration(source);
                case "FORTS_MSG112":
                    return CGateMessageFactory.CreateCgmFortsMsg112(source);
                case "FutChangeClientProhibit":
                    return CGateMessageFactory.CreateCgmFutChangeClientProhibit(source);
                case "FORTS_MSG115":
                    return CGateMessageFactory.CreateCgmFortsMsg115(source);
                case "OptChangeClientProhibit":
                    return CGateMessageFactory.CreateCgmOptChangeClientProhibit(source);
                case "FORTS_MSG117":
                    return CGateMessageFactory.CreateCgmFortsMsg117(source);
                case "FutExchangeBFMoney":
                    return CGateMessageFactory.CreateCgmFutExchangeBfmoney(source);
                case "FORTS_MSG130":
                    return CGateMessageFactory.CreateCgmFortsMsg130(source);
                case "OptRecalcCS":
                    return CGateMessageFactory.CreateCgmOptRecalcCs(source);
                case "FORTS_MSG132":
                    return CGateMessageFactory.CreateCgmFortsMsg132(source);
                case "FutTransferClientPosition":
                    return CGateMessageFactory.CreateCgmFutTransferClientPosition(source);
                case "FORTS_MSG137":
                    return CGateMessageFactory.CreateCgmFortsMsg137(source);
                case "OptTransferClientPosition":
                    return CGateMessageFactory.CreateCgmOptTransferClientPosition(source);
                case "FORTS_MSG138":
                    return CGateMessageFactory.CreateCgmFortsMsg138(source);
                case "OptChangeRiskParameters":
                    return CGateMessageFactory.CreateCgmOptChangeRiskParameters(source);
                case "FORTS_MSG140":
                    return CGateMessageFactory.CreateCgmFortsMsg140(source);
                case "FutTransferRisk":
                    return CGateMessageFactory.CreateCgmFutTransferRisk(source);
                case "FORTS_MSG139":
                    return CGateMessageFactory.CreateCgmFortsMsg139(source);
                case "CODHeartbeat":
                    return CGateMessageFactory.CreateCgmCodheartbeat(source);
                case "FORTS_MSG99":
                    return CGateMessageFactory.CreateCgmFortsMsg99(source);
                case "FORTS_MSG100":
                    return CGateMessageFactory.CreateCgmFortsMsg100(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmFutAddOrder CreateCgmFutAddOrder(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutAddOrder();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_type = streamDataMessage["type"];
            if (value_type != null)
            {
                message.Type = value_type.asInt();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asString();
            }

            var value_comment = streamDataMessage["comment"];
            if (value_comment != null)
            {
                message.Comment = value_comment.asString();
            }

            var value_broker_to = streamDataMessage["broker_to"];
            if (value_broker_to != null)
            {
                message.BrokerTo = value_broker_to.asString();
            }

            var value_ext_id = streamDataMessage["ext_id"];
            if (value_ext_id != null)
            {
                message.ExtId = value_ext_id.asInt();
            }

            var value_du = streamDataMessage["du"];
            if (value_du != null)
            {
                message.Du = value_du.asInt();
            }

            var value_date_exp = streamDataMessage["date_exp"];
            if (value_date_exp != null)
            {
                message.DateExp = value_date_exp.asString();
            }

            var value_hedge = streamDataMessage["hedge"];
            if (value_hedge != null)
            {
                message.Hedge = value_hedge.asInt();
            }

            var value_dont_check_money = streamDataMessage["dont_check_money"];
            if (value_dont_check_money != null)
            {
                message.DontCheckMoney = value_dont_check_money.asInt();
            }

            var value_local_stamp = streamDataMessage["local_stamp"];
            if (value_local_stamp != null)
            {
                message.LocalStamp = value_local_stamp.asDateTime();
            }

            var value_match_ref = streamDataMessage["match_ref"];
            if (value_match_ref != null)
            {
                message.MatchRef = value_match_ref.asString();
            }

                        return message;
        } // CreateCgmFutAddOrder
        
        public static CgmFortsMsg101 CreateCgmFortsMsg101(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg101();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

            var value_order_id = streamDataMessage["order_id"];
            if (value_order_id != null)
            {
                message.OrderId = value_order_id.asLong();
            }

                        return message;
        } // CreateCgmFortsMsg101
        
        public static CgmFutAddMultiLegOrder CreateCgmFutAddMultiLegOrder(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutAddMultiLegOrder();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_type = streamDataMessage["type"];
            if (value_type != null)
            {
                message.Type = value_type.asInt();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asString();
            }

            var value_rate_price = streamDataMessage["rate_price"];
            if (value_rate_price != null)
            {
                message.RatePrice = value_rate_price.asString();
            }

            var value_comment = streamDataMessage["comment"];
            if (value_comment != null)
            {
                message.Comment = value_comment.asString();
            }

            var value_hedge = streamDataMessage["hedge"];
            if (value_hedge != null)
            {
                message.Hedge = value_hedge.asInt();
            }

            var value_broker_to = streamDataMessage["broker_to"];
            if (value_broker_to != null)
            {
                message.BrokerTo = value_broker_to.asString();
            }

            var value_ext_id = streamDataMessage["ext_id"];
            if (value_ext_id != null)
            {
                message.ExtId = value_ext_id.asInt();
            }

            var value_trust = streamDataMessage["trust"];
            if (value_trust != null)
            {
                message.Trust = value_trust.asInt();
            }

            var value_date_exp = streamDataMessage["date_exp"];
            if (value_date_exp != null)
            {
                message.DateExp = value_date_exp.asString();
            }

            var value_trade_mode = streamDataMessage["trade_mode"];
            if (value_trade_mode != null)
            {
                message.TradeMode = value_trade_mode.asInt();
            }

            var value_dont_check_money = streamDataMessage["dont_check_money"];
            if (value_dont_check_money != null)
            {
                message.DontCheckMoney = value_dont_check_money.asInt();
            }

            var value_local_stamp = streamDataMessage["local_stamp"];
            if (value_local_stamp != null)
            {
                message.LocalStamp = value_local_stamp.asDateTime();
            }

            var value_match_ref = streamDataMessage["match_ref"];
            if (value_match_ref != null)
            {
                message.MatchRef = value_match_ref.asString();
            }

                        return message;
        } // CreateCgmFutAddMultiLegOrder
        
        public static CgmFortsMsg129 CreateCgmFortsMsg129(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg129();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

            var value_order_id = streamDataMessage["order_id"];
            if (value_order_id != null)
            {
                message.OrderId = value_order_id.asLong();
            }

                        return message;
        } // CreateCgmFortsMsg129
        
        public static CgmFutDelOrder CreateCgmFutDelOrder(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutDelOrder();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_order_id = streamDataMessage["order_id"];
            if (value_order_id != null)
            {
                message.OrderId = value_order_id.asLong();
            }

            var value_local_stamp = streamDataMessage["local_stamp"];
            if (value_local_stamp != null)
            {
                message.LocalStamp = value_local_stamp.asDateTime();
            }

                        return message;
        } // CreateCgmFutDelOrder
        
        public static CgmFortsMsg102 CreateCgmFortsMsg102(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg102();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

                        return message;
        } // CreateCgmFortsMsg102
        
        public static CgmFutDelUserOrders CreateCgmFutDelUserOrders(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutDelUserOrders();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_buy_sell = streamDataMessage["buy_sell"];
            if (value_buy_sell != null)
            {
                message.BuySell = value_buy_sell.asInt();
            }

            var value_non_system = streamDataMessage["non_system"];
            if (value_non_system != null)
            {
                message.NonSystem = value_non_system.asInt();
            }

            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asString();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_ext_id = streamDataMessage["ext_id"];
            if (value_ext_id != null)
            {
                message.ExtId = value_ext_id.asInt();
            }

            var value_work_mode = streamDataMessage["work_mode"];
            if (value_work_mode != null)
            {
                message.WorkMode = value_work_mode.asInt();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_local_stamp = streamDataMessage["local_stamp"];
            if (value_local_stamp != null)
            {
                message.LocalStamp = value_local_stamp.asDateTime();
            }

                        return message;
        } // CreateCgmFutDelUserOrders
        
        public static CgmFortsMsg103 CreateCgmFortsMsg103(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg103();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

            var value_num_orders = streamDataMessage["num_orders"];
            if (value_num_orders != null)
            {
                message.NumOrders = value_num_orders.asInt();
            }

                        return message;
        } // CreateCgmFortsMsg103
        
        public static CgmFutMoveOrder CreateCgmFutMoveOrder(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutMoveOrder();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_regime = streamDataMessage["regime"];
            if (value_regime != null)
            {
                message.Regime = value_regime.asInt();
            }

            var value_order_id1 = streamDataMessage["order_id1"];
            if (value_order_id1 != null)
            {
                message.OrderId1 = value_order_id1.asLong();
            }

            var value_amount1 = streamDataMessage["amount1"];
            if (value_amount1 != null)
            {
                message.Amount1 = value_amount1.asInt();
            }

            var value_price1 = streamDataMessage["price1"];
            if (value_price1 != null)
            {
                message.Price1 = value_price1.asString();
            }

            var value_ext_id1 = streamDataMessage["ext_id1"];
            if (value_ext_id1 != null)
            {
                message.ExtId1 = value_ext_id1.asInt();
            }

            var value_order_id2 = streamDataMessage["order_id2"];
            if (value_order_id2 != null)
            {
                message.OrderId2 = value_order_id2.asLong();
            }

            var value_amount2 = streamDataMessage["amount2"];
            if (value_amount2 != null)
            {
                message.Amount2 = value_amount2.asInt();
            }

            var value_price2 = streamDataMessage["price2"];
            if (value_price2 != null)
            {
                message.Price2 = value_price2.asString();
            }

            var value_ext_id2 = streamDataMessage["ext_id2"];
            if (value_ext_id2 != null)
            {
                message.ExtId2 = value_ext_id2.asInt();
            }

            var value_local_stamp = streamDataMessage["local_stamp"];
            if (value_local_stamp != null)
            {
                message.LocalStamp = value_local_stamp.asDateTime();
            }

                        return message;
        } // CreateCgmFutMoveOrder
        
        public static CgmFortsMsg105 CreateCgmFortsMsg105(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg105();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

            var value_order_id1 = streamDataMessage["order_id1"];
            if (value_order_id1 != null)
            {
                message.OrderId1 = value_order_id1.asLong();
            }

            var value_order_id2 = streamDataMessage["order_id2"];
            if (value_order_id2 != null)
            {
                message.OrderId2 = value_order_id2.asLong();
            }

                        return message;
        } // CreateCgmFortsMsg105
        
        public static CgmOptAddOrder CreateCgmOptAddOrder(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptAddOrder();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_type = streamDataMessage["type"];
            if (value_type != null)
            {
                message.Type = value_type.asInt();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asString();
            }

            var value_comment = streamDataMessage["comment"];
            if (value_comment != null)
            {
                message.Comment = value_comment.asString();
            }

            var value_broker_to = streamDataMessage["broker_to"];
            if (value_broker_to != null)
            {
                message.BrokerTo = value_broker_to.asString();
            }

            var value_ext_id = streamDataMessage["ext_id"];
            if (value_ext_id != null)
            {
                message.ExtId = value_ext_id.asInt();
            }

            var value_du = streamDataMessage["du"];
            if (value_du != null)
            {
                message.Du = value_du.asInt();
            }

            var value_check_limit = streamDataMessage["check_limit"];
            if (value_check_limit != null)
            {
                message.CheckLimit = value_check_limit.asInt();
            }

            var value_date_exp = streamDataMessage["date_exp"];
            if (value_date_exp != null)
            {
                message.DateExp = value_date_exp.asString();
            }

            var value_hedge = streamDataMessage["hedge"];
            if (value_hedge != null)
            {
                message.Hedge = value_hedge.asInt();
            }

            var value_dont_check_money = streamDataMessage["dont_check_money"];
            if (value_dont_check_money != null)
            {
                message.DontCheckMoney = value_dont_check_money.asInt();
            }

            var value_local_stamp = streamDataMessage["local_stamp"];
            if (value_local_stamp != null)
            {
                message.LocalStamp = value_local_stamp.asDateTime();
            }

            var value_match_ref = streamDataMessage["match_ref"];
            if (value_match_ref != null)
            {
                message.MatchRef = value_match_ref.asString();
            }

                        return message;
        } // CreateCgmOptAddOrder
        
        public static CgmFortsMsg109 CreateCgmFortsMsg109(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg109();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

            var value_order_id = streamDataMessage["order_id"];
            if (value_order_id != null)
            {
                message.OrderId = value_order_id.asLong();
            }

                        return message;
        } // CreateCgmFortsMsg109
        
        public static CgmOptDelOrder CreateCgmOptDelOrder(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptDelOrder();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_order_id = streamDataMessage["order_id"];
            if (value_order_id != null)
            {
                message.OrderId = value_order_id.asLong();
            }

            var value_local_stamp = streamDataMessage["local_stamp"];
            if (value_local_stamp != null)
            {
                message.LocalStamp = value_local_stamp.asDateTime();
            }

                        return message;
        } // CreateCgmOptDelOrder
        
        public static CgmFortsMsg110 CreateCgmFortsMsg110(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg110();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

                        return message;
        } // CreateCgmFortsMsg110
        
        public static CgmOptDelUserOrders CreateCgmOptDelUserOrders(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptDelUserOrders();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_buy_sell = streamDataMessage["buy_sell"];
            if (value_buy_sell != null)
            {
                message.BuySell = value_buy_sell.asInt();
            }

            var value_non_system = streamDataMessage["non_system"];
            if (value_non_system != null)
            {
                message.NonSystem = value_non_system.asInt();
            }

            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asString();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_ext_id = streamDataMessage["ext_id"];
            if (value_ext_id != null)
            {
                message.ExtId = value_ext_id.asInt();
            }

            var value_work_mode = streamDataMessage["work_mode"];
            if (value_work_mode != null)
            {
                message.WorkMode = value_work_mode.asInt();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_local_stamp = streamDataMessage["local_stamp"];
            if (value_local_stamp != null)
            {
                message.LocalStamp = value_local_stamp.asDateTime();
            }

                        return message;
        } // CreateCgmOptDelUserOrders
        
        public static CgmFortsMsg111 CreateCgmFortsMsg111(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg111();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

            var value_num_orders = streamDataMessage["num_orders"];
            if (value_num_orders != null)
            {
                message.NumOrders = value_num_orders.asInt();
            }

                        return message;
        } // CreateCgmFortsMsg111
        
        public static CgmOptMoveOrder CreateCgmOptMoveOrder(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptMoveOrder();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_regime = streamDataMessage["regime"];
            if (value_regime != null)
            {
                message.Regime = value_regime.asInt();
            }

            var value_order_id1 = streamDataMessage["order_id1"];
            if (value_order_id1 != null)
            {
                message.OrderId1 = value_order_id1.asLong();
            }

            var value_amount1 = streamDataMessage["amount1"];
            if (value_amount1 != null)
            {
                message.Amount1 = value_amount1.asInt();
            }

            var value_price1 = streamDataMessage["price1"];
            if (value_price1 != null)
            {
                message.Price1 = value_price1.asString();
            }

            var value_ext_id1 = streamDataMessage["ext_id1"];
            if (value_ext_id1 != null)
            {
                message.ExtId1 = value_ext_id1.asInt();
            }

            var value_check_limit = streamDataMessage["check_limit"];
            if (value_check_limit != null)
            {
                message.CheckLimit = value_check_limit.asInt();
            }

            var value_order_id2 = streamDataMessage["order_id2"];
            if (value_order_id2 != null)
            {
                message.OrderId2 = value_order_id2.asLong();
            }

            var value_amount2 = streamDataMessage["amount2"];
            if (value_amount2 != null)
            {
                message.Amount2 = value_amount2.asInt();
            }

            var value_price2 = streamDataMessage["price2"];
            if (value_price2 != null)
            {
                message.Price2 = value_price2.asString();
            }

            var value_ext_id2 = streamDataMessage["ext_id2"];
            if (value_ext_id2 != null)
            {
                message.ExtId2 = value_ext_id2.asInt();
            }

            var value_local_stamp = streamDataMessage["local_stamp"];
            if (value_local_stamp != null)
            {
                message.LocalStamp = value_local_stamp.asDateTime();
            }

                        return message;
        } // CreateCgmOptMoveOrder
        
        public static CgmFortsMsg113 CreateCgmFortsMsg113(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg113();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

            var value_order_id1 = streamDataMessage["order_id1"];
            if (value_order_id1 != null)
            {
                message.OrderId1 = value_order_id1.asLong();
            }

            var value_order_id2 = streamDataMessage["order_id2"];
            if (value_order_id2 != null)
            {
                message.OrderId2 = value_order_id2.asLong();
            }

                        return message;
        } // CreateCgmFortsMsg113
        
        public static CgmFutChangeClientMoney CreateCgmFutChangeClientMoney(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutChangeClientMoney();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_mode = streamDataMessage["mode"];
            if (value_mode != null)
            {
                message.Mode = value_mode.asInt();
            }

            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asString();
            }

            var value_limit_money = streamDataMessage["limit_money"];
            if (value_limit_money != null)
            {
                message.LimitMoney = value_limit_money.asString();
            }

            var value_limit_pledge = streamDataMessage["limit_pledge"];
            if (value_limit_pledge != null)
            {
                message.LimitPledge = value_limit_pledge.asString();
            }

            var value_coeff_liquidity = streamDataMessage["coeff_liquidity"];
            if (value_coeff_liquidity != null)
            {
                message.CoeffLiquidity = value_coeff_liquidity.asString();
            }

            var value_coeff_go = streamDataMessage["coeff_go"];
            if (value_coeff_go != null)
            {
                message.CoeffGo = value_coeff_go.asString();
            }

            var value_is_auto_update_limit = streamDataMessage["is_auto_update_limit"];
            if (value_is_auto_update_limit != null)
            {
                message.IsAutoUpdateLimit = value_is_auto_update_limit.asInt();
            }

            var value_no_fut_discount = streamDataMessage["no_fut_discount"];
            if (value_no_fut_discount != null)
            {
                message.NoFutDiscount = value_no_fut_discount.asInt();
            }

            var value_check_limit = streamDataMessage["check_limit"];
            if (value_check_limit != null)
            {
                message.CheckLimit = value_check_limit.asInt();
            }

                        return message;
        } // CreateCgmFutChangeClientMoney
        
        public static CgmFortsMsg104 CreateCgmFortsMsg104(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg104();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmFortsMsg104
        
        public static CgmFutChangeBfmoney CreateCgmFutChangeBfmoney(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutChangeBfmoney();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_mode = streamDataMessage["mode"];
            if (value_mode != null)
            {
                message.Mode = value_mode.asInt();
            }

            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asString();
            }

            var value_limit_money = streamDataMessage["limit_money"];
            if (value_limit_money != null)
            {
                message.LimitMoney = value_limit_money.asString();
            }

            var value_limit_pledge = streamDataMessage["limit_pledge"];
            if (value_limit_pledge != null)
            {
                message.LimitPledge = value_limit_pledge.asString();
            }

                        return message;
        } // CreateCgmFutChangeBfmoney
        
        public static CgmFortsMsg107 CreateCgmFortsMsg107(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg107();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmFortsMsg107
        
        public static CgmOptChangeExpiration CreateCgmOptChangeExpiration(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptChangeExpiration();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_mode = streamDataMessage["mode"];
            if (value_mode != null)
            {
                message.Mode = value_mode.asInt();
            }

            var value_order_id = streamDataMessage["order_id"];
            if (value_order_id != null)
            {
                message.OrderId = value_order_id.asInt();
            }

            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asString();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

                        return message;
        } // CreateCgmOptChangeExpiration
        
        public static CgmFortsMsg112 CreateCgmFortsMsg112(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg112();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

            var value_order_id = streamDataMessage["order_id"];
            if (value_order_id != null)
            {
                message.OrderId = value_order_id.asInt();
            }

                        return message;
        } // CreateCgmFortsMsg112
        
        public static CgmFutChangeClientProhibit CreateCgmFutChangeClientProhibit(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutChangeClientProhibit();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_mode = streamDataMessage["mode"];
            if (value_mode != null)
            {
                message.Mode = value_mode.asInt();
            }

            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asString();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_state = streamDataMessage["state"];
            if (value_state != null)
            {
                message.State = value_state.asInt();
            }

            var value_state_mask = streamDataMessage["state_mask"];
            if (value_state_mask != null)
            {
                message.StateMask = value_state_mask.asInt();
            }

                        return message;
        } // CreateCgmFutChangeClientProhibit
        
        public static CgmFortsMsg115 CreateCgmFortsMsg115(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg115();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmFortsMsg115
        
        public static CgmOptChangeClientProhibit CreateCgmOptChangeClientProhibit(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptChangeClientProhibit();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_mode = streamDataMessage["mode"];
            if (value_mode != null)
            {
                message.Mode = value_mode.asInt();
            }

            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asString();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_state = streamDataMessage["state"];
            if (value_state != null)
            {
                message.State = value_state.asInt();
            }

            var value_state_mask = streamDataMessage["state_mask"];
            if (value_state_mask != null)
            {
                message.StateMask = value_state_mask.asInt();
            }

                        return message;
        } // CreateCgmOptChangeClientProhibit
        
        public static CgmFortsMsg117 CreateCgmFortsMsg117(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg117();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmFortsMsg117
        
        public static CgmFutExchangeBfmoney CreateCgmFutExchangeBfmoney(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutExchangeBfmoney();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_mode = streamDataMessage["mode"];
            if (value_mode != null)
            {
                message.Mode = value_mode.asInt();
            }

            var value_code_from = streamDataMessage["code_from"];
            if (value_code_from != null)
            {
                message.CodeFrom = value_code_from.asString();
            }

            var value_code_to = streamDataMessage["code_to"];
            if (value_code_to != null)
            {
                message.CodeTo = value_code_to.asString();
            }

            var value_amount_money = streamDataMessage["amount_money"];
            if (value_amount_money != null)
            {
                message.AmountMoney = value_amount_money.asString();
            }

            var value_amount_pledge = streamDataMessage["amount_pledge"];
            if (value_amount_pledge != null)
            {
                message.AmountPledge = value_amount_pledge.asString();
            }

                        return message;
        } // CreateCgmFutExchangeBfmoney
        
        public static CgmFortsMsg130 CreateCgmFortsMsg130(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg130();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmFortsMsg130
        
        public static CgmOptRecalcCs CreateCgmOptRecalcCs(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptRecalcCs();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

                        return message;
        } // CreateCgmOptRecalcCs
        
        public static CgmFortsMsg132 CreateCgmFortsMsg132(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg132();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmFortsMsg132
        
        public static CgmFutTransferClientPosition CreateCgmFutTransferClientPosition(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutTransferClientPosition();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_code_from = streamDataMessage["code_from"];
            if (value_code_from != null)
            {
                message.CodeFrom = value_code_from.asString();
            }

            var value_code_to = streamDataMessage["code_to"];
            if (value_code_to != null)
            {
                message.CodeTo = value_code_to.asString();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

                        return message;
        } // CreateCgmFutTransferClientPosition
        
        public static CgmFortsMsg137 CreateCgmFortsMsg137(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg137();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmFortsMsg137
        
        public static CgmOptTransferClientPosition CreateCgmOptTransferClientPosition(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptTransferClientPosition();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_code_from = streamDataMessage["code_from"];
            if (value_code_from != null)
            {
                message.CodeFrom = value_code_from.asString();
            }

            var value_code_to = streamDataMessage["code_to"];
            if (value_code_to != null)
            {
                message.CodeTo = value_code_to.asString();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

                        return message;
        } // CreateCgmOptTransferClientPosition
        
        public static CgmFortsMsg138 CreateCgmFortsMsg138(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg138();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmFortsMsg138
        
        public static CgmOptChangeRiskParameters CreateCgmOptChangeRiskParameters(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptChangeRiskParameters();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_num_clr_2delivery = streamDataMessage["num_clr_2delivery"];
            if (value_num_clr_2delivery != null)
            {
                message.NumClr2delivery = value_num_clr_2delivery.asInt();
            }

            var value_use_broker_num_clr_2delivery = streamDataMessage["use_broker_num_clr_2delivery"];
            if (value_use_broker_num_clr_2delivery != null)
            {
                message.UseBrokerNumClr2delivery = value_use_broker_num_clr_2delivery.asSByte();
            }

            var value_exp_weight = streamDataMessage["exp_weight"];
            if (value_exp_weight != null)
            {
                message.ExpWeight = value_exp_weight.asString();
            }

            var value_use_broker_exp_weight = streamDataMessage["use_broker_exp_weight"];
            if (value_use_broker_exp_weight != null)
            {
                message.UseBrokerExpWeight = value_use_broker_exp_weight.asSByte();
            }

                        return message;
        } // CreateCgmOptChangeRiskParameters
        
        public static CgmFortsMsg140 CreateCgmFortsMsg140(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg140();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmFortsMsg140
        
        public static CgmFutTransferRisk CreateCgmFutTransferRisk(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutTransferRisk();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_code_from = streamDataMessage["code_from"];
            if (value_code_from != null)
            {
                message.CodeFrom = value_code_from.asString();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

                        return message;
        } // CreateCgmFutTransferRisk
        
        public static CgmFortsMsg139 CreateCgmFortsMsg139(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg139();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

            var value_deal_id1 = streamDataMessage["deal_id1"];
            if (value_deal_id1 != null)
            {
                message.DealId1 = value_deal_id1.asLong();
            }

            var value_deal_id2 = streamDataMessage["deal_id2"];
            if (value_deal_id2 != null)
            {
                message.DealId2 = value_deal_id2.asLong();
            }

                        return message;
        } // CreateCgmFortsMsg139
        
        public static CgmCodheartbeat CreateCgmCodheartbeat(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmCodheartbeat();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_seq_number = streamDataMessage["seq_number"];
            if (value_seq_number != null)
            {
                message.SeqNumber = value_seq_number.asInt();
            }

                        return message;
        } // CreateCgmCodheartbeat
        
        public static CgmFortsMsg99 CreateCgmFortsMsg99(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg99();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_queue_size = streamDataMessage["queue_size"];
            if (value_queue_size != null)
            {
                message.QueueSize = value_queue_size.asInt();
            }

            var value_penalty_remain = streamDataMessage["penalty_remain"];
            if (value_penalty_remain != null)
            {
                message.PenaltyRemain = value_penalty_remain.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmFortsMsg99
        
        public static CgmFortsMsg100 CreateCgmFortsMsg100(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFortsMsg100();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmFortsMsg100
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.FutCommon
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "common":
                    return CGateMessageFactory.CreateCgmCommon(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmCommon CreateCgmCommon(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmCommon();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_best_sell = streamDataMessage["best_sell"];
            if (value_best_sell != null)
            {
                message.BestSell = value_best_sell.asDouble();
            }

            var value_amount_sell = streamDataMessage["amount_sell"];
            if (value_amount_sell != null)
            {
                message.AmountSell = value_amount_sell.asInt();
            }

            var value_best_buy = streamDataMessage["best_buy"];
            if (value_best_buy != null)
            {
                message.BestBuy = value_best_buy.asDouble();
            }

            var value_amount_buy = streamDataMessage["amount_buy"];
            if (value_amount_buy != null)
            {
                message.AmountBuy = value_amount_buy.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_trend = streamDataMessage["trend"];
            if (value_trend != null)
            {
                message.Trend = value_trend.asDouble();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_deal_time = streamDataMessage["deal_time"];
            if (value_deal_time != null)
            {
                message.DealTime = value_deal_time.asDateTime();
            }

            var value_min_price = streamDataMessage["min_price"];
            if (value_min_price != null)
            {
                message.MinPrice = value_min_price.asDouble();
            }

            var value_max_price = streamDataMessage["max_price"];
            if (value_max_price != null)
            {
                message.MaxPrice = value_max_price.asDouble();
            }

            var value_avr_price = streamDataMessage["avr_price"];
            if (value_avr_price != null)
            {
                message.AvrPrice = value_avr_price.asDouble();
            }

            var value_old_kotir = streamDataMessage["old_kotir"];
            if (value_old_kotir != null)
            {
                message.OldKotir = value_old_kotir.asDouble();
            }

            var value_deal_count = streamDataMessage["deal_count"];
            if (value_deal_count != null)
            {
                message.DealCount = value_deal_count.asInt();
            }

            var value_contr_count = streamDataMessage["contr_count"];
            if (value_contr_count != null)
            {
                message.ContrCount = value_contr_count.asInt();
            }

            var value_capital = streamDataMessage["capital"];
            if (value_capital != null)
            {
                message.Capital = value_capital.asDouble();
            }

            var value_pos = streamDataMessage["pos"];
            if (value_pos != null)
            {
                message.Pos = value_pos.asInt();
            }

            var value_mod_time = streamDataMessage["mod_time"];
            if (value_mod_time != null)
            {
                message.ModTime = value_mod_time.asDateTime();
            }

            var value_cur_kotir = streamDataMessage["cur_kotir"];
            if (value_cur_kotir != null)
            {
                message.CurKotir = value_cur_kotir.asDouble();
            }

            var value_cur_kotir_real = streamDataMessage["cur_kotir_real"];
            if (value_cur_kotir_real != null)
            {
                message.CurKotirReal = value_cur_kotir_real.asDouble();
            }

            var value_orders_sell_qty = streamDataMessage["orders_sell_qty"];
            if (value_orders_sell_qty != null)
            {
                message.OrdersSellQty = value_orders_sell_qty.asInt();
            }

            var value_orders_sell_amount = streamDataMessage["orders_sell_amount"];
            if (value_orders_sell_amount != null)
            {
                message.OrdersSellAmount = value_orders_sell_amount.asInt();
            }

            var value_orders_buy_qty = streamDataMessage["orders_buy_qty"];
            if (value_orders_buy_qty != null)
            {
                message.OrdersBuyQty = value_orders_buy_qty.asInt();
            }

            var value_orders_buy_amount = streamDataMessage["orders_buy_amount"];
            if (value_orders_buy_amount != null)
            {
                message.OrdersBuyAmount = value_orders_buy_amount.asInt();
            }

            var value_open_price = streamDataMessage["open_price"];
            if (value_open_price != null)
            {
                message.OpenPrice = value_open_price.asDouble();
            }

            var value_close_price = streamDataMessage["close_price"];
            if (value_close_price != null)
            {
                message.ClosePrice = value_close_price.asDouble();
            }

            var value_local_time = streamDataMessage["local_time"];
            if (value_local_time != null)
            {
                message.LocalTime = value_local_time.asDateTime();
            }

                        return message;
        } // CreateCgmCommon
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.FutInfo
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "delivery_report":
                    return CGateMessageFactory.CreateCgmDeliveryReport(source);
                case "fut_rejected_orders":
                    return CGateMessageFactory.CreateCgmFutRejectedOrders(source);
                case "fut_intercl_info":
                    return CGateMessageFactory.CreateCgmFutInterclInfo(source);
                case "fut_bond_registry":
                    return CGateMessageFactory.CreateCgmFutBondRegistry(source);
                case "fut_bond_isin":
                    return CGateMessageFactory.CreateCgmFutBondIsin(source);
                case "fut_bond_nkd":
                    return CGateMessageFactory.CreateCgmFutBondNkd(source);
                case "fut_bond_nominal":
                    return CGateMessageFactory.CreateCgmFutBondNominal(source);
                case "usd_online":
                    return CGateMessageFactory.CreateCgmUsdOnline(source);
                case "fut_vcb":
                    return CGateMessageFactory.CreateCgmFutVcb(source);
                case "session":
                    return CGateMessageFactory.CreateCgmSession(source);
                case "multileg_dict":
                    return CGateMessageFactory.CreateCgmMultilegDict(source);
                case "fut_sess_contents":
                    return CGateMessageFactory.CreateCgmFutSessContents(source);
                case "fut_instruments":
                    return CGateMessageFactory.CreateCgmFutInstruments(source);
                case "diler":
                    return CGateMessageFactory.CreateCgmDiler(source);
                case "investr":
                    return CGateMessageFactory.CreateCgmInvestr(source);
                case "fut_sess_settl":
                    return CGateMessageFactory.CreateCgmFutSessSettl(source);
                case "sys_messages":
                    return CGateMessageFactory.CreateCgmSysMessages(source);
                case "fut_settlement_account":
                    return CGateMessageFactory.CreateCgmFutSettlementAccount(source);
                case "fut_margin_type":
                    return CGateMessageFactory.CreateCgmFutMarginType(source);
                case "prohibition":
                    return CGateMessageFactory.CreateCgmProhibition(source);
                case "rates":
                    return CGateMessageFactory.CreateCgmRates(source);
                case "sys_events":
                    return CGateMessageFactory.CreateCgmSysEvents(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmDeliveryReport CreateCgmDeliveryReport(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmDeliveryReport();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_date = streamDataMessage["date"];
            if (value_date != null)
            {
                message.Date = value_date.asDateTime();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_type = streamDataMessage["type"];
            if (value_type != null)
            {
                message.Type = value_type.asString();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_pos = streamDataMessage["pos"];
            if (value_pos != null)
            {
                message.Pos = value_pos.asInt();
            }

            var value_pos_excl = streamDataMessage["pos_excl"];
            if (value_pos_excl != null)
            {
                message.PosExcl = value_pos_excl.asInt();
            }

            var value_pos_unexec = streamDataMessage["pos_unexec"];
            if (value_pos_unexec != null)
            {
                message.PosUnexec = value_pos_unexec.asInt();
            }

            var value_unexec = streamDataMessage["unexec"];
            if (value_unexec != null)
            {
                message.Unexec = value_unexec.asSByte();
            }

            var value_settl_pair = streamDataMessage["settl_pair"];
            if (value_settl_pair != null)
            {
                message.SettlPair = value_settl_pair.asString();
            }

            var value_asset_code = streamDataMessage["asset_code"];
            if (value_asset_code != null)
            {
                message.AssetCode = value_asset_code.asString();
            }

            var value_issue_code = streamDataMessage["issue_code"];
            if (value_issue_code != null)
            {
                message.IssueCode = value_issue_code.asString();
            }

            var value_oblig_rur = streamDataMessage["oblig_rur"];
            if (value_oblig_rur != null)
            {
                message.ObligRur = value_oblig_rur.asDouble();
            }

            var value_oblig_qty = streamDataMessage["oblig_qty"];
            if (value_oblig_qty != null)
            {
                message.ObligQty = value_oblig_qty.asLong();
            }

            var value_fulfil_rur = streamDataMessage["fulfil_rur"];
            if (value_fulfil_rur != null)
            {
                message.FulfilRur = value_fulfil_rur.asDouble();
            }

            var value_fulfil_qty = streamDataMessage["fulfil_qty"];
            if (value_fulfil_qty != null)
            {
                message.FulfilQty = value_fulfil_qty.asLong();
            }

            var value_step = streamDataMessage["step"];
            if (value_step != null)
            {
                message.Step = value_step.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_id_gen = streamDataMessage["id_gen"];
            if (value_id_gen != null)
            {
                message.IdGen = value_id_gen.asInt();
            }

                        return message;
        } // CreateCgmDeliveryReport
        
        public static CgmFutRejectedOrders CreateCgmFutRejectedOrders(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutRejectedOrders();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_order_id = streamDataMessage["order_id"];
            if (value_order_id != null)
            {
                message.OrderId = value_order_id.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_moment_reject = streamDataMessage["moment_reject"];
            if (value_moment_reject != null)
            {
                message.MomentReject = value_moment_reject.asDateTime();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asSByte();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_date_exp = streamDataMessage["date_exp"];
            if (value_date_exp != null)
            {
                message.DateExp = value_date_exp.asDateTime();
            }

            var value_id_ord1 = streamDataMessage["id_ord1"];
            if (value_id_ord1 != null)
            {
                message.IdOrd1 = value_id_ord1.asLong();
            }

            var value_ret_code = streamDataMessage["ret_code"];
            if (value_ret_code != null)
            {
                message.RetCode = value_ret_code.asInt();
            }

            var value_ret_message = streamDataMessage["ret_message"];
            if (value_ret_message != null)
            {
                message.RetMessage = value_ret_message.asString();
            }

            var value_comment = streamDataMessage["comment"];
            if (value_comment != null)
            {
                message.Comment = value_comment.asString();
            }

            var value_login_from = streamDataMessage["login_from"];
            if (value_login_from != null)
            {
                message.LoginFrom = value_login_from.asString();
            }

            var value_ext_id = streamDataMessage["ext_id"];
            if (value_ext_id != null)
            {
                message.ExtId = value_ext_id.asInt();
            }

                        return message;
        } // CreateCgmFutRejectedOrders
        
        public static CgmFutInterclInfo CreateCgmFutInterclInfo(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutInterclInfo();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_vm_intercl = streamDataMessage["vm_intercl"];
            if (value_vm_intercl != null)
            {
                message.VmIntercl = value_vm_intercl.asDouble();
            }

                        return message;
        } // CreateCgmFutInterclInfo
        
        public static CgmFutBondRegistry CreateCgmFutBondRegistry(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutBondRegistry();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_bond_id = streamDataMessage["bond_id"];
            if (value_bond_id != null)
            {
                message.BondId = value_bond_id.asInt();
            }

            var value_small_name = streamDataMessage["small_name"];
            if (value_small_name != null)
            {
                message.SmallName = value_small_name.asString();
            }

            var value_short_isin = streamDataMessage["short_isin"];
            if (value_short_isin != null)
            {
                message.ShortIsin = value_short_isin.asString();
            }

            var value_name = streamDataMessage["name"];
            if (value_name != null)
            {
                message.Name = value_name.asString();
            }

            var value_date_redempt = streamDataMessage["date_redempt"];
            if (value_date_redempt != null)
            {
                message.DateRedempt = value_date_redempt.asDateTime();
            }

            var value_nominal = streamDataMessage["nominal"];
            if (value_nominal != null)
            {
                message.Nominal = value_nominal.asDouble();
            }

            var value_bond_type = streamDataMessage["bond_type"];
            if (value_bond_type != null)
            {
                message.BondType = value_bond_type.asSByte();
            }

            var value_year_base = streamDataMessage["year_base"];
            if (value_year_base != null)
            {
                message.YearBase = value_year_base.asShort();
            }

                        return message;
        } // CreateCgmFutBondRegistry
        
        public static CgmFutBondIsin CreateCgmFutBondIsin(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutBondIsin();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_bond_id = streamDataMessage["bond_id"];
            if (value_bond_id != null)
            {
                message.BondId = value_bond_id.asInt();
            }

            var value_coeff_conversion = streamDataMessage["coeff_conversion"];
            if (value_coeff_conversion != null)
            {
                message.CoeffConversion = value_coeff_conversion.asDouble();
            }

                        return message;
        } // CreateCgmFutBondIsin
        
        public static CgmFutBondNkd CreateCgmFutBondNkd(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutBondNkd();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_bond_id = streamDataMessage["bond_id"];
            if (value_bond_id != null)
            {
                message.BondId = value_bond_id.asInt();
            }

            var value_date = streamDataMessage["date"];
            if (value_date != null)
            {
                message.Date = value_date.asDateTime();
            }

            var value_nkd = streamDataMessage["nkd"];
            if (value_nkd != null)
            {
                message.Nkd = value_nkd.asDouble();
            }

            var value_is_cupon = streamDataMessage["is_cupon"];
            if (value_is_cupon != null)
            {
                message.IsCupon = value_is_cupon.asSByte();
            }

                        return message;
        } // CreateCgmFutBondNkd
        
        public static CgmFutBondNominal CreateCgmFutBondNominal(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutBondNominal();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_bond_id = streamDataMessage["bond_id"];
            if (value_bond_id != null)
            {
                message.BondId = value_bond_id.asInt();
            }

            var value_date = streamDataMessage["date"];
            if (value_date != null)
            {
                message.Date = value_date.asDateTime();
            }

            var value_nominal = streamDataMessage["nominal"];
            if (value_nominal != null)
            {
                message.Nominal = value_nominal.asDouble();
            }

            var value_face_value = streamDataMessage["face_value"];
            if (value_face_value != null)
            {
                message.FaceValue = value_face_value.asDouble();
            }

            var value_coupon_nominal = streamDataMessage["coupon_nominal"];
            if (value_coupon_nominal != null)
            {
                message.CouponNominal = value_coupon_nominal.asDouble();
            }

            var value_is_nominal = streamDataMessage["is_nominal"];
            if (value_is_nominal != null)
            {
                message.IsNominal = value_is_nominal.asSByte();
            }

                        return message;
        } // CreateCgmFutBondNominal
        
        public static CgmUsdOnline CreateCgmUsdOnline(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmUsdOnline();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_id = streamDataMessage["id"];
            if (value_id != null)
            {
                message.Id = value_id.asLong();
            }

            var value_rate = streamDataMessage["rate"];
            if (value_rate != null)
            {
                message.Rate = value_rate.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

                        return message;
        } // CreateCgmUsdOnline
        
        public static CgmFutVcb CreateCgmFutVcb(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutVcb();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_name = streamDataMessage["name"];
            if (value_name != null)
            {
                message.Name = value_name.asString();
            }

            var value_exec_type = streamDataMessage["exec_type"];
            if (value_exec_type != null)
            {
                message.ExecType = value_exec_type.asString();
            }

            var value_curr = streamDataMessage["curr"];
            if (value_curr != null)
            {
                message.Curr = value_curr.asString();
            }

            var value_exch_pay = streamDataMessage["exch_pay"];
            if (value_exch_pay != null)
            {
                message.ExchPay = value_exch_pay.asDouble();
            }

            var value_exch_pay_scalped = streamDataMessage["exch_pay_scalped"];
            if (value_exch_pay_scalped != null)
            {
                message.ExchPayScalped = value_exch_pay_scalped.asSByte();
            }

            var value_clear_pay = streamDataMessage["clear_pay"];
            if (value_clear_pay != null)
            {
                message.ClearPay = value_clear_pay.asDouble();
            }

            var value_clear_pay_scalped = streamDataMessage["clear_pay_scalped"];
            if (value_clear_pay_scalped != null)
            {
                message.ClearPayScalped = value_clear_pay_scalped.asSByte();
            }

            var value_sell_fee = streamDataMessage["sell_fee"];
            if (value_sell_fee != null)
            {
                message.SellFee = value_sell_fee.asDouble();
            }

            var value_buy_fee = streamDataMessage["buy_fee"];
            if (value_buy_fee != null)
            {
                message.BuyFee = value_buy_fee.asDouble();
            }

            var value_trade_scheme = streamDataMessage["trade_scheme"];
            if (value_trade_scheme != null)
            {
                message.TradeScheme = value_trade_scheme.asString();
            }

            var value_section = streamDataMessage["section"];
            if (value_section != null)
            {
                message.Section = value_section.asString();
            }

            var value_exch_pay_spot = streamDataMessage["exch_pay_spot"];
            if (value_exch_pay_spot != null)
            {
                message.ExchPaySpot = value_exch_pay_spot.asDouble();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_exch_pay_spot_repo = streamDataMessage["exch_pay_spot_repo"];
            if (value_exch_pay_spot_repo != null)
            {
                message.ExchPaySpotRepo = value_exch_pay_spot_repo.asDouble();
            }

            var value_rate_id = streamDataMessage["rate_id"];
            if (value_rate_id != null)
            {
                message.RateId = value_rate_id.asInt();
            }

                        return message;
        } // CreateCgmFutVcb
        
        public static CgmSession CreateCgmSession(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmSession();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_begin = streamDataMessage["begin"];
            if (value_begin != null)
            {
                message.Begin = value_begin.asDateTime();
            }

            var value_end = streamDataMessage["end"];
            if (value_end != null)
            {
                message.End = value_end.asDateTime();
            }

            var value_state = streamDataMessage["state"];
            if (value_state != null)
            {
                message.State = value_state.asInt();
            }

            var value_opt_sess_id = streamDataMessage["opt_sess_id"];
            if (value_opt_sess_id != null)
            {
                message.OptSessId = value_opt_sess_id.asInt();
            }

            var value_inter_cl_begin = streamDataMessage["inter_cl_begin"];
            if (value_inter_cl_begin != null)
            {
                message.InterClBegin = value_inter_cl_begin.asDateTime();
            }

            var value_inter_cl_end = streamDataMessage["inter_cl_end"];
            if (value_inter_cl_end != null)
            {
                message.InterClEnd = value_inter_cl_end.asDateTime();
            }

            var value_inter_cl_state = streamDataMessage["inter_cl_state"];
            if (value_inter_cl_state != null)
            {
                message.InterClState = value_inter_cl_state.asInt();
            }

            var value_eve_on = streamDataMessage["eve_on"];
            if (value_eve_on != null)
            {
                message.EveOn = value_eve_on.asSByte();
            }

            var value_eve_begin = streamDataMessage["eve_begin"];
            if (value_eve_begin != null)
            {
                message.EveBegin = value_eve_begin.asDateTime();
            }

            var value_eve_end = streamDataMessage["eve_end"];
            if (value_eve_end != null)
            {
                message.EveEnd = value_eve_end.asDateTime();
            }

            var value_mon_on = streamDataMessage["mon_on"];
            if (value_mon_on != null)
            {
                message.MonOn = value_mon_on.asSByte();
            }

            var value_mon_begin = streamDataMessage["mon_begin"];
            if (value_mon_begin != null)
            {
                message.MonBegin = value_mon_begin.asDateTime();
            }

            var value_mon_end = streamDataMessage["mon_end"];
            if (value_mon_end != null)
            {
                message.MonEnd = value_mon_end.asDateTime();
            }

            var value_pos_transfer_begin = streamDataMessage["pos_transfer_begin"];
            if (value_pos_transfer_begin != null)
            {
                message.PosTransferBegin = value_pos_transfer_begin.asDateTime();
            }

            var value_pos_transfer_end = streamDataMessage["pos_transfer_end"];
            if (value_pos_transfer_end != null)
            {
                message.PosTransferEnd = value_pos_transfer_end.asDateTime();
            }

                        return message;
        } // CreateCgmSession
        
        public static CgmMultilegDict CreateCgmMultilegDict(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmMultilegDict();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_isin_id_leg = streamDataMessage["isin_id_leg"];
            if (value_isin_id_leg != null)
            {
                message.IsinIdLeg = value_isin_id_leg.asInt();
            }

            var value_qty_ratio = streamDataMessage["qty_ratio"];
            if (value_qty_ratio != null)
            {
                message.QtyRatio = value_qty_ratio.asInt();
            }

                        return message;
        } // CreateCgmMultilegDict
        
        public static CgmFutSessContents CreateCgmFutSessContents(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutSessContents();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_short_isin = streamDataMessage["short_isin"];
            if (value_short_isin != null)
            {
                message.ShortIsin = value_short_isin.asString();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_name = streamDataMessage["name"];
            if (value_name != null)
            {
                message.Name = value_name.asString();
            }

            var value_inst_term = streamDataMessage["inst_term"];
            if (value_inst_term != null)
            {
                message.InstTerm = value_inst_term.asInt();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_is_limited = streamDataMessage["is_limited"];
            if (value_is_limited != null)
            {
                message.IsLimited = value_is_limited.asSByte();
            }

            var value_limit_up = streamDataMessage["limit_up"];
            if (value_limit_up != null)
            {
                message.LimitUp = value_limit_up.asDouble();
            }

            var value_limit_down = streamDataMessage["limit_down"];
            if (value_limit_down != null)
            {
                message.LimitDown = value_limit_down.asDouble();
            }

            var value_old_kotir = streamDataMessage["old_kotir"];
            if (value_old_kotir != null)
            {
                message.OldKotir = value_old_kotir.asDouble();
            }

            var value_buy_deposit = streamDataMessage["buy_deposit"];
            if (value_buy_deposit != null)
            {
                message.BuyDeposit = value_buy_deposit.asDouble();
            }

            var value_sell_deposit = streamDataMessage["sell_deposit"];
            if (value_sell_deposit != null)
            {
                message.SellDeposit = value_sell_deposit.asDouble();
            }

            var value_roundto = streamDataMessage["roundto"];
            if (value_roundto != null)
            {
                message.Roundto = value_roundto.asInt();
            }

            var value_min_step = streamDataMessage["min_step"];
            if (value_min_step != null)
            {
                message.MinStep = value_min_step.asDouble();
            }

            var value_lot_volume = streamDataMessage["lot_volume"];
            if (value_lot_volume != null)
            {
                message.LotVolume = value_lot_volume.asInt();
            }

            var value_step_price = streamDataMessage["step_price"];
            if (value_step_price != null)
            {
                message.StepPrice = value_step_price.asDouble();
            }

            var value_d_pg = streamDataMessage["d_pg"];
            if (value_d_pg != null)
            {
                message.DPg = value_d_pg.asDateTime();
            }

            var value_is_spread = streamDataMessage["is_spread"];
            if (value_is_spread != null)
            {
                message.IsSpread = value_is_spread.asSByte();
            }

            var value_coeff = streamDataMessage["coeff"];
            if (value_coeff != null)
            {
                message.Coeff = value_coeff.asDouble();
            }

            var value_d_exp = streamDataMessage["d_exp"];
            if (value_d_exp != null)
            {
                message.DExp = value_d_exp.asDateTime();
            }

            var value_is_percent = streamDataMessage["is_percent"];
            if (value_is_percent != null)
            {
                message.IsPercent = value_is_percent.asSByte();
            }

            var value_percent_rate = streamDataMessage["percent_rate"];
            if (value_percent_rate != null)
            {
                message.PercentRate = value_percent_rate.asDouble();
            }

            var value_last_cl_quote = streamDataMessage["last_cl_quote"];
            if (value_last_cl_quote != null)
            {
                message.LastClQuote = value_last_cl_quote.asDouble();
            }

            var value_signs = streamDataMessage["signs"];
            if (value_signs != null)
            {
                message.Signs = value_signs.asInt();
            }

            var value_is_trade_evening = streamDataMessage["is_trade_evening"];
            if (value_is_trade_evening != null)
            {
                message.IsTradeEvening = value_is_trade_evening.asSByte();
            }

            var value_ticker = streamDataMessage["ticker"];
            if (value_ticker != null)
            {
                message.Ticker = value_ticker.asInt();
            }

            var value_state = streamDataMessage["state"];
            if (value_state != null)
            {
                message.State = value_state.asInt();
            }

            var value_price_dir = streamDataMessage["price_dir"];
            if (value_price_dir != null)
            {
                message.PriceDir = value_price_dir.asSByte();
            }

            var value_multileg_type = streamDataMessage["multileg_type"];
            if (value_multileg_type != null)
            {
                message.MultilegType = value_multileg_type.asInt();
            }

            var value_legs_qty = streamDataMessage["legs_qty"];
            if (value_legs_qty != null)
            {
                message.LegsQty = value_legs_qty.asInt();
            }

            var value_step_price_clr = streamDataMessage["step_price_clr"];
            if (value_step_price_clr != null)
            {
                message.StepPriceClr = value_step_price_clr.asDouble();
            }

            var value_step_price_interclr = streamDataMessage["step_price_interclr"];
            if (value_step_price_interclr != null)
            {
                message.StepPriceInterclr = value_step_price_interclr.asDouble();
            }

            var value_step_price_curr = streamDataMessage["step_price_curr"];
            if (value_step_price_curr != null)
            {
                message.StepPriceCurr = value_step_price_curr.asDouble();
            }

            var value_d_start = streamDataMessage["d_start"];
            if (value_d_start != null)
            {
                message.DStart = value_d_start.asDateTime();
            }

            var value_exch_pay = streamDataMessage["exch_pay"];
            if (value_exch_pay != null)
            {
                message.ExchPay = value_exch_pay.asDouble();
            }

            var value_pctyield_coeff = streamDataMessage["pctyield_coeff"];
            if (value_pctyield_coeff != null)
            {
                message.PctyieldCoeff = value_pctyield_coeff.asDouble();
            }

            var value_pctyield_total = streamDataMessage["pctyield_total"];
            if (value_pctyield_total != null)
            {
                message.PctyieldTotal = value_pctyield_total.asDouble();
            }

                        return message;
        } // CreateCgmFutSessContents
        
        public static CgmFutInstruments CreateCgmFutInstruments(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutInstruments();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_short_isin = streamDataMessage["short_isin"];
            if (value_short_isin != null)
            {
                message.ShortIsin = value_short_isin.asString();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_name = streamDataMessage["name"];
            if (value_name != null)
            {
                message.Name = value_name.asString();
            }

            var value_inst_term = streamDataMessage["inst_term"];
            if (value_inst_term != null)
            {
                message.InstTerm = value_inst_term.asInt();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_is_limited = streamDataMessage["is_limited"];
            if (value_is_limited != null)
            {
                message.IsLimited = value_is_limited.asSByte();
            }

            var value_old_kotir = streamDataMessage["old_kotir"];
            if (value_old_kotir != null)
            {
                message.OldKotir = value_old_kotir.asDouble();
            }

            var value_roundto = streamDataMessage["roundto"];
            if (value_roundto != null)
            {
                message.Roundto = value_roundto.asInt();
            }

            var value_min_step = streamDataMessage["min_step"];
            if (value_min_step != null)
            {
                message.MinStep = value_min_step.asDouble();
            }

            var value_lot_volume = streamDataMessage["lot_volume"];
            if (value_lot_volume != null)
            {
                message.LotVolume = value_lot_volume.asInt();
            }

            var value_step_price = streamDataMessage["step_price"];
            if (value_step_price != null)
            {
                message.StepPrice = value_step_price.asDouble();
            }

            var value_d_pg = streamDataMessage["d_pg"];
            if (value_d_pg != null)
            {
                message.DPg = value_d_pg.asDateTime();
            }

            var value_is_spread = streamDataMessage["is_spread"];
            if (value_is_spread != null)
            {
                message.IsSpread = value_is_spread.asSByte();
            }

            var value_coeff = streamDataMessage["coeff"];
            if (value_coeff != null)
            {
                message.Coeff = value_coeff.asDouble();
            }

            var value_d_exp = streamDataMessage["d_exp"];
            if (value_d_exp != null)
            {
                message.DExp = value_d_exp.asDateTime();
            }

            var value_is_percent = streamDataMessage["is_percent"];
            if (value_is_percent != null)
            {
                message.IsPercent = value_is_percent.asSByte();
            }

            var value_percent_rate = streamDataMessage["percent_rate"];
            if (value_percent_rate != null)
            {
                message.PercentRate = value_percent_rate.asDouble();
            }

            var value_last_cl_quote = streamDataMessage["last_cl_quote"];
            if (value_last_cl_quote != null)
            {
                message.LastClQuote = value_last_cl_quote.asDouble();
            }

            var value_signs = streamDataMessage["signs"];
            if (value_signs != null)
            {
                message.Signs = value_signs.asInt();
            }

            var value_volat_min = streamDataMessage["volat_min"];
            if (value_volat_min != null)
            {
                message.VolatMin = value_volat_min.asDouble();
            }

            var value_volat_max = streamDataMessage["volat_max"];
            if (value_volat_max != null)
            {
                message.VolatMax = value_volat_max.asDouble();
            }

            var value_price_dir = streamDataMessage["price_dir"];
            if (value_price_dir != null)
            {
                message.PriceDir = value_price_dir.asSByte();
            }

            var value_multileg_type = streamDataMessage["multileg_type"];
            if (value_multileg_type != null)
            {
                message.MultilegType = value_multileg_type.asInt();
            }

            var value_legs_qty = streamDataMessage["legs_qty"];
            if (value_legs_qty != null)
            {
                message.LegsQty = value_legs_qty.asInt();
            }

            var value_step_price_clr = streamDataMessage["step_price_clr"];
            if (value_step_price_clr != null)
            {
                message.StepPriceClr = value_step_price_clr.asDouble();
            }

            var value_step_price_interclr = streamDataMessage["step_price_interclr"];
            if (value_step_price_interclr != null)
            {
                message.StepPriceInterclr = value_step_price_interclr.asDouble();
            }

            var value_step_price_curr = streamDataMessage["step_price_curr"];
            if (value_step_price_curr != null)
            {
                message.StepPriceCurr = value_step_price_curr.asDouble();
            }

            var value_d_start = streamDataMessage["d_start"];
            if (value_d_start != null)
            {
                message.DStart = value_d_start.asDateTime();
            }

            var value_is_limit_opt = streamDataMessage["is_limit_opt"];
            if (value_is_limit_opt != null)
            {
                message.IsLimitOpt = value_is_limit_opt.asSByte();
            }

            var value_limit_up_opt = streamDataMessage["limit_up_opt"];
            if (value_limit_up_opt != null)
            {
                message.LimitUpOpt = value_limit_up_opt.asDouble();
            }

            var value_limit_down_opt = streamDataMessage["limit_down_opt"];
            if (value_limit_down_opt != null)
            {
                message.LimitDownOpt = value_limit_down_opt.asDouble();
            }

            var value_adm_lim = streamDataMessage["adm_lim"];
            if (value_adm_lim != null)
            {
                message.AdmLim = value_adm_lim.asDouble();
            }

            var value_adm_lim_offmoney = streamDataMessage["adm_lim_offmoney"];
            if (value_adm_lim_offmoney != null)
            {
                message.AdmLimOffmoney = value_adm_lim_offmoney.asDouble();
            }

            var value_apply_adm_limit = streamDataMessage["apply_adm_limit"];
            if (value_apply_adm_limit != null)
            {
                message.ApplyAdmLimit = value_apply_adm_limit.asSByte();
            }

            var value_pctyield_coeff = streamDataMessage["pctyield_coeff"];
            if (value_pctyield_coeff != null)
            {
                message.PctyieldCoeff = value_pctyield_coeff.asDouble();
            }

            var value_pctyield_total = streamDataMessage["pctyield_total"];
            if (value_pctyield_total != null)
            {
                message.PctyieldTotal = value_pctyield_total.asDouble();
            }

            var value_exec_name = streamDataMessage["exec_name"];
            if (value_exec_name != null)
            {
                message.ExecName = value_exec_name.asString();
            }

                        return message;
        } // CreateCgmFutInstruments
        
        public static CgmDiler CreateCgmDiler(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmDiler();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_name = streamDataMessage["name"];
            if (value_name != null)
            {
                message.Name = value_name.asString();
            }

            var value_rts_code = streamDataMessage["rts_code"];
            if (value_rts_code != null)
            {
                message.RtsCode = value_rts_code.asString();
            }

            var value_transfer_code = streamDataMessage["transfer_code"];
            if (value_transfer_code != null)
            {
                message.TransferCode = value_transfer_code.asString();
            }

            var value_status = streamDataMessage["status"];
            if (value_status != null)
            {
                message.Status = value_status.asInt();
            }

                        return message;
        } // CreateCgmDiler
        
        public static CgmInvestr CreateCgmInvestr(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmInvestr();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_name = streamDataMessage["name"];
            if (value_name != null)
            {
                message.Name = value_name.asString();
            }

            var value_status = streamDataMessage["status"];
            if (value_status != null)
            {
                message.Status = value_status.asInt();
            }

                        return message;
        } // CreateCgmInvestr
        
        public static CgmFutSessSettl CreateCgmFutSessSettl(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutSessSettl();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_date_clr = streamDataMessage["date_clr"];
            if (value_date_clr != null)
            {
                message.DateClr = value_date_clr.asDateTime();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_settl_price = streamDataMessage["settl_price"];
            if (value_settl_price != null)
            {
                message.SettlPrice = value_settl_price.asDouble();
            }

                        return message;
        } // CreateCgmFutSessSettl
        
        public static CgmSysMessages CreateCgmSysMessages(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmSysMessages();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_msg_id = streamDataMessage["msg_id"];
            if (value_msg_id != null)
            {
                message.MsgId = value_msg_id.asInt();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_lang_code = streamDataMessage["lang_code"];
            if (value_lang_code != null)
            {
                message.LangCode = value_lang_code.asString();
            }

            var value_urgency = streamDataMessage["urgency"];
            if (value_urgency != null)
            {
                message.Urgency = value_urgency.asSByte();
            }

            var value_status = streamDataMessage["status"];
            if (value_status != null)
            {
                message.Status = value_status.asSByte();
            }

            var value_text = streamDataMessage["text"];
            if (value_text != null)
            {
                message.Text = value_text.asString();
            }

            var value_message_body = streamDataMessage["message_body"];
            if (value_message_body != null)
            {
                message.MessageBody = value_message_body.asString();
            }

                        return message;
        } // CreateCgmSysMessages
        
        public static CgmFutSettlementAccount CreateCgmFutSettlementAccount(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutSettlementAccount();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asString();
            }

            var value_type = streamDataMessage["type"];
            if (value_type != null)
            {
                message.Type = value_type.asSByte();
            }

            var value_settlement_account = streamDataMessage["settlement_account"];
            if (value_settlement_account != null)
            {
                message.SettlementAccount = value_settlement_account.asString();
            }

                        return message;
        } // CreateCgmFutSettlementAccount
        
        public static CgmFutMarginType CreateCgmFutMarginType(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutMarginType();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_code = streamDataMessage["code"];
            if (value_code != null)
            {
                message.Code = value_code.asString();
            }

            var value_type = streamDataMessage["type"];
            if (value_type != null)
            {
                message.Type = value_type.asSByte();
            }

            var value_margin_type = streamDataMessage["margin_type"];
            if (value_margin_type != null)
            {
                message.MarginType = value_margin_type.asSByte();
            }

                        return message;
        } // CreateCgmFutMarginType
        
        public static CgmProhibition CreateCgmProhibition(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmProhibition();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_prohib_id = streamDataMessage["prohib_id"];
            if (value_prohib_id != null)
            {
                message.ProhibId = value_prohib_id.asInt();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_initiator = streamDataMessage["initiator"];
            if (value_initiator != null)
            {
                message.Initiator = value_initiator.asInt();
            }

            var value_section = streamDataMessage["section"];
            if (value_section != null)
            {
                message.Section = value_section.asString();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_priority = streamDataMessage["priority"];
            if (value_priority != null)
            {
                message.Priority = value_priority.asInt();
            }

            var value_group_mask = streamDataMessage["group_mask"];
            if (value_group_mask != null)
            {
                message.GroupMask = value_group_mask.asLong();
            }

            var value_type = streamDataMessage["type"];
            if (value_type != null)
            {
                message.Type = value_type.asInt();
            }

            var value_is_legacy = streamDataMessage["is_legacy"];
            if (value_is_legacy != null)
            {
                message.IsLegacy = value_is_legacy.asInt();
            }

                        return message;
        } // CreateCgmProhibition
        
        public static CgmRates CreateCgmRates(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmRates();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_rate_id = streamDataMessage["rate_id"];
            if (value_rate_id != null)
            {
                message.RateId = value_rate_id.asInt();
            }

            var value_curr_base = streamDataMessage["curr_base"];
            if (value_curr_base != null)
            {
                message.CurrBase = value_curr_base.asString();
            }

            var value_curr_coupled = streamDataMessage["curr_coupled"];
            if (value_curr_coupled != null)
            {
                message.CurrCoupled = value_curr_coupled.asString();
            }

            var value_radius = streamDataMessage["radius"];
            if (value_radius != null)
            {
                message.Radius = value_radius.asDouble();
            }

                        return message;
        } // CreateCgmRates
        
        public static CgmSysEvents CreateCgmSysEvents(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmSysEvents();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_event_id = streamDataMessage["event_id"];
            if (value_event_id != null)
            {
                message.EventId = value_event_id.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_event_type = streamDataMessage["event_type"];
            if (value_event_type != null)
            {
                message.EventType = value_event_type.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmSysEvents
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.FutTrades
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "orders_log":
                    return CGateMessageFactory.CreateCgmOrdersLog(source);
                case "multileg_orders_log":
                    return CGateMessageFactory.CreateCgmMultilegOrdersLog(source);
                case "deal":
                    return CGateMessageFactory.CreateCgmDeal(source);
                case "multileg_deal":
                    return CGateMessageFactory.CreateCgmMultilegDeal(source);
                case "heartbeat":
                    return CGateMessageFactory.CreateCgmHeartbeat(source);
                case "sys_events":
                    return CGateMessageFactory.CreateCgmSysEvents(source);
                case "user_deal":
                    return CGateMessageFactory.CreateCgmUserDeal(source);
                case "user_multileg_deal":
                    return CGateMessageFactory.CreateCgmUserMultilegDeal(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmOrdersLog CreateCgmOrdersLog(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOrdersLog();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_id_ord = streamDataMessage["id_ord"];
            if (value_id_ord != null)
            {
                message.IdOrd = value_id_ord.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_amount_rest = streamDataMessage["amount_rest"];
            if (value_amount_rest != null)
            {
                message.AmountRest = value_amount_rest.asInt();
            }

            var value_id_deal = streamDataMessage["id_deal"];
            if (value_id_deal != null)
            {
                message.IdDeal = value_id_deal.asLong();
            }

            var value_xstatus = streamDataMessage["xstatus"];
            if (value_xstatus != null)
            {
                message.Xstatus = value_xstatus.asLong();
            }

            var value_status = streamDataMessage["status"];
            if (value_status != null)
            {
                message.Status = value_status.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asSByte();
            }

            var value_action = streamDataMessage["action"];
            if (value_action != null)
            {
                message.Action = value_action.asSByte();
            }

            var value_deal_price = streamDataMessage["deal_price"];
            if (value_deal_price != null)
            {
                message.DealPrice = value_deal_price.asDouble();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_login_from = streamDataMessage["login_from"];
            if (value_login_from != null)
            {
                message.LoginFrom = value_login_from.asString();
            }

            var value_comment = streamDataMessage["comment"];
            if (value_comment != null)
            {
                message.Comment = value_comment.asString();
            }

            var value_hedge = streamDataMessage["hedge"];
            if (value_hedge != null)
            {
                message.Hedge = value_hedge.asSByte();
            }

            var value_trust = streamDataMessage["trust"];
            if (value_trust != null)
            {
                message.Trust = value_trust.asSByte();
            }

            var value_ext_id = streamDataMessage["ext_id"];
            if (value_ext_id != null)
            {
                message.ExtId = value_ext_id.asInt();
            }

            var value_broker_to = streamDataMessage["broker_to"];
            if (value_broker_to != null)
            {
                message.BrokerTo = value_broker_to.asString();
            }

            var value_broker_to_rts = streamDataMessage["broker_to_rts"];
            if (value_broker_to_rts != null)
            {
                message.BrokerToRts = value_broker_to_rts.asString();
            }

            var value_broker_from_rts = streamDataMessage["broker_from_rts"];
            if (value_broker_from_rts != null)
            {
                message.BrokerFromRts = value_broker_from_rts.asString();
            }

            var value_date_exp = streamDataMessage["date_exp"];
            if (value_date_exp != null)
            {
                message.DateExp = value_date_exp.asDateTime();
            }

            var value_id_ord1 = streamDataMessage["id_ord1"];
            if (value_id_ord1 != null)
            {
                message.IdOrd1 = value_id_ord1.asLong();
            }

            var value_local_stamp = streamDataMessage["local_stamp"];
            if (value_local_stamp != null)
            {
                message.LocalStamp = value_local_stamp.asDateTime();
            }

                        return message;
        } // CreateCgmOrdersLog
        
        public static CgmMultilegOrdersLog CreateCgmMultilegOrdersLog(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmMultilegOrdersLog();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_id_ord = streamDataMessage["id_ord"];
            if (value_id_ord != null)
            {
                message.IdOrd = value_id_ord.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_amount_rest = streamDataMessage["amount_rest"];
            if (value_amount_rest != null)
            {
                message.AmountRest = value_amount_rest.asInt();
            }

            var value_id_deal = streamDataMessage["id_deal"];
            if (value_id_deal != null)
            {
                message.IdDeal = value_id_deal.asLong();
            }

            var value_xstatus = streamDataMessage["xstatus"];
            if (value_xstatus != null)
            {
                message.Xstatus = value_xstatus.asLong();
            }

            var value_status = streamDataMessage["status"];
            if (value_status != null)
            {
                message.Status = value_status.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asSByte();
            }

            var value_action = streamDataMessage["action"];
            if (value_action != null)
            {
                message.Action = value_action.asSByte();
            }

            var value_deal_price = streamDataMessage["deal_price"];
            if (value_deal_price != null)
            {
                message.DealPrice = value_deal_price.asDouble();
            }

            var value_rate_price = streamDataMessage["rate_price"];
            if (value_rate_price != null)
            {
                message.RatePrice = value_rate_price.asDouble();
            }

            var value_swap_price = streamDataMessage["swap_price"];
            if (value_swap_price != null)
            {
                message.SwapPrice = value_swap_price.asDouble();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_login_from = streamDataMessage["login_from"];
            if (value_login_from != null)
            {
                message.LoginFrom = value_login_from.asString();
            }

            var value_comment = streamDataMessage["comment"];
            if (value_comment != null)
            {
                message.Comment = value_comment.asString();
            }

            var value_hedge = streamDataMessage["hedge"];
            if (value_hedge != null)
            {
                message.Hedge = value_hedge.asSByte();
            }

            var value_trust = streamDataMessage["trust"];
            if (value_trust != null)
            {
                message.Trust = value_trust.asSByte();
            }

            var value_ext_id = streamDataMessage["ext_id"];
            if (value_ext_id != null)
            {
                message.ExtId = value_ext_id.asInt();
            }

            var value_broker_to = streamDataMessage["broker_to"];
            if (value_broker_to != null)
            {
                message.BrokerTo = value_broker_to.asString();
            }

            var value_broker_to_rts = streamDataMessage["broker_to_rts"];
            if (value_broker_to_rts != null)
            {
                message.BrokerToRts = value_broker_to_rts.asString();
            }

            var value_broker_from_rts = streamDataMessage["broker_from_rts"];
            if (value_broker_from_rts != null)
            {
                message.BrokerFromRts = value_broker_from_rts.asString();
            }

            var value_date_exp = streamDataMessage["date_exp"];
            if (value_date_exp != null)
            {
                message.DateExp = value_date_exp.asDateTime();
            }

            var value_id_ord1 = streamDataMessage["id_ord1"];
            if (value_id_ord1 != null)
            {
                message.IdOrd1 = value_id_ord1.asLong();
            }

            var value_local_stamp = streamDataMessage["local_stamp"];
            if (value_local_stamp != null)
            {
                message.LocalStamp = value_local_stamp.asDateTime();
            }

                        return message;
        } // CreateCgmMultilegOrdersLog
        
        public static CgmDeal CreateCgmDeal(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmDeal();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_id_deal = streamDataMessage["id_deal"];
            if (value_id_deal != null)
            {
                message.IdDeal = value_id_deal.asLong();
            }

            var value_id_deal_multileg = streamDataMessage["id_deal_multileg"];
            if (value_id_deal_multileg != null)
            {
                message.IdDealMultileg = value_id_deal_multileg.asLong();
            }

            var value_id_repo = streamDataMessage["id_repo"];
            if (value_id_repo != null)
            {
                message.IdRepo = value_id_repo.asLong();
            }

            var value_pos = streamDataMessage["pos"];
            if (value_pos != null)
            {
                message.Pos = value_pos.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_id_ord_buy = streamDataMessage["id_ord_buy"];
            if (value_id_ord_buy != null)
            {
                message.IdOrdBuy = value_id_ord_buy.asLong();
            }

            var value_id_ord_sell = streamDataMessage["id_ord_sell"];
            if (value_id_ord_sell != null)
            {
                message.IdOrdSell = value_id_ord_sell.asLong();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_nosystem = streamDataMessage["nosystem"];
            if (value_nosystem != null)
            {
                message.Nosystem = value_nosystem.asSByte();
            }

            var value_xstatus_buy = streamDataMessage["xstatus_buy"];
            if (value_xstatus_buy != null)
            {
                message.XstatusBuy = value_xstatus_buy.asLong();
            }

            var value_xstatus_sell = streamDataMessage["xstatus_sell"];
            if (value_xstatus_sell != null)
            {
                message.XstatusSell = value_xstatus_sell.asLong();
            }

            var value_status_buy = streamDataMessage["status_buy"];
            if (value_status_buy != null)
            {
                message.StatusBuy = value_status_buy.asInt();
            }

            var value_status_sell = streamDataMessage["status_sell"];
            if (value_status_sell != null)
            {
                message.StatusSell = value_status_sell.asInt();
            }

            var value_ext_id_buy = streamDataMessage["ext_id_buy"];
            if (value_ext_id_buy != null)
            {
                message.ExtIdBuy = value_ext_id_buy.asInt();
            }

            var value_ext_id_sell = streamDataMessage["ext_id_sell"];
            if (value_ext_id_sell != null)
            {
                message.ExtIdSell = value_ext_id_sell.asInt();
            }

            var value_code_buy = streamDataMessage["code_buy"];
            if (value_code_buy != null)
            {
                message.CodeBuy = value_code_buy.asString();
            }

            var value_code_sell = streamDataMessage["code_sell"];
            if (value_code_sell != null)
            {
                message.CodeSell = value_code_sell.asString();
            }

            var value_comment_buy = streamDataMessage["comment_buy"];
            if (value_comment_buy != null)
            {
                message.CommentBuy = value_comment_buy.asString();
            }

            var value_comment_sell = streamDataMessage["comment_sell"];
            if (value_comment_sell != null)
            {
                message.CommentSell = value_comment_sell.asString();
            }

            var value_trust_buy = streamDataMessage["trust_buy"];
            if (value_trust_buy != null)
            {
                message.TrustBuy = value_trust_buy.asSByte();
            }

            var value_trust_sell = streamDataMessage["trust_sell"];
            if (value_trust_sell != null)
            {
                message.TrustSell = value_trust_sell.asSByte();
            }

            var value_hedge_buy = streamDataMessage["hedge_buy"];
            if (value_hedge_buy != null)
            {
                message.HedgeBuy = value_hedge_buy.asSByte();
            }

            var value_hedge_sell = streamDataMessage["hedge_sell"];
            if (value_hedge_sell != null)
            {
                message.HedgeSell = value_hedge_sell.asSByte();
            }

            var value_fee_buy = streamDataMessage["fee_buy"];
            if (value_fee_buy != null)
            {
                message.FeeBuy = value_fee_buy.asDouble();
            }

            var value_fee_sell = streamDataMessage["fee_sell"];
            if (value_fee_sell != null)
            {
                message.FeeSell = value_fee_sell.asDouble();
            }

            var value_login_buy = streamDataMessage["login_buy"];
            if (value_login_buy != null)
            {
                message.LoginBuy = value_login_buy.asString();
            }

            var value_login_sell = streamDataMessage["login_sell"];
            if (value_login_sell != null)
            {
                message.LoginSell = value_login_sell.asString();
            }

            var value_code_rts_buy = streamDataMessage["code_rts_buy"];
            if (value_code_rts_buy != null)
            {
                message.CodeRtsBuy = value_code_rts_buy.asString();
            }

            var value_code_rts_sell = streamDataMessage["code_rts_sell"];
            if (value_code_rts_sell != null)
            {
                message.CodeRtsSell = value_code_rts_sell.asString();
            }

                        return message;
        } // CreateCgmDeal
        
        public static CgmMultilegDeal CreateCgmMultilegDeal(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmMultilegDeal();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_isin_id_rd = streamDataMessage["isin_id_rd"];
            if (value_isin_id_rd != null)
            {
                message.IsinIdRd = value_isin_id_rd.asInt();
            }

            var value_isin_id_rb = streamDataMessage["isin_id_rb"];
            if (value_isin_id_rb != null)
            {
                message.IsinIdRb = value_isin_id_rb.asInt();
            }

            var value_isin_id_repo = streamDataMessage["isin_id_repo"];
            if (value_isin_id_repo != null)
            {
                message.IsinIdRepo = value_isin_id_repo.asInt();
            }

            var value_duration = streamDataMessage["duration"];
            if (value_duration != null)
            {
                message.Duration = value_duration.asInt();
            }

            var value_id_deal = streamDataMessage["id_deal"];
            if (value_id_deal != null)
            {
                message.IdDeal = value_id_deal.asLong();
            }

            var value_id_deal_rd = streamDataMessage["id_deal_rd"];
            if (value_id_deal_rd != null)
            {
                message.IdDealRd = value_id_deal_rd.asLong();
            }

            var value_id_deal_rb = streamDataMessage["id_deal_rb"];
            if (value_id_deal_rb != null)
            {
                message.IdDealRb = value_id_deal_rb.asLong();
            }

            var value_id_ord_buy = streamDataMessage["id_ord_buy"];
            if (value_id_ord_buy != null)
            {
                message.IdOrdBuy = value_id_ord_buy.asLong();
            }

            var value_id_ord_sell = streamDataMessage["id_ord_sell"];
            if (value_id_ord_sell != null)
            {
                message.IdOrdSell = value_id_ord_sell.asLong();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_rate_price = streamDataMessage["rate_price"];
            if (value_rate_price != null)
            {
                message.RatePrice = value_rate_price.asDouble();
            }

            var value_swap_price = streamDataMessage["swap_price"];
            if (value_swap_price != null)
            {
                message.SwapPrice = value_swap_price.asDouble();
            }

            var value_buyback_amount = streamDataMessage["buyback_amount"];
            if (value_buyback_amount != null)
            {
                message.BuybackAmount = value_buyback_amount.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_nosystem = streamDataMessage["nosystem"];
            if (value_nosystem != null)
            {
                message.Nosystem = value_nosystem.asSByte();
            }

            var value_xstatus_buy = streamDataMessage["xstatus_buy"];
            if (value_xstatus_buy != null)
            {
                message.XstatusBuy = value_xstatus_buy.asLong();
            }

            var value_xstatus_sell = streamDataMessage["xstatus_sell"];
            if (value_xstatus_sell != null)
            {
                message.XstatusSell = value_xstatus_sell.asLong();
            }

            var value_status_buy = streamDataMessage["status_buy"];
            if (value_status_buy != null)
            {
                message.StatusBuy = value_status_buy.asInt();
            }

            var value_status_sell = streamDataMessage["status_sell"];
            if (value_status_sell != null)
            {
                message.StatusSell = value_status_sell.asInt();
            }

            var value_ext_id_buy = streamDataMessage["ext_id_buy"];
            if (value_ext_id_buy != null)
            {
                message.ExtIdBuy = value_ext_id_buy.asInt();
            }

            var value_ext_id_sell = streamDataMessage["ext_id_sell"];
            if (value_ext_id_sell != null)
            {
                message.ExtIdSell = value_ext_id_sell.asInt();
            }

            var value_code_buy = streamDataMessage["code_buy"];
            if (value_code_buy != null)
            {
                message.CodeBuy = value_code_buy.asString();
            }

            var value_code_sell = streamDataMessage["code_sell"];
            if (value_code_sell != null)
            {
                message.CodeSell = value_code_sell.asString();
            }

            var value_comment_buy = streamDataMessage["comment_buy"];
            if (value_comment_buy != null)
            {
                message.CommentBuy = value_comment_buy.asString();
            }

            var value_comment_sell = streamDataMessage["comment_sell"];
            if (value_comment_sell != null)
            {
                message.CommentSell = value_comment_sell.asString();
            }

            var value_trust_buy = streamDataMessage["trust_buy"];
            if (value_trust_buy != null)
            {
                message.TrustBuy = value_trust_buy.asSByte();
            }

            var value_trust_sell = streamDataMessage["trust_sell"];
            if (value_trust_sell != null)
            {
                message.TrustSell = value_trust_sell.asSByte();
            }

            var value_hedge_buy = streamDataMessage["hedge_buy"];
            if (value_hedge_buy != null)
            {
                message.HedgeBuy = value_hedge_buy.asSByte();
            }

            var value_hedge_sell = streamDataMessage["hedge_sell"];
            if (value_hedge_sell != null)
            {
                message.HedgeSell = value_hedge_sell.asSByte();
            }

            var value_login_buy = streamDataMessage["login_buy"];
            if (value_login_buy != null)
            {
                message.LoginBuy = value_login_buy.asString();
            }

            var value_login_sell = streamDataMessage["login_sell"];
            if (value_login_sell != null)
            {
                message.LoginSell = value_login_sell.asString();
            }

            var value_code_rts_buy = streamDataMessage["code_rts_buy"];
            if (value_code_rts_buy != null)
            {
                message.CodeRtsBuy = value_code_rts_buy.asString();
            }

            var value_code_rts_sell = streamDataMessage["code_rts_sell"];
            if (value_code_rts_sell != null)
            {
                message.CodeRtsSell = value_code_rts_sell.asString();
            }

                        return message;
        } // CreateCgmMultilegDeal
        
        public static CgmHeartbeat CreateCgmHeartbeat(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmHeartbeat();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_server_time = streamDataMessage["server_time"];
            if (value_server_time != null)
            {
                message.ServerTime = value_server_time.asDateTime();
            }

                        return message;
        } // CreateCgmHeartbeat
        
        public static CgmSysEvents CreateCgmSysEvents(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmSysEvents();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_event_id = streamDataMessage["event_id"];
            if (value_event_id != null)
            {
                message.EventId = value_event_id.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_event_type = streamDataMessage["event_type"];
            if (value_event_type != null)
            {
                message.EventType = value_event_type.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmSysEvents
        
        public static CgmUserDeal CreateCgmUserDeal(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmUserDeal();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_id_deal = streamDataMessage["id_deal"];
            if (value_id_deal != null)
            {
                message.IdDeal = value_id_deal.asLong();
            }

            var value_id_deal_multileg = streamDataMessage["id_deal_multileg"];
            if (value_id_deal_multileg != null)
            {
                message.IdDealMultileg = value_id_deal_multileg.asLong();
            }

            var value_id_repo = streamDataMessage["id_repo"];
            if (value_id_repo != null)
            {
                message.IdRepo = value_id_repo.asLong();
            }

            var value_pos = streamDataMessage["pos"];
            if (value_pos != null)
            {
                message.Pos = value_pos.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_id_ord_buy = streamDataMessage["id_ord_buy"];
            if (value_id_ord_buy != null)
            {
                message.IdOrdBuy = value_id_ord_buy.asLong();
            }

            var value_id_ord_sell = streamDataMessage["id_ord_sell"];
            if (value_id_ord_sell != null)
            {
                message.IdOrdSell = value_id_ord_sell.asLong();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_nosystem = streamDataMessage["nosystem"];
            if (value_nosystem != null)
            {
                message.Nosystem = value_nosystem.asSByte();
            }

            var value_xstatus_buy = streamDataMessage["xstatus_buy"];
            if (value_xstatus_buy != null)
            {
                message.XstatusBuy = value_xstatus_buy.asLong();
            }

            var value_xstatus_sell = streamDataMessage["xstatus_sell"];
            if (value_xstatus_sell != null)
            {
                message.XstatusSell = value_xstatus_sell.asLong();
            }

            var value_status_buy = streamDataMessage["status_buy"];
            if (value_status_buy != null)
            {
                message.StatusBuy = value_status_buy.asInt();
            }

            var value_status_sell = streamDataMessage["status_sell"];
            if (value_status_sell != null)
            {
                message.StatusSell = value_status_sell.asInt();
            }

            var value_ext_id_buy = streamDataMessage["ext_id_buy"];
            if (value_ext_id_buy != null)
            {
                message.ExtIdBuy = value_ext_id_buy.asInt();
            }

            var value_ext_id_sell = streamDataMessage["ext_id_sell"];
            if (value_ext_id_sell != null)
            {
                message.ExtIdSell = value_ext_id_sell.asInt();
            }

            var value_code_buy = streamDataMessage["code_buy"];
            if (value_code_buy != null)
            {
                message.CodeBuy = value_code_buy.asString();
            }

            var value_code_sell = streamDataMessage["code_sell"];
            if (value_code_sell != null)
            {
                message.CodeSell = value_code_sell.asString();
            }

            var value_comment_buy = streamDataMessage["comment_buy"];
            if (value_comment_buy != null)
            {
                message.CommentBuy = value_comment_buy.asString();
            }

            var value_comment_sell = streamDataMessage["comment_sell"];
            if (value_comment_sell != null)
            {
                message.CommentSell = value_comment_sell.asString();
            }

            var value_trust_buy = streamDataMessage["trust_buy"];
            if (value_trust_buy != null)
            {
                message.TrustBuy = value_trust_buy.asSByte();
            }

            var value_trust_sell = streamDataMessage["trust_sell"];
            if (value_trust_sell != null)
            {
                message.TrustSell = value_trust_sell.asSByte();
            }

            var value_hedge_buy = streamDataMessage["hedge_buy"];
            if (value_hedge_buy != null)
            {
                message.HedgeBuy = value_hedge_buy.asSByte();
            }

            var value_hedge_sell = streamDataMessage["hedge_sell"];
            if (value_hedge_sell != null)
            {
                message.HedgeSell = value_hedge_sell.asSByte();
            }

            var value_fee_buy = streamDataMessage["fee_buy"];
            if (value_fee_buy != null)
            {
                message.FeeBuy = value_fee_buy.asDouble();
            }

            var value_fee_sell = streamDataMessage["fee_sell"];
            if (value_fee_sell != null)
            {
                message.FeeSell = value_fee_sell.asDouble();
            }

            var value_login_buy = streamDataMessage["login_buy"];
            if (value_login_buy != null)
            {
                message.LoginBuy = value_login_buy.asString();
            }

            var value_login_sell = streamDataMessage["login_sell"];
            if (value_login_sell != null)
            {
                message.LoginSell = value_login_sell.asString();
            }

            var value_code_rts_buy = streamDataMessage["code_rts_buy"];
            if (value_code_rts_buy != null)
            {
                message.CodeRtsBuy = value_code_rts_buy.asString();
            }

            var value_code_rts_sell = streamDataMessage["code_rts_sell"];
            if (value_code_rts_sell != null)
            {
                message.CodeRtsSell = value_code_rts_sell.asString();
            }

                        return message;
        } // CreateCgmUserDeal
        
        public static CgmUserMultilegDeal CreateCgmUserMultilegDeal(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmUserMultilegDeal();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_id_deal = streamDataMessage["id_deal"];
            if (value_id_deal != null)
            {
                message.IdDeal = value_id_deal.asLong();
            }

            var value_isin_id_rd = streamDataMessage["isin_id_rd"];
            if (value_isin_id_rd != null)
            {
                message.IsinIdRd = value_isin_id_rd.asInt();
            }

            var value_isin_id_rb = streamDataMessage["isin_id_rb"];
            if (value_isin_id_rb != null)
            {
                message.IsinIdRb = value_isin_id_rb.asInt();
            }

            var value_isin_id_repo = streamDataMessage["isin_id_repo"];
            if (value_isin_id_repo != null)
            {
                message.IsinIdRepo = value_isin_id_repo.asInt();
            }

            var value_duration = streamDataMessage["duration"];
            if (value_duration != null)
            {
                message.Duration = value_duration.asInt();
            }

            var value_id_deal_rd = streamDataMessage["id_deal_rd"];
            if (value_id_deal_rd != null)
            {
                message.IdDealRd = value_id_deal_rd.asLong();
            }

            var value_id_deal_rb = streamDataMessage["id_deal_rb"];
            if (value_id_deal_rb != null)
            {
                message.IdDealRb = value_id_deal_rb.asLong();
            }

            var value_id_ord_buy = streamDataMessage["id_ord_buy"];
            if (value_id_ord_buy != null)
            {
                message.IdOrdBuy = value_id_ord_buy.asLong();
            }

            var value_id_ord_sell = streamDataMessage["id_ord_sell"];
            if (value_id_ord_sell != null)
            {
                message.IdOrdSell = value_id_ord_sell.asLong();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_rate_price = streamDataMessage["rate_price"];
            if (value_rate_price != null)
            {
                message.RatePrice = value_rate_price.asDouble();
            }

            var value_swap_price = streamDataMessage["swap_price"];
            if (value_swap_price != null)
            {
                message.SwapPrice = value_swap_price.asDouble();
            }

            var value_buyback_amount = streamDataMessage["buyback_amount"];
            if (value_buyback_amount != null)
            {
                message.BuybackAmount = value_buyback_amount.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_nosystem = streamDataMessage["nosystem"];
            if (value_nosystem != null)
            {
                message.Nosystem = value_nosystem.asSByte();
            }

            var value_xstatus_buy = streamDataMessage["xstatus_buy"];
            if (value_xstatus_buy != null)
            {
                message.XstatusBuy = value_xstatus_buy.asLong();
            }

            var value_xstatus_sell = streamDataMessage["xstatus_sell"];
            if (value_xstatus_sell != null)
            {
                message.XstatusSell = value_xstatus_sell.asLong();
            }

            var value_status_buy = streamDataMessage["status_buy"];
            if (value_status_buy != null)
            {
                message.StatusBuy = value_status_buy.asInt();
            }

            var value_status_sell = streamDataMessage["status_sell"];
            if (value_status_sell != null)
            {
                message.StatusSell = value_status_sell.asInt();
            }

            var value_ext_id_buy = streamDataMessage["ext_id_buy"];
            if (value_ext_id_buy != null)
            {
                message.ExtIdBuy = value_ext_id_buy.asInt();
            }

            var value_ext_id_sell = streamDataMessage["ext_id_sell"];
            if (value_ext_id_sell != null)
            {
                message.ExtIdSell = value_ext_id_sell.asInt();
            }

            var value_code_buy = streamDataMessage["code_buy"];
            if (value_code_buy != null)
            {
                message.CodeBuy = value_code_buy.asString();
            }

            var value_code_sell = streamDataMessage["code_sell"];
            if (value_code_sell != null)
            {
                message.CodeSell = value_code_sell.asString();
            }

            var value_comment_buy = streamDataMessage["comment_buy"];
            if (value_comment_buy != null)
            {
                message.CommentBuy = value_comment_buy.asString();
            }

            var value_comment_sell = streamDataMessage["comment_sell"];
            if (value_comment_sell != null)
            {
                message.CommentSell = value_comment_sell.asString();
            }

            var value_trust_buy = streamDataMessage["trust_buy"];
            if (value_trust_buy != null)
            {
                message.TrustBuy = value_trust_buy.asSByte();
            }

            var value_trust_sell = streamDataMessage["trust_sell"];
            if (value_trust_sell != null)
            {
                message.TrustSell = value_trust_sell.asSByte();
            }

            var value_hedge_buy = streamDataMessage["hedge_buy"];
            if (value_hedge_buy != null)
            {
                message.HedgeBuy = value_hedge_buy.asSByte();
            }

            var value_hedge_sell = streamDataMessage["hedge_sell"];
            if (value_hedge_sell != null)
            {
                message.HedgeSell = value_hedge_sell.asSByte();
            }

            var value_login_buy = streamDataMessage["login_buy"];
            if (value_login_buy != null)
            {
                message.LoginBuy = value_login_buy.asString();
            }

            var value_login_sell = streamDataMessage["login_sell"];
            if (value_login_sell != null)
            {
                message.LoginSell = value_login_sell.asString();
            }

            var value_code_rts_buy = streamDataMessage["code_rts_buy"];
            if (value_code_rts_buy != null)
            {
                message.CodeRtsBuy = value_code_rts_buy.asString();
            }

            var value_code_rts_sell = streamDataMessage["code_rts_sell"];
            if (value_code_rts_sell != null)
            {
                message.CodeRtsSell = value_code_rts_sell.asString();
            }

                        return message;
        } // CreateCgmUserMultilegDeal
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.FutTradesHeartbeat
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "heartbeat":
                    return CGateMessageFactory.CreateCgmHeartbeat(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmHeartbeat CreateCgmHeartbeat(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmHeartbeat();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_server_time = streamDataMessage["server_time"];
            if (value_server_time != null)
            {
                message.ServerTime = value_server_time.asDateTime();
            }

                        return message;
        } // CreateCgmHeartbeat
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.Info
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "base_contracts_params":
                    return CGateMessageFactory.CreateCgmBaseContractsParams(source);
                case "futures_params":
                    return CGateMessageFactory.CreateCgmFuturesParams(source);
                case "virtual_futures_params":
                    return CGateMessageFactory.CreateCgmVirtualFuturesParams(source);
                case "options_params":
                    return CGateMessageFactory.CreateCgmOptionsParams(source);
                case "broker_params":
                    return CGateMessageFactory.CreateCgmBrokerParams(source);
                case "client_params":
                    return CGateMessageFactory.CreateCgmClientParams(source);
                case "sys_events":
                    return CGateMessageFactory.CreateCgmSysEvents(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmBaseContractsParams CreateCgmBaseContractsParams(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmBaseContractsParams();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_code_mcs = streamDataMessage["code_mcs"];
            if (value_code_mcs != null)
            {
                message.CodeMcs = value_code_mcs.asString();
            }

            var value_volat_num = streamDataMessage["volat_num"];
            if (value_volat_num != null)
            {
                message.VolatNum = value_volat_num.asSByte();
            }

            var value_points_num = streamDataMessage["points_num"];
            if (value_points_num != null)
            {
                message.PointsNum = value_points_num.asSByte();
            }

            var value_subrisk_step = streamDataMessage["subrisk_step"];
            if (value_subrisk_step != null)
            {
                message.SubriskStep = value_subrisk_step.asDouble();
            }

            var value_is_percent = streamDataMessage["is_percent"];
            if (value_is_percent != null)
            {
                message.IsPercent = value_is_percent.asSByte();
            }

            var value_percent_rate = streamDataMessage["percent_rate"];
            if (value_percent_rate != null)
            {
                message.PercentRate = value_percent_rate.asDouble();
            }

            var value_currency_volat = streamDataMessage["currency_volat"];
            if (value_currency_volat != null)
            {
                message.CurrencyVolat = value_currency_volat.asDouble();
            }

            var value_is_usd = streamDataMessage["is_usd"];
            if (value_is_usd != null)
            {
                message.IsUsd = value_is_usd.asSByte();
            }

            var value_usd_rate_curv_radius = streamDataMessage["usd_rate_curv_radius"];
            if (value_usd_rate_curv_radius != null)
            {
                message.UsdRateCurvRadius = value_usd_rate_curv_radius.asDouble();
            }

            var value_somc = streamDataMessage["somc"];
            if (value_somc != null)
            {
                message.Somc = value_somc.asDouble();
            }

                        return message;
        } // CreateCgmBaseContractsParams
        
        public static CgmFuturesParams CreateCgmFuturesParams(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFuturesParams();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_limit = streamDataMessage["limit"];
            if (value_limit != null)
            {
                message.Limit = value_limit.asDouble();
            }

            var value_settl_price = streamDataMessage["settl_price"];
            if (value_settl_price != null)
            {
                message.SettlPrice = value_settl_price.asDouble();
            }

            var value_spread_aspect = streamDataMessage["spread_aspect"];
            if (value_spread_aspect != null)
            {
                message.SpreadAspect = value_spread_aspect.asSByte();
            }

            var value_subrisk = streamDataMessage["subrisk"];
            if (value_subrisk != null)
            {
                message.Subrisk = value_subrisk.asSByte();
            }

            var value_step_price = streamDataMessage["step_price"];
            if (value_step_price != null)
            {
                message.StepPrice = value_step_price.asDouble();
            }

            var value_base_go = streamDataMessage["base_go"];
            if (value_base_go != null)
            {
                message.BaseGo = value_base_go.asDouble();
            }

            var value_exp_date = streamDataMessage["exp_date"];
            if (value_exp_date != null)
            {
                message.ExpDate = value_exp_date.asDateTime();
            }

            var value_spot_signs = streamDataMessage["spot_signs"];
            if (value_spot_signs != null)
            {
                message.SpotSigns = value_spot_signs.asSByte();
            }

            var value_settl_price_real = streamDataMessage["settl_price_real"];
            if (value_settl_price_real != null)
            {
                message.SettlPriceReal = value_settl_price_real.asDouble();
            }

            var value_min_step = streamDataMessage["min_step"];
            if (value_min_step != null)
            {
                message.MinStep = value_min_step.asDouble();
            }

                        return message;
        } // CreateCgmFuturesParams
        
        public static CgmVirtualFuturesParams CreateCgmVirtualFuturesParams(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmVirtualFuturesParams();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_isin_base = streamDataMessage["isin_base"];
            if (value_isin_base != null)
            {
                message.IsinBase = value_isin_base.asString();
            }

            var value_is_net_positive = streamDataMessage["is_net_positive"];
            if (value_is_net_positive != null)
            {
                message.IsNetPositive = value_is_net_positive.asSByte();
            }

            var value_volat_range = streamDataMessage["volat_range"];
            if (value_volat_range != null)
            {
                message.VolatRange = value_volat_range.asDouble();
            }

            var value_t_squared = streamDataMessage["t_squared"];
            if (value_t_squared != null)
            {
                message.TSquared = value_t_squared.asDouble();
            }

            var value_max_addrisk = streamDataMessage["max_addrisk"];
            if (value_max_addrisk != null)
            {
                message.MaxAddrisk = value_max_addrisk.asDouble();
            }

            var value_a = streamDataMessage["a"];
            if (value_a != null)
            {
                message.A = value_a.asDouble();
            }

            var value_b = streamDataMessage["b"];
            if (value_b != null)
            {
                message.B = value_b.asDouble();
            }

            var value_c = streamDataMessage["c"];
            if (value_c != null)
            {
                message.C = value_c.asDouble();
            }

            var value_d = streamDataMessage["d"];
            if (value_d != null)
            {
                message.D = value_d.asDouble();
            }

            var value_e = streamDataMessage["e"];
            if (value_e != null)
            {
                message.E = value_e.asDouble();
            }

            var value_s = streamDataMessage["s"];
            if (value_s != null)
            {
                message.S = value_s.asDouble();
            }

            var value_exp_date = streamDataMessage["exp_date"];
            if (value_exp_date != null)
            {
                message.ExpDate = value_exp_date.asDateTime();
            }

            var value_fut_type = streamDataMessage["fut_type"];
            if (value_fut_type != null)
            {
                message.FutType = value_fut_type.asSByte();
            }

            var value_use_null_volat = streamDataMessage["use_null_volat"];
            if (value_use_null_volat != null)
            {
                message.UseNullVolat = value_use_null_volat.asSByte();
            }

            var value_exp_clearings_bf = streamDataMessage["exp_clearings_bf"];
            if (value_exp_clearings_bf != null)
            {
                message.ExpClearingsBf = value_exp_clearings_bf.asInt();
            }

            var value_exp_clearings_cc = streamDataMessage["exp_clearings_cc"];
            if (value_exp_clearings_cc != null)
            {
                message.ExpClearingsCc = value_exp_clearings_cc.asInt();
            }

                        return message;
        } // CreateCgmVirtualFuturesParams
        
        public static CgmOptionsParams CreateCgmOptionsParams(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptionsParams();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_isin_base = streamDataMessage["isin_base"];
            if (value_isin_base != null)
            {
                message.IsinBase = value_isin_base.asString();
            }

            var value_strike = streamDataMessage["strike"];
            if (value_strike != null)
            {
                message.Strike = value_strike.asDouble();
            }

            var value_opt_type = streamDataMessage["opt_type"];
            if (value_opt_type != null)
            {
                message.OptType = value_opt_type.asSByte();
            }

            var value_settl_price = streamDataMessage["settl_price"];
            if (value_settl_price != null)
            {
                message.SettlPrice = value_settl_price.asDouble();
            }

            var value_base_go_sell = streamDataMessage["base_go_sell"];
            if (value_base_go_sell != null)
            {
                message.BaseGoSell = value_base_go_sell.asDouble();
            }

            var value_synth_base_go = streamDataMessage["synth_base_go"];
            if (value_synth_base_go != null)
            {
                message.SynthBaseGo = value_synth_base_go.asDouble();
            }

            var value_base_go_buy = streamDataMessage["base_go_buy"];
            if (value_base_go_buy != null)
            {
                message.BaseGoBuy = value_base_go_buy.asDouble();
            }

                        return message;
        } // CreateCgmOptionsParams
        
        public static CgmBrokerParams CreateCgmBrokerParams(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmBrokerParams();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_broker_code = streamDataMessage["broker_code"];
            if (value_broker_code != null)
            {
                message.BrokerCode = value_broker_code.asString();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_limit_spot_sell = streamDataMessage["limit_spot_sell"];
            if (value_limit_spot_sell != null)
            {
                message.LimitSpotSell = value_limit_spot_sell.asInt();
            }

            var value_used_limit_spot_sell = streamDataMessage["used_limit_spot_sell"];
            if (value_used_limit_spot_sell != null)
            {
                message.UsedLimitSpotSell = value_used_limit_spot_sell.asInt();
            }

                        return message;
        } // CreateCgmBrokerParams
        
        public static CgmClientParams CreateCgmClientParams(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmClientParams();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_coeff_go = streamDataMessage["coeff_go"];
            if (value_coeff_go != null)
            {
                message.CoeffGo = value_coeff_go.asDouble();
            }

            var value_limit_spot_sell = streamDataMessage["limit_spot_sell"];
            if (value_limit_spot_sell != null)
            {
                message.LimitSpotSell = value_limit_spot_sell.asInt();
            }

            var value_used_limit_spot_sell = streamDataMessage["used_limit_spot_sell"];
            if (value_used_limit_spot_sell != null)
            {
                message.UsedLimitSpotSell = value_used_limit_spot_sell.asInt();
            }

                        return message;
        } // CreateCgmClientParams
        
        public static CgmSysEvents CreateCgmSysEvents(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmSysEvents();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_event_id = streamDataMessage["event_id"];
            if (value_event_id != null)
            {
                message.EventId = value_event_id.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_event_type = streamDataMessage["event_type"];
            if (value_event_type != null)
            {
                message.EventType = value_event_type.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmSysEvents
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.MiscInfo
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "volat_coeff":
                    return CGateMessageFactory.CreateCgmVolatCoeff(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmVolatCoeff CreateCgmVolatCoeff(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmVolatCoeff();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_a = streamDataMessage["a"];
            if (value_a != null)
            {
                message.A = value_a.asDouble();
            }

            var value_b = streamDataMessage["b"];
            if (value_b != null)
            {
                message.B = value_b.asDouble();
            }

            var value_c = streamDataMessage["c"];
            if (value_c != null)
            {
                message.C = value_c.asDouble();
            }

            var value_d = streamDataMessage["d"];
            if (value_d != null)
            {
                message.D = value_d.asDouble();
            }

            var value_e = streamDataMessage["e"];
            if (value_e != null)
            {
                message.E = value_e.asDouble();
            }

            var value_s = streamDataMessage["s"];
            if (value_s != null)
            {
                message.S = value_s.asDouble();
            }

                        return message;
        } // CreateCgmVolatCoeff
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.Mm
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "fut_MM_info":
                    return CGateMessageFactory.CreateCgmFutMmInfo(source);
                case "opt_MM_info":
                    return CGateMessageFactory.CreateCgmOptMmInfo(source);
                case "cs_mm_rule":
                    return CGateMessageFactory.CreateCgmCsMmRule(source);
                case "mm_agreement_filter":
                    return CGateMessageFactory.CreateCgmMmAgreementFilter(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmFutMmInfo CreateCgmFutMmInfo(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutMmInfo();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_spread = streamDataMessage["spread"];
            if (value_spread != null)
            {
                message.Spread = value_spread.asDouble();
            }

            var value_price_edge_sell = streamDataMessage["price_edge_sell"];
            if (value_price_edge_sell != null)
            {
                message.PriceEdgeSell = value_price_edge_sell.asDouble();
            }

            var value_amount_sells = streamDataMessage["amount_sells"];
            if (value_amount_sells != null)
            {
                message.AmountSells = value_amount_sells.asInt();
            }

            var value_price_edge_buy = streamDataMessage["price_edge_buy"];
            if (value_price_edge_buy != null)
            {
                message.PriceEdgeBuy = value_price_edge_buy.asDouble();
            }

            var value_amount_buys = streamDataMessage["amount_buys"];
            if (value_amount_buys != null)
            {
                message.AmountBuys = value_amount_buys.asInt();
            }

            var value_mm_spread = streamDataMessage["mm_spread"];
            if (value_mm_spread != null)
            {
                message.MmSpread = value_mm_spread.asDouble();
            }

            var value_mm_amount = streamDataMessage["mm_amount"];
            if (value_mm_amount != null)
            {
                message.MmAmount = value_mm_amount.asInt();
            }

            var value_spread_sign = streamDataMessage["spread_sign"];
            if (value_spread_sign != null)
            {
                message.SpreadSign = value_spread_sign.asSByte();
            }

            var value_amount_sign = streamDataMessage["amount_sign"];
            if (value_amount_sign != null)
            {
                message.AmountSign = value_amount_sign.asSByte();
            }

            var value_percent_time = streamDataMessage["percent_time"];
            if (value_percent_time != null)
            {
                message.PercentTime = value_percent_time.asDouble();
            }

            var value_period_start = streamDataMessage["period_start"];
            if (value_period_start != null)
            {
                message.PeriodStart = value_period_start.asDateTime();
            }

            var value_period_end = streamDataMessage["period_end"];
            if (value_period_end != null)
            {
                message.PeriodEnd = value_period_end.asDateTime();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_active_sign = streamDataMessage["active_sign"];
            if (value_active_sign != null)
            {
                message.ActiveSign = value_active_sign.asInt();
            }

            var value_agmt_id = streamDataMessage["agmt_id"];
            if (value_agmt_id != null)
            {
                message.AgmtId = value_agmt_id.asInt();
            }

            var value_fulfil_min = streamDataMessage["fulfil_min"];
            if (value_fulfil_min != null)
            {
                message.FulfilMin = value_fulfil_min.asDouble();
            }

            var value_fulfil_partial = streamDataMessage["fulfil_partial"];
            if (value_fulfil_partial != null)
            {
                message.FulfilPartial = value_fulfil_partial.asDouble();
            }

            var value_fulfil_total = streamDataMessage["fulfil_total"];
            if (value_fulfil_total != null)
            {
                message.FulfilTotal = value_fulfil_total.asDouble();
            }

            var value_is_fulfil_min = streamDataMessage["is_fulfil_min"];
            if (value_is_fulfil_min != null)
            {
                message.IsFulfilMin = value_is_fulfil_min.asSByte();
            }

            var value_is_fulfil_partial = streamDataMessage["is_fulfil_partial"];
            if (value_is_fulfil_partial != null)
            {
                message.IsFulfilPartial = value_is_fulfil_partial.asSByte();
            }

            var value_is_fulfil_total = streamDataMessage["is_fulfil_total"];
            if (value_is_fulfil_total != null)
            {
                message.IsFulfilTotal = value_is_fulfil_total.asSByte();
            }

            var value_is_rf = streamDataMessage["is_rf"];
            if (value_is_rf != null)
            {
                message.IsRf = value_is_rf.asSByte();
            }

            var value_id_group = streamDataMessage["id_group"];
            if (value_id_group != null)
            {
                message.IdGroup = value_id_group.asInt();
            }

                        return message;
        } // CreateCgmFutMmInfo
        
        public static CgmOptMmInfo CreateCgmOptMmInfo(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptMmInfo();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_spread = streamDataMessage["spread"];
            if (value_spread != null)
            {
                message.Spread = value_spread.asDouble();
            }

            var value_price_edge_sell = streamDataMessage["price_edge_sell"];
            if (value_price_edge_sell != null)
            {
                message.PriceEdgeSell = value_price_edge_sell.asDouble();
            }

            var value_amount_sells = streamDataMessage["amount_sells"];
            if (value_amount_sells != null)
            {
                message.AmountSells = value_amount_sells.asInt();
            }

            var value_price_edge_buy = streamDataMessage["price_edge_buy"];
            if (value_price_edge_buy != null)
            {
                message.PriceEdgeBuy = value_price_edge_buy.asDouble();
            }

            var value_amount_buys = streamDataMessage["amount_buys"];
            if (value_amount_buys != null)
            {
                message.AmountBuys = value_amount_buys.asInt();
            }

            var value_mm_spread = streamDataMessage["mm_spread"];
            if (value_mm_spread != null)
            {
                message.MmSpread = value_mm_spread.asDouble();
            }

            var value_mm_amount = streamDataMessage["mm_amount"];
            if (value_mm_amount != null)
            {
                message.MmAmount = value_mm_amount.asInt();
            }

            var value_spread_sign = streamDataMessage["spread_sign"];
            if (value_spread_sign != null)
            {
                message.SpreadSign = value_spread_sign.asSByte();
            }

            var value_amount_sign = streamDataMessage["amount_sign"];
            if (value_amount_sign != null)
            {
                message.AmountSign = value_amount_sign.asSByte();
            }

            var value_percent_time = streamDataMessage["percent_time"];
            if (value_percent_time != null)
            {
                message.PercentTime = value_percent_time.asDouble();
            }

            var value_period_start = streamDataMessage["period_start"];
            if (value_period_start != null)
            {
                message.PeriodStart = value_period_start.asDateTime();
            }

            var value_period_end = streamDataMessage["period_end"];
            if (value_period_end != null)
            {
                message.PeriodEnd = value_period_end.asDateTime();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_cstrike_offset = streamDataMessage["cstrike_offset"];
            if (value_cstrike_offset != null)
            {
                message.CstrikeOffset = value_cstrike_offset.asDouble();
            }

            var value_active_sign = streamDataMessage["active_sign"];
            if (value_active_sign != null)
            {
                message.ActiveSign = value_active_sign.asInt();
            }

            var value_agmt_id = streamDataMessage["agmt_id"];
            if (value_agmt_id != null)
            {
                message.AgmtId = value_agmt_id.asInt();
            }

            var value_fulfil_min = streamDataMessage["fulfil_min"];
            if (value_fulfil_min != null)
            {
                message.FulfilMin = value_fulfil_min.asDouble();
            }

            var value_fulfil_partial = streamDataMessage["fulfil_partial"];
            if (value_fulfil_partial != null)
            {
                message.FulfilPartial = value_fulfil_partial.asDouble();
            }

            var value_fulfil_total = streamDataMessage["fulfil_total"];
            if (value_fulfil_total != null)
            {
                message.FulfilTotal = value_fulfil_total.asDouble();
            }

            var value_is_fulfil_min = streamDataMessage["is_fulfil_min"];
            if (value_is_fulfil_min != null)
            {
                message.IsFulfilMin = value_is_fulfil_min.asSByte();
            }

            var value_is_fulfil_partial = streamDataMessage["is_fulfil_partial"];
            if (value_is_fulfil_partial != null)
            {
                message.IsFulfilPartial = value_is_fulfil_partial.asSByte();
            }

            var value_is_fulfil_total = streamDataMessage["is_fulfil_total"];
            if (value_is_fulfil_total != null)
            {
                message.IsFulfilTotal = value_is_fulfil_total.asSByte();
            }

            var value_is_rf = streamDataMessage["is_rf"];
            if (value_is_rf != null)
            {
                message.IsRf = value_is_rf.asSByte();
            }

            var value_id_group = streamDataMessage["id_group"];
            if (value_id_group != null)
            {
                message.IdGroup = value_id_group.asInt();
            }

                        return message;
        } // CreateCgmOptMmInfo
        
        public static CgmCsMmRule CreateCgmCsMmRule(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmCsMmRule();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

                        return message;
        } // CreateCgmCsMmRule
        
        public static CgmMmAgreementFilter CreateCgmMmAgreementFilter(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmMmAgreementFilter();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_agmt_id = streamDataMessage["agmt_id"];
            if (value_agmt_id != null)
            {
                message.AgmtId = value_agmt_id.asInt();
            }

            var value_agreement = streamDataMessage["agreement"];
            if (value_agreement != null)
            {
                message.Agreement = value_agreement.asString();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_is_fut = streamDataMessage["is_fut"];
            if (value_is_fut != null)
            {
                message.IsFut = value_is_fut.asSByte();
            }

                        return message;
        } // CreateCgmMmAgreementFilter
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.OptCommon
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "common":
                    return CGateMessageFactory.CreateCgmCommon(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmCommon CreateCgmCommon(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmCommon();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_best_sell = streamDataMessage["best_sell"];
            if (value_best_sell != null)
            {
                message.BestSell = value_best_sell.asDouble();
            }

            var value_amount_sell = streamDataMessage["amount_sell"];
            if (value_amount_sell != null)
            {
                message.AmountSell = value_amount_sell.asInt();
            }

            var value_best_buy = streamDataMessage["best_buy"];
            if (value_best_buy != null)
            {
                message.BestBuy = value_best_buy.asDouble();
            }

            var value_amount_buy = streamDataMessage["amount_buy"];
            if (value_amount_buy != null)
            {
                message.AmountBuy = value_amount_buy.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_trend = streamDataMessage["trend"];
            if (value_trend != null)
            {
                message.Trend = value_trend.asDouble();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_deal_time = streamDataMessage["deal_time"];
            if (value_deal_time != null)
            {
                message.DealTime = value_deal_time.asDateTime();
            }

            var value_min_price = streamDataMessage["min_price"];
            if (value_min_price != null)
            {
                message.MinPrice = value_min_price.asDouble();
            }

            var value_max_price = streamDataMessage["max_price"];
            if (value_max_price != null)
            {
                message.MaxPrice = value_max_price.asDouble();
            }

            var value_avr_price = streamDataMessage["avr_price"];
            if (value_avr_price != null)
            {
                message.AvrPrice = value_avr_price.asDouble();
            }

            var value_old_kotir = streamDataMessage["old_kotir"];
            if (value_old_kotir != null)
            {
                message.OldKotir = value_old_kotir.asDouble();
            }

            var value_deal_count = streamDataMessage["deal_count"];
            if (value_deal_count != null)
            {
                message.DealCount = value_deal_count.asInt();
            }

            var value_contr_count = streamDataMessage["contr_count"];
            if (value_contr_count != null)
            {
                message.ContrCount = value_contr_count.asInt();
            }

            var value_capital = streamDataMessage["capital"];
            if (value_capital != null)
            {
                message.Capital = value_capital.asDouble();
            }

            var value_pos = streamDataMessage["pos"];
            if (value_pos != null)
            {
                message.Pos = value_pos.asInt();
            }

            var value_mod_time = streamDataMessage["mod_time"];
            if (value_mod_time != null)
            {
                message.ModTime = value_mod_time.asDateTime();
            }

            var value_isin_is_spec = streamDataMessage["isin_is_spec"];
            if (value_isin_is_spec != null)
            {
                message.IsinIsSpec = value_isin_is_spec.asSByte();
            }

            var value_orders_sell_qty = streamDataMessage["orders_sell_qty"];
            if (value_orders_sell_qty != null)
            {
                message.OrdersSellQty = value_orders_sell_qty.asInt();
            }

            var value_orders_sell_amount = streamDataMessage["orders_sell_amount"];
            if (value_orders_sell_amount != null)
            {
                message.OrdersSellAmount = value_orders_sell_amount.asInt();
            }

            var value_orders_buy_qty = streamDataMessage["orders_buy_qty"];
            if (value_orders_buy_qty != null)
            {
                message.OrdersBuyQty = value_orders_buy_qty.asInt();
            }

            var value_orders_buy_amount = streamDataMessage["orders_buy_amount"];
            if (value_orders_buy_amount != null)
            {
                message.OrdersBuyAmount = value_orders_buy_amount.asInt();
            }

            var value_open_price = streamDataMessage["open_price"];
            if (value_open_price != null)
            {
                message.OpenPrice = value_open_price.asDouble();
            }

            var value_close_price = streamDataMessage["close_price"];
            if (value_close_price != null)
            {
                message.ClosePrice = value_close_price.asDouble();
            }

            var value_local_time = streamDataMessage["local_time"];
            if (value_local_time != null)
            {
                message.LocalTime = value_local_time.asDateTime();
            }

                        return message;
        } // CreateCgmCommon
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.OptInfo
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "opt_rejected_orders":
                    return CGateMessageFactory.CreateCgmOptRejectedOrders(source);
                case "opt_intercl_info":
                    return CGateMessageFactory.CreateCgmOptInterclInfo(source);
                case "opt_exp_orders":
                    return CGateMessageFactory.CreateCgmOptExpOrders(source);
                case "opt_vcb":
                    return CGateMessageFactory.CreateCgmOptVcb(source);
                case "opt_sess_contents":
                    return CGateMessageFactory.CreateCgmOptSessContents(source);
                case "opt_sess_settl":
                    return CGateMessageFactory.CreateCgmOptSessSettl(source);
                case "sys_events":
                    return CGateMessageFactory.CreateCgmSysEvents(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmOptRejectedOrders CreateCgmOptRejectedOrders(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptRejectedOrders();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_order_id = streamDataMessage["order_id"];
            if (value_order_id != null)
            {
                message.OrderId = value_order_id.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_moment_reject = streamDataMessage["moment_reject"];
            if (value_moment_reject != null)
            {
                message.MomentReject = value_moment_reject.asDateTime();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asSByte();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_date_exp = streamDataMessage["date_exp"];
            if (value_date_exp != null)
            {
                message.DateExp = value_date_exp.asDateTime();
            }

            var value_id_ord1 = streamDataMessage["id_ord1"];
            if (value_id_ord1 != null)
            {
                message.IdOrd1 = value_id_ord1.asLong();
            }

            var value_ret_code = streamDataMessage["ret_code"];
            if (value_ret_code != null)
            {
                message.RetCode = value_ret_code.asInt();
            }

            var value_ret_message = streamDataMessage["ret_message"];
            if (value_ret_message != null)
            {
                message.RetMessage = value_ret_message.asString();
            }

            var value_comment = streamDataMessage["comment"];
            if (value_comment != null)
            {
                message.Comment = value_comment.asString();
            }

            var value_login_from = streamDataMessage["login_from"];
            if (value_login_from != null)
            {
                message.LoginFrom = value_login_from.asString();
            }

            var value_ext_id = streamDataMessage["ext_id"];
            if (value_ext_id != null)
            {
                message.ExtId = value_ext_id.asInt();
            }

                        return message;
        } // CreateCgmOptRejectedOrders
        
        public static CgmOptInterclInfo CreateCgmOptInterclInfo(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptInterclInfo();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_vm_intercl = streamDataMessage["vm_intercl"];
            if (value_vm_intercl != null)
            {
                message.VmIntercl = value_vm_intercl.asDouble();
            }

                        return message;
        } // CreateCgmOptInterclInfo
        
        public static CgmOptExpOrders CreateCgmOptExpOrders(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptExpOrders();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_exporder_id = streamDataMessage["exporder_id"];
            if (value_exporder_id != null)
            {
                message.ExporderId = value_exporder_id.asLong();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_date = streamDataMessage["date"];
            if (value_date != null)
            {
                message.Date = value_date.asDateTime();
            }

            var value_amount_apply = streamDataMessage["amount_apply"];
            if (value_amount_apply != null)
            {
                message.AmountApply = value_amount_apply.asInt();
            }

                        return message;
        } // CreateCgmOptExpOrders
        
        public static CgmOptVcb CreateCgmOptVcb(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptVcb();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_name = streamDataMessage["name"];
            if (value_name != null)
            {
                message.Name = value_name.asString();
            }

            var value_exec_type = streamDataMessage["exec_type"];
            if (value_exec_type != null)
            {
                message.ExecType = value_exec_type.asString();
            }

            var value_curr = streamDataMessage["curr"];
            if (value_curr != null)
            {
                message.Curr = value_curr.asString();
            }

            var value_exch_pay = streamDataMessage["exch_pay"];
            if (value_exch_pay != null)
            {
                message.ExchPay = value_exch_pay.asDouble();
            }

            var value_exch_pay_scalped = streamDataMessage["exch_pay_scalped"];
            if (value_exch_pay_scalped != null)
            {
                message.ExchPayScalped = value_exch_pay_scalped.asSByte();
            }

            var value_clear_pay = streamDataMessage["clear_pay"];
            if (value_clear_pay != null)
            {
                message.ClearPay = value_clear_pay.asDouble();
            }

            var value_clear_pay_scalped = streamDataMessage["clear_pay_scalped"];
            if (value_clear_pay_scalped != null)
            {
                message.ClearPayScalped = value_clear_pay_scalped.asSByte();
            }

            var value_sell_fee = streamDataMessage["sell_fee"];
            if (value_sell_fee != null)
            {
                message.SellFee = value_sell_fee.asDouble();
            }

            var value_buy_fee = streamDataMessage["buy_fee"];
            if (value_buy_fee != null)
            {
                message.BuyFee = value_buy_fee.asDouble();
            }

            var value_trade_scheme = streamDataMessage["trade_scheme"];
            if (value_trade_scheme != null)
            {
                message.TradeScheme = value_trade_scheme.asString();
            }

            var value_coeff_out = streamDataMessage["coeff_out"];
            if (value_coeff_out != null)
            {
                message.CoeffOut = value_coeff_out.asDouble();
            }

            var value_is_spec = streamDataMessage["is_spec"];
            if (value_is_spec != null)
            {
                message.IsSpec = value_is_spec.asSByte();
            }

            var value_spec_spread = streamDataMessage["spec_spread"];
            if (value_spec_spread != null)
            {
                message.SpecSpread = value_spec_spread.asDouble();
            }

            var value_min_vol = streamDataMessage["min_vol"];
            if (value_min_vol != null)
            {
                message.MinVol = value_min_vol.asInt();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_rate_id = streamDataMessage["rate_id"];
            if (value_rate_id != null)
            {
                message.RateId = value_rate_id.asInt();
            }

                        return message;
        } // CreateCgmOptVcb
        
        public static CgmOptSessContents CreateCgmOptSessContents(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptSessContents();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_short_isin = streamDataMessage["short_isin"];
            if (value_short_isin != null)
            {
                message.ShortIsin = value_short_isin.asString();
            }

            var value_name = streamDataMessage["name"];
            if (value_name != null)
            {
                message.Name = value_name.asString();
            }

            var value_code_vcb = streamDataMessage["code_vcb"];
            if (value_code_vcb != null)
            {
                message.CodeVcb = value_code_vcb.asString();
            }

            var value_fut_isin_id = streamDataMessage["fut_isin_id"];
            if (value_fut_isin_id != null)
            {
                message.FutIsinId = value_fut_isin_id.asInt();
            }

            var value_is_limited = streamDataMessage["is_limited"];
            if (value_is_limited != null)
            {
                message.IsLimited = value_is_limited.asSByte();
            }

            var value_limit_up = streamDataMessage["limit_up"];
            if (value_limit_up != null)
            {
                message.LimitUp = value_limit_up.asDouble();
            }

            var value_limit_down = streamDataMessage["limit_down"];
            if (value_limit_down != null)
            {
                message.LimitDown = value_limit_down.asDouble();
            }

            var value_old_kotir = streamDataMessage["old_kotir"];
            if (value_old_kotir != null)
            {
                message.OldKotir = value_old_kotir.asDouble();
            }

            var value_bgo_c = streamDataMessage["bgo_c"];
            if (value_bgo_c != null)
            {
                message.BgoC = value_bgo_c.asDouble();
            }

            var value_bgo_nc = streamDataMessage["bgo_nc"];
            if (value_bgo_nc != null)
            {
                message.BgoNc = value_bgo_nc.asDouble();
            }

            var value_europe = streamDataMessage["europe"];
            if (value_europe != null)
            {
                message.Europe = value_europe.asSByte();
            }

            var value_put = streamDataMessage["put"];
            if (value_put != null)
            {
                message.Put = value_put.asSByte();
            }

            var value_strike = streamDataMessage["strike"];
            if (value_strike != null)
            {
                message.Strike = value_strike.asDouble();
            }

            var value_roundto = streamDataMessage["roundto"];
            if (value_roundto != null)
            {
                message.Roundto = value_roundto.asInt();
            }

            var value_min_step = streamDataMessage["min_step"];
            if (value_min_step != null)
            {
                message.MinStep = value_min_step.asDouble();
            }

            var value_lot_volume = streamDataMessage["lot_volume"];
            if (value_lot_volume != null)
            {
                message.LotVolume = value_lot_volume.asInt();
            }

            var value_step_price = streamDataMessage["step_price"];
            if (value_step_price != null)
            {
                message.StepPrice = value_step_price.asDouble();
            }

            var value_d_pg = streamDataMessage["d_pg"];
            if (value_d_pg != null)
            {
                message.DPg = value_d_pg.asDateTime();
            }

            var value_d_exec_beg = streamDataMessage["d_exec_beg"];
            if (value_d_exec_beg != null)
            {
                message.DExecBeg = value_d_exec_beg.asDateTime();
            }

            var value_d_exec_end = streamDataMessage["d_exec_end"];
            if (value_d_exec_end != null)
            {
                message.DExecEnd = value_d_exec_end.asDateTime();
            }

            var value_signs = streamDataMessage["signs"];
            if (value_signs != null)
            {
                message.Signs = value_signs.asInt();
            }

            var value_last_cl_quote = streamDataMessage["last_cl_quote"];
            if (value_last_cl_quote != null)
            {
                message.LastClQuote = value_last_cl_quote.asDouble();
            }

            var value_bgo_buy = streamDataMessage["bgo_buy"];
            if (value_bgo_buy != null)
            {
                message.BgoBuy = value_bgo_buy.asDouble();
            }

            var value_base_isin_id = streamDataMessage["base_isin_id"];
            if (value_base_isin_id != null)
            {
                message.BaseIsinId = value_base_isin_id.asInt();
            }

            var value_d_start = streamDataMessage["d_start"];
            if (value_d_start != null)
            {
                message.DStart = value_d_start.asDateTime();
            }

            var value_exch_pay = streamDataMessage["exch_pay"];
            if (value_exch_pay != null)
            {
                message.ExchPay = value_exch_pay.asDouble();
            }

                        return message;
        } // CreateCgmOptSessContents
        
        public static CgmOptSessSettl CreateCgmOptSessSettl(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptSessSettl();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_date_clr = streamDataMessage["date_clr"];
            if (value_date_clr != null)
            {
                message.DateClr = value_date_clr.asDateTime();
            }

            var value_isin = streamDataMessage["isin"];
            if (value_isin != null)
            {
                message.Isin = value_isin.asString();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_volat = streamDataMessage["volat"];
            if (value_volat != null)
            {
                message.Volat = value_volat.asDouble();
            }

            var value_theor_price = streamDataMessage["theor_price"];
            if (value_theor_price != null)
            {
                message.TheorPrice = value_theor_price.asDouble();
            }

                        return message;
        } // CreateCgmOptSessSettl
        
        public static CgmSysEvents CreateCgmSysEvents(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmSysEvents();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_event_id = streamDataMessage["event_id"];
            if (value_event_id != null)
            {
                message.EventId = value_event_id.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_event_type = streamDataMessage["event_type"];
            if (value_event_type != null)
            {
                message.EventType = value_event_type.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmSysEvents
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.OptTrades
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "orders_log":
                    return CGateMessageFactory.CreateCgmOrdersLog(source);
                case "deal":
                    return CGateMessageFactory.CreateCgmDeal(source);
                case "heartbeat":
                    return CGateMessageFactory.CreateCgmHeartbeat(source);
                case "sys_events":
                    return CGateMessageFactory.CreateCgmSysEvents(source);
                case "user_deal":
                    return CGateMessageFactory.CreateCgmUserDeal(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmOrdersLog CreateCgmOrdersLog(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOrdersLog();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_id_ord = streamDataMessage["id_ord"];
            if (value_id_ord != null)
            {
                message.IdOrd = value_id_ord.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_amount_rest = streamDataMessage["amount_rest"];
            if (value_amount_rest != null)
            {
                message.AmountRest = value_amount_rest.asInt();
            }

            var value_id_deal = streamDataMessage["id_deal"];
            if (value_id_deal != null)
            {
                message.IdDeal = value_id_deal.asLong();
            }

            var value_xstatus = streamDataMessage["xstatus"];
            if (value_xstatus != null)
            {
                message.Xstatus = value_xstatus.asLong();
            }

            var value_status = streamDataMessage["status"];
            if (value_status != null)
            {
                message.Status = value_status.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asSByte();
            }

            var value_action = streamDataMessage["action"];
            if (value_action != null)
            {
                message.Action = value_action.asSByte();
            }

            var value_deal_price = streamDataMessage["deal_price"];
            if (value_deal_price != null)
            {
                message.DealPrice = value_deal_price.asDouble();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_login_from = streamDataMessage["login_from"];
            if (value_login_from != null)
            {
                message.LoginFrom = value_login_from.asString();
            }

            var value_comment = streamDataMessage["comment"];
            if (value_comment != null)
            {
                message.Comment = value_comment.asString();
            }

            var value_hedge = streamDataMessage["hedge"];
            if (value_hedge != null)
            {
                message.Hedge = value_hedge.asSByte();
            }

            var value_trust = streamDataMessage["trust"];
            if (value_trust != null)
            {
                message.Trust = value_trust.asSByte();
            }

            var value_ext_id = streamDataMessage["ext_id"];
            if (value_ext_id != null)
            {
                message.ExtId = value_ext_id.asInt();
            }

            var value_broker_to = streamDataMessage["broker_to"];
            if (value_broker_to != null)
            {
                message.BrokerTo = value_broker_to.asString();
            }

            var value_broker_to_rts = streamDataMessage["broker_to_rts"];
            if (value_broker_to_rts != null)
            {
                message.BrokerToRts = value_broker_to_rts.asString();
            }

            var value_broker_from_rts = streamDataMessage["broker_from_rts"];
            if (value_broker_from_rts != null)
            {
                message.BrokerFromRts = value_broker_from_rts.asString();
            }

            var value_date_exp = streamDataMessage["date_exp"];
            if (value_date_exp != null)
            {
                message.DateExp = value_date_exp.asDateTime();
            }

            var value_id_ord1 = streamDataMessage["id_ord1"];
            if (value_id_ord1 != null)
            {
                message.IdOrd1 = value_id_ord1.asLong();
            }

            var value_local_stamp = streamDataMessage["local_stamp"];
            if (value_local_stamp != null)
            {
                message.LocalStamp = value_local_stamp.asDateTime();
            }

                        return message;
        } // CreateCgmOrdersLog
        
        public static CgmDeal CreateCgmDeal(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmDeal();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_id_deal = streamDataMessage["id_deal"];
            if (value_id_deal != null)
            {
                message.IdDeal = value_id_deal.asLong();
            }

            var value_id_deal_multileg = streamDataMessage["id_deal_multileg"];
            if (value_id_deal_multileg != null)
            {
                message.IdDealMultileg = value_id_deal_multileg.asLong();
            }

            var value_pos = streamDataMessage["pos"];
            if (value_pos != null)
            {
                message.Pos = value_pos.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_id_ord_buy = streamDataMessage["id_ord_buy"];
            if (value_id_ord_buy != null)
            {
                message.IdOrdBuy = value_id_ord_buy.asLong();
            }

            var value_id_ord_sell = streamDataMessage["id_ord_sell"];
            if (value_id_ord_sell != null)
            {
                message.IdOrdSell = value_id_ord_sell.asLong();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_nosystem = streamDataMessage["nosystem"];
            if (value_nosystem != null)
            {
                message.Nosystem = value_nosystem.asSByte();
            }

            var value_xstatus_buy = streamDataMessage["xstatus_buy"];
            if (value_xstatus_buy != null)
            {
                message.XstatusBuy = value_xstatus_buy.asLong();
            }

            var value_xstatus_sell = streamDataMessage["xstatus_sell"];
            if (value_xstatus_sell != null)
            {
                message.XstatusSell = value_xstatus_sell.asLong();
            }

            var value_status_buy = streamDataMessage["status_buy"];
            if (value_status_buy != null)
            {
                message.StatusBuy = value_status_buy.asInt();
            }

            var value_status_sell = streamDataMessage["status_sell"];
            if (value_status_sell != null)
            {
                message.StatusSell = value_status_sell.asInt();
            }

            var value_ext_id_buy = streamDataMessage["ext_id_buy"];
            if (value_ext_id_buy != null)
            {
                message.ExtIdBuy = value_ext_id_buy.asInt();
            }

            var value_ext_id_sell = streamDataMessage["ext_id_sell"];
            if (value_ext_id_sell != null)
            {
                message.ExtIdSell = value_ext_id_sell.asInt();
            }

            var value_code_buy = streamDataMessage["code_buy"];
            if (value_code_buy != null)
            {
                message.CodeBuy = value_code_buy.asString();
            }

            var value_code_sell = streamDataMessage["code_sell"];
            if (value_code_sell != null)
            {
                message.CodeSell = value_code_sell.asString();
            }

            var value_comment_buy = streamDataMessage["comment_buy"];
            if (value_comment_buy != null)
            {
                message.CommentBuy = value_comment_buy.asString();
            }

            var value_comment_sell = streamDataMessage["comment_sell"];
            if (value_comment_sell != null)
            {
                message.CommentSell = value_comment_sell.asString();
            }

            var value_trust_buy = streamDataMessage["trust_buy"];
            if (value_trust_buy != null)
            {
                message.TrustBuy = value_trust_buy.asSByte();
            }

            var value_trust_sell = streamDataMessage["trust_sell"];
            if (value_trust_sell != null)
            {
                message.TrustSell = value_trust_sell.asSByte();
            }

            var value_hedge_buy = streamDataMessage["hedge_buy"];
            if (value_hedge_buy != null)
            {
                message.HedgeBuy = value_hedge_buy.asSByte();
            }

            var value_hedge_sell = streamDataMessage["hedge_sell"];
            if (value_hedge_sell != null)
            {
                message.HedgeSell = value_hedge_sell.asSByte();
            }

            var value_fee_buy = streamDataMessage["fee_buy"];
            if (value_fee_buy != null)
            {
                message.FeeBuy = value_fee_buy.asDouble();
            }

            var value_fee_sell = streamDataMessage["fee_sell"];
            if (value_fee_sell != null)
            {
                message.FeeSell = value_fee_sell.asDouble();
            }

            var value_login_buy = streamDataMessage["login_buy"];
            if (value_login_buy != null)
            {
                message.LoginBuy = value_login_buy.asString();
            }

            var value_login_sell = streamDataMessage["login_sell"];
            if (value_login_sell != null)
            {
                message.LoginSell = value_login_sell.asString();
            }

            var value_code_rts_buy = streamDataMessage["code_rts_buy"];
            if (value_code_rts_buy != null)
            {
                message.CodeRtsBuy = value_code_rts_buy.asString();
            }

            var value_code_rts_sell = streamDataMessage["code_rts_sell"];
            if (value_code_rts_sell != null)
            {
                message.CodeRtsSell = value_code_rts_sell.asString();
            }

                        return message;
        } // CreateCgmDeal
        
        public static CgmHeartbeat CreateCgmHeartbeat(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmHeartbeat();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_server_time = streamDataMessage["server_time"];
            if (value_server_time != null)
            {
                message.ServerTime = value_server_time.asDateTime();
            }

                        return message;
        } // CreateCgmHeartbeat
        
        public static CgmSysEvents CreateCgmSysEvents(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmSysEvents();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_event_id = streamDataMessage["event_id"];
            if (value_event_id != null)
            {
                message.EventId = value_event_id.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_event_type = streamDataMessage["event_type"];
            if (value_event_type != null)
            {
                message.EventType = value_event_type.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmSysEvents
        
        public static CgmUserDeal CreateCgmUserDeal(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmUserDeal();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_id_deal = streamDataMessage["id_deal"];
            if (value_id_deal != null)
            {
                message.IdDeal = value_id_deal.asLong();
            }

            var value_id_deal_multileg = streamDataMessage["id_deal_multileg"];
            if (value_id_deal_multileg != null)
            {
                message.IdDealMultileg = value_id_deal_multileg.asLong();
            }

            var value_pos = streamDataMessage["pos"];
            if (value_pos != null)
            {
                message.Pos = value_pos.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_id_ord_buy = streamDataMessage["id_ord_buy"];
            if (value_id_ord_buy != null)
            {
                message.IdOrdBuy = value_id_ord_buy.asLong();
            }

            var value_id_ord_sell = streamDataMessage["id_ord_sell"];
            if (value_id_ord_sell != null)
            {
                message.IdOrdSell = value_id_ord_sell.asLong();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_nosystem = streamDataMessage["nosystem"];
            if (value_nosystem != null)
            {
                message.Nosystem = value_nosystem.asSByte();
            }

            var value_xstatus_buy = streamDataMessage["xstatus_buy"];
            if (value_xstatus_buy != null)
            {
                message.XstatusBuy = value_xstatus_buy.asLong();
            }

            var value_xstatus_sell = streamDataMessage["xstatus_sell"];
            if (value_xstatus_sell != null)
            {
                message.XstatusSell = value_xstatus_sell.asLong();
            }

            var value_status_buy = streamDataMessage["status_buy"];
            if (value_status_buy != null)
            {
                message.StatusBuy = value_status_buy.asInt();
            }

            var value_status_sell = streamDataMessage["status_sell"];
            if (value_status_sell != null)
            {
                message.StatusSell = value_status_sell.asInt();
            }

            var value_ext_id_buy = streamDataMessage["ext_id_buy"];
            if (value_ext_id_buy != null)
            {
                message.ExtIdBuy = value_ext_id_buy.asInt();
            }

            var value_ext_id_sell = streamDataMessage["ext_id_sell"];
            if (value_ext_id_sell != null)
            {
                message.ExtIdSell = value_ext_id_sell.asInt();
            }

            var value_code_buy = streamDataMessage["code_buy"];
            if (value_code_buy != null)
            {
                message.CodeBuy = value_code_buy.asString();
            }

            var value_code_sell = streamDataMessage["code_sell"];
            if (value_code_sell != null)
            {
                message.CodeSell = value_code_sell.asString();
            }

            var value_comment_buy = streamDataMessage["comment_buy"];
            if (value_comment_buy != null)
            {
                message.CommentBuy = value_comment_buy.asString();
            }

            var value_comment_sell = streamDataMessage["comment_sell"];
            if (value_comment_sell != null)
            {
                message.CommentSell = value_comment_sell.asString();
            }

            var value_trust_buy = streamDataMessage["trust_buy"];
            if (value_trust_buy != null)
            {
                message.TrustBuy = value_trust_buy.asSByte();
            }

            var value_trust_sell = streamDataMessage["trust_sell"];
            if (value_trust_sell != null)
            {
                message.TrustSell = value_trust_sell.asSByte();
            }

            var value_hedge_buy = streamDataMessage["hedge_buy"];
            if (value_hedge_buy != null)
            {
                message.HedgeBuy = value_hedge_buy.asSByte();
            }

            var value_hedge_sell = streamDataMessage["hedge_sell"];
            if (value_hedge_sell != null)
            {
                message.HedgeSell = value_hedge_sell.asSByte();
            }

            var value_fee_buy = streamDataMessage["fee_buy"];
            if (value_fee_buy != null)
            {
                message.FeeBuy = value_fee_buy.asDouble();
            }

            var value_fee_sell = streamDataMessage["fee_sell"];
            if (value_fee_sell != null)
            {
                message.FeeSell = value_fee_sell.asDouble();
            }

            var value_login_buy = streamDataMessage["login_buy"];
            if (value_login_buy != null)
            {
                message.LoginBuy = value_login_buy.asString();
            }

            var value_login_sell = streamDataMessage["login_sell"];
            if (value_login_sell != null)
            {
                message.LoginSell = value_login_sell.asString();
            }

            var value_code_rts_buy = streamDataMessage["code_rts_buy"];
            if (value_code_rts_buy != null)
            {
                message.CodeRtsBuy = value_code_rts_buy.asString();
            }

            var value_code_rts_sell = streamDataMessage["code_rts_sell"];
            if (value_code_rts_sell != null)
            {
                message.CodeRtsSell = value_code_rts_sell.asString();
            }

                        return message;
        } // CreateCgmUserDeal
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.Ordbook
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "orders":
                    return CGateMessageFactory.CreateCgmOrders(source);
                case "info":
                    return CGateMessageFactory.CreateCgmInfo(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmOrders CreateCgmOrders(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOrders();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_id_ord = streamDataMessage["id_ord"];
            if (value_id_ord != null)
            {
                message.IdOrd = value_id_ord.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_xstatus = streamDataMessage["xstatus"];
            if (value_xstatus != null)
            {
                message.Xstatus = value_xstatus.asLong();
            }

            var value_status = streamDataMessage["status"];
            if (value_status != null)
            {
                message.Status = value_status.asInt();
            }

            var value_action = streamDataMessage["action"];
            if (value_action != null)
            {
                message.Action = value_action.asSByte();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asSByte();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_amount_rest = streamDataMessage["amount_rest"];
            if (value_amount_rest != null)
            {
                message.AmountRest = value_amount_rest.asInt();
            }

            var value_init_moment = streamDataMessage["init_moment"];
            if (value_init_moment != null)
            {
                message.InitMoment = value_init_moment.asDateTime();
            }

            var value_init_amount = streamDataMessage["init_amount"];
            if (value_init_amount != null)
            {
                message.InitAmount = value_init_amount.asInt();
            }

                        return message;
        } // CreateCgmOrders
        
        public static CgmInfo CreateCgmInfo(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmInfo();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_infoID = streamDataMessage["infoID"];
            if (value_infoID != null)
            {
                message.InfoId = value_infoID.asLong();
            }

            var value_logRev = streamDataMessage["logRev"];
            if (value_logRev != null)
            {
                message.LogRev = value_logRev.asLong();
            }

            var value_lifeNum = streamDataMessage["lifeNum"];
            if (value_lifeNum != null)
            {
                message.LifeNum = value_lifeNum.asInt();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

                        return message;
        } // CreateCgmInfo
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.Orderbook
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "orders":
                    return CGateMessageFactory.CreateCgmOrders(source);
                case "info":
                    return CGateMessageFactory.CreateCgmInfo(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmOrders CreateCgmOrders(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOrders();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_id_ord = streamDataMessage["id_ord"];
            if (value_id_ord != null)
            {
                message.IdOrd = value_id_ord.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_xstatus = streamDataMessage["xstatus"];
            if (value_xstatus != null)
            {
                message.Xstatus = value_xstatus.asLong();
            }

            var value_status = streamDataMessage["status"];
            if (value_status != null)
            {
                message.Status = value_status.asInt();
            }

            var value_action = streamDataMessage["action"];
            if (value_action != null)
            {
                message.Action = value_action.asSByte();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asSByte();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_amount_rest = streamDataMessage["amount_rest"];
            if (value_amount_rest != null)
            {
                message.AmountRest = value_amount_rest.asInt();
            }

            var value_comment = streamDataMessage["comment"];
            if (value_comment != null)
            {
                message.Comment = value_comment.asString();
            }

            var value_hedge = streamDataMessage["hedge"];
            if (value_hedge != null)
            {
                message.Hedge = value_hedge.asSByte();
            }

            var value_trust = streamDataMessage["trust"];
            if (value_trust != null)
            {
                message.Trust = value_trust.asSByte();
            }

            var value_ext_id = streamDataMessage["ext_id"];
            if (value_ext_id != null)
            {
                message.ExtId = value_ext_id.asInt();
            }

            var value_login_from = streamDataMessage["login_from"];
            if (value_login_from != null)
            {
                message.LoginFrom = value_login_from.asString();
            }

            var value_broker_to = streamDataMessage["broker_to"];
            if (value_broker_to != null)
            {
                message.BrokerTo = value_broker_to.asString();
            }

            var value_broker_to_rts = streamDataMessage["broker_to_rts"];
            if (value_broker_to_rts != null)
            {
                message.BrokerToRts = value_broker_to_rts.asString();
            }

            var value_date_exp = streamDataMessage["date_exp"];
            if (value_date_exp != null)
            {
                message.DateExp = value_date_exp.asDateTime();
            }

            var value_id_ord1 = streamDataMessage["id_ord1"];
            if (value_id_ord1 != null)
            {
                message.IdOrd1 = value_id_ord1.asLong();
            }

            var value_broker_from_rts = streamDataMessage["broker_from_rts"];
            if (value_broker_from_rts != null)
            {
                message.BrokerFromRts = value_broker_from_rts.asString();
            }

            var value_init_moment = streamDataMessage["init_moment"];
            if (value_init_moment != null)
            {
                message.InitMoment = value_init_moment.asDateTime();
            }

            var value_init_amount = streamDataMessage["init_amount"];
            if (value_init_amount != null)
            {
                message.InitAmount = value_init_amount.asInt();
            }

                        return message;
        } // CreateCgmOrders
        
        public static CgmInfo CreateCgmInfo(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmInfo();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_infoID = streamDataMessage["infoID"];
            if (value_infoID != null)
            {
                message.InfoId = value_infoID.asLong();
            }

            var value_logRev = streamDataMessage["logRev"];
            if (value_logRev != null)
            {
                message.LogRev = value_logRev.asLong();
            }

            var value_lifeNum = streamDataMessage["lifeNum"];
            if (value_lifeNum != null)
            {
                message.LifeNum = value_lifeNum.asInt();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

                        return message;
        } // CreateCgmInfo
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.OrdersAggr
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "orders_aggr":
                    return CGateMessageFactory.CreateCgmOrdersAggr(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmOrdersAggr CreateCgmOrdersAggr(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOrdersAggr();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_volume = streamDataMessage["volume"];
            if (value_volume != null)
            {
                message.Volume = value_volume.asLong();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asSByte();
            }

                        return message;
        } // CreateCgmOrdersAggr
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.OrdLogTrades
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "orders_log":
                    return CGateMessageFactory.CreateCgmOrdersLog(source);
                case "multileg_orders_log":
                    return CGateMessageFactory.CreateCgmMultilegOrdersLog(source);
                case "heartbeat":
                    return CGateMessageFactory.CreateCgmHeartbeat(source);
                case "sys_events":
                    return CGateMessageFactory.CreateCgmSysEvents(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmOrdersLog CreateCgmOrdersLog(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOrdersLog();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_id_ord = streamDataMessage["id_ord"];
            if (value_id_ord != null)
            {
                message.IdOrd = value_id_ord.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_amount_rest = streamDataMessage["amount_rest"];
            if (value_amount_rest != null)
            {
                message.AmountRest = value_amount_rest.asInt();
            }

            var value_id_deal = streamDataMessage["id_deal"];
            if (value_id_deal != null)
            {
                message.IdDeal = value_id_deal.asLong();
            }

            var value_xstatus = streamDataMessage["xstatus"];
            if (value_xstatus != null)
            {
                message.Xstatus = value_xstatus.asLong();
            }

            var value_status = streamDataMessage["status"];
            if (value_status != null)
            {
                message.Status = value_status.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asSByte();
            }

            var value_action = streamDataMessage["action"];
            if (value_action != null)
            {
                message.Action = value_action.asSByte();
            }

            var value_deal_price = streamDataMessage["deal_price"];
            if (value_deal_price != null)
            {
                message.DealPrice = value_deal_price.asDouble();
            }

                        return message;
        } // CreateCgmOrdersLog
        
        public static CgmMultilegOrdersLog CreateCgmMultilegOrdersLog(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmMultilegOrdersLog();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_id_ord = streamDataMessage["id_ord"];
            if (value_id_ord != null)
            {
                message.IdOrd = value_id_ord.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_amount = streamDataMessage["amount"];
            if (value_amount != null)
            {
                message.Amount = value_amount.asInt();
            }

            var value_amount_rest = streamDataMessage["amount_rest"];
            if (value_amount_rest != null)
            {
                message.AmountRest = value_amount_rest.asInt();
            }

            var value_id_deal = streamDataMessage["id_deal"];
            if (value_id_deal != null)
            {
                message.IdDeal = value_id_deal.asLong();
            }

            var value_xstatus = streamDataMessage["xstatus"];
            if (value_xstatus != null)
            {
                message.Xstatus = value_xstatus.asLong();
            }

            var value_status = streamDataMessage["status"];
            if (value_status != null)
            {
                message.Status = value_status.asInt();
            }

            var value_price = streamDataMessage["price"];
            if (value_price != null)
            {
                message.Price = value_price.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_dir = streamDataMessage["dir"];
            if (value_dir != null)
            {
                message.Dir = value_dir.asSByte();
            }

            var value_action = streamDataMessage["action"];
            if (value_action != null)
            {
                message.Action = value_action.asSByte();
            }

            var value_deal_price = streamDataMessage["deal_price"];
            if (value_deal_price != null)
            {
                message.DealPrice = value_deal_price.asDouble();
            }

            var value_rate_price = streamDataMessage["rate_price"];
            if (value_rate_price != null)
            {
                message.RatePrice = value_rate_price.asDouble();
            }

            var value_swap_price = streamDataMessage["swap_price"];
            if (value_swap_price != null)
            {
                message.SwapPrice = value_swap_price.asDouble();
            }

                        return message;
        } // CreateCgmMultilegOrdersLog
        
        public static CgmHeartbeat CreateCgmHeartbeat(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmHeartbeat();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_server_time = streamDataMessage["server_time"];
            if (value_server_time != null)
            {
                message.ServerTime = value_server_time.asDateTime();
            }

                        return message;
        } // CreateCgmHeartbeat
        
        public static CgmSysEvents CreateCgmSysEvents(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmSysEvents();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_event_id = streamDataMessage["event_id"];
            if (value_event_id != null)
            {
                message.EventId = value_event_id.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_event_type = streamDataMessage["event_type"];
            if (value_event_type != null)
            {
                message.EventType = value_event_type.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmSysEvents
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.Part
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "part":
                    return CGateMessageFactory.CreateCgmPart(source);
                case "part_sa":
                    return CGateMessageFactory.CreateCgmPartSa(source);
                case "sys_events":
                    return CGateMessageFactory.CreateCgmSysEvents(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmPart CreateCgmPart(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmPart();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_money_free = streamDataMessage["money_free"];
            if (value_money_free != null)
            {
                message.MoneyFree = value_money_free.asDouble();
            }

            var value_money_blocked = streamDataMessage["money_blocked"];
            if (value_money_blocked != null)
            {
                message.MoneyBlocked = value_money_blocked.asDouble();
            }

            var value_pledge_free = streamDataMessage["pledge_free"];
            if (value_pledge_free != null)
            {
                message.PledgeFree = value_pledge_free.asDouble();
            }

            var value_pledge_blocked = streamDataMessage["pledge_blocked"];
            if (value_pledge_blocked != null)
            {
                message.PledgeBlocked = value_pledge_blocked.asDouble();
            }

            var value_vm_reserve = streamDataMessage["vm_reserve"];
            if (value_vm_reserve != null)
            {
                message.VmReserve = value_vm_reserve.asDouble();
            }

            var value_fee = streamDataMessage["fee"];
            if (value_fee != null)
            {
                message.Fee = value_fee.asDouble();
            }

            var value_balance_money = streamDataMessage["balance_money"];
            if (value_balance_money != null)
            {
                message.BalanceMoney = value_balance_money.asDouble();
            }

            var value_coeff_go = streamDataMessage["coeff_go"];
            if (value_coeff_go != null)
            {
                message.CoeffGo = value_coeff_go.asDouble();
            }

            var value_coeff_liquidity = streamDataMessage["coeff_liquidity"];
            if (value_coeff_liquidity != null)
            {
                message.CoeffLiquidity = value_coeff_liquidity.asDouble();
            }

            var value_limits_set = streamDataMessage["limits_set"];
            if (value_limits_set != null)
            {
                message.LimitsSet = value_limits_set.asSByte();
            }

            var value_money_old = streamDataMessage["money_old"];
            if (value_money_old != null)
            {
                message.MoneyOld = value_money_old.asDouble();
            }

            var value_money_amount = streamDataMessage["money_amount"];
            if (value_money_amount != null)
            {
                message.MoneyAmount = value_money_amount.asDouble();
            }

            var value_pledge_old = streamDataMessage["pledge_old"];
            if (value_pledge_old != null)
            {
                message.PledgeOld = value_pledge_old.asDouble();
            }

            var value_pledge_amount = streamDataMessage["pledge_amount"];
            if (value_pledge_amount != null)
            {
                message.PledgeAmount = value_pledge_amount.asDouble();
            }

            var value_money_pledge_amount = streamDataMessage["money_pledge_amount"];
            if (value_money_pledge_amount != null)
            {
                message.MoneyPledgeAmount = value_money_pledge_amount.asDouble();
            }

            var value_vm_intercl = streamDataMessage["vm_intercl"];
            if (value_vm_intercl != null)
            {
                message.VmIntercl = value_vm_intercl.asDouble();
            }

            var value_is_auto_update_limit = streamDataMessage["is_auto_update_limit"];
            if (value_is_auto_update_limit != null)
            {
                message.IsAutoUpdateLimit = value_is_auto_update_limit.asSByte();
            }

            var value_no_fut_discount = streamDataMessage["no_fut_discount"];
            if (value_no_fut_discount != null)
            {
                message.NoFutDiscount = value_no_fut_discount.asSByte();
            }

            var value_num_clr_2delivery = streamDataMessage["num_clr_2delivery"];
            if (value_num_clr_2delivery != null)
            {
                message.NumClr2delivery = value_num_clr_2delivery.asInt();
            }

                        return message;
        } // CreateCgmPart
        
        public static CgmPartSa CreateCgmPartSa(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmPartSa();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_settlement_account = streamDataMessage["settlement_account"];
            if (value_settlement_account != null)
            {
                message.SettlementAccount = value_settlement_account.asString();
            }

            var value_money_amount = streamDataMessage["money_amount"];
            if (value_money_amount != null)
            {
                message.MoneyAmount = value_money_amount.asDouble();
            }

            var value_money_free = streamDataMessage["money_free"];
            if (value_money_free != null)
            {
                message.MoneyFree = value_money_free.asDouble();
            }

            var value_pledge_amount = streamDataMessage["pledge_amount"];
            if (value_pledge_amount != null)
            {
                message.PledgeAmount = value_pledge_amount.asDouble();
            }

            var value_money_pledge_amount = streamDataMessage["money_pledge_amount"];
            if (value_money_pledge_amount != null)
            {
                message.MoneyPledgeAmount = value_money_pledge_amount.asDouble();
            }

            var value_liquidity_ratio = streamDataMessage["liquidity_ratio"];
            if (value_liquidity_ratio != null)
            {
                message.LiquidityRatio = value_liquidity_ratio.asDouble();
            }

                        return message;
        } // CreateCgmPartSa
        
        public static CgmSysEvents CreateCgmSysEvents(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmSysEvents();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_event_id = streamDataMessage["event_id"];
            if (value_event_id != null)
            {
                message.EventId = value_event_id.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_event_type = streamDataMessage["event_type"];
            if (value_event_type != null)
            {
                message.EventType = value_event_type.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmSysEvents
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.Pos
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "position":
                    return CGateMessageFactory.CreateCgmPosition(source);
                case "sys_events":
                    return CGateMessageFactory.CreateCgmSysEvents(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmPosition CreateCgmPosition(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmPosition();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_pos = streamDataMessage["pos"];
            if (value_pos != null)
            {
                message.Pos = value_pos.asInt();
            }

            var value_buys_qty = streamDataMessage["buys_qty"];
            if (value_buys_qty != null)
            {
                message.BuysQty = value_buys_qty.asInt();
            }

            var value_sells_qty = streamDataMessage["sells_qty"];
            if (value_sells_qty != null)
            {
                message.SellsQty = value_sells_qty.asInt();
            }

            var value_open_qty = streamDataMessage["open_qty"];
            if (value_open_qty != null)
            {
                message.OpenQty = value_open_qty.asInt();
            }

            var value_waprice = streamDataMessage["waprice"];
            if (value_waprice != null)
            {
                message.Waprice = value_waprice.asDouble();
            }

            var value_net_volume_rur = streamDataMessage["net_volume_rur"];
            if (value_net_volume_rur != null)
            {
                message.NetVolumeRur = value_net_volume_rur.asDouble();
            }

            var value_last_deal_id = streamDataMessage["last_deal_id"];
            if (value_last_deal_id != null)
            {
                message.LastDealId = value_last_deal_id.asLong();
            }

                        return message;
        } // CreateCgmPosition
        
        public static CgmSysEvents CreateCgmSysEvents(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmSysEvents();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_event_id = streamDataMessage["event_id"];
            if (value_event_id != null)
            {
                message.EventId = value_event_id.asLong();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_event_type = streamDataMessage["event_type"];
            if (value_event_type != null)
            {
                message.EventType = value_event_type.asInt();
            }

            var value_message = streamDataMessage["message"];
            if (value_message != null)
            {
                message.Message = value_message.asString();
            }

                        return message;
        } // CreateCgmSysEvents
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.Rates
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "curr_online":
                    return CGateMessageFactory.CreateCgmCurrOnline(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmCurrOnline CreateCgmCurrOnline(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmCurrOnline();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_rate_id = streamDataMessage["rate_id"];
            if (value_rate_id != null)
            {
                message.RateId = value_rate_id.asInt();
            }

            var value_value = streamDataMessage["value"];
            if (value_value != null)
            {
                message.Value = value_value.asDouble();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

                        return message;
        } // CreateCgmCurrOnline
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.RtsIndex
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "rts_index":
                    return CGateMessageFactory.CreateCgmRtsIndex(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmRtsIndex CreateCgmRtsIndex(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmRtsIndex();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_name = streamDataMessage["name"];
            if (value_name != null)
            {
                message.Name = value_name.asString();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_value = streamDataMessage["value"];
            if (value_value != null)
            {
                message.Value = value_value.asDouble();
            }

            var value_prev_close_value = streamDataMessage["prev_close_value"];
            if (value_prev_close_value != null)
            {
                message.PrevCloseValue = value_prev_close_value.asDouble();
            }

            var value_open_value = streamDataMessage["open_value"];
            if (value_open_value != null)
            {
                message.OpenValue = value_open_value.asDouble();
            }

            var value_max_value = streamDataMessage["max_value"];
            if (value_max_value != null)
            {
                message.MaxValue = value_max_value.asDouble();
            }

            var value_min_value = streamDataMessage["min_value"];
            if (value_min_value != null)
            {
                message.MinValue = value_min_value.asDouble();
            }

            var value_usd_rate = streamDataMessage["usd_rate"];
            if (value_usd_rate != null)
            {
                message.UsdRate = value_usd_rate.asDouble();
            }

            var value_cap = streamDataMessage["cap"];
            if (value_cap != null)
            {
                message.Cap = value_cap.asDouble();
            }

            var value_volume = streamDataMessage["volume"];
            if (value_volume != null)
            {
                message.Volume = value_volume.asDouble();
            }

                        return message;
        } // CreateCgmRtsIndex
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.RtsIndexlog
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "rts_index_log":
                    return CGateMessageFactory.CreateCgmRtsIndexLog(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmRtsIndexLog CreateCgmRtsIndexLog(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmRtsIndexLog();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_name = streamDataMessage["name"];
            if (value_name != null)
            {
                message.Name = value_name.asString();
            }

            var value_moment = streamDataMessage["moment"];
            if (value_moment != null)
            {
                message.Moment = value_moment.asDateTime();
            }

            var value_value = streamDataMessage["value"];
            if (value_value != null)
            {
                message.Value = value_value.asDouble();
            }

            var value_prev_close_value = streamDataMessage["prev_close_value"];
            if (value_prev_close_value != null)
            {
                message.PrevCloseValue = value_prev_close_value.asDouble();
            }

            var value_open_value = streamDataMessage["open_value"];
            if (value_open_value != null)
            {
                message.OpenValue = value_open_value.asDouble();
            }

            var value_max_value = streamDataMessage["max_value"];
            if (value_max_value != null)
            {
                message.MaxValue = value_max_value.asDouble();
            }

            var value_min_value = streamDataMessage["min_value"];
            if (value_min_value != null)
            {
                message.MinValue = value_min_value.asDouble();
            }

            var value_usd_rate = streamDataMessage["usd_rate"];
            if (value_usd_rate != null)
            {
                message.UsdRate = value_usd_rate.asDouble();
            }

            var value_cap = streamDataMessage["cap"];
            if (value_cap != null)
            {
                message.Cap = value_cap.asDouble();
            }

            var value_volume = streamDataMessage["volume"];
            if (value_volume != null)
            {
                message.Volume = value_volume.asDouble();
            }

                        return message;
        } // CreateCgmRtsIndexLog
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.Tnpenalty
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "fee_all":
                    return CGateMessageFactory.CreateCgmFeeAll(source);
                case "fee_tn":
                    return CGateMessageFactory.CreateCgmFeeTn(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmFeeAll CreateCgmFeeAll(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFeeAll();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_time = streamDataMessage["time"];
            if (value_time != null)
            {
                message.Time = value_time.asLong();
            }

            var value_p2login = streamDataMessage["p2login"];
            if (value_p2login != null)
            {
                message.P2login = value_p2login.asString();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_points = streamDataMessage["points"];
            if (value_points != null)
            {
                message.Points = value_points.asInt();
            }

            var value_fee = streamDataMessage["fee"];
            if (value_fee != null)
            {
                message.Fee = value_fee.asDouble();
            }

                        return message;
        } // CreateCgmFeeAll
        
        public static CgmFeeTn CreateCgmFeeTn(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFeeTn();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_time = streamDataMessage["time"];
            if (value_time != null)
            {
                message.Time = value_time.asLong();
            }

            var value_p2login = streamDataMessage["p2login"];
            if (value_p2login != null)
            {
                message.P2login = value_p2login.asString();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_tn_type = streamDataMessage["tn_type"];
            if (value_tn_type != null)
            {
                message.TnType = value_tn_type.asInt();
            }

            var value_err_code = streamDataMessage["err_code"];
            if (value_err_code != null)
            {
                message.ErrCode = value_err_code.asInt();
            }

            var value_count = streamDataMessage["count"];
            if (value_count != null)
            {
                message.Count = value_count.asInt();
            }

                        return message;
        } // CreateCgmFeeTn
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.Vm
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "fut_vm":
                    return CGateMessageFactory.CreateCgmFutVm(source);
                case "opt_vm":
                    return CGateMessageFactory.CreateCgmOptVm(source);
                case "fut_vm_sa":
                    return CGateMessageFactory.CreateCgmFutVmSa(source);
                case "opt_vm_sa":
                    return CGateMessageFactory.CreateCgmOptVmSa(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmFutVm CreateCgmFutVm(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutVm();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_vm = streamDataMessage["vm"];
            if (value_vm != null)
            {
                message.Vm = value_vm.asDouble();
            }

            var value_vm_real = streamDataMessage["vm_real"];
            if (value_vm_real != null)
            {
                message.VmReal = value_vm_real.asDouble();
            }

                        return message;
        } // CreateCgmFutVm
        
        public static CgmOptVm CreateCgmOptVm(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptVm();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_client_code = streamDataMessage["client_code"];
            if (value_client_code != null)
            {
                message.ClientCode = value_client_code.asString();
            }

            var value_vm = streamDataMessage["vm"];
            if (value_vm != null)
            {
                message.Vm = value_vm.asDouble();
            }

            var value_vm_real = streamDataMessage["vm_real"];
            if (value_vm_real != null)
            {
                message.VmReal = value_vm_real.asDouble();
            }

                        return message;
        } // CreateCgmOptVm
        
        public static CgmFutVmSa CreateCgmFutVmSa(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmFutVmSa();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_settlement_account = streamDataMessage["settlement_account"];
            if (value_settlement_account != null)
            {
                message.SettlementAccount = value_settlement_account.asString();
            }

            var value_vm = streamDataMessage["vm"];
            if (value_vm != null)
            {
                message.Vm = value_vm.asDouble();
            }

            var value_vm_real = streamDataMessage["vm_real"];
            if (value_vm_real != null)
            {
                message.VmReal = value_vm_real.asDouble();
            }

                        return message;
        } // CreateCgmFutVmSa
        
        public static CgmOptVmSa CreateCgmOptVmSa(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmOptVmSa();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_settlement_account = streamDataMessage["settlement_account"];
            if (value_settlement_account != null)
            {
                message.SettlementAccount = value_settlement_account.asString();
            }

            var value_vm = streamDataMessage["vm"];
            if (value_vm != null)
            {
                message.Vm = value_vm.asDouble();
            }

            var value_vm_real = streamDataMessage["vm_real"];
            if (value_vm_real != null)
            {
                message.VmReal = value_vm_real.asDouble();
            }

                        return message;
        } // CreateCgmOptVmSa
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages.Volat
{
    /// <summary>
    /// Конвертер потокового cgate сообщения StreamDataMessage в типизированное сообщение адаптера
    /// </summary>
    internal sealed class StreamDataMessageConverter : IMessageConverter
    {
        public CGateMessage ConvertToAdapterMessage(StreamDataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        public CGateMessage ConvertToAdapterMessage(DataMessage source)
        {
            return ConvertToAdapterMessage(source.MsgName, source);
        }
        
        private CGateMessage ConvertToAdapterMessage(string msgName, AbstractDataMessage source)
        {
            switch(msgName)
            {
                case "volat":
                    return CGateMessageFactory.CreateCgmVolat(source);
            
            }
            return null;
        } // ConvertToAdapterMessage
    } // StreamDataMessageConverter
    
    internal static class CGateMessageFactory
    {public static CgmVolat CreateCgmVolat(AbstractDataMessage streamDataMessage)
        {
            var message = new CgmVolat();

			        var dataMessage = streamDataMessage as StreamDataMessage;
        if (dataMessage != null)
			{
			    message.MsgIndex = dataMessage.MsgId;
			}
            
            var value_replID = streamDataMessage["replID"];
            if (value_replID != null)
            {
                message.ReplId = value_replID.asLong();
            }

            var value_replRev = streamDataMessage["replRev"];
            if (value_replRev != null)
            {
                message.ReplRev = value_replRev.asLong();
            }

            var value_replAct = streamDataMessage["replAct"];
            if (value_replAct != null)
            {
                message.ReplAct = value_replAct.asLong();
            }

            var value_isin_id = streamDataMessage["isin_id"];
            if (value_isin_id != null)
            {
                message.IsinId = value_isin_id.asInt();
            }

            var value_sess_id = streamDataMessage["sess_id"];
            if (value_sess_id != null)
            {
                message.SessId = value_sess_id.asInt();
            }

            var value_volat = streamDataMessage["volat"];
            if (value_volat != null)
            {
                message.Volat = value_volat.asDouble();
            }

            var value_theor_price = streamDataMessage["theor_price"];
            if (value_theor_price != null)
            {
                message.TheorPrice = value_theor_price.asDouble();
            }

            var value_theor_price_limit = streamDataMessage["theor_price_limit"];
            if (value_theor_price_limit != null)
            {
                message.TheorPriceLimit = value_theor_price_limit.asDouble();
            }

            var value_up_prem = streamDataMessage["up_prem"];
            if (value_up_prem != null)
            {
                message.UpPrem = value_up_prem.asDouble();
            }

            var value_down_prem = streamDataMessage["down_prem"];
            if (value_down_prem != null)
            {
                message.DownPrem = value_down_prem.asDouble();
            }

                        return message;
        } // CreateCgmVolat
        
         
    } // CGateMessageFactory    
}

namespace CGateAdapter.Messages
{
    internal static partial class CGateMessageConverter
    {
        private sealed partial class CGateMessageConverterVisitor : ICGateMessageVisitor
        {
            private readonly DataMessage dataMessage;

            public CGateMessageConverterVisitor(DataMessage dataMessage)
            {
                this.dataMessage = dataMessage;
            }

        public void Handle(StreamStateChange message)
        {
            message.CopyToDataMessage(dataMessage);
        }

        public void Handle(CGateOrder message)
        {
            message.CopyToDataMessage(dataMessage);
        }

        public void Handle(CGateDelOrderReply message)
        {
            message.CopyToDataMessage(dataMessage);
        }

        public void Handle(CGateDeal message)
        {
            message.CopyToDataMessage(dataMessage);
        }

        public void Handle(CGateDataEnd message)
        {
            message.CopyToDataMessage(dataMessage);
        }

        public void Handle(CGateDataBegin message)
        {
            message.CopyToDataMessage(dataMessage);
        }

        public void Handle(CGateAddOrderReply message)
        {
            message.CopyToDataMessage(dataMessage);
        }

        public void Handle(CGConnectionStateChange message)
        {
            message.CopyToDataMessage(dataMessage);
        }

        public void Handle(CGateClearTableMessage message)
        {
            message.CopyToDataMessage(dataMessage);
        }

            public void Handle(Clr.CgmMoneyClearing message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Clr.CgmMoneyClearingSa message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Clr.CgmClrRate message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Clr.CgmFutPos message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Clr.CgmOptPos message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Clr.CgmFutPosSa message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Clr.CgmOptPosSa message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Clr.CgmFutSessSettl message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Clr.CgmOptSessSettl message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Clr.CgmPledgeDetails message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Clr.CgmSysEvents message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFutAddOrder message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg101 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFutAddMultiLegOrder message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg129 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFutDelOrder message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg102 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFutDelUserOrders message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg103 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFutMoveOrder message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg105 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmOptAddOrder message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg109 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmOptDelOrder message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg110 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmOptDelUserOrders message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg111 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmOptMoveOrder message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg113 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFutChangeClientMoney message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg104 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFutChangeBfmoney message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg107 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmOptChangeExpiration message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg112 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFutChangeClientProhibit message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg115 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmOptChangeClientProhibit message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg117 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFutExchangeBfmoney message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg130 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmOptRecalcCs message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg132 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFutTransferClientPosition message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg137 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmOptTransferClientPosition message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg138 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmOptChangeRiskParameters message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg140 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFutTransferRisk message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg139 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmCodheartbeat message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg99 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FortsMessages.CgmFortsMsg100 message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutCommon.CgmCommon message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmDeliveryReport message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmFutRejectedOrders message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmFutInterclInfo message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmFutBondRegistry message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmFutBondIsin message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmFutBondNkd message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmFutBondNominal message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmUsdOnline message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmFutVcb message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmSession message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmMultilegDict message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmFutSessContents message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmFutInstruments message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmDiler message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmInvestr message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmFutSessSettl message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmSysMessages message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmFutSettlementAccount message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmFutMarginType message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmProhibition message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmRates message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutInfo.CgmSysEvents message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutTrades.CgmOrdersLog message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutTrades.CgmMultilegOrdersLog message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutTrades.CgmDeal message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutTrades.CgmMultilegDeal message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutTrades.CgmHeartbeat message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutTrades.CgmSysEvents message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutTrades.CgmUserDeal message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutTrades.CgmUserMultilegDeal message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(FutTradesHeartbeat.CgmHeartbeat message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Info.CgmBaseContractsParams message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Info.CgmFuturesParams message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Info.CgmVirtualFuturesParams message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Info.CgmOptionsParams message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Info.CgmBrokerParams message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Info.CgmClientParams message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Info.CgmSysEvents message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(MiscInfo.CgmVolatCoeff message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Mm.CgmFutMmInfo message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Mm.CgmOptMmInfo message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Mm.CgmCsMmRule message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Mm.CgmMmAgreementFilter message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptCommon.CgmCommon message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptInfo.CgmOptRejectedOrders message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptInfo.CgmOptInterclInfo message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptInfo.CgmOptExpOrders message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptInfo.CgmOptVcb message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptInfo.CgmOptSessContents message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptInfo.CgmOptSessSettl message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptInfo.CgmSysEvents message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptTrades.CgmOrdersLog message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptTrades.CgmDeal message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptTrades.CgmHeartbeat message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptTrades.CgmSysEvents message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OptTrades.CgmUserDeal message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Ordbook.CgmOrders message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Ordbook.CgmInfo message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Orderbook.CgmOrders message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Orderbook.CgmInfo message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OrdersAggr.CgmOrdersAggr message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OrdLogTrades.CgmOrdersLog message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OrdLogTrades.CgmMultilegOrdersLog message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OrdLogTrades.CgmHeartbeat message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(OrdLogTrades.CgmSysEvents message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Part.CgmPart message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Part.CgmPartSa message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Part.CgmSysEvents message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Pos.CgmPosition message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Pos.CgmSysEvents message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Rates.CgmCurrOnline message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(RtsIndex.CgmRtsIndex message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(RtsIndexlog.CgmRtsIndexLog message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Tnpenalty.CgmFeeAll message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Tnpenalty.CgmFeeTn message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Vm.CgmFutVm message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Vm.CgmOptVm message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Vm.CgmFutVmSa message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Vm.CgmOptVmSa message)
            {
                message.CopyToDataMessage(dataMessage);
            }

            public void Handle(Volat.CgmVolat message)
            {
                message.CopyToDataMessage(dataMessage);
            }

        }
        
        public static void CopyToDataMessage(this CGateMessage source, DataMessage dataMessage)
        {
            source.Accept(new CGateMessageConverterVisitor(dataMessage));
        }

        public static void CopyToDataMessage(this StreamStateChange source, DataMessage dataMessage) { }

        public static void CopyToDataMessage(this CGateOrder source, DataMessage dataMessage) { }

        public static void CopyToDataMessage(this CGateDelOrderReply source, DataMessage dataMessage) { }

        public static void CopyToDataMessage(this CGateDeal source, DataMessage dataMessage) { }

        public static void CopyToDataMessage(this CGateDataEnd source, DataMessage dataMessage) { }

        public static void CopyToDataMessage(this CGateDataBegin source, DataMessage dataMessage) { }

        public static void CopyToDataMessage(this CGateAddOrderReply source, DataMessage dataMessage) { }

        public static void CopyToDataMessage(this CGConnectionStateChange source, DataMessage dataMessage) { }

        public static void CopyToDataMessage(this CGateClearTableMessage source, DataMessage dataMessage) { }

        public static void CopyToDataMessage(this CGateAdapter.Messages.Clr.CgmMoneyClearing source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле share
            field =  dataMessage["share"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Share);
            }

            // Поле amount_beg
            field =  dataMessage["amount_beg"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountBeg);
            }

            // Поле vm
            field =  dataMessage["vm"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Vm);
            }

            // Поле premium
            field =  dataMessage["premium"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Premium);
            }

            // Поле pay
            field =  dataMessage["pay"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Pay);
            }

            // Поле fee_fut
            field =  dataMessage["fee_fut"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeFut);
            }

            // Поле fee_opt
            field =  dataMessage["fee_opt"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeOpt);
            }

            // Поле go
            field =  dataMessage["go"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Go);
            }

            // Поле amount_end
            field =  dataMessage["amount_end"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountEnd);
            }

            // Поле free
            field =  dataMessage["free"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Free);
            }

            // Поле ext_reserve
            field =  dataMessage["ext_reserve"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtReserve);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Clr.CgmMoneyClearing)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Clr.CgmMoneyClearingSa source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле settlement_account
            field =  dataMessage["settlement_account"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.SettlementAccount))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.SettlementAccount);
            }

            // Поле share
            field =  dataMessage["share"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Share);
            }

            // Поле amount_beg
            field =  dataMessage["amount_beg"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountBeg);
            }

            // Поле vm
            field =  dataMessage["vm"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Vm);
            }

            // Поле premium
            field =  dataMessage["premium"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Premium);
            }

            // Поле pay
            field =  dataMessage["pay"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Pay);
            }

            // Поле fee_fut
            field =  dataMessage["fee_fut"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeFut);
            }

            // Поле fee_opt
            field =  dataMessage["fee_opt"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeOpt);
            }

            // Поле go
            field =  dataMessage["go"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Go);
            }

            // Поле amount_end
            field =  dataMessage["amount_end"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountEnd);
            }

            // Поле free
            field =  dataMessage["free"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Free);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Clr.CgmMoneyClearingSa)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Clr.CgmClrRate source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле rate
            field =  dataMessage["rate"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Rate);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле signs
            field =  dataMessage["signs"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Signs);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле rate_id
            field =  dataMessage["rate_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.RateId);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Clr.CgmClrRate)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Clr.CgmFutPos source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле account
            field =  dataMessage["account"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Account);
            }

            // Поле pos_beg
            field =  dataMessage["pos_beg"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PosBeg);
            }

            // Поле pos_end
            field =  dataMessage["pos_end"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PosEnd);
            }

            // Поле vm
            field =  dataMessage["vm"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Vm);
            }

            // Поле fee
            field =  dataMessage["fee"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Fee);
            }

            // Поле accum_go
            field =  dataMessage["accum_go"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AccumGo);
            }

            // Поле fee_ex
            field =  dataMessage["fee_ex"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeEx);
            }

            // Поле vat_ex
            field =  dataMessage["vat_ex"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VatEx);
            }

            // Поле fee_cc
            field =  dataMessage["fee_cc"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeCc);
            }

            // Поле vat_cc
            field =  dataMessage["vat_cc"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VatCc);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Clr.CgmFutPos)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Clr.CgmOptPos source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле account
            field =  dataMessage["account"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Account);
            }

            // Поле pos_beg
            field =  dataMessage["pos_beg"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PosBeg);
            }

            // Поле pos_end
            field =  dataMessage["pos_end"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PosEnd);
            }

            // Поле vm
            field =  dataMessage["vm"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Vm);
            }

            // Поле fee
            field =  dataMessage["fee"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Fee);
            }

            // Поле fee_ex
            field =  dataMessage["fee_ex"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeEx);
            }

            // Поле vat_ex
            field =  dataMessage["vat_ex"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VatEx);
            }

            // Поле fee_cc
            field =  dataMessage["fee_cc"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeCc);
            }

            // Поле vat_cc
            field =  dataMessage["vat_cc"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VatCc);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Clr.CgmOptPos)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Clr.CgmFutPosSa source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле settlement_account
            field =  dataMessage["settlement_account"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.SettlementAccount))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.SettlementAccount);
            }

            // Поле pos_beg
            field =  dataMessage["pos_beg"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PosBeg);
            }

            // Поле pos_end
            field =  dataMessage["pos_end"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PosEnd);
            }

            // Поле vm
            field =  dataMessage["vm"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Vm);
            }

            // Поле fee
            field =  dataMessage["fee"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Fee);
            }

            // Поле fee_ex
            field =  dataMessage["fee_ex"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeEx);
            }

            // Поле vat_ex
            field =  dataMessage["vat_ex"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VatEx);
            }

            // Поле fee_cc
            field =  dataMessage["fee_cc"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeCc);
            }

            // Поле vat_cc
            field =  dataMessage["vat_cc"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VatCc);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Clr.CgmFutPosSa)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Clr.CgmOptPosSa source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле settlement_account
            field =  dataMessage["settlement_account"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.SettlementAccount))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.SettlementAccount);
            }

            // Поле pos_beg
            field =  dataMessage["pos_beg"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PosBeg);
            }

            // Поле pos_end
            field =  dataMessage["pos_end"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PosEnd);
            }

            // Поле vm
            field =  dataMessage["vm"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Vm);
            }

            // Поле fee
            field =  dataMessage["fee"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Fee);
            }

            // Поле fee_ex
            field =  dataMessage["fee_ex"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeEx);
            }

            // Поле vat_ex
            field =  dataMessage["vat_ex"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VatEx);
            }

            // Поле fee_cc
            field =  dataMessage["fee_cc"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeCc);
            }

            // Поле vat_cc
            field =  dataMessage["vat_cc"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VatCc);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Clr.CgmOptPosSa)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Clr.CgmFutSessSettl source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле date_clr
            field =  dataMessage["date_clr"];
            shouldSet = field != null;
            if(shouldSet && (source.DateClr == null || source.DateClr == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateClr);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле settl_price
            field =  dataMessage["settl_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SettlPrice);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Clr.CgmFutSessSettl)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Clr.CgmOptSessSettl source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле date_clr
            field =  dataMessage["date_clr"];
            shouldSet = field != null;
            if(shouldSet && (source.DateClr == null || source.DateClr == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateClr);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле volat
            field =  dataMessage["volat"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Volat);
            }

            // Поле theor_price
            field =  dataMessage["theor_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TheorPrice);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Clr.CgmOptSessSettl)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Clr.CgmPledgeDetails source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле pledge_name
            field =  dataMessage["pledge_name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.PledgeName))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.PledgeName);
            }

            // Поле amount_beg
            field =  dataMessage["amount_beg"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountBeg);
            }

            // Поле pay
            field =  dataMessage["pay"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Pay);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле rate
            field =  dataMessage["rate"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Rate);
            }

            // Поле amount_beg_money
            field =  dataMessage["amount_beg_money"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountBegMoney);
            }

            // Поле pay_money
            field =  dataMessage["pay_money"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PayMoney);
            }

            // Поле amount_money
            field =  dataMessage["amount_money"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountMoney);
            }

            // Поле com_ensure
            field =  dataMessage["com_ensure"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ComEnsure);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Clr.CgmPledgeDetails)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Clr.CgmSysEvents source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле event_id
            field =  dataMessage["event_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле event_type
            field =  dataMessage["event_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventType);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Clr.CgmSysEvents)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFutAddOrder source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле type
            field =  dataMessage["type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Type);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Price))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле comment
            field =  dataMessage["comment"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Comment))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Comment);
            }

            // Поле broker_to
            field =  dataMessage["broker_to"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerTo))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerTo);
            }

            // Поле ext_id
            field =  dataMessage["ext_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId);
            }

            // Поле du
            field =  dataMessage["du"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Du);
            }

            // Поле date_exp
            field =  dataMessage["date_exp"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.DateExp))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateExp);
            }

            // Поле hedge
            field =  dataMessage["hedge"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Hedge);
            }

            // Поле dont_check_money
            field =  dataMessage["dont_check_money"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DontCheckMoney);
            }

            // Поле local_stamp
            field =  dataMessage["local_stamp"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalStamp == null || source.LocalStamp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalStamp);
            }

            // Поле match_ref
            field =  dataMessage["match_ref"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.MatchRef))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.MatchRef);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFutAddOrder)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg101 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

            // Поле order_id
            field =  dataMessage["order_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg101)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFutAddMultiLegOrder source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле type
            field =  dataMessage["type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Type);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Price))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле rate_price
            field =  dataMessage["rate_price"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.RatePrice))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.RatePrice);
            }

            // Поле comment
            field =  dataMessage["comment"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Comment))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Comment);
            }

            // Поле hedge
            field =  dataMessage["hedge"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Hedge);
            }

            // Поле broker_to
            field =  dataMessage["broker_to"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerTo))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerTo);
            }

            // Поле ext_id
            field =  dataMessage["ext_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId);
            }

            // Поле trust
            field =  dataMessage["trust"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Trust);
            }

            // Поле date_exp
            field =  dataMessage["date_exp"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.DateExp))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateExp);
            }

            // Поле trade_mode
            field =  dataMessage["trade_mode"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TradeMode);
            }

            // Поле dont_check_money
            field =  dataMessage["dont_check_money"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DontCheckMoney);
            }

            // Поле local_stamp
            field =  dataMessage["local_stamp"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalStamp == null || source.LocalStamp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalStamp);
            }

            // Поле match_ref
            field =  dataMessage["match_ref"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.MatchRef))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.MatchRef);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFutAddMultiLegOrder)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg129 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

            // Поле order_id
            field =  dataMessage["order_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg129)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFutDelOrder source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле order_id
            field =  dataMessage["order_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId);
            }

            // Поле local_stamp
            field =  dataMessage["local_stamp"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalStamp == null || source.LocalStamp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalStamp);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFutDelOrder)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg102 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg102)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFutDelUserOrders source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле buy_sell
            field =  dataMessage["buy_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BuySell);
            }

            // Поле non_system
            field =  dataMessage["non_system"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.NonSystem);
            }

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Code))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле ext_id
            field =  dataMessage["ext_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId);
            }

            // Поле work_mode
            field =  dataMessage["work_mode"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.WorkMode);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле local_stamp
            field =  dataMessage["local_stamp"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalStamp == null || source.LocalStamp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalStamp);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFutDelUserOrders)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg103 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

            // Поле num_orders
            field =  dataMessage["num_orders"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.NumOrders);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg103)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFutMoveOrder source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле regime
            field =  dataMessage["regime"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Regime);
            }

            // Поле order_id1
            field =  dataMessage["order_id1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId1);
            }

            // Поле amount1
            field =  dataMessage["amount1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount1);
            }

            // Поле price1
            field =  dataMessage["price1"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Price1))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Price1);
            }

            // Поле ext_id1
            field =  dataMessage["ext_id1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId1);
            }

            // Поле order_id2
            field =  dataMessage["order_id2"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId2);
            }

            // Поле amount2
            field =  dataMessage["amount2"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount2);
            }

            // Поле price2
            field =  dataMessage["price2"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Price2))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Price2);
            }

            // Поле ext_id2
            field =  dataMessage["ext_id2"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId2);
            }

            // Поле local_stamp
            field =  dataMessage["local_stamp"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalStamp == null || source.LocalStamp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalStamp);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFutMoveOrder)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg105 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

            // Поле order_id1
            field =  dataMessage["order_id1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId1);
            }

            // Поле order_id2
            field =  dataMessage["order_id2"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId2);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg105)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmOptAddOrder source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле type
            field =  dataMessage["type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Type);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Price))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле comment
            field =  dataMessage["comment"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Comment))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Comment);
            }

            // Поле broker_to
            field =  dataMessage["broker_to"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerTo))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerTo);
            }

            // Поле ext_id
            field =  dataMessage["ext_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId);
            }

            // Поле du
            field =  dataMessage["du"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Du);
            }

            // Поле check_limit
            field =  dataMessage["check_limit"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CheckLimit);
            }

            // Поле date_exp
            field =  dataMessage["date_exp"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.DateExp))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateExp);
            }

            // Поле hedge
            field =  dataMessage["hedge"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Hedge);
            }

            // Поле dont_check_money
            field =  dataMessage["dont_check_money"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DontCheckMoney);
            }

            // Поле local_stamp
            field =  dataMessage["local_stamp"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalStamp == null || source.LocalStamp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalStamp);
            }

            // Поле match_ref
            field =  dataMessage["match_ref"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.MatchRef))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.MatchRef);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmOptAddOrder)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg109 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

            // Поле order_id
            field =  dataMessage["order_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg109)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmOptDelOrder source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле order_id
            field =  dataMessage["order_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId);
            }

            // Поле local_stamp
            field =  dataMessage["local_stamp"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalStamp == null || source.LocalStamp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalStamp);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmOptDelOrder)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg110 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg110)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmOptDelUserOrders source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле buy_sell
            field =  dataMessage["buy_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BuySell);
            }

            // Поле non_system
            field =  dataMessage["non_system"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.NonSystem);
            }

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Code))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле ext_id
            field =  dataMessage["ext_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId);
            }

            // Поле work_mode
            field =  dataMessage["work_mode"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.WorkMode);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле local_stamp
            field =  dataMessage["local_stamp"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalStamp == null || source.LocalStamp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalStamp);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmOptDelUserOrders)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg111 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

            // Поле num_orders
            field =  dataMessage["num_orders"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.NumOrders);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg111)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmOptMoveOrder source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле regime
            field =  dataMessage["regime"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Regime);
            }

            // Поле order_id1
            field =  dataMessage["order_id1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId1);
            }

            // Поле amount1
            field =  dataMessage["amount1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount1);
            }

            // Поле price1
            field =  dataMessage["price1"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Price1))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Price1);
            }

            // Поле ext_id1
            field =  dataMessage["ext_id1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId1);
            }

            // Поле check_limit
            field =  dataMessage["check_limit"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CheckLimit);
            }

            // Поле order_id2
            field =  dataMessage["order_id2"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId2);
            }

            // Поле amount2
            field =  dataMessage["amount2"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount2);
            }

            // Поле price2
            field =  dataMessage["price2"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Price2))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Price2);
            }

            // Поле ext_id2
            field =  dataMessage["ext_id2"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId2);
            }

            // Поле local_stamp
            field =  dataMessage["local_stamp"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalStamp == null || source.LocalStamp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalStamp);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmOptMoveOrder)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg113 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

            // Поле order_id1
            field =  dataMessage["order_id1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId1);
            }

            // Поле order_id2
            field =  dataMessage["order_id2"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId2);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg113)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFutChangeClientMoney source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле mode
            field =  dataMessage["mode"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Mode);
            }

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Code))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле limit_money
            field =  dataMessage["limit_money"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LimitMoney))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LimitMoney);
            }

            // Поле limit_pledge
            field =  dataMessage["limit_pledge"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LimitPledge))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LimitPledge);
            }

            // Поле coeff_liquidity
            field =  dataMessage["coeff_liquidity"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CoeffLiquidity))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CoeffLiquidity);
            }

            // Поле coeff_go
            field =  dataMessage["coeff_go"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CoeffGo))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CoeffGo);
            }

            // Поле is_auto_update_limit
            field =  dataMessage["is_auto_update_limit"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsAutoUpdateLimit);
            }

            // Поле no_fut_discount
            field =  dataMessage["no_fut_discount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.NoFutDiscount);
            }

            // Поле check_limit
            field =  dataMessage["check_limit"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CheckLimit);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFutChangeClientMoney)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg104 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg104)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFutChangeBfmoney source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле mode
            field =  dataMessage["mode"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Mode);
            }

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Code))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле limit_money
            field =  dataMessage["limit_money"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LimitMoney))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LimitMoney);
            }

            // Поле limit_pledge
            field =  dataMessage["limit_pledge"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LimitPledge))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LimitPledge);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFutChangeBfmoney)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg107 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg107)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmOptChangeExpiration source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле mode
            field =  dataMessage["mode"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Mode);
            }

            // Поле order_id
            field =  dataMessage["order_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId);
            }

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Code))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmOptChangeExpiration)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg112 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

            // Поле order_id
            field =  dataMessage["order_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg112)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFutChangeClientProhibit source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле mode
            field =  dataMessage["mode"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Mode);
            }

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Code))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле state
            field =  dataMessage["state"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.State);
            }

            // Поле state_mask
            field =  dataMessage["state_mask"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StateMask);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFutChangeClientProhibit)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg115 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg115)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmOptChangeClientProhibit source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле mode
            field =  dataMessage["mode"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Mode);
            }

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Code))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле state
            field =  dataMessage["state"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.State);
            }

            // Поле state_mask
            field =  dataMessage["state_mask"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StateMask);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmOptChangeClientProhibit)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg117 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg117)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFutExchangeBfmoney source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле mode
            field =  dataMessage["mode"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Mode);
            }

            // Поле code_from
            field =  dataMessage["code_from"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeFrom))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeFrom);
            }

            // Поле code_to
            field =  dataMessage["code_to"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeTo))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeTo);
            }

            // Поле amount_money
            field =  dataMessage["amount_money"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.AmountMoney))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.AmountMoney);
            }

            // Поле amount_pledge
            field =  dataMessage["amount_pledge"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.AmountPledge))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.AmountPledge);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFutExchangeBfmoney)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg130 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg130)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmOptRecalcCs source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmOptRecalcCs)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg132 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg132)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFutTransferClientPosition source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле code_from
            field =  dataMessage["code_from"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeFrom))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeFrom);
            }

            // Поле code_to
            field =  dataMessage["code_to"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeTo))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeTo);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFutTransferClientPosition)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg137 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg137)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmOptTransferClientPosition source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле code_from
            field =  dataMessage["code_from"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeFrom))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeFrom);
            }

            // Поле code_to
            field =  dataMessage["code_to"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeTo))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeTo);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmOptTransferClientPosition)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg138 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg138)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmOptChangeRiskParameters source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле num_clr_2delivery
            field =  dataMessage["num_clr_2delivery"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.NumClr2delivery);
            }

            // Поле use_broker_num_clr_2delivery
            field =  dataMessage["use_broker_num_clr_2delivery"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.UseBrokerNumClr2delivery);
            }

            // Поле exp_weight
            field =  dataMessage["exp_weight"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ExpWeight))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ExpWeight);
            }

            // Поле use_broker_exp_weight
            field =  dataMessage["use_broker_exp_weight"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.UseBrokerExpWeight);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmOptChangeRiskParameters)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg140 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg140)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFutTransferRisk source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле code_from
            field =  dataMessage["code_from"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeFrom))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeFrom);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFutTransferRisk)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg139 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

            // Поле deal_id1
            field =  dataMessage["deal_id1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DealId1);
            }

            // Поле deal_id2
            field =  dataMessage["deal_id2"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DealId2);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg139)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmCodheartbeat source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле seq_number
            field =  dataMessage["seq_number"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SeqNumber);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmCodheartbeat)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg99 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле queue_size
            field =  dataMessage["queue_size"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.QueueSize);
            }

            // Поле penalty_remain
            field =  dataMessage["penalty_remain"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PenaltyRemain);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg99)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FortsMessages.CgmFortsMsg100 source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FortsMessages.CgmFortsMsg100)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutCommon.CgmCommon source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле best_sell
            field =  dataMessage["best_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BestSell);
            }

            // Поле amount_sell
            field =  dataMessage["amount_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountSell);
            }

            // Поле best_buy
            field =  dataMessage["best_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BestBuy);
            }

            // Поле amount_buy
            field =  dataMessage["amount_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountBuy);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле trend
            field =  dataMessage["trend"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Trend);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле deal_time
            field =  dataMessage["deal_time"];
            shouldSet = field != null;
            if(shouldSet && (source.DealTime == null || source.DealTime == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DealTime);
            }

            // Поле min_price
            field =  dataMessage["min_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MinPrice);
            }

            // Поле max_price
            field =  dataMessage["max_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MaxPrice);
            }

            // Поле avr_price
            field =  dataMessage["avr_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AvrPrice);
            }

            // Поле old_kotir
            field =  dataMessage["old_kotir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OldKotir);
            }

            // Поле deal_count
            field =  dataMessage["deal_count"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DealCount);
            }

            // Поле contr_count
            field =  dataMessage["contr_count"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ContrCount);
            }

            // Поле capital
            field =  dataMessage["capital"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Capital);
            }

            // Поле pos
            field =  dataMessage["pos"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Pos);
            }

            // Поле mod_time
            field =  dataMessage["mod_time"];
            shouldSet = field != null;
            if(shouldSet && (source.ModTime == null || source.ModTime == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ModTime);
            }

            // Поле cur_kotir
            field =  dataMessage["cur_kotir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CurKotir);
            }

            // Поле cur_kotir_real
            field =  dataMessage["cur_kotir_real"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CurKotirReal);
            }

            // Поле orders_sell_qty
            field =  dataMessage["orders_sell_qty"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrdersSellQty);
            }

            // Поле orders_sell_amount
            field =  dataMessage["orders_sell_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrdersSellAmount);
            }

            // Поле orders_buy_qty
            field =  dataMessage["orders_buy_qty"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrdersBuyQty);
            }

            // Поле orders_buy_amount
            field =  dataMessage["orders_buy_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrdersBuyAmount);
            }

            // Поле open_price
            field =  dataMessage["open_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OpenPrice);
            }

            // Поле close_price
            field =  dataMessage["close_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ClosePrice);
            }

            // Поле local_time
            field =  dataMessage["local_time"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalTime == null || source.LocalTime == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalTime);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutCommon.CgmCommon)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmDeliveryReport source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле date
            field =  dataMessage["date"];
            shouldSet = field != null;
            if(shouldSet && (source.Date == null || source.Date == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Date);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле type
            field =  dataMessage["type"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Type))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Type);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле pos
            field =  dataMessage["pos"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Pos);
            }

            // Поле pos_excl
            field =  dataMessage["pos_excl"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PosExcl);
            }

            // Поле pos_unexec
            field =  dataMessage["pos_unexec"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PosUnexec);
            }

            // Поле unexec
            field =  dataMessage["unexec"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Unexec);
            }

            // Поле settl_pair
            field =  dataMessage["settl_pair"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.SettlPair))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.SettlPair);
            }

            // Поле asset_code
            field =  dataMessage["asset_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.AssetCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.AssetCode);
            }

            // Поле issue_code
            field =  dataMessage["issue_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.IssueCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.IssueCode);
            }

            // Поле oblig_rur
            field =  dataMessage["oblig_rur"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ObligRur);
            }

            // Поле oblig_qty
            field =  dataMessage["oblig_qty"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ObligQty);
            }

            // Поле fulfil_rur
            field =  dataMessage["fulfil_rur"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FulfilRur);
            }

            // Поле fulfil_qty
            field =  dataMessage["fulfil_qty"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FulfilQty);
            }

            // Поле step
            field =  dataMessage["step"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Step);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле id_gen
            field =  dataMessage["id_gen"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdGen);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmDeliveryReport)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmFutRejectedOrders source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле order_id
            field =  dataMessage["order_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле moment_reject
            field =  dataMessage["moment_reject"];
            shouldSet = field != null;
            if(shouldSet && (source.MomentReject == null || source.MomentReject == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.MomentReject);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле date_exp
            field =  dataMessage["date_exp"];
            shouldSet = field != null;
            if(shouldSet && (source.DateExp == null || source.DateExp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateExp);
            }

            // Поле id_ord1
            field =  dataMessage["id_ord1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd1);
            }

            // Поле ret_code
            field =  dataMessage["ret_code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.RetCode);
            }

            // Поле ret_message
            field =  dataMessage["ret_message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.RetMessage))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.RetMessage);
            }

            // Поле comment
            field =  dataMessage["comment"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Comment))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Comment);
            }

            // Поле login_from
            field =  dataMessage["login_from"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginFrom))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginFrom);
            }

            // Поле ext_id
            field =  dataMessage["ext_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmFutRejectedOrders)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmFutInterclInfo source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле vm_intercl
            field =  dataMessage["vm_intercl"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VmIntercl);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmFutInterclInfo)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmFutBondRegistry source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле bond_id
            field =  dataMessage["bond_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BondId);
            }

            // Поле small_name
            field =  dataMessage["small_name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.SmallName))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.SmallName);
            }

            // Поле short_isin
            field =  dataMessage["short_isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ShortIsin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ShortIsin);
            }

            // Поле name
            field =  dataMessage["name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Name))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Name);
            }

            // Поле date_redempt
            field =  dataMessage["date_redempt"];
            shouldSet = field != null;
            if(shouldSet && (source.DateRedempt == null || source.DateRedempt == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateRedempt);
            }

            // Поле nominal
            field =  dataMessage["nominal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Nominal);
            }

            // Поле bond_type
            field =  dataMessage["bond_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BondType);
            }

            // Поле year_base
            field =  dataMessage["year_base"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.YearBase);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmFutBondRegistry)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmFutBondIsin source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле bond_id
            field =  dataMessage["bond_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BondId);
            }

            // Поле coeff_conversion
            field =  dataMessage["coeff_conversion"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CoeffConversion);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmFutBondIsin)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmFutBondNkd source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле bond_id
            field =  dataMessage["bond_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BondId);
            }

            // Поле date
            field =  dataMessage["date"];
            shouldSet = field != null;
            if(shouldSet && (source.Date == null || source.Date == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Date);
            }

            // Поле nkd
            field =  dataMessage["nkd"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Nkd);
            }

            // Поле is_cupon
            field =  dataMessage["is_cupon"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsCupon);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmFutBondNkd)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmFutBondNominal source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле bond_id
            field =  dataMessage["bond_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BondId);
            }

            // Поле date
            field =  dataMessage["date"];
            shouldSet = field != null;
            if(shouldSet && (source.Date == null || source.Date == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Date);
            }

            // Поле nominal
            field =  dataMessage["nominal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Nominal);
            }

            // Поле face_value
            field =  dataMessage["face_value"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FaceValue);
            }

            // Поле coupon_nominal
            field =  dataMessage["coupon_nominal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CouponNominal);
            }

            // Поле is_nominal
            field =  dataMessage["is_nominal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsNominal);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmFutBondNominal)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmUsdOnline source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле id
            field =  dataMessage["id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Id);
            }

            // Поле rate
            field =  dataMessage["rate"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Rate);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmUsdOnline)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmFutVcb source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле name
            field =  dataMessage["name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Name))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Name);
            }

            // Поле exec_type
            field =  dataMessage["exec_type"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ExecType))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ExecType);
            }

            // Поле curr
            field =  dataMessage["curr"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Curr))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Curr);
            }

            // Поле exch_pay
            field =  dataMessage["exch_pay"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExchPay);
            }

            // Поле exch_pay_scalped
            field =  dataMessage["exch_pay_scalped"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExchPayScalped);
            }

            // Поле clear_pay
            field =  dataMessage["clear_pay"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ClearPay);
            }

            // Поле clear_pay_scalped
            field =  dataMessage["clear_pay_scalped"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ClearPayScalped);
            }

            // Поле sell_fee
            field =  dataMessage["sell_fee"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SellFee);
            }

            // Поле buy_fee
            field =  dataMessage["buy_fee"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BuyFee);
            }

            // Поле trade_scheme
            field =  dataMessage["trade_scheme"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.TradeScheme))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.TradeScheme);
            }

            // Поле section
            field =  dataMessage["section"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Section))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Section);
            }

            // Поле exch_pay_spot
            field =  dataMessage["exch_pay_spot"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExchPaySpot);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле exch_pay_spot_repo
            field =  dataMessage["exch_pay_spot_repo"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExchPaySpotRepo);
            }

            // Поле rate_id
            field =  dataMessage["rate_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.RateId);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmFutVcb)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmSession source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле begin
            field =  dataMessage["begin"];
            shouldSet = field != null;
            if(shouldSet && (source.Begin == null || source.Begin == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Begin);
            }

            // Поле end
            field =  dataMessage["end"];
            shouldSet = field != null;
            if(shouldSet && (source.End == null || source.End == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.End);
            }

            // Поле state
            field =  dataMessage["state"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.State);
            }

            // Поле opt_sess_id
            field =  dataMessage["opt_sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OptSessId);
            }

            // Поле inter_cl_begin
            field =  dataMessage["inter_cl_begin"];
            shouldSet = field != null;
            if(shouldSet && (source.InterClBegin == null || source.InterClBegin == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.InterClBegin);
            }

            // Поле inter_cl_end
            field =  dataMessage["inter_cl_end"];
            shouldSet = field != null;
            if(shouldSet && (source.InterClEnd == null || source.InterClEnd == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.InterClEnd);
            }

            // Поле inter_cl_state
            field =  dataMessage["inter_cl_state"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.InterClState);
            }

            // Поле eve_on
            field =  dataMessage["eve_on"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EveOn);
            }

            // Поле eve_begin
            field =  dataMessage["eve_begin"];
            shouldSet = field != null;
            if(shouldSet && (source.EveBegin == null || source.EveBegin == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.EveBegin);
            }

            // Поле eve_end
            field =  dataMessage["eve_end"];
            shouldSet = field != null;
            if(shouldSet && (source.EveEnd == null || source.EveEnd == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.EveEnd);
            }

            // Поле mon_on
            field =  dataMessage["mon_on"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MonOn);
            }

            // Поле mon_begin
            field =  dataMessage["mon_begin"];
            shouldSet = field != null;
            if(shouldSet && (source.MonBegin == null || source.MonBegin == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.MonBegin);
            }

            // Поле mon_end
            field =  dataMessage["mon_end"];
            shouldSet = field != null;
            if(shouldSet && (source.MonEnd == null || source.MonEnd == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.MonEnd);
            }

            // Поле pos_transfer_begin
            field =  dataMessage["pos_transfer_begin"];
            shouldSet = field != null;
            if(shouldSet && (source.PosTransferBegin == null || source.PosTransferBegin == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.PosTransferBegin);
            }

            // Поле pos_transfer_end
            field =  dataMessage["pos_transfer_end"];
            shouldSet = field != null;
            if(shouldSet && (source.PosTransferEnd == null || source.PosTransferEnd == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.PosTransferEnd);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmSession)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmMultilegDict source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле isin_id_leg
            field =  dataMessage["isin_id_leg"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinIdLeg);
            }

            // Поле qty_ratio
            field =  dataMessage["qty_ratio"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.QtyRatio);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmMultilegDict)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmFutSessContents source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле short_isin
            field =  dataMessage["short_isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ShortIsin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ShortIsin);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле name
            field =  dataMessage["name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Name))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Name);
            }

            // Поле inst_term
            field =  dataMessage["inst_term"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.InstTerm);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле is_limited
            field =  dataMessage["is_limited"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsLimited);
            }

            // Поле limit_up
            field =  dataMessage["limit_up"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LimitUp);
            }

            // Поле limit_down
            field =  dataMessage["limit_down"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LimitDown);
            }

            // Поле old_kotir
            field =  dataMessage["old_kotir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OldKotir);
            }

            // Поле buy_deposit
            field =  dataMessage["buy_deposit"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BuyDeposit);
            }

            // Поле sell_deposit
            field =  dataMessage["sell_deposit"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SellDeposit);
            }

            // Поле roundto
            field =  dataMessage["roundto"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Roundto);
            }

            // Поле min_step
            field =  dataMessage["min_step"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MinStep);
            }

            // Поле lot_volume
            field =  dataMessage["lot_volume"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LotVolume);
            }

            // Поле step_price
            field =  dataMessage["step_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StepPrice);
            }

            // Поле d_pg
            field =  dataMessage["d_pg"];
            shouldSet = field != null;
            if(shouldSet && (source.DPg == null || source.DPg == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DPg);
            }

            // Поле is_spread
            field =  dataMessage["is_spread"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsSpread);
            }

            // Поле coeff
            field =  dataMessage["coeff"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Coeff);
            }

            // Поле d_exp
            field =  dataMessage["d_exp"];
            shouldSet = field != null;
            if(shouldSet && (source.DExp == null || source.DExp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DExp);
            }

            // Поле is_percent
            field =  dataMessage["is_percent"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsPercent);
            }

            // Поле percent_rate
            field =  dataMessage["percent_rate"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PercentRate);
            }

            // Поле last_cl_quote
            field =  dataMessage["last_cl_quote"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LastClQuote);
            }

            // Поле signs
            field =  dataMessage["signs"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Signs);
            }

            // Поле is_trade_evening
            field =  dataMessage["is_trade_evening"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsTradeEvening);
            }

            // Поле ticker
            field =  dataMessage["ticker"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Ticker);
            }

            // Поле state
            field =  dataMessage["state"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.State);
            }

            // Поле price_dir
            field =  dataMessage["price_dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PriceDir);
            }

            // Поле multileg_type
            field =  dataMessage["multileg_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MultilegType);
            }

            // Поле legs_qty
            field =  dataMessage["legs_qty"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LegsQty);
            }

            // Поле step_price_clr
            field =  dataMessage["step_price_clr"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StepPriceClr);
            }

            // Поле step_price_interclr
            field =  dataMessage["step_price_interclr"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StepPriceInterclr);
            }

            // Поле step_price_curr
            field =  dataMessage["step_price_curr"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StepPriceCurr);
            }

            // Поле d_start
            field =  dataMessage["d_start"];
            shouldSet = field != null;
            if(shouldSet && (source.DStart == null || source.DStart == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DStart);
            }

            // Поле exch_pay
            field =  dataMessage["exch_pay"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExchPay);
            }

            // Поле pctyield_coeff
            field =  dataMessage["pctyield_coeff"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PctyieldCoeff);
            }

            // Поле pctyield_total
            field =  dataMessage["pctyield_total"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PctyieldTotal);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmFutSessContents)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmFutInstruments source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле short_isin
            field =  dataMessage["short_isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ShortIsin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ShortIsin);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле name
            field =  dataMessage["name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Name))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Name);
            }

            // Поле inst_term
            field =  dataMessage["inst_term"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.InstTerm);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле is_limited
            field =  dataMessage["is_limited"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsLimited);
            }

            // Поле old_kotir
            field =  dataMessage["old_kotir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OldKotir);
            }

            // Поле roundto
            field =  dataMessage["roundto"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Roundto);
            }

            // Поле min_step
            field =  dataMessage["min_step"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MinStep);
            }

            // Поле lot_volume
            field =  dataMessage["lot_volume"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LotVolume);
            }

            // Поле step_price
            field =  dataMessage["step_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StepPrice);
            }

            // Поле d_pg
            field =  dataMessage["d_pg"];
            shouldSet = field != null;
            if(shouldSet && (source.DPg == null || source.DPg == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DPg);
            }

            // Поле is_spread
            field =  dataMessage["is_spread"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsSpread);
            }

            // Поле coeff
            field =  dataMessage["coeff"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Coeff);
            }

            // Поле d_exp
            field =  dataMessage["d_exp"];
            shouldSet = field != null;
            if(shouldSet && (source.DExp == null || source.DExp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DExp);
            }

            // Поле is_percent
            field =  dataMessage["is_percent"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsPercent);
            }

            // Поле percent_rate
            field =  dataMessage["percent_rate"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PercentRate);
            }

            // Поле last_cl_quote
            field =  dataMessage["last_cl_quote"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LastClQuote);
            }

            // Поле signs
            field =  dataMessage["signs"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Signs);
            }

            // Поле volat_min
            field =  dataMessage["volat_min"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VolatMin);
            }

            // Поле volat_max
            field =  dataMessage["volat_max"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VolatMax);
            }

            // Поле price_dir
            field =  dataMessage["price_dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PriceDir);
            }

            // Поле multileg_type
            field =  dataMessage["multileg_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MultilegType);
            }

            // Поле legs_qty
            field =  dataMessage["legs_qty"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LegsQty);
            }

            // Поле step_price_clr
            field =  dataMessage["step_price_clr"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StepPriceClr);
            }

            // Поле step_price_interclr
            field =  dataMessage["step_price_interclr"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StepPriceInterclr);
            }

            // Поле step_price_curr
            field =  dataMessage["step_price_curr"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StepPriceCurr);
            }

            // Поле d_start
            field =  dataMessage["d_start"];
            shouldSet = field != null;
            if(shouldSet && (source.DStart == null || source.DStart == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DStart);
            }

            // Поле is_limit_opt
            field =  dataMessage["is_limit_opt"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsLimitOpt);
            }

            // Поле limit_up_opt
            field =  dataMessage["limit_up_opt"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LimitUpOpt);
            }

            // Поле limit_down_opt
            field =  dataMessage["limit_down_opt"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LimitDownOpt);
            }

            // Поле adm_lim
            field =  dataMessage["adm_lim"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AdmLim);
            }

            // Поле adm_lim_offmoney
            field =  dataMessage["adm_lim_offmoney"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AdmLimOffmoney);
            }

            // Поле apply_adm_limit
            field =  dataMessage["apply_adm_limit"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ApplyAdmLimit);
            }

            // Поле pctyield_coeff
            field =  dataMessage["pctyield_coeff"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PctyieldCoeff);
            }

            // Поле pctyield_total
            field =  dataMessage["pctyield_total"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PctyieldTotal);
            }

            // Поле exec_name
            field =  dataMessage["exec_name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ExecName))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ExecName);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmFutInstruments)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmDiler source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле name
            field =  dataMessage["name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Name))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Name);
            }

            // Поле rts_code
            field =  dataMessage["rts_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.RtsCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.RtsCode);
            }

            // Поле transfer_code
            field =  dataMessage["transfer_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.TransferCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.TransferCode);
            }

            // Поле status
            field =  dataMessage["status"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Status);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmDiler)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmInvestr source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле name
            field =  dataMessage["name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Name))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Name);
            }

            // Поле status
            field =  dataMessage["status"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Status);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmInvestr)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmFutSessSettl source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле date_clr
            field =  dataMessage["date_clr"];
            shouldSet = field != null;
            if(shouldSet && (source.DateClr == null || source.DateClr == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateClr);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле settl_price
            field =  dataMessage["settl_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SettlPrice);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmFutSessSettl)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmSysMessages source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле msg_id
            field =  dataMessage["msg_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MsgId);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле lang_code
            field =  dataMessage["lang_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LangCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LangCode);
            }

            // Поле urgency
            field =  dataMessage["urgency"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Urgency);
            }

            // Поле status
            field =  dataMessage["status"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Status);
            }

            // Поле text
            field =  dataMessage["text"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Text))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Text);
            }

            // Поле message_body
            field =  dataMessage["message_body"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.MessageBody))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.MessageBody);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmSysMessages)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmFutSettlementAccount source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Code))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле type
            field =  dataMessage["type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Type);
            }

            // Поле settlement_account
            field =  dataMessage["settlement_account"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.SettlementAccount))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.SettlementAccount);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmFutSettlementAccount)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmFutMarginType source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле code
            field =  dataMessage["code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Code))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Code);
            }

            // Поле type
            field =  dataMessage["type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Type);
            }

            // Поле margin_type
            field =  dataMessage["margin_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MarginType);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmFutMarginType)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmProhibition source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле prohib_id
            field =  dataMessage["prohib_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ProhibId);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле initiator
            field =  dataMessage["initiator"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Initiator);
            }

            // Поле section
            field =  dataMessage["section"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Section))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Section);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле priority
            field =  dataMessage["priority"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Priority);
            }

            // Поле group_mask
            field =  dataMessage["group_mask"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.GroupMask);
            }

            // Поле type
            field =  dataMessage["type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Type);
            }

            // Поле is_legacy
            field =  dataMessage["is_legacy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsLegacy);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmProhibition)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmRates source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле rate_id
            field =  dataMessage["rate_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.RateId);
            }

            // Поле curr_base
            field =  dataMessage["curr_base"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CurrBase))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CurrBase);
            }

            // Поле curr_coupled
            field =  dataMessage["curr_coupled"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CurrCoupled))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CurrCoupled);
            }

            // Поле radius
            field =  dataMessage["radius"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Radius);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmRates)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutInfo.CgmSysEvents source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле event_id
            field =  dataMessage["event_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле event_type
            field =  dataMessage["event_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventType);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutInfo.CgmSysEvents)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutTrades.CgmOrdersLog source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле id_ord
            field =  dataMessage["id_ord"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле amount_rest
            field =  dataMessage["amount_rest"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountRest);
            }

            // Поле id_deal
            field =  dataMessage["id_deal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDeal);
            }

            // Поле xstatus
            field =  dataMessage["xstatus"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Xstatus);
            }

            // Поле status
            field =  dataMessage["status"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Status);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

            // Поле action
            field =  dataMessage["action"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Action);
            }

            // Поле deal_price
            field =  dataMessage["deal_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DealPrice);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле login_from
            field =  dataMessage["login_from"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginFrom))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginFrom);
            }

            // Поле comment
            field =  dataMessage["comment"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Comment))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Comment);
            }

            // Поле hedge
            field =  dataMessage["hedge"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Hedge);
            }

            // Поле trust
            field =  dataMessage["trust"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Trust);
            }

            // Поле ext_id
            field =  dataMessage["ext_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId);
            }

            // Поле broker_to
            field =  dataMessage["broker_to"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerTo))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerTo);
            }

            // Поле broker_to_rts
            field =  dataMessage["broker_to_rts"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerToRts))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerToRts);
            }

            // Поле broker_from_rts
            field =  dataMessage["broker_from_rts"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerFromRts))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerFromRts);
            }

            // Поле date_exp
            field =  dataMessage["date_exp"];
            shouldSet = field != null;
            if(shouldSet && (source.DateExp == null || source.DateExp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateExp);
            }

            // Поле id_ord1
            field =  dataMessage["id_ord1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd1);
            }

            // Поле local_stamp
            field =  dataMessage["local_stamp"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalStamp == null || source.LocalStamp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalStamp);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutTrades.CgmOrdersLog)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutTrades.CgmMultilegOrdersLog source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле id_ord
            field =  dataMessage["id_ord"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле amount_rest
            field =  dataMessage["amount_rest"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountRest);
            }

            // Поле id_deal
            field =  dataMessage["id_deal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDeal);
            }

            // Поле xstatus
            field =  dataMessage["xstatus"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Xstatus);
            }

            // Поле status
            field =  dataMessage["status"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Status);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

            // Поле action
            field =  dataMessage["action"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Action);
            }

            // Поле deal_price
            field =  dataMessage["deal_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DealPrice);
            }

            // Поле rate_price
            field =  dataMessage["rate_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.RatePrice);
            }

            // Поле swap_price
            field =  dataMessage["swap_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SwapPrice);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле login_from
            field =  dataMessage["login_from"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginFrom))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginFrom);
            }

            // Поле comment
            field =  dataMessage["comment"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Comment))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Comment);
            }

            // Поле hedge
            field =  dataMessage["hedge"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Hedge);
            }

            // Поле trust
            field =  dataMessage["trust"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Trust);
            }

            // Поле ext_id
            field =  dataMessage["ext_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId);
            }

            // Поле broker_to
            field =  dataMessage["broker_to"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerTo))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerTo);
            }

            // Поле broker_to_rts
            field =  dataMessage["broker_to_rts"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerToRts))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerToRts);
            }

            // Поле broker_from_rts
            field =  dataMessage["broker_from_rts"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerFromRts))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerFromRts);
            }

            // Поле date_exp
            field =  dataMessage["date_exp"];
            shouldSet = field != null;
            if(shouldSet && (source.DateExp == null || source.DateExp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateExp);
            }

            // Поле id_ord1
            field =  dataMessage["id_ord1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd1);
            }

            // Поле local_stamp
            field =  dataMessage["local_stamp"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalStamp == null || source.LocalStamp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalStamp);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutTrades.CgmMultilegOrdersLog)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutTrades.CgmDeal source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле id_deal
            field =  dataMessage["id_deal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDeal);
            }

            // Поле id_deal_multileg
            field =  dataMessage["id_deal_multileg"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDealMultileg);
            }

            // Поле id_repo
            field =  dataMessage["id_repo"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdRepo);
            }

            // Поле pos
            field =  dataMessage["pos"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Pos);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле id_ord_buy
            field =  dataMessage["id_ord_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrdBuy);
            }

            // Поле id_ord_sell
            field =  dataMessage["id_ord_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrdSell);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле nosystem
            field =  dataMessage["nosystem"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Nosystem);
            }

            // Поле xstatus_buy
            field =  dataMessage["xstatus_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.XstatusBuy);
            }

            // Поле xstatus_sell
            field =  dataMessage["xstatus_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.XstatusSell);
            }

            // Поле status_buy
            field =  dataMessage["status_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StatusBuy);
            }

            // Поле status_sell
            field =  dataMessage["status_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StatusSell);
            }

            // Поле ext_id_buy
            field =  dataMessage["ext_id_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtIdBuy);
            }

            // Поле ext_id_sell
            field =  dataMessage["ext_id_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtIdSell);
            }

            // Поле code_buy
            field =  dataMessage["code_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeBuy);
            }

            // Поле code_sell
            field =  dataMessage["code_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeSell);
            }

            // Поле comment_buy
            field =  dataMessage["comment_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CommentBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CommentBuy);
            }

            // Поле comment_sell
            field =  dataMessage["comment_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CommentSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CommentSell);
            }

            // Поле trust_buy
            field =  dataMessage["trust_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TrustBuy);
            }

            // Поле trust_sell
            field =  dataMessage["trust_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TrustSell);
            }

            // Поле hedge_buy
            field =  dataMessage["hedge_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.HedgeBuy);
            }

            // Поле hedge_sell
            field =  dataMessage["hedge_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.HedgeSell);
            }

            // Поле fee_buy
            field =  dataMessage["fee_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeBuy);
            }

            // Поле fee_sell
            field =  dataMessage["fee_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeSell);
            }

            // Поле login_buy
            field =  dataMessage["login_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginBuy);
            }

            // Поле login_sell
            field =  dataMessage["login_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginSell);
            }

            // Поле code_rts_buy
            field =  dataMessage["code_rts_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeRtsBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeRtsBuy);
            }

            // Поле code_rts_sell
            field =  dataMessage["code_rts_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeRtsSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeRtsSell);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutTrades.CgmDeal)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutTrades.CgmMultilegDeal source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле isin_id_rd
            field =  dataMessage["isin_id_rd"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinIdRd);
            }

            // Поле isin_id_rb
            field =  dataMessage["isin_id_rb"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinIdRb);
            }

            // Поле isin_id_repo
            field =  dataMessage["isin_id_repo"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinIdRepo);
            }

            // Поле duration
            field =  dataMessage["duration"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Duration);
            }

            // Поле id_deal
            field =  dataMessage["id_deal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDeal);
            }

            // Поле id_deal_rd
            field =  dataMessage["id_deal_rd"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDealRd);
            }

            // Поле id_deal_rb
            field =  dataMessage["id_deal_rb"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDealRb);
            }

            // Поле id_ord_buy
            field =  dataMessage["id_ord_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrdBuy);
            }

            // Поле id_ord_sell
            field =  dataMessage["id_ord_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrdSell);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле rate_price
            field =  dataMessage["rate_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.RatePrice);
            }

            // Поле swap_price
            field =  dataMessage["swap_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SwapPrice);
            }

            // Поле buyback_amount
            field =  dataMessage["buyback_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BuybackAmount);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле nosystem
            field =  dataMessage["nosystem"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Nosystem);
            }

            // Поле xstatus_buy
            field =  dataMessage["xstatus_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.XstatusBuy);
            }

            // Поле xstatus_sell
            field =  dataMessage["xstatus_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.XstatusSell);
            }

            // Поле status_buy
            field =  dataMessage["status_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StatusBuy);
            }

            // Поле status_sell
            field =  dataMessage["status_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StatusSell);
            }

            // Поле ext_id_buy
            field =  dataMessage["ext_id_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtIdBuy);
            }

            // Поле ext_id_sell
            field =  dataMessage["ext_id_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtIdSell);
            }

            // Поле code_buy
            field =  dataMessage["code_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeBuy);
            }

            // Поле code_sell
            field =  dataMessage["code_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeSell);
            }

            // Поле comment_buy
            field =  dataMessage["comment_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CommentBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CommentBuy);
            }

            // Поле comment_sell
            field =  dataMessage["comment_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CommentSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CommentSell);
            }

            // Поле trust_buy
            field =  dataMessage["trust_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TrustBuy);
            }

            // Поле trust_sell
            field =  dataMessage["trust_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TrustSell);
            }

            // Поле hedge_buy
            field =  dataMessage["hedge_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.HedgeBuy);
            }

            // Поле hedge_sell
            field =  dataMessage["hedge_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.HedgeSell);
            }

            // Поле login_buy
            field =  dataMessage["login_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginBuy);
            }

            // Поле login_sell
            field =  dataMessage["login_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginSell);
            }

            // Поле code_rts_buy
            field =  dataMessage["code_rts_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeRtsBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeRtsBuy);
            }

            // Поле code_rts_sell
            field =  dataMessage["code_rts_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeRtsSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeRtsSell);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutTrades.CgmMultilegDeal)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutTrades.CgmHeartbeat source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле server_time
            field =  dataMessage["server_time"];
            shouldSet = field != null;
            if(shouldSet && (source.ServerTime == null || source.ServerTime == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ServerTime);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutTrades.CgmHeartbeat)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutTrades.CgmSysEvents source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле event_id
            field =  dataMessage["event_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле event_type
            field =  dataMessage["event_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventType);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutTrades.CgmSysEvents)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutTrades.CgmUserDeal source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле id_deal
            field =  dataMessage["id_deal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDeal);
            }

            // Поле id_deal_multileg
            field =  dataMessage["id_deal_multileg"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDealMultileg);
            }

            // Поле id_repo
            field =  dataMessage["id_repo"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdRepo);
            }

            // Поле pos
            field =  dataMessage["pos"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Pos);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле id_ord_buy
            field =  dataMessage["id_ord_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrdBuy);
            }

            // Поле id_ord_sell
            field =  dataMessage["id_ord_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrdSell);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле nosystem
            field =  dataMessage["nosystem"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Nosystem);
            }

            // Поле xstatus_buy
            field =  dataMessage["xstatus_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.XstatusBuy);
            }

            // Поле xstatus_sell
            field =  dataMessage["xstatus_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.XstatusSell);
            }

            // Поле status_buy
            field =  dataMessage["status_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StatusBuy);
            }

            // Поле status_sell
            field =  dataMessage["status_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StatusSell);
            }

            // Поле ext_id_buy
            field =  dataMessage["ext_id_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtIdBuy);
            }

            // Поле ext_id_sell
            field =  dataMessage["ext_id_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtIdSell);
            }

            // Поле code_buy
            field =  dataMessage["code_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeBuy);
            }

            // Поле code_sell
            field =  dataMessage["code_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeSell);
            }

            // Поле comment_buy
            field =  dataMessage["comment_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CommentBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CommentBuy);
            }

            // Поле comment_sell
            field =  dataMessage["comment_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CommentSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CommentSell);
            }

            // Поле trust_buy
            field =  dataMessage["trust_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TrustBuy);
            }

            // Поле trust_sell
            field =  dataMessage["trust_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TrustSell);
            }

            // Поле hedge_buy
            field =  dataMessage["hedge_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.HedgeBuy);
            }

            // Поле hedge_sell
            field =  dataMessage["hedge_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.HedgeSell);
            }

            // Поле fee_buy
            field =  dataMessage["fee_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeBuy);
            }

            // Поле fee_sell
            field =  dataMessage["fee_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeSell);
            }

            // Поле login_buy
            field =  dataMessage["login_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginBuy);
            }

            // Поле login_sell
            field =  dataMessage["login_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginSell);
            }

            // Поле code_rts_buy
            field =  dataMessage["code_rts_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeRtsBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeRtsBuy);
            }

            // Поле code_rts_sell
            field =  dataMessage["code_rts_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeRtsSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeRtsSell);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutTrades.CgmUserDeal)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutTrades.CgmUserMultilegDeal source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле id_deal
            field =  dataMessage["id_deal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDeal);
            }

            // Поле isin_id_rd
            field =  dataMessage["isin_id_rd"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinIdRd);
            }

            // Поле isin_id_rb
            field =  dataMessage["isin_id_rb"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinIdRb);
            }

            // Поле isin_id_repo
            field =  dataMessage["isin_id_repo"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinIdRepo);
            }

            // Поле duration
            field =  dataMessage["duration"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Duration);
            }

            // Поле id_deal_rd
            field =  dataMessage["id_deal_rd"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDealRd);
            }

            // Поле id_deal_rb
            field =  dataMessage["id_deal_rb"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDealRb);
            }

            // Поле id_ord_buy
            field =  dataMessage["id_ord_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrdBuy);
            }

            // Поле id_ord_sell
            field =  dataMessage["id_ord_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrdSell);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле rate_price
            field =  dataMessage["rate_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.RatePrice);
            }

            // Поле swap_price
            field =  dataMessage["swap_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SwapPrice);
            }

            // Поле buyback_amount
            field =  dataMessage["buyback_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BuybackAmount);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле nosystem
            field =  dataMessage["nosystem"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Nosystem);
            }

            // Поле xstatus_buy
            field =  dataMessage["xstatus_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.XstatusBuy);
            }

            // Поле xstatus_sell
            field =  dataMessage["xstatus_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.XstatusSell);
            }

            // Поле status_buy
            field =  dataMessage["status_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StatusBuy);
            }

            // Поле status_sell
            field =  dataMessage["status_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StatusSell);
            }

            // Поле ext_id_buy
            field =  dataMessage["ext_id_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtIdBuy);
            }

            // Поле ext_id_sell
            field =  dataMessage["ext_id_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtIdSell);
            }

            // Поле code_buy
            field =  dataMessage["code_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeBuy);
            }

            // Поле code_sell
            field =  dataMessage["code_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeSell);
            }

            // Поле comment_buy
            field =  dataMessage["comment_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CommentBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CommentBuy);
            }

            // Поле comment_sell
            field =  dataMessage["comment_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CommentSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CommentSell);
            }

            // Поле trust_buy
            field =  dataMessage["trust_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TrustBuy);
            }

            // Поле trust_sell
            field =  dataMessage["trust_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TrustSell);
            }

            // Поле hedge_buy
            field =  dataMessage["hedge_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.HedgeBuy);
            }

            // Поле hedge_sell
            field =  dataMessage["hedge_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.HedgeSell);
            }

            // Поле login_buy
            field =  dataMessage["login_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginBuy);
            }

            // Поле login_sell
            field =  dataMessage["login_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginSell);
            }

            // Поле code_rts_buy
            field =  dataMessage["code_rts_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeRtsBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeRtsBuy);
            }

            // Поле code_rts_sell
            field =  dataMessage["code_rts_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeRtsSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeRtsSell);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutTrades.CgmUserMultilegDeal)

        public static void CopyToDataMessage(this CGateAdapter.Messages.FutTradesHeartbeat.CgmHeartbeat source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле server_time
            field =  dataMessage["server_time"];
            shouldSet = field != null;
            if(shouldSet && (source.ServerTime == null || source.ServerTime == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ServerTime);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.FutTradesHeartbeat.CgmHeartbeat)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Info.CgmBaseContractsParams source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле code_mcs
            field =  dataMessage["code_mcs"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeMcs))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeMcs);
            }

            // Поле volat_num
            field =  dataMessage["volat_num"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VolatNum);
            }

            // Поле points_num
            field =  dataMessage["points_num"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PointsNum);
            }

            // Поле subrisk_step
            field =  dataMessage["subrisk_step"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SubriskStep);
            }

            // Поле is_percent
            field =  dataMessage["is_percent"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsPercent);
            }

            // Поле percent_rate
            field =  dataMessage["percent_rate"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PercentRate);
            }

            // Поле currency_volat
            field =  dataMessage["currency_volat"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CurrencyVolat);
            }

            // Поле is_usd
            field =  dataMessage["is_usd"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsUsd);
            }

            // Поле usd_rate_curv_radius
            field =  dataMessage["usd_rate_curv_radius"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.UsdRateCurvRadius);
            }

            // Поле somc
            field =  dataMessage["somc"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Somc);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Info.CgmBaseContractsParams)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Info.CgmFuturesParams source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле limit
            field =  dataMessage["limit"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Limit);
            }

            // Поле settl_price
            field =  dataMessage["settl_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SettlPrice);
            }

            // Поле spread_aspect
            field =  dataMessage["spread_aspect"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SpreadAspect);
            }

            // Поле subrisk
            field =  dataMessage["subrisk"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Subrisk);
            }

            // Поле step_price
            field =  dataMessage["step_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StepPrice);
            }

            // Поле base_go
            field =  dataMessage["base_go"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BaseGo);
            }

            // Поле exp_date
            field =  dataMessage["exp_date"];
            shouldSet = field != null;
            if(shouldSet && (source.ExpDate == null || source.ExpDate == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ExpDate);
            }

            // Поле spot_signs
            field =  dataMessage["spot_signs"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SpotSigns);
            }

            // Поле settl_price_real
            field =  dataMessage["settl_price_real"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SettlPriceReal);
            }

            // Поле min_step
            field =  dataMessage["min_step"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MinStep);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Info.CgmFuturesParams)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Info.CgmVirtualFuturesParams source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле isin_base
            field =  dataMessage["isin_base"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.IsinBase))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.IsinBase);
            }

            // Поле is_net_positive
            field =  dataMessage["is_net_positive"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsNetPositive);
            }

            // Поле volat_range
            field =  dataMessage["volat_range"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VolatRange);
            }

            // Поле t_squared
            field =  dataMessage["t_squared"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TSquared);
            }

            // Поле max_addrisk
            field =  dataMessage["max_addrisk"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MaxAddrisk);
            }

            // Поле a
            field =  dataMessage["a"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.A);
            }

            // Поле b
            field =  dataMessage["b"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.B);
            }

            // Поле c
            field =  dataMessage["c"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.C);
            }

            // Поле d
            field =  dataMessage["d"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.D);
            }

            // Поле e
            field =  dataMessage["e"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.E);
            }

            // Поле s
            field =  dataMessage["s"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.S);
            }

            // Поле exp_date
            field =  dataMessage["exp_date"];
            shouldSet = field != null;
            if(shouldSet && (source.ExpDate == null || source.ExpDate == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ExpDate);
            }

            // Поле fut_type
            field =  dataMessage["fut_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FutType);
            }

            // Поле use_null_volat
            field =  dataMessage["use_null_volat"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.UseNullVolat);
            }

            // Поле exp_clearings_bf
            field =  dataMessage["exp_clearings_bf"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExpClearingsBf);
            }

            // Поле exp_clearings_cc
            field =  dataMessage["exp_clearings_cc"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExpClearingsCc);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Info.CgmVirtualFuturesParams)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Info.CgmOptionsParams source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле isin_base
            field =  dataMessage["isin_base"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.IsinBase))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.IsinBase);
            }

            // Поле strike
            field =  dataMessage["strike"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Strike);
            }

            // Поле opt_type
            field =  dataMessage["opt_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OptType);
            }

            // Поле settl_price
            field =  dataMessage["settl_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SettlPrice);
            }

            // Поле base_go_sell
            field =  dataMessage["base_go_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BaseGoSell);
            }

            // Поле synth_base_go
            field =  dataMessage["synth_base_go"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SynthBaseGo);
            }

            // Поле base_go_buy
            field =  dataMessage["base_go_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BaseGoBuy);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Info.CgmOptionsParams)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Info.CgmBrokerParams source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле broker_code
            field =  dataMessage["broker_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerCode);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле limit_spot_sell
            field =  dataMessage["limit_spot_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LimitSpotSell);
            }

            // Поле used_limit_spot_sell
            field =  dataMessage["used_limit_spot_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.UsedLimitSpotSell);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Info.CgmBrokerParams)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Info.CgmClientParams source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле coeff_go
            field =  dataMessage["coeff_go"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CoeffGo);
            }

            // Поле limit_spot_sell
            field =  dataMessage["limit_spot_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LimitSpotSell);
            }

            // Поле used_limit_spot_sell
            field =  dataMessage["used_limit_spot_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.UsedLimitSpotSell);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Info.CgmClientParams)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Info.CgmSysEvents source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле event_id
            field =  dataMessage["event_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле event_type
            field =  dataMessage["event_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventType);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Info.CgmSysEvents)

        public static void CopyToDataMessage(this CGateAdapter.Messages.MiscInfo.CgmVolatCoeff source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле a
            field =  dataMessage["a"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.A);
            }

            // Поле b
            field =  dataMessage["b"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.B);
            }

            // Поле c
            field =  dataMessage["c"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.C);
            }

            // Поле d
            field =  dataMessage["d"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.D);
            }

            // Поле e
            field =  dataMessage["e"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.E);
            }

            // Поле s
            field =  dataMessage["s"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.S);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.MiscInfo.CgmVolatCoeff)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Mm.CgmFutMmInfo source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле spread
            field =  dataMessage["spread"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Spread);
            }

            // Поле price_edge_sell
            field =  dataMessage["price_edge_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PriceEdgeSell);
            }

            // Поле amount_sells
            field =  dataMessage["amount_sells"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountSells);
            }

            // Поле price_edge_buy
            field =  dataMessage["price_edge_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PriceEdgeBuy);
            }

            // Поле amount_buys
            field =  dataMessage["amount_buys"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountBuys);
            }

            // Поле mm_spread
            field =  dataMessage["mm_spread"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MmSpread);
            }

            // Поле mm_amount
            field =  dataMessage["mm_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MmAmount);
            }

            // Поле spread_sign
            field =  dataMessage["spread_sign"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SpreadSign);
            }

            // Поле amount_sign
            field =  dataMessage["amount_sign"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountSign);
            }

            // Поле percent_time
            field =  dataMessage["percent_time"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PercentTime);
            }

            // Поле period_start
            field =  dataMessage["period_start"];
            shouldSet = field != null;
            if(shouldSet && (source.PeriodStart == null || source.PeriodStart == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.PeriodStart);
            }

            // Поле period_end
            field =  dataMessage["period_end"];
            shouldSet = field != null;
            if(shouldSet && (source.PeriodEnd == null || source.PeriodEnd == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.PeriodEnd);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле active_sign
            field =  dataMessage["active_sign"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ActiveSign);
            }

            // Поле agmt_id
            field =  dataMessage["agmt_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AgmtId);
            }

            // Поле fulfil_min
            field =  dataMessage["fulfil_min"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FulfilMin);
            }

            // Поле fulfil_partial
            field =  dataMessage["fulfil_partial"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FulfilPartial);
            }

            // Поле fulfil_total
            field =  dataMessage["fulfil_total"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FulfilTotal);
            }

            // Поле is_fulfil_min
            field =  dataMessage["is_fulfil_min"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsFulfilMin);
            }

            // Поле is_fulfil_partial
            field =  dataMessage["is_fulfil_partial"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsFulfilPartial);
            }

            // Поле is_fulfil_total
            field =  dataMessage["is_fulfil_total"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsFulfilTotal);
            }

            // Поле is_rf
            field =  dataMessage["is_rf"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsRf);
            }

            // Поле id_group
            field =  dataMessage["id_group"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdGroup);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Mm.CgmFutMmInfo)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Mm.CgmOptMmInfo source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле spread
            field =  dataMessage["spread"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Spread);
            }

            // Поле price_edge_sell
            field =  dataMessage["price_edge_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PriceEdgeSell);
            }

            // Поле amount_sells
            field =  dataMessage["amount_sells"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountSells);
            }

            // Поле price_edge_buy
            field =  dataMessage["price_edge_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PriceEdgeBuy);
            }

            // Поле amount_buys
            field =  dataMessage["amount_buys"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountBuys);
            }

            // Поле mm_spread
            field =  dataMessage["mm_spread"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MmSpread);
            }

            // Поле mm_amount
            field =  dataMessage["mm_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MmAmount);
            }

            // Поле spread_sign
            field =  dataMessage["spread_sign"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SpreadSign);
            }

            // Поле amount_sign
            field =  dataMessage["amount_sign"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountSign);
            }

            // Поле percent_time
            field =  dataMessage["percent_time"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PercentTime);
            }

            // Поле period_start
            field =  dataMessage["period_start"];
            shouldSet = field != null;
            if(shouldSet && (source.PeriodStart == null || source.PeriodStart == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.PeriodStart);
            }

            // Поле period_end
            field =  dataMessage["period_end"];
            shouldSet = field != null;
            if(shouldSet && (source.PeriodEnd == null || source.PeriodEnd == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.PeriodEnd);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле cstrike_offset
            field =  dataMessage["cstrike_offset"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CstrikeOffset);
            }

            // Поле active_sign
            field =  dataMessage["active_sign"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ActiveSign);
            }

            // Поле agmt_id
            field =  dataMessage["agmt_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AgmtId);
            }

            // Поле fulfil_min
            field =  dataMessage["fulfil_min"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FulfilMin);
            }

            // Поле fulfil_partial
            field =  dataMessage["fulfil_partial"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FulfilPartial);
            }

            // Поле fulfil_total
            field =  dataMessage["fulfil_total"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FulfilTotal);
            }

            // Поле is_fulfil_min
            field =  dataMessage["is_fulfil_min"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsFulfilMin);
            }

            // Поле is_fulfil_partial
            field =  dataMessage["is_fulfil_partial"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsFulfilPartial);
            }

            // Поле is_fulfil_total
            field =  dataMessage["is_fulfil_total"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsFulfilTotal);
            }

            // Поле is_rf
            field =  dataMessage["is_rf"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsRf);
            }

            // Поле id_group
            field =  dataMessage["id_group"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdGroup);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Mm.CgmOptMmInfo)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Mm.CgmCsMmRule source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Mm.CgmCsMmRule)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Mm.CgmMmAgreementFilter source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле agmt_id
            field =  dataMessage["agmt_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AgmtId);
            }

            // Поле agreement
            field =  dataMessage["agreement"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Agreement))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Agreement);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле is_fut
            field =  dataMessage["is_fut"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsFut);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Mm.CgmMmAgreementFilter)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptCommon.CgmCommon source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле best_sell
            field =  dataMessage["best_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BestSell);
            }

            // Поле amount_sell
            field =  dataMessage["amount_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountSell);
            }

            // Поле best_buy
            field =  dataMessage["best_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BestBuy);
            }

            // Поле amount_buy
            field =  dataMessage["amount_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountBuy);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле trend
            field =  dataMessage["trend"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Trend);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле deal_time
            field =  dataMessage["deal_time"];
            shouldSet = field != null;
            if(shouldSet && (source.DealTime == null || source.DealTime == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DealTime);
            }

            // Поле min_price
            field =  dataMessage["min_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MinPrice);
            }

            // Поле max_price
            field =  dataMessage["max_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MaxPrice);
            }

            // Поле avr_price
            field =  dataMessage["avr_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AvrPrice);
            }

            // Поле old_kotir
            field =  dataMessage["old_kotir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OldKotir);
            }

            // Поле deal_count
            field =  dataMessage["deal_count"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DealCount);
            }

            // Поле contr_count
            field =  dataMessage["contr_count"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ContrCount);
            }

            // Поле capital
            field =  dataMessage["capital"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Capital);
            }

            // Поле pos
            field =  dataMessage["pos"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Pos);
            }

            // Поле mod_time
            field =  dataMessage["mod_time"];
            shouldSet = field != null;
            if(shouldSet && (source.ModTime == null || source.ModTime == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ModTime);
            }

            // Поле isin_is_spec
            field =  dataMessage["isin_is_spec"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinIsSpec);
            }

            // Поле orders_sell_qty
            field =  dataMessage["orders_sell_qty"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrdersSellQty);
            }

            // Поле orders_sell_amount
            field =  dataMessage["orders_sell_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrdersSellAmount);
            }

            // Поле orders_buy_qty
            field =  dataMessage["orders_buy_qty"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrdersBuyQty);
            }

            // Поле orders_buy_amount
            field =  dataMessage["orders_buy_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrdersBuyAmount);
            }

            // Поле open_price
            field =  dataMessage["open_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OpenPrice);
            }

            // Поле close_price
            field =  dataMessage["close_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ClosePrice);
            }

            // Поле local_time
            field =  dataMessage["local_time"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalTime == null || source.LocalTime == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalTime);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptCommon.CgmCommon)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptInfo.CgmOptRejectedOrders source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле order_id
            field =  dataMessage["order_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OrderId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле moment_reject
            field =  dataMessage["moment_reject"];
            shouldSet = field != null;
            if(shouldSet && (source.MomentReject == null || source.MomentReject == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.MomentReject);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле date_exp
            field =  dataMessage["date_exp"];
            shouldSet = field != null;
            if(shouldSet && (source.DateExp == null || source.DateExp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateExp);
            }

            // Поле id_ord1
            field =  dataMessage["id_ord1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd1);
            }

            // Поле ret_code
            field =  dataMessage["ret_code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.RetCode);
            }

            // Поле ret_message
            field =  dataMessage["ret_message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.RetMessage))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.RetMessage);
            }

            // Поле comment
            field =  dataMessage["comment"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Comment))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Comment);
            }

            // Поле login_from
            field =  dataMessage["login_from"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginFrom))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginFrom);
            }

            // Поле ext_id
            field =  dataMessage["ext_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptInfo.CgmOptRejectedOrders)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptInfo.CgmOptInterclInfo source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле vm_intercl
            field =  dataMessage["vm_intercl"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VmIntercl);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptInfo.CgmOptInterclInfo)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptInfo.CgmOptExpOrders source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле exporder_id
            field =  dataMessage["exporder_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExporderId);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле date
            field =  dataMessage["date"];
            shouldSet = field != null;
            if(shouldSet && (source.Date == null || source.Date == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Date);
            }

            // Поле amount_apply
            field =  dataMessage["amount_apply"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountApply);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptInfo.CgmOptExpOrders)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptInfo.CgmOptVcb source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле name
            field =  dataMessage["name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Name))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Name);
            }

            // Поле exec_type
            field =  dataMessage["exec_type"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ExecType))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ExecType);
            }

            // Поле curr
            field =  dataMessage["curr"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Curr))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Curr);
            }

            // Поле exch_pay
            field =  dataMessage["exch_pay"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExchPay);
            }

            // Поле exch_pay_scalped
            field =  dataMessage["exch_pay_scalped"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExchPayScalped);
            }

            // Поле clear_pay
            field =  dataMessage["clear_pay"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ClearPay);
            }

            // Поле clear_pay_scalped
            field =  dataMessage["clear_pay_scalped"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ClearPayScalped);
            }

            // Поле sell_fee
            field =  dataMessage["sell_fee"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SellFee);
            }

            // Поле buy_fee
            field =  dataMessage["buy_fee"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BuyFee);
            }

            // Поле trade_scheme
            field =  dataMessage["trade_scheme"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.TradeScheme))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.TradeScheme);
            }

            // Поле coeff_out
            field =  dataMessage["coeff_out"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CoeffOut);
            }

            // Поле is_spec
            field =  dataMessage["is_spec"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsSpec);
            }

            // Поле spec_spread
            field =  dataMessage["spec_spread"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SpecSpread);
            }

            // Поле min_vol
            field =  dataMessage["min_vol"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MinVol);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле rate_id
            field =  dataMessage["rate_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.RateId);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptInfo.CgmOptVcb)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptInfo.CgmOptSessContents source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле short_isin
            field =  dataMessage["short_isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ShortIsin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ShortIsin);
            }

            // Поле name
            field =  dataMessage["name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Name))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Name);
            }

            // Поле code_vcb
            field =  dataMessage["code_vcb"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeVcb))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeVcb);
            }

            // Поле fut_isin_id
            field =  dataMessage["fut_isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FutIsinId);
            }

            // Поле is_limited
            field =  dataMessage["is_limited"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsLimited);
            }

            // Поле limit_up
            field =  dataMessage["limit_up"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LimitUp);
            }

            // Поле limit_down
            field =  dataMessage["limit_down"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LimitDown);
            }

            // Поле old_kotir
            field =  dataMessage["old_kotir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OldKotir);
            }

            // Поле bgo_c
            field =  dataMessage["bgo_c"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BgoC);
            }

            // Поле bgo_nc
            field =  dataMessage["bgo_nc"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BgoNc);
            }

            // Поле europe
            field =  dataMessage["europe"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Europe);
            }

            // Поле put
            field =  dataMessage["put"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Put);
            }

            // Поле strike
            field =  dataMessage["strike"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Strike);
            }

            // Поле roundto
            field =  dataMessage["roundto"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Roundto);
            }

            // Поле min_step
            field =  dataMessage["min_step"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MinStep);
            }

            // Поле lot_volume
            field =  dataMessage["lot_volume"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LotVolume);
            }

            // Поле step_price
            field =  dataMessage["step_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StepPrice);
            }

            // Поле d_pg
            field =  dataMessage["d_pg"];
            shouldSet = field != null;
            if(shouldSet && (source.DPg == null || source.DPg == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DPg);
            }

            // Поле d_exec_beg
            field =  dataMessage["d_exec_beg"];
            shouldSet = field != null;
            if(shouldSet && (source.DExecBeg == null || source.DExecBeg == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DExecBeg);
            }

            // Поле d_exec_end
            field =  dataMessage["d_exec_end"];
            shouldSet = field != null;
            if(shouldSet && (source.DExecEnd == null || source.DExecEnd == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DExecEnd);
            }

            // Поле signs
            field =  dataMessage["signs"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Signs);
            }

            // Поле last_cl_quote
            field =  dataMessage["last_cl_quote"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LastClQuote);
            }

            // Поле bgo_buy
            field =  dataMessage["bgo_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BgoBuy);
            }

            // Поле base_isin_id
            field =  dataMessage["base_isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BaseIsinId);
            }

            // Поле d_start
            field =  dataMessage["d_start"];
            shouldSet = field != null;
            if(shouldSet && (source.DStart == null || source.DStart == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DStart);
            }

            // Поле exch_pay
            field =  dataMessage["exch_pay"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExchPay);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptInfo.CgmOptSessContents)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptInfo.CgmOptSessSettl source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле date_clr
            field =  dataMessage["date_clr"];
            shouldSet = field != null;
            if(shouldSet && (source.DateClr == null || source.DateClr == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateClr);
            }

            // Поле isin
            field =  dataMessage["isin"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Isin))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Isin);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле volat
            field =  dataMessage["volat"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Volat);
            }

            // Поле theor_price
            field =  dataMessage["theor_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TheorPrice);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptInfo.CgmOptSessSettl)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptInfo.CgmSysEvents source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле event_id
            field =  dataMessage["event_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле event_type
            field =  dataMessage["event_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventType);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptInfo.CgmSysEvents)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptTrades.CgmOrdersLog source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле id_ord
            field =  dataMessage["id_ord"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле amount_rest
            field =  dataMessage["amount_rest"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountRest);
            }

            // Поле id_deal
            field =  dataMessage["id_deal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDeal);
            }

            // Поле xstatus
            field =  dataMessage["xstatus"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Xstatus);
            }

            // Поле status
            field =  dataMessage["status"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Status);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

            // Поле action
            field =  dataMessage["action"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Action);
            }

            // Поле deal_price
            field =  dataMessage["deal_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DealPrice);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле login_from
            field =  dataMessage["login_from"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginFrom))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginFrom);
            }

            // Поле comment
            field =  dataMessage["comment"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Comment))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Comment);
            }

            // Поле hedge
            field =  dataMessage["hedge"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Hedge);
            }

            // Поле trust
            field =  dataMessage["trust"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Trust);
            }

            // Поле ext_id
            field =  dataMessage["ext_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId);
            }

            // Поле broker_to
            field =  dataMessage["broker_to"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerTo))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerTo);
            }

            // Поле broker_to_rts
            field =  dataMessage["broker_to_rts"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerToRts))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerToRts);
            }

            // Поле broker_from_rts
            field =  dataMessage["broker_from_rts"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerFromRts))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerFromRts);
            }

            // Поле date_exp
            field =  dataMessage["date_exp"];
            shouldSet = field != null;
            if(shouldSet && (source.DateExp == null || source.DateExp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateExp);
            }

            // Поле id_ord1
            field =  dataMessage["id_ord1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd1);
            }

            // Поле local_stamp
            field =  dataMessage["local_stamp"];
            shouldSet = field != null;
            if(shouldSet && (source.LocalStamp == null || source.LocalStamp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LocalStamp);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptTrades.CgmOrdersLog)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptTrades.CgmDeal source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле id_deal
            field =  dataMessage["id_deal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDeal);
            }

            // Поле id_deal_multileg
            field =  dataMessage["id_deal_multileg"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDealMultileg);
            }

            // Поле pos
            field =  dataMessage["pos"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Pos);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле id_ord_buy
            field =  dataMessage["id_ord_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrdBuy);
            }

            // Поле id_ord_sell
            field =  dataMessage["id_ord_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrdSell);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле nosystem
            field =  dataMessage["nosystem"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Nosystem);
            }

            // Поле xstatus_buy
            field =  dataMessage["xstatus_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.XstatusBuy);
            }

            // Поле xstatus_sell
            field =  dataMessage["xstatus_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.XstatusSell);
            }

            // Поле status_buy
            field =  dataMessage["status_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StatusBuy);
            }

            // Поле status_sell
            field =  dataMessage["status_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StatusSell);
            }

            // Поле ext_id_buy
            field =  dataMessage["ext_id_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtIdBuy);
            }

            // Поле ext_id_sell
            field =  dataMessage["ext_id_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtIdSell);
            }

            // Поле code_buy
            field =  dataMessage["code_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeBuy);
            }

            // Поле code_sell
            field =  dataMessage["code_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeSell);
            }

            // Поле comment_buy
            field =  dataMessage["comment_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CommentBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CommentBuy);
            }

            // Поле comment_sell
            field =  dataMessage["comment_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CommentSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CommentSell);
            }

            // Поле trust_buy
            field =  dataMessage["trust_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TrustBuy);
            }

            // Поле trust_sell
            field =  dataMessage["trust_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TrustSell);
            }

            // Поле hedge_buy
            field =  dataMessage["hedge_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.HedgeBuy);
            }

            // Поле hedge_sell
            field =  dataMessage["hedge_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.HedgeSell);
            }

            // Поле fee_buy
            field =  dataMessage["fee_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeBuy);
            }

            // Поле fee_sell
            field =  dataMessage["fee_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeSell);
            }

            // Поле login_buy
            field =  dataMessage["login_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginBuy);
            }

            // Поле login_sell
            field =  dataMessage["login_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginSell);
            }

            // Поле code_rts_buy
            field =  dataMessage["code_rts_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeRtsBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeRtsBuy);
            }

            // Поле code_rts_sell
            field =  dataMessage["code_rts_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeRtsSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeRtsSell);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptTrades.CgmDeal)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptTrades.CgmHeartbeat source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле server_time
            field =  dataMessage["server_time"];
            shouldSet = field != null;
            if(shouldSet && (source.ServerTime == null || source.ServerTime == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ServerTime);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptTrades.CgmHeartbeat)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptTrades.CgmSysEvents source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле event_id
            field =  dataMessage["event_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле event_type
            field =  dataMessage["event_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventType);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptTrades.CgmSysEvents)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OptTrades.CgmUserDeal source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле id_deal
            field =  dataMessage["id_deal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDeal);
            }

            // Поле id_deal_multileg
            field =  dataMessage["id_deal_multileg"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDealMultileg);
            }

            // Поле pos
            field =  dataMessage["pos"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Pos);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле id_ord_buy
            field =  dataMessage["id_ord_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrdBuy);
            }

            // Поле id_ord_sell
            field =  dataMessage["id_ord_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrdSell);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле nosystem
            field =  dataMessage["nosystem"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Nosystem);
            }

            // Поле xstatus_buy
            field =  dataMessage["xstatus_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.XstatusBuy);
            }

            // Поле xstatus_sell
            field =  dataMessage["xstatus_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.XstatusSell);
            }

            // Поле status_buy
            field =  dataMessage["status_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StatusBuy);
            }

            // Поле status_sell
            field =  dataMessage["status_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.StatusSell);
            }

            // Поле ext_id_buy
            field =  dataMessage["ext_id_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtIdBuy);
            }

            // Поле ext_id_sell
            field =  dataMessage["ext_id_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtIdSell);
            }

            // Поле code_buy
            field =  dataMessage["code_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeBuy);
            }

            // Поле code_sell
            field =  dataMessage["code_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeSell);
            }

            // Поле comment_buy
            field =  dataMessage["comment_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CommentBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CommentBuy);
            }

            // Поле comment_sell
            field =  dataMessage["comment_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CommentSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CommentSell);
            }

            // Поле trust_buy
            field =  dataMessage["trust_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TrustBuy);
            }

            // Поле trust_sell
            field =  dataMessage["trust_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TrustSell);
            }

            // Поле hedge_buy
            field =  dataMessage["hedge_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.HedgeBuy);
            }

            // Поле hedge_sell
            field =  dataMessage["hedge_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.HedgeSell);
            }

            // Поле fee_buy
            field =  dataMessage["fee_buy"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeBuy);
            }

            // Поле fee_sell
            field =  dataMessage["fee_sell"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.FeeSell);
            }

            // Поле login_buy
            field =  dataMessage["login_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginBuy);
            }

            // Поле login_sell
            field =  dataMessage["login_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginSell);
            }

            // Поле code_rts_buy
            field =  dataMessage["code_rts_buy"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeRtsBuy))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeRtsBuy);
            }

            // Поле code_rts_sell
            field =  dataMessage["code_rts_sell"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.CodeRtsSell))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.CodeRtsSell);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OptTrades.CgmUserDeal)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Ordbook.CgmOrders source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле id_ord
            field =  dataMessage["id_ord"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле xstatus
            field =  dataMessage["xstatus"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Xstatus);
            }

            // Поле status
            field =  dataMessage["status"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Status);
            }

            // Поле action
            field =  dataMessage["action"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Action);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле amount_rest
            field =  dataMessage["amount_rest"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountRest);
            }

            // Поле init_moment
            field =  dataMessage["init_moment"];
            shouldSet = field != null;
            if(shouldSet && (source.InitMoment == null || source.InitMoment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.InitMoment);
            }

            // Поле init_amount
            field =  dataMessage["init_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.InitAmount);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Ordbook.CgmOrders)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Ordbook.CgmInfo source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле infoID
            field =  dataMessage["infoID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.InfoId);
            }

            // Поле logRev
            field =  dataMessage["logRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LogRev);
            }

            // Поле lifeNum
            field =  dataMessage["lifeNum"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LifeNum);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Ordbook.CgmInfo)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Orderbook.CgmOrders source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле id_ord
            field =  dataMessage["id_ord"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле xstatus
            field =  dataMessage["xstatus"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Xstatus);
            }

            // Поле status
            field =  dataMessage["status"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Status);
            }

            // Поле action
            field =  dataMessage["action"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Action);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле amount_rest
            field =  dataMessage["amount_rest"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountRest);
            }

            // Поле comment
            field =  dataMessage["comment"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Comment))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Comment);
            }

            // Поле hedge
            field =  dataMessage["hedge"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Hedge);
            }

            // Поле trust
            field =  dataMessage["trust"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Trust);
            }

            // Поле ext_id
            field =  dataMessage["ext_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ExtId);
            }

            // Поле login_from
            field =  dataMessage["login_from"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.LoginFrom))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.LoginFrom);
            }

            // Поле broker_to
            field =  dataMessage["broker_to"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerTo))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerTo);
            }

            // Поле broker_to_rts
            field =  dataMessage["broker_to_rts"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerToRts))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerToRts);
            }

            // Поле date_exp
            field =  dataMessage["date_exp"];
            shouldSet = field != null;
            if(shouldSet && (source.DateExp == null || source.DateExp == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.DateExp);
            }

            // Поле id_ord1
            field =  dataMessage["id_ord1"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd1);
            }

            // Поле broker_from_rts
            field =  dataMessage["broker_from_rts"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.BrokerFromRts))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.BrokerFromRts);
            }

            // Поле init_moment
            field =  dataMessage["init_moment"];
            shouldSet = field != null;
            if(shouldSet && (source.InitMoment == null || source.InitMoment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.InitMoment);
            }

            // Поле init_amount
            field =  dataMessage["init_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.InitAmount);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Orderbook.CgmOrders)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Orderbook.CgmInfo source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле infoID
            field =  dataMessage["infoID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.InfoId);
            }

            // Поле logRev
            field =  dataMessage["logRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LogRev);
            }

            // Поле lifeNum
            field =  dataMessage["lifeNum"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LifeNum);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Orderbook.CgmInfo)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OrdersAggr.CgmOrdersAggr source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле volume
            field =  dataMessage["volume"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Volume);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OrdersAggr.CgmOrdersAggr)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OrdLogTrades.CgmOrdersLog source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле id_ord
            field =  dataMessage["id_ord"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле amount_rest
            field =  dataMessage["amount_rest"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountRest);
            }

            // Поле id_deal
            field =  dataMessage["id_deal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDeal);
            }

            // Поле xstatus
            field =  dataMessage["xstatus"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Xstatus);
            }

            // Поле status
            field =  dataMessage["status"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Status);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

            // Поле action
            field =  dataMessage["action"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Action);
            }

            // Поле deal_price
            field =  dataMessage["deal_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DealPrice);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OrdLogTrades.CgmOrdersLog)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OrdLogTrades.CgmMultilegOrdersLog source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле id_ord
            field =  dataMessage["id_ord"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdOrd);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле amount
            field =  dataMessage["amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Amount);
            }

            // Поле amount_rest
            field =  dataMessage["amount_rest"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.AmountRest);
            }

            // Поле id_deal
            field =  dataMessage["id_deal"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IdDeal);
            }

            // Поле xstatus
            field =  dataMessage["xstatus"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Xstatus);
            }

            // Поле status
            field =  dataMessage["status"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Status);
            }

            // Поле price
            field =  dataMessage["price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Price);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле dir
            field =  dataMessage["dir"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Dir);
            }

            // Поле action
            field =  dataMessage["action"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Action);
            }

            // Поле deal_price
            field =  dataMessage["deal_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DealPrice);
            }

            // Поле rate_price
            field =  dataMessage["rate_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.RatePrice);
            }

            // Поле swap_price
            field =  dataMessage["swap_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SwapPrice);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OrdLogTrades.CgmMultilegOrdersLog)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OrdLogTrades.CgmHeartbeat source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле server_time
            field =  dataMessage["server_time"];
            shouldSet = field != null;
            if(shouldSet && (source.ServerTime == null || source.ServerTime == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ServerTime);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OrdLogTrades.CgmHeartbeat)

        public static void CopyToDataMessage(this CGateAdapter.Messages.OrdLogTrades.CgmSysEvents source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле event_id
            field =  dataMessage["event_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле event_type
            field =  dataMessage["event_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventType);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.OrdLogTrades.CgmSysEvents)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Part.CgmPart source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле money_free
            field =  dataMessage["money_free"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MoneyFree);
            }

            // Поле money_blocked
            field =  dataMessage["money_blocked"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MoneyBlocked);
            }

            // Поле pledge_free
            field =  dataMessage["pledge_free"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PledgeFree);
            }

            // Поле pledge_blocked
            field =  dataMessage["pledge_blocked"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PledgeBlocked);
            }

            // Поле vm_reserve
            field =  dataMessage["vm_reserve"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VmReserve);
            }

            // Поле fee
            field =  dataMessage["fee"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Fee);
            }

            // Поле balance_money
            field =  dataMessage["balance_money"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BalanceMoney);
            }

            // Поле coeff_go
            field =  dataMessage["coeff_go"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CoeffGo);
            }

            // Поле coeff_liquidity
            field =  dataMessage["coeff_liquidity"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.CoeffLiquidity);
            }

            // Поле limits_set
            field =  dataMessage["limits_set"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LimitsSet);
            }

            // Поле money_old
            field =  dataMessage["money_old"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MoneyOld);
            }

            // Поле money_amount
            field =  dataMessage["money_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MoneyAmount);
            }

            // Поле pledge_old
            field =  dataMessage["pledge_old"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PledgeOld);
            }

            // Поле pledge_amount
            field =  dataMessage["pledge_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PledgeAmount);
            }

            // Поле money_pledge_amount
            field =  dataMessage["money_pledge_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MoneyPledgeAmount);
            }

            // Поле vm_intercl
            field =  dataMessage["vm_intercl"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VmIntercl);
            }

            // Поле is_auto_update_limit
            field =  dataMessage["is_auto_update_limit"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsAutoUpdateLimit);
            }

            // Поле no_fut_discount
            field =  dataMessage["no_fut_discount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.NoFutDiscount);
            }

            // Поле num_clr_2delivery
            field =  dataMessage["num_clr_2delivery"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.NumClr2delivery);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Part.CgmPart)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Part.CgmPartSa source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле settlement_account
            field =  dataMessage["settlement_account"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.SettlementAccount))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.SettlementAccount);
            }

            // Поле money_amount
            field =  dataMessage["money_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MoneyAmount);
            }

            // Поле money_free
            field =  dataMessage["money_free"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MoneyFree);
            }

            // Поле pledge_amount
            field =  dataMessage["pledge_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PledgeAmount);
            }

            // Поле money_pledge_amount
            field =  dataMessage["money_pledge_amount"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MoneyPledgeAmount);
            }

            // Поле liquidity_ratio
            field =  dataMessage["liquidity_ratio"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LiquidityRatio);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Part.CgmPartSa)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Part.CgmSysEvents source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле event_id
            field =  dataMessage["event_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле event_type
            field =  dataMessage["event_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventType);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Part.CgmSysEvents)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Pos.CgmPosition source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле pos
            field =  dataMessage["pos"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Pos);
            }

            // Поле buys_qty
            field =  dataMessage["buys_qty"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.BuysQty);
            }

            // Поле sells_qty
            field =  dataMessage["sells_qty"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SellsQty);
            }

            // Поле open_qty
            field =  dataMessage["open_qty"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OpenQty);
            }

            // Поле waprice
            field =  dataMessage["waprice"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Waprice);
            }

            // Поле net_volume_rur
            field =  dataMessage["net_volume_rur"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.NetVolumeRur);
            }

            // Поле last_deal_id
            field =  dataMessage["last_deal_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.LastDealId);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Pos.CgmPosition)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Pos.CgmSysEvents source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле event_id
            field =  dataMessage["event_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле event_type
            field =  dataMessage["event_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.EventType);
            }

            // Поле message
            field =  dataMessage["message"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Message))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Message);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Pos.CgmSysEvents)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Rates.CgmCurrOnline source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле rate_id
            field =  dataMessage["rate_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.RateId);
            }

            // Поле value
            field =  dataMessage["value"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Value);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Rates.CgmCurrOnline)

        public static void CopyToDataMessage(this CGateAdapter.Messages.RtsIndex.CgmRtsIndex source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле name
            field =  dataMessage["name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Name))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Name);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле value
            field =  dataMessage["value"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Value);
            }

            // Поле prev_close_value
            field =  dataMessage["prev_close_value"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PrevCloseValue);
            }

            // Поле open_value
            field =  dataMessage["open_value"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OpenValue);
            }

            // Поле max_value
            field =  dataMessage["max_value"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MaxValue);
            }

            // Поле min_value
            field =  dataMessage["min_value"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MinValue);
            }

            // Поле usd_rate
            field =  dataMessage["usd_rate"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.UsdRate);
            }

            // Поле cap
            field =  dataMessage["cap"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Cap);
            }

            // Поле volume
            field =  dataMessage["volume"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Volume);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.RtsIndex.CgmRtsIndex)

        public static void CopyToDataMessage(this CGateAdapter.Messages.RtsIndexlog.CgmRtsIndexLog source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле name
            field =  dataMessage["name"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.Name))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Name);
            }

            // Поле moment
            field =  dataMessage["moment"];
            shouldSet = field != null;
            if(shouldSet && (source.Moment == null || source.Moment == DateTime.MinValue))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.Moment);
            }

            // Поле value
            field =  dataMessage["value"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Value);
            }

            // Поле prev_close_value
            field =  dataMessage["prev_close_value"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.PrevCloseValue);
            }

            // Поле open_value
            field =  dataMessage["open_value"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.OpenValue);
            }

            // Поле max_value
            field =  dataMessage["max_value"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MaxValue);
            }

            // Поле min_value
            field =  dataMessage["min_value"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.MinValue);
            }

            // Поле usd_rate
            field =  dataMessage["usd_rate"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.UsdRate);
            }

            // Поле cap
            field =  dataMessage["cap"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Cap);
            }

            // Поле volume
            field =  dataMessage["volume"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Volume);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.RtsIndexlog.CgmRtsIndexLog)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Tnpenalty.CgmFeeAll source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле time
            field =  dataMessage["time"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Time);
            }

            // Поле p2login
            field =  dataMessage["p2login"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.P2login))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.P2login);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле points
            field =  dataMessage["points"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Points);
            }

            // Поле fee
            field =  dataMessage["fee"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Fee);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Tnpenalty.CgmFeeAll)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Tnpenalty.CgmFeeTn source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле time
            field =  dataMessage["time"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Time);
            }

            // Поле p2login
            field =  dataMessage["p2login"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.P2login))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.P2login);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле tn_type
            field =  dataMessage["tn_type"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TnType);
            }

            // Поле err_code
            field =  dataMessage["err_code"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ErrCode);
            }

            // Поле count
            field =  dataMessage["count"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Count);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Tnpenalty.CgmFeeTn)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Vm.CgmFutVm source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле vm
            field =  dataMessage["vm"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Vm);
            }

            // Поле vm_real
            field =  dataMessage["vm_real"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VmReal);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Vm.CgmFutVm)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Vm.CgmOptVm source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле client_code
            field =  dataMessage["client_code"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.ClientCode))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.ClientCode);
            }

            // Поле vm
            field =  dataMessage["vm"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Vm);
            }

            // Поле vm_real
            field =  dataMessage["vm_real"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VmReal);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Vm.CgmOptVm)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Vm.CgmFutVmSa source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле settlement_account
            field =  dataMessage["settlement_account"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.SettlementAccount))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.SettlementAccount);
            }

            // Поле vm
            field =  dataMessage["vm"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Vm);
            }

            // Поле vm_real
            field =  dataMessage["vm_real"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VmReal);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Vm.CgmFutVmSa)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Vm.CgmOptVmSa source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле settlement_account
            field =  dataMessage["settlement_account"];
            shouldSet = field != null;
            if(shouldSet && string.IsNullOrEmpty(source.SettlementAccount))
            {
                shouldSet = false;
            }
            if(shouldSet)
            {
                field.set(source.SettlementAccount);
            }

            // Поле vm
            field =  dataMessage["vm"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Vm);
            }

            // Поле vm_real
            field =  dataMessage["vm_real"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.VmReal);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Vm.CgmOptVmSa)

        public static void CopyToDataMessage(this CGateAdapter.Messages.Volat.CgmVolat source, DataMessage dataMessage)
        {
            Value field;
            bool shouldSet;

            // Поле replID
            field =  dataMessage["replID"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplId);
            }

            // Поле replRev
            field =  dataMessage["replRev"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplRev);
            }

            // Поле replAct
            field =  dataMessage["replAct"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.ReplAct);
            }

            // Поле isin_id
            field =  dataMessage["isin_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.IsinId);
            }

            // Поле sess_id
            field =  dataMessage["sess_id"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.SessId);
            }

            // Поле volat
            field =  dataMessage["volat"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.Volat);
            }

            // Поле theor_price
            field =  dataMessage["theor_price"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TheorPrice);
            }

            // Поле theor_price_limit
            field =  dataMessage["theor_price_limit"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.TheorPriceLimit);
            }

            // Поле up_prem
            field =  dataMessage["up_prem"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.UpPrem);
            }

            // Поле down_prem
            field =  dataMessage["down_prem"];
            shouldSet = field != null;
            if(shouldSet)
            {
                field.set(source.DownPrem);
            }

        } // CopyToDataMessage(CGateAdapter.Messages.Volat.CgmVolat)

    } // CGateMessageConverter
} // CGateAdapter.Messages