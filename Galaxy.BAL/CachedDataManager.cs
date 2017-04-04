using Galaxy.BAL.Common;
using Galaxy.BAL.Interface;
using Galaxy.BAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL
{
    public class CachedDataManager : IProdcutInfo
    {
        ProductDataManager backendDataManager = new ProductDataManager();

        public ViewModel.MultipleTimeSeriesViewModel FetchProductNetValueDistViewModel(int productId)
        {
            MultipleTimeSeriesViewModel resultModel = null;
            String key = String.Format("FetchProductNetValueDistViewModel_productId_{0}", productId);
            if (CacheManager.Instance.GetItem(key) == null) 
            {
                resultModel = backendDataManager.FetchProductNetValueDistViewModel(productId);
                CacheManager.Instance.AddItemByHour(resultModel,key,10);
            }
            resultModel = (MultipleTimeSeriesViewModel)CacheManager.Instance.GetItem(key);
            return resultModel;
            
        }

        public ViewModel.MultipleCategoriesViewModel FetchProductFundAssetDist(int productId, DateTime asOfDate)
        {
            MultipleCategoriesViewModel resultModel = null;
            String key = String.Format("FetchProductFundAssetDist_productId_{0}_asOfDate_{1}", productId, asOfDate.ToShortDateString());
            if (CacheManager.Instance.GetItem(key) == null)
            {
                resultModel = backendDataManager.FetchProductFundAssetDist(productId,asOfDate);
                CacheManager.Instance.AddItemByHour(resultModel, key, 10);
            }
            resultModel = (MultipleCategoriesViewModel)CacheManager.Instance.GetItem(key);
            return resultModel;
        }

        public List<ViewModel.CategoryDataViewModel> FetchCurrentProductFundAssetDist(int productId, DateTime asOfDate)
        {
            List<ViewModel.CategoryDataViewModel> resultModel = null;
            String key = String.Format("FetchCurrentProductFundAssetDist_productId_{0}_asOfDate_{1}", productId, asOfDate.ToShortDateString());
            if (CacheManager.Instance.GetItem(key) == null)
            {
                resultModel = backendDataManager.FetchCurrentProductFundAssetDist(productId, asOfDate);
                CacheManager.Instance.AddItemByHour(resultModel, key, 10);
            }
            resultModel = (List<ViewModel.CategoryDataViewModel>)CacheManager.Instance.GetItem(key);
            return resultModel;
        }

        public List<ViewModel.TimeSeriesDataViewModel> FetchPiggyBackDistViewModel(int productId)
        {
            return backendDataManager.FetchPiggyBackDistViewModel(productId);
        }

        public List<ViewModel.CategoryDataViewModel> FetchReturnDistViewModel(int productId, DateTime asOfDate)
        {
            List<ViewModel.CategoryDataViewModel> resultModel = null;
            String key = String.Format("FetchReturnDistViewModel_productId_{0}_asOfDate_{1}", productId, asOfDate.ToShortDateString());
            if (CacheManager.Instance.GetItem(key) == null)
            {
                resultModel = backendDataManager.FetchReturnDistViewModel(productId, asOfDate);
                CacheManager.Instance.AddItemByHour(resultModel, key, 10);
            }
            resultModel = (List<ViewModel.CategoryDataViewModel>)CacheManager.Instance.GetItem(key);
            return resultModel;
        }

        public List<ViewModel.CategoryDataViewModel> FetchPnLDistViewModel(int productId, DateTime asOfDate)
        {
            List<ViewModel.CategoryDataViewModel> resultModel = null;
            String key = String.Format("FetchPnLDistViewModel_productId_{0}_asOfDate_{1}", productId, asOfDate.ToShortDateString());
            if (CacheManager.Instance.GetItem(key) == null)
            {
                resultModel = backendDataManager.FetchPnLDistViewModel(productId, asOfDate);
                CacheManager.Instance.AddItemByHour(resultModel, key, 10);
            }
            resultModel = (List<ViewModel.CategoryDataViewModel>)CacheManager.Instance.GetItem(key);
            return resultModel;
        }

        public List<ViewModel.ProductBriefViewModel> FetchProducts()
        {
            return backendDataManager.FetchProducts();
        }

        public ViewModel.ProductBriefViewModel FetchProduct(int productId, DateTime asOfDate, string securityType)
        {
            ProductBriefViewModel resultModel = null;
            String key = String.Format("FetchProduct_productId_{0}_asOfDate_{1}_securityType_{2}", productId,asOfDate.ToShortDateString(),securityType);
            if (CacheManager.Instance.GetItem(key) == null)
            {
                resultModel = backendDataManager.FetchProduct(productId,asOfDate,securityType);
                CacheManager.Instance.AddItemByHour(resultModel, key, 10);
            }
            resultModel = (ProductBriefViewModel)CacheManager.Instance.GetItem(key);
            return resultModel;
        }

        public ViewModel.MultipleCategoriesViewModel FetchProductEquityAssetDist(int productId, DateTime asOfDate)
        {
            MultipleCategoriesViewModel resultModel = null;
            String key = String.Format("FetchProductEquityAssetDist_{0}_asOfDate_{1}", productId, asOfDate.ToShortDateString());
            if (CacheManager.Instance.GetItem(key) == null)
            {
                resultModel = backendDataManager.FetchProductEquityAssetDist(productId, asOfDate);
                CacheManager.Instance.AddItemByHour(resultModel, key, 10);
            }
            resultModel = (MultipleCategoriesViewModel)CacheManager.Instance.GetItem(key);
            return resultModel;
        }

        public ViewModel.ProductPerformanceIndexViewModel FetchPerformanceViewModel(int productId, DateTime asOfDate)
        {
            ProductPerformanceIndexViewModel resultModel = null;
            String key = String.Format("FetchPerformanceViewModel_{0}_asOfDate_{1}", productId, asOfDate.ToShortDateString());
            if (CacheManager.Instance.GetItem(key) == null)
            {
                resultModel = backendDataManager.FetchPerformanceViewModel(productId, asOfDate);
                CacheManager.Instance.AddItemByHour(resultModel, key, 10);
            }
            resultModel = (ProductPerformanceIndexViewModel)CacheManager.Instance.GetItem(key);
            return resultModel;
        }
    }
}
