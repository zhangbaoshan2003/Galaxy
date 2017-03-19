using System.Globalization;
using Galaxy.BAL;
using Galaxy.BAL.Interface;
using Galaxy.BAL.ViewModel;
using Galaxy.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Galaxy.Controllers
{
    public class ProductController : Controller
    {
        private DateTime toDate(String dateStr)
        {
            if (String.IsNullOrEmpty(dateStr))
                return DateTime.Today;

            DateTime asOfDate = DateTime.Today;
            if (DateTime.TryParseExact(dateStr, "MM/dd/yyyy", null, DateTimeStyles.None, out asOfDate) == true)
            {
                
            }
            return asOfDate;
        }

        private IProdcutInfo prodcutInfoFetcher = new DummyProdcutInfoFetcher();
        
        //
        // GET: /Product/
        public ActionResult Index()
        {
            List<ProductBriefViewModel> models = prodcutInfoFetcher.FetchProducts();
            return View(models);
        }

        public ActionResult Detail(int? id,String asOfDateStr)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("Index");
            }

            DateTime asOfDate = toDate(asOfDateStr);
            ProductBriefViewModel product = prodcutInfoFetcher.FetchProduct(id.Value,asOfDate);
            ViewBag.ProductId = id.Value;
            return View(product);
        }

        public ActionResult GetNetValues(int productId)
        {
            try
            {
                MultipleTimeSeriesViewModel viewModel = prodcutInfoFetcher.FetchProductNetValueDistViewModel(productId);
                var jsonResult = Json(viewModel.Dictionary, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when fetching product net value distribution",ex);
                return Json(null);
            }
        }

        public ActionResult GetFuncAssetDist(int productId,String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                MultipleCategoriesViewModel viewModel = prodcutInfoFetcher.FetchProductFundAssetDist(productId,asOfDate);
                var jsonResult = Json(viewModel.Dictionary, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when fetching product net value distribution", ex);
                return Json(null);
            }
        }

        public ActionResult GetCurrentFuncAssetDist(int productId, String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                List<CategoryDataViewModel> viewModel = prodcutInfoFetcher.FetchCurrentProductFundAssetDist(productId, asOfDate);
                var jsonResult = Json(viewModel, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when fetching product net value distribution", ex);
                return Json(null);
            }
        }

        public ActionResult GetPiggyBackDist(int productId)
        {
            try
            {
                List<TimeSeriesDataViewModel> viewModel = prodcutInfoFetcher.FetchPiggyBackDistViewModel(productId);
                var jsonResult = Json(viewModel, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when fetching product piggyback distribution", ex);
                return Json(null);
            }
        }

        public ActionResult GetReturnDist(int productId,String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                List<CategoryDataViewModel> viewModel = prodcutInfoFetcher.FetchReturnDistViewModel(productId,asOfDate);
                var jsonResult = Json(viewModel, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when GetReturnDist", ex);
                return Json(null);
            }
        }

        public ActionResult GetPnlDist(int productId,String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                List<CategoryDataViewModel> viewModel = prodcutInfoFetcher.FetchPnLDistViewModel(productId,asOfDate);
                var jsonResult = Json(viewModel, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when fetching product return distribution", ex);
                return Json(null);
            }
        }
        public ActionResult GetHoldings([DataSourceRequest]DataSourceRequest request,int productId,String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                ProductBriefViewModel product = prodcutInfoFetcher.FetchProduct(productId, asOfDate);
                return Json(product.Portfolio.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when GetHoldings", ex);
                return Json(null);
            }
        }
        public ActionResult GetEquitAssetDist([DataSourceRequest]DataSourceRequest request, int productId, String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                MultipleCategoriesViewModel viewModel = prodcutInfoFetcher.FetchProductEquityAssetDist(productId, asOfDate);
                var jsonResult = Json(viewModel.Dictionary, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when GetEquitAssetDist", ex);
                return Json(null);
            }
        }
	}
}