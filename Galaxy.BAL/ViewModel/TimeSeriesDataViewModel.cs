using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL.ViewModel
{
    [Serializable]
    public class TimeSeriesDataViewModel
    {
        public DateTime ReportedDataTime { get; set; }
        public double ReportedNetValue { get; set; }
        public double Yield { get; set; }
        public double AnnualYield { get; set; }
        public double RelativeYield { get; set; }
        public double Beta { get; set; }
        public double Volitility { get; set; }
        public double MaxWtihdraw { get; set; }
        public double SharpRatio { get; set; }
        public double RiskProfitRatio { get; set; }
        public double CoverionsWithIndex { get; set; }
        public double OrderOfSimliarStrategy { get; set; }
        public String Name { get; set; }

        public int Year
        {
            get { return ReportedDataTime.Year; }
        }

        public int Month
        {
            get { return ReportedDataTime.Month - 1; }
        }

        public int Day
        {
            get { return ReportedDataTime.Day; }
        }
    }

    public class MultipleTimeSeriesViewModel
    {
        private Dictionary<String, List<TimeSeriesDataViewModel>> dictionary = new Dictionary<string, List<TimeSeriesDataViewModel>>();

        public void addTimeSeries(String name, List<TimeSeriesDataViewModel> timeSeries)
        {
            dictionary.Add(name, timeSeries);
        }

        public Dictionary<String, List<TimeSeriesDataViewModel>> Dictionary
        {
            get { return dictionary; }
        }
    }
}
