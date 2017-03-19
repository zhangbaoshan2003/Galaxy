using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL.ViewModel
{
    public class ProductPerformanceIndexViewModel
    {
        public double Return { get; set; }
        public double AnnualReturn { get; set; }
        public double RelativeReturn { get; set; }
        public double Beta { get; set; }
        public double Volitility { get; set; }
        public double MaxWithdraw { get; set; }
        public double SharpRatio { get; set; }
        public double RiskToReturn { get; set; }
        public double ConvarianceOfIndex { get; set; }
        public int OrderOfSimilarStragtigy { get; set; }
        public DateTime AsOfDate { get; set; }
        public int ProductId { get; set; }
    }
}
