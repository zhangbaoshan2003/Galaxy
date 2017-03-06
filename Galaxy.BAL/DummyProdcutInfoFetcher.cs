using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.BAL.Interface;
using Galaxy.BAL.ViewModel;

namespace Galaxy.BAL
{
    public class DummyProdcutInfoFetcher : IProdcutInfo
    {
        Random random = new Random(100);
        public ViewModel.MultipleTimeSeriesViewModel FetchProductNetValueDistViewModel(int productId)
        {
            MultipleTimeSeriesViewModel multipleTime = new MultipleTimeSeriesViewModel();


            List<TimeSeriesDataViewModel> models = new List<TimeSeriesDataViewModel>();
            for (int i = 0; i < 100; i++)
            {
                TimeSeriesDataViewModel model = new TimeSeriesDataViewModel();
                model.Name = "净值";
                model.ReportedDataTime = DateTime.Today.AddDays(i);
                model.ReportedValue = random.NextDouble();
                if (model.ReportedValue < 0.5)
                {
                    model.ReportedValue += 0.5;
                }
                models.Add(model);
            }
            multipleTime.addTimeSeries(models[0].Name, models);

            models = new List<TimeSeriesDataViewModel>();
            for (int i = 0; i < 100; i++)
            {
                TimeSeriesDataViewModel model = new TimeSeriesDataViewModel();
                model.Name = "沪深300指数";
                model.ReportedDataTime = DateTime.Today.AddDays(i);
                model.ReportedValue = random.NextDouble();
                if (model.ReportedValue < 0.5)
                {
                    model.ReportedValue += 0.5;
                }
                models.Add(model);
            }
            multipleTime.addTimeSeries(models[0].Name, models);
            return multipleTime;
        }


        public List<TimeSeriesDataViewModel> FetchPiggyBackDistViewModel(int productId)
        {
            List<TimeSeriesDataViewModel> models = new List<TimeSeriesDataViewModel>();
            for (int i = 0; i < 100; i++)
            {
                TimeSeriesDataViewModel model = new TimeSeriesDataViewModel();
                model.Name = "回撤率";
                model.ReportedDataTime = DateTime.Today.AddDays(i);
                model.ReportedValue = random.NextDouble();
                if (model.ReportedValue < 0.5)
                {
                    model.ReportedValue += 0.5;
                }
                models.Add(model);
            }

            return models;
        }


        public List<CategoryDataViewModel> FetchReturnDistViewModel(int productId)
        {
            List<CategoryDataViewModel> models = new List<CategoryDataViewModel>();
            for (double i= -0.1; i < 0.1; i+=0.02 )
            {
                CategoryDataViewModel model = new CategoryDataViewModel();
                model.Name = "收益分布";
                model.Label= i.ToString();
                model.Value = random.Next();
                models.Add(model);
            }

            return models;
        }
    }
}
