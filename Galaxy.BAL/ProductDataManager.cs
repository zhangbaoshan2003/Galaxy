using Galaxy.BAL.Exceptions;
using Galaxy.BAL.Interface;
using Galaxy.BAL.ViewModel;
using Galaxy.DAL2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL
{
    public class ProductDataManager : IProdcutInfo
    {

        public ViewModel.MultipleTimeSeriesViewModel FetchProductNetValueDistViewModel(int productId)
        {
            throw new NotImplementedException();
        }

        public ViewModel.MultipleCategoriesViewModel FetchProductFundAssetDist(int productId, DateTime asOfDate)
        {
            throw new NotImplementedException();
        }

        public List<ViewModel.CategoryDataViewModel> FetchCurrentProductFundAssetDist(int productId, DateTime asOfDate)
        {
            List<CategoryDataViewModel> models = new List<CategoryDataViewModel>();

            String sqlText = @"WITH Pre AS (
	                    SELECT SUM(volume) AS vol ,assetclass_name,l_date,l_fund_id,ROW_NUMBER() OVER(PARTITION BY assetclass_name ORDER BY l_date) AS rn
                      FROM [cydb].[dbo].[FundHoldings] WHERE l_fund_id={0} GROUP BY assetclass_name,l_date,l_fund_id  
                    ),
                    Cur AS (
                    SELECT SUM(volume) AS vol ,assetclass_name,l_date,l_fund_id,ROW_NUMBER() OVER(PARTITION BY assetclass_name ORDER BY l_date) AS rn
                      FROM [cydb].[dbo].[FundHoldings] WHERE l_fund_id={0} GROUP BY assetclass_name,l_date,l_fund_id 
                    )
                    SELECT P.vol AS pre_vol,C.vol AS cur_vol,P.rn,C.rn,C.l_date,C.assetclass_name FROM Pre P JOIN Cur C ON P.l_fund_id=C.l_fund_id AND P.assetclass_name=C.assetclass_name AND P.rn=C.rn-1
                    WHERE C.l_date='{1}'";
            sqlText = String.Format(sqlText, productId, asOfDate.ToString("yyyy-MM-dd"));
            using (SQLSession session = new SQLSession("GalaxyDB")) 
            {
                DataTable table = session.SQLQuery(sqlText);


            }


        }

        public List<ViewModel.TimeSeriesDataViewModel> FetchPiggyBackDistViewModel(int productId)
        {
            throw new NotImplementedException();
        }

        public List<ViewModel.CategoryDataViewModel> FetchReturnDistViewModel(int productId, DateTime asOfDate)
        {
            throw new NotImplementedException();
        }

        public List<ViewModel.CategoryDataViewModel> FetchPnLDistViewModel(int productId, DateTime asOfDate)
        {
            throw new NotImplementedException();
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
  FROM [cydb].[dbo].[fundinfo] A left outer join cydb..deptinfo B on A.l_dept_id=B.l_dept_id ");
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

        public ViewModel.ProductBriefViewModel FetchProduct(int productId, DateTime asOfDate)
        {
            ProductBriefViewModel product = new ProductBriefViewModel();

            String sqlTxt = String.Format(@"SELECT H.vc_inter_code,P.IndustriesName,H.Pclose,H.volume,H.Preclose,H.Amount,
    P.StockRatio,P.FundRatio,H.tradestatus,H.lastestTradeDate,P.idxPriceSus,P.idxPriceNow,L.vc_fund_caption
FROM cydb..FundHoldings H LEFT OUTER JOIN cydb..FundPerformance P 
ON H.l_fund_id=P.l_fund_id AND H.l_date=P.l_date
AND P.wind_code = H.wind_code JOIN cydb..fundinfo L ON L.l_fund_id=H.l_fund_id
WHERE H.l_fund_id='{0}' AND H.stocktype_name='股票'
AND H.l_date=(SELECT MAX(l_date) FROM cydb..FundHoldings WHERE l_fund_id='{0}' AND l_date<='{1}' AND Amount IS NOT NULL)", productId, asOfDate.ToString("yyyy-MM-dd"));
            using (SQLSession session = new SQLSession("GalaxyDB"))
            {
                DataTable table = session.SQLQuery(sqlTxt);
                if(table.Rows.Count==0)
                    throw new NotFundException(productId.ToString(), asOfDate);
                table.AsEnumerable().ToList().ForEach(tr => {
                    product.Caption = tr.Field<String>("vc_fund_caption");
                    PorductHoldingViewModel holding = new PorductHoldingViewModel();
                    holding.SecurityCode = tr.Field<String>("vc_inter_code");
                    holding.SecurityName = "Unknown";
                    holding.IndustryName = tr.Field<String>("IndustriesName");
                    holding.CostPerShare = Convert.ToDouble(tr.Field<decimal?>("Pclose"));
                    holding.Quantity = Convert.ToDouble(tr.Field<decimal?>("volume"));
                    holding.PreClosePrice = Convert.ToDouble(tr.Field<decimal?>("Preclose"));
                    holding.MarketValue = Convert.ToDouble(tr.Field<decimal?>("Amount"));
                    holding.PropotionOfTotalEquity =Convert.ToDouble( tr.Field<decimal?>("StockRatio"));
                    holding.PropotionOfTotalAssets = Convert.ToDouble(tr.Field<decimal?>("FundRatio"));
                    holding.TradeState = tr.Field<String>("tradestatus");
                    holding.LastTradeDate = tr.Field<DateTime?>("lastestTradeDate");
                    holding.IndustryIndexBeforeHalt = Convert.ToDouble(tr.Field<decimal?>("idxPriceSus"));
                    holding.IndustryIndexNow = Convert.ToDouble(tr.Field<decimal?>("idxPriceNow"));
                    product.Portfolio.Add(holding);
                });
            }

            return product;
        }

        public ViewModel.MultipleCategoriesViewModel FetchProductEquityAssetDist(int productId, DateTime asOfDate)
        {
            throw new NotImplementedException();
        }

        public ViewModel.ProductPerformanceIndexViewModel FetchPerformanceViewModel(int productId, DateTime asOfDate)
        {
            throw new NotImplementedException();
        }
    }
}
