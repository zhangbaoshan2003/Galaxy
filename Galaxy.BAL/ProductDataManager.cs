using Galaxy.BAL.Exceptions;
using Galaxy.BAL.Interface;
using Galaxy.BAL.ViewModel;
using Galaxy.DAL2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Galaxy.BAL
{
    public class ProductDataManager : IProdcutInfo
    {

        public ViewModel.MultipleTimeSeriesViewModel FetchProductNetValueDistViewModel(int productId)
        {
            MultipleTimeSeriesViewModel multipleTime = new MultipleTimeSeriesViewModel();
            List<TimeSeriesDataViewModel> netValueModels= new List<TimeSeriesDataViewModel>();
            List<TimeSeriesDataViewModel> indexModels = new List<TimeSeriesDataViewModel>();

            String sqlText = String.Format(@"WITH A AS
            (
	            SELECT H.c_nav,H.l_fund_id,l_date FROM cydb..his_fundasset H WHERE H.l_fund_id={0} AND c_nav>0.0 
            ),
            I AS (
	            SELECT Pclose,WindCode,TradingDay,(SELECT TOP(1) Pclose FROM cydb..AIndexEODPrices WHERE WindCode='000300.SH' AND TradingDay>=(SELECT MIN(l_date) FROM A)) AS BenchMark
	             FROM cydb..AIndexEODPrices WHERE WindCode='000300.SH' AND TradingDay>=(SELECT MIN(l_date) FROM A)
            )
            SELECT A.l_fund_id,A.l_date,I.TradingDay,Pclose/BenchMark AS Index300,A.c_nav FROM I JOIN A ON A.l_date=I.TradingDay",productId);

            using (SQLSession session = new SQLSession("GalaxyDB"))
            {
                DataTable table = session.SQLQuery(sqlText);
                table.AsEnumerable().ToList().ForEach(tr =>
                {
                    TimeSeriesDataViewModel model = new TimeSeriesDataViewModel();
                    model.Name = "沪深300指数";
                    model.ReportedDataTime = tr.Field<DateTime>("TradingDay");
                    model.ReportedNetValue = Convert.ToDouble(tr.Field<decimal>("Index300"));
                    indexModels.Add(model);

                    model = new TimeSeriesDataViewModel();
                    model.Name = "净值";
                    model.ReportedDataTime = tr.Field<DateTime>("l_date");
                    model.ReportedNetValue = Convert.ToDouble(tr.Field<decimal>("c_nav"));
                    netValueModels.Add(model);
                });
            }
            multipleTime.addTimeSeries("沪深300指数", indexModels);
            multipleTime.addTimeSeries("净值", netValueModels);
            return multipleTime;
        }

        public ViewModel.MultipleCategoriesViewModel FetchProductFundAssetDist(int productId, DateTime asOfDate)
        {
            MultipleCategoriesViewModel multipleCategories = new MultipleCategoriesViewModel();

            List<CategoryDataViewModel> modelsEquity = new List<CategoryDataViewModel>();
            List<CategoryDataViewModel> modelsBond = new List<CategoryDataViewModel>();
            List<CategoryDataViewModel> modelsFund = new List<CategoryDataViewModel>();
            List<CategoryDataViewModel> modelsOthers = new List<CategoryDataViewModel>();
            List<CategoryDataViewModel> modelsCash = new List<CategoryDataViewModel>();
            String sqlText =
                String.Format(@"SELECT l_fund_id, YEAR(l_date) AS l_year,MONTH(l_date) AS l_month, SUM(en_stock_asset) AS en_stock_asset, SUM(en_bond_asset) AS en_bond_asset,SUM(en_fund_asset) AS en_fund_asset,
                SUM(en_hg_asset) AS en_hg_asset,SUM(en_futures_asset) as en_futures_asset,SUM(en_other_asset) AS en_other_asset ,SUM(en_current_cash) AS en_current_cash FROM cydb..his_fundasset H
                WHERE H.l_fund_id={0} GROUP BY l_fund_id,YEAR(l_date),MONTH(l_date) ORDER BY YEAR(l_date),MONTH(l_date) ", productId);
            using (SQLSession session = new SQLSession("GalaxyDB"))
            {
                DataTable table = session.SQLQuery(sqlText);
                table.AsEnumerable().ToList().ForEach(tr =>
                {
                    CategoryDataViewModel model = new CategoryDataViewModel();
                    model.Name = "股票";
                    model.Label = tr.Field<int>("l_year").ToString() + "-" + tr.Field<int>("l_month").ToString();
                    model.Value = Convert.ToDouble(tr.Field<Decimal>("en_stock_asset"));
                    modelsEquity.Add(model);

                    model = new CategoryDataViewModel();
                    model.Name = "债券";
                    model.Label = tr.Field<int>("l_year").ToString() + "-" + tr.Field<int>("l_month").ToString();
                    model.Value = Convert.ToDouble(tr.Field<Decimal>("en_bond_asset"));
                    modelsBond.Add(model);

                    model = new CategoryDataViewModel();
                    model.Name = "基金";
                    model.Label = tr.Field<int>("l_year").ToString() + "-" + tr.Field<int>("l_month").ToString();
                    model.Value = Convert.ToDouble(tr.Field<Decimal>("en_fund_asset"));
                    modelsFund.Add(model);

                    model = new CategoryDataViewModel();
                    model.Name = "其它";
                    model.Label = tr.Field<int>("l_year").ToString() + "-" + tr.Field<int>("l_month").ToString();
                    model.Value = Convert.ToDouble(tr.Field<Decimal>("en_other_asset"));
                    modelsOthers.Add(model);

                    model = new CategoryDataViewModel();
                    model.Name = "现金";
                    model.Label = tr.Field<int>("l_year").ToString() + "-" + tr.Field<int>("l_month").ToString();
                    model.Value = Convert.ToDouble(tr.Field<Decimal>("en_current_cash"));
                    modelsCash.Add(model);
                   
                });
            }

            multipleCategories.Dictionary.Add("股票",modelsEquity);
            multipleCategories.Dictionary.Add("债券", modelsBond);
            multipleCategories.Dictionary.Add("基金", modelsFund);
            multipleCategories.Dictionary.Add("现金", modelsCash);
            multipleCategories.Dictionary.Add("其它", modelsOthers);

            return multipleCategories;
        }

        public List<ViewModel.CategoryDataViewModel> FetchCurrentProductFundAssetDist(int productId, DateTime asOfDate)
        {
            List<CategoryDataViewModel> models = new List<CategoryDataViewModel>();

            String sqlText = @"  WITH Pre AS (
	                    SELECT SUM(volume) AS vol ,assetclass_name,l_date,l_fund_id,ROW_NUMBER() OVER(PARTITION BY assetclass_name ORDER BY l_date) AS rn
                      FROM [cydb].[dbo].[FundHoldings] WHERE l_fund_id={0} GROUP BY assetclass_name,l_date,l_fund_id  
                    ),
                    Cur AS (
                    SELECT SUM(volume) AS vol ,assetclass_name,l_date,l_fund_id,ROW_NUMBER() OVER(PARTITION BY assetclass_name ORDER BY l_date) AS rn
                      FROM [cydb].[dbo].[FundHoldings] WHERE l_fund_id={0} GROUP BY assetclass_name,l_date,l_fund_id 
                    )
                    SELECT P.vol AS pre_vol,C.vol AS cur_vol,P.rn,C.rn,C.l_date,C.assetclass_name FROM Pre P JOIN Cur C ON P.l_fund_id=C.l_fund_id AND P.assetclass_name=C.assetclass_name AND P.rn=C.rn-1
                    WHERE C.l_date=(SELECT MAX(l_date) FROM cydb..FundHoldings WHERE l_fund_id={0} AND l_date<='{1}' AND Amount IS NOT NULL)";
            sqlText = String.Format(sqlText, productId, asOfDate.ToString("yyyy-MM-dd"));
            using (SQLSession session = new SQLSession("GalaxyDB"))
            {
                DataTable table = session.SQLQuery(sqlText);

                table.AsEnumerable().ToList().ForEach(tr =>
                {
                    CategoryDataViewModel vm = new CategoryDataViewModel();
                    vm.Label = tr.Field<String>("assetclass_name");
                    vm.Name = vm.Label;
                    vm.Value = Convert.ToDouble(tr.Field<Decimal>("cur_vol"));
                    vm.LastValue = Convert.ToDouble(tr.Field<Decimal>("pre_vol"));
                    if (vm.Value > 0.0)
                    {
                        models.Add(vm);
                    }

                });
            }
            var totalVol = models.Sum(x => x.Value);
            if (totalVol > 0)
            {
                models.ForEach(m => { m.PctOfTotal = m.Value / totalVol; });
            }

            return models;

        }

        public List<ViewModel.TimeSeriesDataViewModel> FetchPiggyBackDistViewModel(int productId)
        {
            var dummpy = new DummyProdcutInfoFetcher();
            return dummpy.FetchPiggyBackDistViewModel(productId);
        }

        public List<ViewModel.CategoryDataViewModel> FetchReturnDistViewModel(int productId, DateTime asOfDate)
        {
            List<CategoryDataViewModel> models = new List<CategoryDataViewModel>();
            var totalReturns = this.FetchProductNetValueDistViewModel(productId);
            var netValues = totalReturns.Dictionary["净值"];
            var minValues = netValues.Where(x => (x.ReportedNetValue - 1) < -0.1).Count();
            //if (minValues > 0)
            //{
            //    CategoryDataViewModel model = new CategoryDataViewModel();
            //    model.Name = "收益分布";
            //    model.Label = "<-0.1";
            //    model.Value = minValues;
            //    models.Add(model);
            //}


            int step = 0;
            for (double i = -0.1; i <= 0.1; i += 0.02)
            {
                step++;
                CategoryDataViewModel model = new CategoryDataViewModel();
                model.Name = "收益分布";
                var range = netValues.Where(x => ((x.ReportedNetValue-1) >= i) && ((x.ReportedNetValue-1) <= (i+0.02))).Count();
                model.Label = i.ToString();
                model.Value = range;
                if (step == 6)
                {
                    model.Label = "0";
                }
                models.Add(model);
            }

            var maxValues = netValues.Where(x => (x.ReportedNetValue - 1) > 0.1).Count();
            //if (minValues > 0)
            //{
            //    CategoryDataViewModel model = new CategoryDataViewModel();
            //    model.Name = "收益分布";
            //    model.Label = ">0.1";
            //    model.Value = maxValues;
            //    models.Add(model);
            //}

            models[0].Value += minValues;
            models[models.Count - 1].Value += maxValues;

            return models;
        }

        public List<ViewModel.CategoryDataViewModel> FetchPnLDistViewModel(int productId, DateTime asOfDate)
        {
            //Thread.Sleep(3000);
            List<CategoryDataViewModel> models = new List<CategoryDataViewModel>();
            CategoryDataViewModel vm = new CategoryDataViewModel();
            var totalReturns = this.FetchProductNetValueDistViewModel(productId);
            var netValues = totalReturns.Dictionary["净值"];
            var totalPositiveDays = netValues.Where(x => x.ReportedNetValue >= 1.0).Count();
            var totalNegativeDays= netValues.Where(x => x.ReportedNetValue < 1.0).Count();

            vm.Name = "负收益天数";
            vm.Label = "负收益天数";
            vm.Value = totalNegativeDays;
            models.Add(vm);

            vm = new CategoryDataViewModel();
            vm.Name = "正收益天数";
            vm.Label = "正收益天数";
            vm.Value = totalPositiveDays;
            models.Add(vm);

            return models;
        }

        public List<ViewModel.ProductBriefViewModel> FetchProducts()
        {
            List<ViewModel.ProductBriefViewModel> products = new List<ViewModel.ProductBriefViewModel>();
            using (SQLSession session = new SQLSession("GalaxyDB"))
            {
                DataTable table = session.SQLQuery(@"select  [l_fund_id]
      ,[vc_fund_name]
      ,[vc_fund_caption]
      ,[l_default_asset]
      ,[l_default_combi]
      ,[l_open_date]
      ,[c_fund_type]
      ,[c_fund_status]
      ,[vc_currency_no]
      ,[vc_permit_market],
	  B.vc_dept_shortname
  FROM [cydb].[dbo].[fundinfo] A left outer join cydb..deptinfo B on A.l_dept_id=B.l_dept_id WHERE l_fund_id IN(SELECT DISTINCT(l_fund_id) FROM cydb..his_fundasset)");
                table.AsEnumerable().ToList().ForEach(tr =>
                {
                    ProductBriefViewModel vm = new ProductBriefViewModel();
                    vm.ProductID = tr.Field<Int16>("l_fund_id");
                    vm.Name = tr.Field<String>("vc_fund_name");
                    vm.Caption = tr.Field<String>("vc_fund_caption");
                    vm.IssueDate = tr.Field<DateTime>("l_open_date");
                    vm.Department = tr.Field<String>("vc_dept_shortname");
                    products.Add(vm);
                });
            }

            return products;
        }

        public ViewModel.ProductBriefViewModel FetchProduct(int productId, DateTime asOfDate, String securityType)
        {
            ProductBriefViewModel product = new ProductBriefViewModel();

            String sqlTxt = String.Format(@"SELECT H.vc_inter_code,H.vc_stock_name,P.IndustriesName,H.Pclose,H.volume,H.Preclose,H.Amount,
    P.StockRatio,P.FundRatio,H.tradestatus,H.lastestTradeDate,P.idxPriceSus,P.idxPriceNow,L.vc_fund_caption,H.l_date
FROM cydb..FundHoldings H LEFT OUTER JOIN cydb..FundPerformance P 
ON H.l_fund_id=P.l_fund_id AND H.l_date=P.l_date
AND P.wind_code = H.wind_code JOIN cydb..fundinfo L ON L.l_fund_id=H.l_fund_id
WHERE H.l_fund_id='{0}' AND H.stocktype_name='{2}' and H.Amount>0.0
AND H.l_date=(SELECT MAX(l_date) FROM cydb..FundHoldings WHERE l_fund_id='{0}' AND l_date<='{1}' AND Amount IS NOT NULL)", productId, asOfDate.ToString("yyyy-MM-dd"), securityType);
            using (SQLSession session = new SQLSession("GalaxyDB"))
            {
                DataTable table = session.SQLQuery(sqlTxt);
                if (table.Rows.Count == 0)
                    throw new NotFundException(productId.ToString(), asOfDate);
                table.AsEnumerable().ToList().ForEach(tr =>
                {
                    product.Caption = tr.Field<String>("vc_fund_caption");
                    PorductHoldingViewModel holding = new PorductHoldingViewModel();
                    holding.SecurityCode = tr.Field<String>("vc_inter_code");
                    holding.SecurityName = tr.Field<String>("vc_stock_name");
                    holding.IndustryName = tr.Field<String>("IndustriesName");
                    holding.CostPerShare = Convert.ToDouble(tr.Field<decimal?>("Pclose"));
                    holding.Quantity = Convert.ToDouble(tr.Field<decimal?>("volume"));
                    holding.PreClosePrice = Convert.ToDouble(tr.Field<decimal?>("Preclose"));
                    holding.MarketValue = Convert.ToDouble(tr.Field<decimal?>("Amount"));
                    holding.PropotionOfTotalEquity = Convert.ToDouble(tr.Field<decimal?>("StockRatio"));
                    holding.PropotionOfTotalAssets = Convert.ToDouble(tr.Field<decimal?>("FundRatio"));
                    holding.TradeState = tr.Field<String>("tradestatus");
                    holding.LastTradeDate = tr.Field<DateTime?>("lastestTradeDate");
                    holding.IndustryIndexBeforeHalt = Convert.ToDouble(tr.Field<decimal?>("idxPriceSus"));
                    holding.IndustryIndexNow = Convert.ToDouble(tr.Field<decimal?>("idxPriceNow"));
                    holding.UpdatedDate = tr.Field<DateTime>("l_date");
                    product.Portfolio.Add(holding);
                });
            }

            //fill in product info
            sqlTxt = String.Format(@"SELECT TOP 1 [l_date]
                  ,[l_fund_id]
                  ,[vc_currency_no]
                  ,[en_current_cash]
                  ,[en_basic_frozen_balance]
                  ,[en_buy_balance]
                  ,[en_sale_balance]
                  ,[en_other_expense]
                  ,[en_other_profit]
                  ,[en_stock_asset]
                  ,[en_bond_asset]
                  ,[en_fund_asset]
                  ,[en_warrant_asset]
                  ,[en_hg_asset]
                  ,[en_other_asset]
                  ,[en_futures_asset]
                  ,[en_futures_balance]
                  ,[en_futures_deposit]
                  ,[en_futures_prepare]
                  ,[en_fund_share]
                  ,[en_fund_value]
                  ,[en_unit_dividends]
                  ,[en_bond_interest]
                  ,[en_option_asset]
                  ,[en_foreign_asset]
                  ,[c_nav]
                  ,[c_cash_flow1]
                  ,[c_cash_flow2]
                  ,[c_remarks]
                  ,[updatetime]
                FROM [cydb].[dbo].[his_fundasset] WHERE l_fund_id={0} AND l_date<='{1}' ORDER BY l_date DESC", productId,asOfDate.ToString("yyyy-MM-dd"));
            using (SQLSession session = new SQLSession("GalaxyDB"))
            {
                DataTable table = session.SQLQuery(sqlTxt);
                table.AsEnumerable().ToList().ForEach(tr =>
                {
                    product.EquityMarketValue = Convert.ToDouble(tr.Field<Decimal>("en_stock_asset"));
                    product.FundMarketValue = Convert.ToDouble(tr.Field<Decimal>("en_fund_asset"));
                    product.BondMarketValue = Convert.ToDouble(tr.Field<Decimal>("en_bond_asset"));
                    product.FutureMarketValue = Convert.ToDouble(tr.Field<Decimal>("en_futures_asset"));
                    product.TotalAssetMarketValue = Convert.ToDouble(tr.Field<Decimal>("en_fund_value"));
                    product.ProductQuantity = Convert.ToDouble(tr.Field<Decimal>("en_fund_share"));
                    product.NetValueAnaunced = Convert.ToDouble(tr.Field<Decimal>("c_nav"));
                });
            }

            return product;
        }

        public ViewModel.MultipleCategoriesViewModel FetchProductEquityAssetDist(int productId, DateTime asOfDate)
        {
            Regex dateTimeMatchReg = new Regex(@"^\d{1}\d{2}\d{4}$", RegexOptions.Compiled);

            MultipleCategoriesViewModel multipleCategories = new MultipleCategoriesViewModel();

            List<CategoryDataViewModel> models = new List<CategoryDataViewModel>();

            String sqlText = String.Format(@"SELECT IndustriesName, CONVERT(VARCHAR(20),DATEPART(MONTH,l_date))+'/01/'+CONVERT(VARCHAR(20),DATEPART(YEAR,l_date)) AS updateDate ,SUM(amount) AS amount
FROM cydb..FundPerformance WHERE assetclass_name='股票资产' AND l_fund_id={0}
GROUP BY IndustriesName,CONVERT(VARCHAR(20),DATEPART(MONTH,l_date))+'/01/'+CONVERT(VARCHAR(20),DATEPART(YEAR,l_date))", productId);
            using (SQLSession session = new SQLSession("GalaxyDB"))
            {
                DataTable table = session.SQLQuery(sqlText);
                table.AsEnumerable().ToList().ForEach(tr =>
                {
                    CategoryDataViewModel vm = new CategoryDataViewModel();
                    vm.Name = tr.Field<String>("IndustriesName");
                    vm.Label = tr.Field<String>("updateDate");
                    if (vm.Label.Length == 9)
                    {
                        vm.Label = "0" + vm.Label;
                    }
                    vm.UpdatedDate = DateTime.ParseExact(vm.Label, "MM/dd/yyyy", null);
                    if (tr.Field<Decimal?>("amount").HasValue)
                    {
                        vm.Value = Convert.ToDouble(tr.Field<Decimal>("amount"));
                    }
                    else
                    {
                        vm.Value = 0.0;
                    }

                    models.Add(vm);
                });
            }
            var industrNames = models.Select(r => r.Name).Distinct().ToList();
            industrNames.ForEach(industryName =>
            {
                var valueList = models.Where(x => x.Name == industryName).OrderBy(x => x.UpdatedDate).ToList();
                multipleCategories.AddCategories(industryName, valueList);
            });

            //need to flat the data
            var from = models.Min(x => x.UpdatedDate);
            var to = models.Max(x => x.UpdatedDate);

            for (DateTime uDate = from; uDate < to; uDate = uDate.AddMonths(1))
            {
                industrNames.ForEach(name =>
                {
                    var dataList = multipleCategories.Dictionary[name];
                    if (dataList.Find(x => x.UpdatedDate == uDate) == null)
                    {
                        CategoryDataViewModel supplement = new CategoryDataViewModel();
                        supplement.Label = uDate.ToString("MM/dd/yyyy");
                        supplement.Value = 0.0;
                        supplement.Name = name;
                        supplement.UpdatedDate = uDate;
                        dataList.Add(supplement);
                    }
                    dataList = dataList.OrderBy(x => x.UpdatedDate).ToList();
                    multipleCategories.Dictionary[name] = dataList;
                });
            }

            return multipleCategories;
        }

        public ViewModel.ProductPerformanceIndexViewModel FetchPerformanceViewModel(int productId, DateTime asOfDate)
        {
            ProductPerformanceIndexViewModel vm = new ProductPerformanceIndexViewModel();

            String sqlText =
                String.Format(
                    @"SELECT P.annuReturn,beta,annuVolatility,maxDrawDown,annuSharpe,correlation,l_date FROM cydb..performance P WHERE P.l_fund_id={0} AND l_date
            =(SELECT MAX(l_date) FROM cydb..performance WHERE l_fund_id={0} AND l_date<='{1}' )",productId,asOfDate.ToString("yyyy-MM-dd"));
            using (SQLSession session = new SQLSession("GalaxyDB"))
            {
                DataTable table = session.SQLQuery(sqlText);
                if (table.Rows.Count == 0)
                    throw new NotFundException(productId.ToString(), asOfDate);
                table.AsEnumerable().ToList().ForEach(tr =>
                {
                    vm.ProductId = productId;
                    vm.AsOfDate = asOfDate;
                    //vm.Return = random.NextDouble();
                    vm.AnnualReturn = Convert.ToDouble(tr.Field<Decimal>("annuReturn"));
                    //vm.RelativeReturn = random.NextDouble();
                    vm.Beta = Convert.ToDouble(tr.Field<Decimal>("beta"));
                    vm.Volitility = Convert.ToDouble(tr.Field<Decimal>("annuVolatility"));
                    vm.MaxWithdraw = Convert.ToDouble(tr.Field<Decimal>("maxDrawDown"));
                    vm.SharpRatio = Convert.ToDouble(tr.Field<Decimal>("annuSharpe"));  
                    //vm.RiskToReturn = random.NextDouble();
                    vm.ConvarianceOfIndex = Convert.ToDouble(tr.Field<Decimal>("correlation")); 
                    //vm.OrderOfSimilarStragtigy = random.Next(1, 100);
                });
            }
            
            
            return vm;
        }
    }
}
