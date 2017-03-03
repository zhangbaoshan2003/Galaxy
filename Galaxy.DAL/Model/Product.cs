using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Galaxy.DAL.Model
{
    public class Product
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        public int? ConsultentID { get; set; }

        public virtual Consultant Consultant { get; set; }

        public int? AdministratorID { get; set; }

        public virtual Administrator Administrator { get; set; }
        
        public int? ProductTypeID{ get; set; }

        public virtual ProductType ProductType { get; set; }

        public String CustodyBank { get; set; }
        public String Broker { get; set; }

        public int? LifeTime { get; set; }

        public decimal? InitialInvestment { get; set; }

        public double? ProportionOfConsultant { get; set; }

        public decimal? MinimumBuy { get; set; }
        public int? StepBuy { get; set; }
        public String ClosureTerm { get;set; }

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

        public virtual StrategyType StrategyType { get; set; }

        public String Mem { get; set; }


    }
}
