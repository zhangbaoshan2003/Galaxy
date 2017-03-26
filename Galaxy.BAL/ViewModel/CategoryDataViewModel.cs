using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL.ViewModel
{
    public class CategoryDataViewModel
    {
        public DateTime UpdatedDate { get; set; }
        public String Label { get; set; }
        public String Name { get; set; }
        public double Value { get; set; }
        public double PctOfTotal { get; set; }
        public double LastValue { get; set; }
        public double CompareToLastValuePct 
        { 
            get{ return LastValue > 0.0 ? (Value - LastValue) / LastValue : 0.0; } 
        }
    }

    public class MultipleCategoriesViewModel
    {
        private Dictionary<String, List<CategoryDataViewModel>> dictionary = new Dictionary<string, List<CategoryDataViewModel>>();

        public void AddCategories(String name, List<CategoryDataViewModel> timeSeries)
        {
            dictionary.Add(name, timeSeries);
        }

        public Dictionary<String, List<CategoryDataViewModel>> Dictionary
        {
            get { return dictionary; }
        }
    }
}
