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
            throw new NotImplementedException();
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
                DataTable table = session.SQLQuery(@"select top (10) [l_fund_id]
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
            throw new NotImplementedException();
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
