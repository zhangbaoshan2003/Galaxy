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
        public DateTime IssueDate { get; set; }
        public String ConsusityBank { get; set; }
        public String Administrator { get; set; }
        public String Consultant { get; set; }

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
    }
}
