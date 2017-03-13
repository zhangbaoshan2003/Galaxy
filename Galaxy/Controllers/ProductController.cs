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
        private IProdcutInfo prodcutInfoFetcher = new DummyProdcutInfoFetcher();
        
        //
        // GET: /Product/
        public ActionResult Index()
        {
            List<ProductBriefViewModel> models = prodcutInfoFetcher.FetchProducts();
            return View(models);
        }

        public ActionResult Detail(int? id) 
        {
            if (id.HasValue == false) {
                return RedirectToAction("Index");
            }
            ProductBriefViewModel product = prodcutInfoFetcher.FetchProduct(id.Value);
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

        public ActionResult GetReturnDist(int productId,String asofDate)
        {
            try
            {
                List<CategoryDataViewModel> viewModel = prodcutInfoFetcher.FetchReturnDistViewModel(productId);
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

        public ActionResult GetPnlDist(int productId,String asOfDate)
        {
            try
            {
                List<CategoryDataViewModel> viewModel = prodcutInfoFetcher.FetchPnLDistViewModel(productId);
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
        public ActionResult GetHoldings([DataSourceRequest]DataSourceRequest request,String asOfDate,String productId)
        {
            ProductBriefViewModel product = prodcutInfoFetcher.FetchProduct(1);
            return Json(product.Portfolio.ToDataSourceResult(request));
        }
	}
}