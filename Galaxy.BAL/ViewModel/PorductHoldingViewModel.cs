using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL.ViewModel
{
    public class PorductHoldingViewModel
    {
        public String SecurityName { get; set; }

        public String IndustryName { get; set; }
        public double Cost { get; set; }
        public double Quantity { get; set; }

        public double Amount
        {
            get { return Cost * Quantity; }
        }

        public double PreClosePrice { get; set; }

        public double MarketValue
        {
            get { return PreClosePrice * Quantity; }
        }

        public double PropotionOfTotalAssets { get; set; }
        public double PropotionOfTotalEquity { get; set; }
        public double Pnl { get; set; }
        public double PnLYield { get; set; }
        public String TradeState { get; set; }
        public DateTime LastTradeDate { get; set; }
        public double ClosePriceBeforeHalt { get; set; }
        public double IndustryIndexBeforeHalt { get; set; }
        public double IndustryIndexNow { get; set; }
        public double PercentageOfPnL { get; set; }
        public double MarketValueEstimated { get; set; }
        public double MarketValueDecrease { get; set; }
    }
}
