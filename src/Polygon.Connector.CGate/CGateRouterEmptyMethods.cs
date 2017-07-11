using CGateAdapter.Messages;
using CGateAdapter.Messages.Clr;
using CGateAdapter.Messages.FortsMessages;
using CGateAdapter.Messages.FutCommon;
using CGateAdapter.Messages.FutInfo;
using CGateAdapter.Messages.FutTrades;
using CGateAdapter.Messages.Info;
using CGateAdapter.Messages.MiscInfo;
using CGateAdapter.Messages.Mm;
using CGateAdapter.Messages.OptInfo;
using CGateAdapter.Messages.Ordbook;
using CGateAdapter.Messages.OrdersAggr;
using CGateAdapter.Messages.Part;
using CGateAdapter.Messages.Rates;
using CGateAdapter.Messages.RtsIndex;
using CGateAdapter.Messages.RtsIndexlog;
using CGateAdapter.Messages.Tnpenalty;
using CGateAdapter.Messages.Vm;
using CGateAdapter.Messages.Volat;
using CgmFutSessSettl = CGateAdapter.Messages.Clr.CgmFutSessSettl;
using CgmHeartbeat = CGateAdapter.Messages.OptTrades.CgmHeartbeat;
using CgmMultilegOrdersLog = CGateAdapter.Messages.FutTrades.CgmMultilegOrdersLog;
using CgmOptSessSettl = CGateAdapter.Messages.Clr.CgmOptSessSettl;
using CgmOrdersLog = CGateAdapter.Messages.OrdLogTrades.CgmOrdersLog;
using CgmSysEvents = CGateAdapter.Messages.Clr.CgmSysEvents;

namespace Polygon.Connector.CGate
{
    partial class CGateRouter
    {
        /// <summary>
        /// Размещённые ниже методы ICGateMessageVisitor.Handle не реализуются за ненадобностью
        /// </summary>
        #region Not implemented
        
        public void Handle(CGateDelOrderReply message)
        {
            
        }

        public void Handle(CGateDeal message)
        {
            
        }

        public void Handle(CGateDataEnd message)
        {
            
        }

        public void Handle(CGateDataBegin message)
        {
            
        }

        public void Handle(CGateAddOrderReply message)
        {
            
        }

        public void Handle(CGConnectionStateChange message)
        {
            
        }

        public void Handle(CGateClearTableMessage message)
        {
            
        }

        public void Handle(CgmMoneyClearing message)
        {
            
        }

        public void Handle(CgmMoneyClearingSa message)
        {
            
        }

        public void Handle(CgmClrRate message)
        {
            
        }

        public void Handle(CgmFutPos message)
        {
            
        }

        public void Handle(CgmOptPos message)
        {
            
        }

        public void Handle(CgmFutPosSa message)
        {
            
        }

        public void Handle(CgmOptPosSa message)
        {
            
        }

        public void Handle(CgmFutSessSettl message)
        {
            
        }

        public void Handle(CgmOptSessSettl message)
        {
            
        }

        public void Handle(CgmPledgeDetails message)
        {
            
        }

        public void Handle(CgmSysEvents message)
        {
            
        }

        public void Handle(CgmFutAddOrder message)
        {
            
        }

        public void Handle(CgmFutAddMultiLegOrder message)
        {
            
        }

        public void Handle(CgmFortsMsg129 message)
        {
            
        }

        public void Handle(CgmFutDelOrder message)
        {
            
        }

        public void Handle(CgmFutDelUserOrders message)
        {
            
        }

        public void Handle(CgmFortsMsg103 message)
        {
            
        }

        public void Handle(CgmFutMoveOrder message)
        {
            
        }

        public void Handle(CgmOptAddOrder message)
        {
            
        }

        public void Handle(CgmOptDelUserOrders message)
        {
            
        }

        public void Handle(CgmFortsMsg111 message)
        {
            
        }

        public void Handle(CgmOptMoveOrder message)
        {
            
        }

        public void Handle(CgmFortsMsg113 message)
        {
            
        }

        public void Handle(CgmFutChangeClientMoney message)
        {
            
        }

        public void Handle(CgmFortsMsg104 message)
        {
            
        }

        public void Handle(CgmFutChangeBfmoney message)
        {
            
        }

        public void Handle(CgmFortsMsg107 message)
        {
            
        }

        public void Handle(CgmOptChangeExpiration message)
        {
            
        }

        public void Handle(CgmFortsMsg112 message)
        {
            
        }

        public void Handle(CgmFutChangeClientProhibit message)
        {
            
        }

        public void Handle(CgmFortsMsg115 message)
        {
            
        }

        public void Handle(CgmOptChangeClientProhibit message)
        {
            
        }

        public void Handle(CgmFortsMsg117 message)
        {
            
        }

        public void Handle(CgmFutExchangeBfmoney message)
        {
            
        }

        public void Handle(CgmFortsMsg130 message)
        {
            
        }

        public void Handle(CgmOptRecalcCs message)
        {
            
        }

        public void Handle(CgmFortsMsg132 message)
        {
            
        }

        public void Handle(CgmFutTransferClientPosition message)
        {
            
        }

        public void Handle(CgmFortsMsg137 message)
        {
            
        }

        public void Handle(CgmOptTransferClientPosition message)
        {
            
        }

        public void Handle(CgmFortsMsg138 message)
        {
            
        }

        public void Handle(CgmOptChangeRiskParameters message)
        {
            
        }

        public void Handle(CgmFortsMsg140 message)
        {
            
        }

        public void Handle(CgmFutTransferRisk message)
        {
            
        }

        public void Handle(CgmFortsMsg139 message)
        {
            
        }

        public void Handle(CgmCodheartbeat message)
        {
            
        }

        public void Handle(CgmCommon message)
        {
            
        }

        public void Handle(CgmDeliveryReport message)
        {
            
        }

        public void Handle(CgmFutRejectedOrders message)
        {
            
        }

        public void Handle(CgmFutInterclInfo message)
        {
            
        }

        public void Handle(CgmFutBondRegistry message)
        {
            
        }

        public void Handle(CgmFutBondIsin message)
        {
            
        }

        public void Handle(CgmFutBondNkd message)
        {
            
        }

        public void Handle(CgmFutBondNominal message)
        {
            
        }

        public void Handle(CgmUsdOnline message)
        {
            
        }

        public void Handle(CgmFutVcb message)
        {
            
        }

        public void Handle(CgmMultilegDict message)
        {
            
        }

        public void Handle(CgmFutSessContents message)
        {
            
        }

        public void Handle(CgmFutInstruments message)
        {
            
        }

        public void Handle(CgmDiler message)
        {
            
        }

        public void Handle(CgmInvestr message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.FutInfo.CgmFutSessSettl message)
        {
            
        }

        public void Handle(CgmSysMessages message)
        {
            
        }

        public void Handle(CgmFutSettlementAccount message)
        {
            
        }

        public void Handle(CgmFutMarginType message)
        {
            
        }

        public void Handle(CgmProhibition message)
        {
            
        }

        public void Handle(CgmRates message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.FutInfo.CgmSysEvents message)
        {
            
        }

        public void Handle(CgmMultilegOrdersLog message)
        {
            
        }

        public void Handle(CgmMultilegDeal message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.FutTrades.CgmSysEvents message)
        {
            
        }

        public void Handle(CgmDeal message)
        {
            
        }

        public void Handle(CgmUserMultilegDeal message)
        {
            
        }

        public void Handle(CgmBaseContractsParams message)
        {
            
        }

        public void Handle(CgmFuturesParams message)
        {
            
        }

        public void Handle(CgmVirtualFuturesParams message)
        {
            
        }

        public void Handle(CgmOptionsParams message)
        {
            
        }

        public void Handle(CgmBrokerParams message)
        {
            
        }

        public void Handle(CgmClientParams message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.Info.CgmSysEvents message)
        {
            
        }

        public void Handle(CgmVolatCoeff message)
        {
            
        }

        public void Handle(CgmFutMmInfo message)
        {
            
        }

        public void Handle(CgmOptMmInfo message)
        {
            
        }

        public void Handle(CgmCsMmRule message)
        {
            
        }

        public void Handle(CgmMmAgreementFilter message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.OptCommon.CgmCommon message)
        {
            
        }

        public void Handle(CgmOptRejectedOrders message)
        {
            
        }

        public void Handle(CgmOptInterclInfo message)
        {
            
        }

        public void Handle(CgmOptExpOrders message)
        {
            
        }

        public void Handle(CgmOptVcb message)
        {
            
        }

        public void Handle(CgmOptSessContents message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.OptInfo.CgmOptSessSettl message)
        {
            
        }

        public void Handle(CgmHeartbeat message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.OptTrades.CgmSysEvents message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.OptTrades.CgmDeal message)
        {
            
        }

        public void Handle(CgmOrders message)
        {
            
        }

        public void Handle(CgmInfo message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.Orderbook.CgmOrders message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.Orderbook.CgmInfo message)
        {
            
        }

        public void Handle(CgmOrdersAggr message)
        {
            
        }

        public void Handle(CgmOrdersLog message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.OrdLogTrades.CgmMultilegOrdersLog message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.OrdLogTrades.CgmHeartbeat message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.OrdLogTrades.CgmSysEvents message)
        {
            
        }

        public void Handle(CgmPartSa message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.Part.CgmSysEvents message)
        {
            
        }

        public void Handle(CGateAdapter.Messages.Pos.CgmSysEvents message)
        {
            
        }

        public void Handle(CgmCurrOnline message)
        {
            
        }

        public void Handle(CgmRtsIndex message)
        {
            
        }

        public void Handle(CgmRtsIndexLog message)
        {
            
        }

        public void Handle(CgmFeeAll message)
        {
            
        }

        public void Handle(CgmFeeTn message)
        {
            
        }

        public void Handle(CgmFutVm message)
        {
            
        }

        public void Handle(CgmOptVm message)
        {
            
        }

        public void Handle(CgmFutVmSa message)
        {
            
        }

        public void Handle(CgmOptVmSa message)
        {
            
        }

        public void Handle(CgmVolat message)
        {
            
        }

        #endregion

    }
}

