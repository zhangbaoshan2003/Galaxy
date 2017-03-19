using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.BAL.Interface;
using Galaxy.BAL.ViewModel;
using System.Threading;

namespace Galaxy.BAL
{
    public class DummyProdcutInfoFetcher : IProdcutInfo
    {
        private static Random random = new Random();
        public ViewModel.MultipleTimeSeriesViewModel FetchProductNetValueDistViewModel(int productId)
        {
            Thread.Sleep(3000);
            MultipleTimeSeriesViewModel multipleTime = new MultipleTimeSeriesViewModel();


            List<TimeSeriesDataViewModel> models = new List<TimeSeriesDataViewModel>();
            for (int i = 0; i < 100; i++)
            {
                TimeSeriesDataViewModel model = new TimeSeriesDataViewModel();
                model.Name = "净值";
                model.ReportedDataTime = DateTime.Today.AddDays(i);
                model.ReportedNetValue = random.NextDouble();
                if (model.ReportedNetValue < 0.5)
                {
                    model.ReportedNetValue += 0.5;
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
                model.ReportedNetValue = random.NextDouble();
                if (model.ReportedNetValue < 0.5)
                {
                    model.ReportedNetValue += 0.5;
                }
                models.Add(model);
            }
            multipleTime.addTimeSeries(models[0].Name, models);
            return multipleTime;
        }

        public List<TimeSeriesDataViewModel> FetchPiggyBackDistViewModel(int productId)
        {
            Thread.Sleep(3000);
            List<TimeSeriesDataViewModel> models = new List<TimeSeriesDataViewModel>();
            for (int i = 0; i < 100; i++)
            {
                TimeSeriesDataViewModel model = new TimeSeriesDataViewModel();
                model.Name = "回撤率";
                model.ReportedDataTime = DateTime.Today.AddDays(i);
                model.ReportedNetValue = random.NextDouble();
                if (model.ReportedNetValue < 0.5)
                {
                    model.ReportedNetValue += 0.5;
                }
                models.Add(model);
            }

            return models;
        }


        public List<CategoryDataViewModel> FetchReturnDistViewModel(int productId, DateTime asOfDate)
        {
            Thread.Sleep(3000);

            List<CategoryDataViewModel> models = new List<CategoryDataViewModel>();
            int step = 0;
            for (double i= -0.1; i <= 0.1; i+=0.02 )
            {
                step++;
                
                CategoryDataViewModel model = new CategoryDataViewModel();
                model.Name = "收益分布";
                
                model.Label= i.ToString(); 
                if (step == 6) {
                    model.Label = "0";
                }
                model.Value = random.Next(100);
                models.Add(model);
            }

            return models;
        }


        public List<CategoryDataViewModel> FetchPnLDistViewModel(int productId, DateTime asOfDate)
        {
            Thread.Sleep(3000);
            List<CategoryDataViewModel> models = new List<CategoryDataViewModel>();
            CategoryDataViewModel vm = new CategoryDataViewModel();
            vm.Name = "负收益周数";
            vm.Label = "负收益周数";
            vm.Value = 100;
            models.Add(vm);

            vm = new CategoryDataViewModel();
            vm.Name = "正收益周数";
            vm.Label = "正收益周数";
            vm.Value = 40;
            models.Add(vm);

            return models;
        }


        public List<ProductBriefViewModel> FetchProducts()
        {
            Thread.Sleep(3000);
            List<ProductBriefViewModel> vms = new List<ProductBriefViewModel>();
            for (int i = 0; i < 100; i++)
            {
                ProductBriefViewModel vm = new ProductBriefViewModel();
                vm.ProductID = i;
                vm.Name = "元达信资本-履泰2期证券投资基金_"+i;
                vm.Administrator = "元达信资本";
                vm.Consultant = "天津履泰投资有限公司";
                vm.ConsusityBank = "中信建投证券";
                vm.IssueDate = DateTime.Today.AddMonths(-4);
                vms.Add(vm);
            }
            return vms;
        }


        public ProductBriefViewModel FetchProduct(int productId, DateTime asOfDate)
        {
            Thread.Sleep(3000);
            ProductBriefViewModel vm = new ProductBriefViewModel();
            vm.Name = "元达信资本-履泰2期证券投资基金_" + productId;
            vm.Administrator = "元达信资本";
            vm.Consultant = "天津履泰投资有限公司";
            vm.ProductType = "风险缓冲转管理型";
            vm.ConsusityBank = "中信建投证券";
            vm.Broker = "中信建投证券";
            vm.IssueDate = DateTime.Today.AddMonths(-productId);
            vm.LifeTime = 10;
            vm.InitialInvestment = 100;
            vm.ProportionOfConsultant = 0.15;
            vm.MinimumBuy = 1000000;
            vm.StepBuy = 100000;
            vm.ClosureTerm = "1年，第一个开放日之后，产品转为管理型 ";
            vm.OpenTerm = "封闭期后每季度X日开放，可申购赎回。（遇节假日顺延） ";
            vm.SubscriptionFee = 0;
            vm.RedemptionFee = 0.003;
            vm.BenchmarkToRewardConsultant = "封闭期届满后的第一个开放日，计提净值超过1以上的30%，转管理型后提取超额业绩的20%";
            vm.WayToCalcManagementFee = "按照前一日的计划资产净值，按日计提，按季支付 ";
            vm.WayToCollectReward = "逢开放日/产品终止清算日计提 ";
            vm.WayToReceiveBonus = "无";
            vm.WarningDesc = "0.90（产品净值触及预警线或在预警线以下，投顾指定投资者可追加增强资金，使产品净值回到预警线以上，若投顾方选择不追加增强资金或追加增强资金后产品净值仍低于0.95，管理人会将风险资产仓位控制在50%以下） ";
            vm.SecurityTypeToInvest = "主要投资于沪、深交易所A股股票、沪港通、基金、债券、新股申购（网下新股申购除外）、银行存款等。";
            vm.ConstrainsOfInvestment = @"•本基金投资于一家上市公司所发行的股票，不得超过该上市公司总股本的4.99%，同时不得超过该上市公司流通股本的10%；
•购买单一非创业板公司股票不超过基金总资产的40％；购买单一创业板公司股票不超过基金总资产的20％，所有投于创业板的股票不超过基金总资产的30%。
•不得投资于ST、*ST、S类股票；
•不得投资于权证。
•不得购买管理人自身发行的股票以及与管理人存在关联关系的上市公司股票。";

            for (int i = 0; i < 100; i++)
            {
                PorductHoldingViewModel holding = new PorductHoldingViewModel();
                holding.SecurityName = "浦发银行_" + i;
                holding.SecurityCode = "00000" + i;
                holding.IndustryName = "银行";
                holding.CostPerShare= random.Next(60);
                holding.Quantity = random.Next();
                holding.MarketValue = holding.TotalCost + random.Next(100);
                holding.PreClosePrice = random.Next(60);
                holding.PropotionOfTotalAssets = random.NextDouble();
                holding.PropotionOfTotalEquity = random.NextDouble();
                if (random.Next(1, 10) > 5)
                {
                    holding.TradeState = "正常交易";
                }
                else {
                    holding.TradeState = "停牌";
                }
               
                holding.LastTradeDate = DateTime.Today.AddDays(-7);

                holding.ClosePriceBeforeHalt = random.Next(60);
                holding.IndustryIndexBeforeHalt = random.Next(3000, 3300);
                holding.IndustryIndexNow = random.Next(3000, 3300);

                vm.Portfolio.Add(holding);
            }
            return vm;
        }
        public MultipleCategoriesViewModel FetchProductFundAssetDist(int productId, DateTime asOfDate)
        {
            Thread.Sleep(3000);
            MultipleCategoriesViewModel multipleCategories = new MultipleCategoriesViewModel();

            List<CategoryDataViewModel> models = new List<CategoryDataViewModel>();
            for (int i = 2010; i < 2017; i++)
            {
                CategoryDataViewModel model = new CategoryDataViewModel();
                model.Name = "股票";
                model.Label = i.ToString();
                model.Value = random.Next(5,10);
                models.Add(model);
            }
            multipleCategories.AddCategories(models[0].Name, models);

            models = new List<CategoryDataViewModel>();
            for (int i = 2010; i < 2017; i++)
            {
                CategoryDataViewModel model = new CategoryDataViewModel();
                model.Name = "债券";
                model.Label = i.ToString();
                model.Value = random.Next(5, 10);
                models.Add(model);
            }
            multipleCategories.AddCategories(models[0].Name, models);

            models = new List<CategoryDataViewModel>();
            for (int i = 2010; i < 2017; i++)
            {
                CategoryDataViewModel model = new CategoryDataViewModel();
                model.Name = "基金";
                model.Label = i.ToString();
                model.Value = random.Next(5, 10);
                models.Add(model);
            }
            multipleCategories.AddCategories(models[0].Name, models);

            return multipleCategories;
        }


        public MultipleCategoriesViewModel FetchProductEquityAssetDist(int productId, DateTime asOfDate)
        {
            Thread.Sleep(3000);
            MultipleCategoriesViewModel multipleCategories = new MultipleCategoriesViewModel();

            List<CategoryDataViewModel> models = new List<CategoryDataViewModel>();
            //DateTime date = DateTime.Today;
            for (DateTime date = new DateTime(2016,1,1); date <= DateTime.Today;date =date.AddMonths(2) )
            {
                CategoryDataViewModel model = new CategoryDataViewModel();
                model.Name = "能源";
                model.Label = date.ToShortDateString();
                model.Value = random.Next(5, 10);
                models.Add(model);
            }
            multipleCategories.AddCategories(models[0].Name, models);

            models = new List<CategoryDataViewModel>();
            for (DateTime date = new DateTime(2016, 1, 1); date <= DateTime.Today; date = date.AddMonths(2))
            {
                CategoryDataViewModel model = new CategoryDataViewModel();
                model.Name = "采矿业";
                model.Label = date.ToShortDateString();
                model.Value = random.Next(5, 10);
                models.Add(model);
            }
            multipleCategories.AddCategories(models[0].Name, models);

            models = new List<CategoryDataViewModel>();
            for (DateTime date = new DateTime(2016, 1, 1); date <= DateTime.Today; date = date.AddMonths(2))
            {
                CategoryDataViewModel model = new CategoryDataViewModel();
                model.Name = "贵金属";
                model.Label = date.ToShortDateString();
                model.Value = random.Next(5, 10);
                models.Add(model);
            }
            multipleCategories.AddCategories(models[0].Name, models);

            models = new List<CategoryDataViewModel>();
            for (DateTime date = new DateTime(2016, 1, 1); date <= DateTime.Today; date = date.AddMonths(2))
            {
                CategoryDataViewModel model = new CategoryDataViewModel();
                model.Name = "服务业";
                model.Label = date.ToShortDateString();
                model.Value = random.Next(5, 10);
                models.Add(model);
            }
            multipleCategories.AddCategories(models[0].Name, models);

            return multipleCategories;
        }


        public List<CategoryDataViewModel> FetchCurrentProductFundAssetDist(int productId, DateTime asOfDate)
        {
            Thread.Sleep(3000);
            List<CategoryDataViewModel> models = new List<CategoryDataViewModel>();
            CategoryDataViewModel vm = new CategoryDataViewModel();
            vm.Name = "股票";
            vm.Label = "股票";
            vm.Value = random.Next(1,10);
            models.Add(vm);

            vm = new CategoryDataViewModel();
            vm.Name = "债券";
            vm.Label = "债券";
            vm.Value = random.Next(1, 10);
            models.Add(vm);

            vm = new CategoryDataViewModel();
            vm.Name = "现金";
            vm.Label = "现金";
            vm.Value = random.Next(1, 10);
            models.Add(vm);

            vm = new CategoryDataViewModel();
            vm.Name = "其他资产";
            vm.Label = "其他资产";
            vm.Value = random.Next(1, 10);
            models.Add(vm);

            return models;
        }
    }
}
