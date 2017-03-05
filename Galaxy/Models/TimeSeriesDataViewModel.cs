using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Galaxy.Models
{
    [Serializable]
    public class TimeSeriesDataViewModel
    {
        public DateTime ReportedDataTime { get; set; }
        public double ReportedValue { get; set; }
        public String Name { get; set; }
    }
}