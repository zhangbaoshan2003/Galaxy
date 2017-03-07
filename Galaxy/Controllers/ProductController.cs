using Galaxy.BAL;
using Galaxy.BAL.Interface;
using Galaxy.BAL.ViewModel;
using Galaxy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Detail(int id) 
        {
            ProductBriefViewModel product = prodcutInfoFetcher.FetchProduct(id);
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

        public ActionResult GetReturnDist(int productId)
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

        public ActionResult GetPnlDist(int productId)
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
	}
}