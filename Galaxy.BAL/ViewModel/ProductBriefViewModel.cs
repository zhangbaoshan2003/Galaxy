using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL.ViewModel
{
    public class ProductBriefViewModel
    {
        public ProductBriefViewModel() { }
        public int ProductID { get; set; }
        public String Name { get; set; }

        public String Caption { get; set; }
        public DateTime IssueDate { get; set; }

        public String IssueDateStr { get { return IssueDate.ToShortDateString(); } }
        public String ConsusityBank { get; set; }
        public String Administrator { get; set; }
        public String Consultant { get; set; }

        public String Department { get; set; }

        public int? ProductTypeID { get; set; }

        public String ProductType { get; set; }

        public String CustodyBank { get; set; }
        public String Broker { get; set; }

        public int? LifeTime { get; set; }

        public decimal? InitialInvestment { get; set; }

        public double? ProportionOfConsultant { get; set; }

        public decimal? MinimumBuy { get; set; }
        public int? StepBuy { get; set; }
        public String ClosureTerm { get; set; }

        public String OpenTerm { get; set; }

        public double? SubscriptionFee { get; set; }

        public double? RedemptionFee { get; set; }

        public double? ManagementFee { get; set; }

        public String WayToCalcManagementFee { get; set; }

        public String BenchmarkToRewardConsultant { get; set; }

        public String WayToCollectReward { get; set; }

        public String WayToReceiveBonus { get; set; }

        public double? ThresholdOfWarning { get; set; }
        public String WarningDesc { get; set; }
        public double? TresholdOfStopLoss { get; set; }
        public String StopLossDesc { get; set; }
        public String SecurityTypeToInvest { get; set; }

        public String ConstrainsOfInvestment { get; set; }

        public int? StrategyTypeID { get; set; }

        public String StrategyType { get; set; }

        public String Mem { get; set; }

        #region market value  
        public double EquityMarketValue { get; set; }
        public double MoneyLeft { get; set; }
        public double AssetMarketValue { get; set; }
        public double ProductQuantity { get; set; }
        public double PropertionOfEquity { get; set; }
        public double PropertionOfEquityPrevDay { get; set; }

        public double ChangeOfEuqityPropertion
        {
            get
            {
                return PropertionOfEquityPrevDay > 0.0
                    ? (PropertionOfEquity - PropertionOfEquityPrevDay)/PropertionOfEquityPrevDay
                    : 0.0;
            }
        }

        public double NetValueEstimated
        {
            get
            {
                return ProductQuantity>0.0?AssetMarketValue/ProductQuantity:
                0.0;
            }
        }

        public double NetValueAnanced { get; set; }
        public double ChangeOfNetValue { get { return NetValueEstimated - NetValueAnanced; }}
        public double NetValueEstimatedPreDay { get; set; }
        public double ChangeOfNetValueEstimated
        {
            get
            {
                return NetValueEstimatedPreDay > 0.0
                    ? (NetValueEstimated - NetValueEstimatedPreDay)/NetValueEstimatedPreDay
                    : 0.0;
            }
        }
        public double TreshholdOfWarning { get; set; }
        public double TreshholdOfStopLoss { get; set; }

        #endregion

        private List<PorductHoldingViewModel> _portfolio=new List<PorductHoldingViewModel>();
        public List<PorductHoldingViewModel> Portfolio { get { return _portfolio; }}
    }
}
