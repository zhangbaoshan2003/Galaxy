using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL.ViewModel
{
    public enum SecurityTypeEnum 
    {
        Equity,
        Bond,
        Fund,
        Cash,
        Others
    }
    public class PorductHoldingViewModel
    {
        public SecurityTypeEnum SecurityType { get; set; }
        public String SecurityName { get; set; }
        public String SecurityCode { get; set; }
        public String IndustryName { get; set; }
        public DateTime UpdatedDate { get; set; }
        public double CostPerShare { get; set; }
        public double Quantity { get; set; }

        public double TotalCost
        {
            get { return CostPerShare * Quantity; }
        }

        public double TheLatest3MPnL { get; set; }

        public double PreClosePrice { get; set; }

        public double MarketValue { get; set; }

        public double PropotionOfTotalAssets { get; set; }
        public double PropotionOfTotalAssetsPrev { get; set; }
        public double PctChangeCompareToPrev
        {
            get 
            {
                return PropotionOfTotalAssetsPrev > 0.0 ? (PropotionOfTotalAssets - PropotionOfTotalAssetsPrev) / PropotionOfTotalAssets : 0.0;
            }
        }
        public double PropotionOfTotalEquity { get; set; }
        public double PropotionOfTotalSameEquity { get; set; }

        public double PnL
        {
            get { return MarketValue - TotalCost; }
        }

        public double PnLYield
        {
            get { return TotalCost > 0.0 ? PnL/TotalCost : 0.0; }
        }

        public String TradeState { get; set; }
        public DateTime LastTradeDate { get; set; }
        public double ClosePriceBeforeHalt { get; set; }
        public double IndustryIndexBeforeHalt { get; set; }
        public double IndustryIndexNow { get; set; }

        public double IndustryIndexChangePercent
        {
            get
            {
                return IndustryIndexBeforeHalt > 0.0
                    ? (IndustryIndexNow - IndustryIndexBeforeHalt)/IndustryIndexBeforeHalt
                    : 0.0;
            }
        }

        public double PricePerShareEstimated
        {
            get { return PreClosePrice*(1 + IndustryIndexChangePercent); }
        }

        public double MarketValueEstimated
        {
            get { return PricePerShareEstimated * Quantity; }
        }

        public double MarketValueDecrease { get { return MarketValueEstimated - MarketValue; }}
    }
}
