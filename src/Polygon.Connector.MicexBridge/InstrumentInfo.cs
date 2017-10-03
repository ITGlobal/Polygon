using System;

namespace Polygon.Connector.MicexBridge
{
    public class InstrumentInfo
    {
        private string mode;
        private string code;

        private decimal price;
        private decimal amount;

        private decimal bid;
        private decimal offer;
        private int amountBid;
        private int amountOffer;

        private DateTime expiration;
        private double priceStep;

        private int decimals;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public decimal Bid
        {
            get { return bid; }
            set { bid = value; }
        }

        public decimal Offer
        {
            get { return offer; }
            set { offer = value; }
        }

        public int AmountBid
        {
            get { return amountBid; }
            set { amountBid = value; }
        }

        public int AmountOffer
        {
            get { return amountOffer; }
            set { amountOffer = value; }
        }

        public DateTime Expiration
        {
            get { return expiration; }
            set { expiration = value; }
        }

        public double PriceStep
        {
            get { return priceStep; }
            set { priceStep = value; }
        }

        public int Decimals
        {
            get { return decimals; }
            set { decimals = value; }
        }

        public string Mode
        {
            get { return mode; }
            set { mode = value; }
        }

    }
}