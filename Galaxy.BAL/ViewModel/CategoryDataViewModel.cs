using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL.ViewModel
{
    public class CategoryDataViewModel
    {
        public String Label { get; set; }
        public String Name { get; set; }
        public double Value { get; set; }
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
