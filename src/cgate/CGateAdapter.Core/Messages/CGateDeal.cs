using System;
using System.Diagnostics;
using JetBrains.Annotations;
using ProtoBuf;

namespace CGateAdapter.Messages
{
    /// <summary>
    ///     Класс содержащий общие свойства от двух автогенерённых классов сделки по фьючерсам и по опционам - cgm_deal
    /// </summary>
    [PublicAPI, ProtoContract]
    public sealed class CGateDeal : CGateMessage
    {
        /// <summary>
        ///     Создать <see cref="CGateDeal"/> из <see cref="FutTrades.CgmDeal"/>
        /// </summary>
        public static CGateDeal Create(FutTrades.CgmDeal rec)
        {
            return new CGateDeal
            {
                UserId = rec.UserId,
                CodeSell = rec.CodeSell,
                CodeBuy = rec.CodeBuy,
                IsinId = rec.IsinId,
                Moment = rec.Moment,
                IdDeal = rec.IdDeal,
                Price = rec.Price,
                Amount = rec.Amount,
                IdOrdBuy = rec.IdOrdBuy,
                IdOrdSell = rec.IdOrdSell,
                CommentBuy = rec.CommentBuy,
                CommentSell = rec.CommentSell,
                StreamRegime = rec.StreamRegime,
                StreamName = rec.StreamName
            };
        }

        /// <summary>
        ///     Создать <see cref="CGateDeal"/> из <see cref="FutTrades.CgmUserDeal"/>
        /// </summary>
        public static CGateDeal Create(FutTrades.CgmUserDeal rec)
        {
            return new CGateDeal
            {
                UserId = rec.UserId,
                CodeSell = rec.CodeSell,
                CodeBuy = rec.CodeBuy,
                IsinId = rec.IsinId,
                Moment = rec.Moment,
                IdDeal = rec.IdDeal,
                Price = rec.Price,
                Amount = rec.Amount,
                IdOrdBuy = rec.IdOrdBuy,
                IdOrdSell = rec.IdOrdSell,
                CommentBuy = rec.CommentBuy,
                CommentSell = rec.CommentSell,
                StreamRegime = rec.StreamRegime,
                StreamName = rec.StreamName
            };
        }

        /// <summary>
        ///     Создать <see cref="CGateDeal"/> из <see cref="OptTrades.CgmDeal"/>
        /// </summary>
        public static CGateDeal Create(OptTrades.CgmDeal rec)
        {
            return new CGateDeal
            {
                UserId = rec.UserId,
                CodeSell = rec.CodeSell,
                CodeBuy = rec.CodeBuy,
                IsinId = rec.IsinId,
                Moment = rec.Moment,
                IdDeal = rec.IdDeal,
                Price = rec.Price,
                Amount = rec.Amount,
                IdOrdBuy = rec.IdOrdBuy,
                IdOrdSell = rec.IdOrdSell,
                CommentBuy = rec.CommentBuy,
                CommentSell = rec.CommentSell,
                StreamRegime = rec.StreamRegime,
                StreamName = rec.StreamName
            };
        }

        /// <summary>
        ///     Создать <see cref="CGateDeal"/> из <see cref="OptTrades.CgmUserDeal"/>
        /// </summary>
        public static CGateDeal Create(OptTrades.CgmUserDeal rec)
        {
            return new CGateDeal
            {
                UserId = rec.UserId,
                CodeSell = rec.CodeSell,
                CodeBuy = rec.CodeBuy,
                IsinId = rec.IsinId,
                Moment = rec.Moment,
                IdDeal = rec.IdDeal,
                Price = rec.Price,
                Amount = rec.Amount,
                IdOrdBuy = rec.IdOrdBuy,
                IdOrdSell = rec.IdOrdSell,
                CommentBuy = rec.CommentBuy,
                CommentSell = rec.CommentSell,
                StreamRegime = rec.StreamRegime,
                StreamName = rec.StreamName
            };
        }

        /// <inheritdoc />
        [ProtoIgnore]
        public override string MessageTypeName => "CGateDeal";

        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateMessageType MessageType => CGateMessageType.CGateDeal;

        /// <inheritdoc />
        [ProtoIgnore]
        public override CGateStreamType StreamType => CGateStreamType.Preudo;

        /// <summary>
        ///     Поле isin_id
        /// </summary>
        [ProtoMember(101)]
        public int IsinId { get; set; }

        /// <summary>
        ///     Поле moment
        /// </summary>
        [ProtoMember(102)]
        public DateTime Moment { get; set; }

        /// <summary>
        ///     Поле id_deal
        /// </summary>
        [ProtoMember(103)]
        public long IdDeal { get; set; }

        /// <summary>
        ///     Поле price
        /// </summary>
        [ProtoMember(104)]
        public double Price { get; set; }

        /// <summary>
        ///     Поле amount
        /// </summary>
        [ProtoMember(105)]
        public int Amount { get; set; }

        /// <summary>
        ///     Поле id_ord_buy
        /// </summary>
        [ProtoMember(106)]
        public long IdOrdBuy { get; set; }

        /// <summary>
        ///     Поле id_ord_sell
        /// </summary>
        [ProtoMember(107)]
        public long IdOrdSell { get; set; }

        /// <summary>
        ///     Поле comment_buy
        /// </summary>
        [ProtoMember(108)]
        public string CommentBuy { get; set; }

        /// <summary>
        ///     Поле comment_sell
        /// </summary>
        [ProtoMember(109)]
        public string CommentSell { get; set; }

        /// <summary>
        ///     Поле code_buy
        /// </summary>
        [ProtoMember(110)]
        public string CodeBuy { get; set; }

        /// <summary>
        ///     Поле code_sell
        /// </summary>
        [ProtoMember(111)]
        public string CodeSell { get; set; }

        /// <inheritdoc />
        public override void Accept(ICGateMessageVisitor visitor) => visitor.Handle(this);

        /// <inheritdoc />
        [DebuggerStepThrough]
        public override string ToString()
        {
            var builder = new CGateMessageTextBuilder(this);
            builder.Add("isin_id", IsinId);
            builder.Add("moment", Moment);
            builder.Add("id_deal", IdDeal);
            builder.Add("price", Price);
            builder.Add("amount", Amount);
            builder.Add("id_ord_buy", IdOrdBuy);
            builder.Add("id_ord_sell", IdOrdSell);
            builder.Add("comment_buy", CommentBuy);
            builder.Add("comment_sell", CommentSell);
            builder.Add("code_buy", CodeBuy);
            builder.Add("code_sell", CodeSell);

            return builder.ToString();
        }
    }
}