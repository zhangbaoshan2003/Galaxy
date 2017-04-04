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
using Galaxy.BAL.Exceptions;

namespace Galaxy.Controllers
{
    public class ProductController : Controller
    {
        private DateTime toDate(String dateStr)
        {
            if (String.IsNullOrEmpty(dateStr))
                if (Session["asOfDate"] == null)
                {
                    return DateTime.Today;
                }
                else
                {
                    dateStr = Session["asOfDate"].ToString();
                }

           
            DateTime asOfDate = DateTime.Today;
            if (DateTime.TryParseExact(dateStr, "MM/dd/yyyy", null, DateTimeStyles.None, out asOfDate) == true)
            {
                Session["asOfDate"] = dateStr;
                return asOfDate;
            }

            return DateTime.Today;
        }

        //private IProdcutInfo prodcutInfoFetcher = new DummyProdcutInfoFetcher();
        //private IProdcutInfo prodcutInfoFetcher = new ProductDataManager();
        private IProdcutInfo prodcutInfoFetcher = new CachedDataManager();
        //
        // GET: /Product/
        public ActionResult Index()
        {
            List<ProductBriefViewModel> models = prodcutInfoFetcher.FetchProducts();
            return View(models);
        }

        public ActionResult Detail(int? id, String asOfDateStr)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("Index");
            }

            try
            {
                Session["asOfDate"] = null;
                DateTime asOfDate = toDate(asOfDateStr);
                ProductBriefViewModel product = prodcutInfoFetcher.FetchProduct(id.Value, asOfDate, "股票");
                ViewBag.ProductId = id.Value;
                ViewBag.SubTitle = product.Caption;
                return View(product);
            }
            catch (NotFundException ex)
            {
                LogUtility.Fatal(ex.Message, ex);
                throw ex;
            }
        }
        public ActionResult GetDetail([DataSourceRequest]DataSourceRequest request, int productId, String asOfDateStr)
        {
            try
            {
                List<ProductBriefViewModel> listpProducts= new List<ProductBriefViewModel>();

                DateTime asOfDate = toDate(asOfDateStr);
                ProductBriefViewModel product = prodcutInfoFetcher.FetchProduct(productId, asOfDate, "股票");
                listpProducts.Add(product);
                var jsonResult = Json(listpProducts.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when GetEquitAssetDist", ex);
                return Json(null);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
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
                LogUtility.Fatal("Error happend when fetching product net value distribution", ex);
                return Json(null);
            }
        }

        public ActionResult GetFuncAssetDist(int productId, String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                MultipleCategoriesViewModel viewModel = prodcutInfoFetcher.FetchProductFundAssetDist(productId, asOfDate);
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
        public ActionResult GetPerformanceIndex(int productId, String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                ProductPerformanceIndexViewModel viewModel = prodcutInfoFetcher.FetchPerformanceViewModel(productId, asOfDate);
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
        public ActionResult GetFuncAssetDistAsOf([DataSourceRequest]DataSourceRequest request, int productId, String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                List<CategoryDataViewModel> viewModel = prodcutInfoFetcher.FetchCurrentProductFundAssetDist(productId, asOfDate);
                return Json(viewModel.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when fetching product net value distribution", ex);
                return Json(null);
            }
        }
        public ActionResult GetPiggyBackDist(int productId, String asOfDateStr)
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

        public ActionResult GetReturnDist(int productId, String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                List<CategoryDataViewModel> viewModel = prodcutInfoFetcher.FetchReturnDistViewModel(productId, asOfDate);
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

        public ActionResult GetPnlDist(int productId, String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                List<CategoryDataViewModel> viewModel = prodcutInfoFetcher.FetchPnLDistViewModel(productId, asOfDate);
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
        public ActionResult GetHoldings([DataSourceRequest]DataSourceRequest request, int productId, String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                ProductBriefViewModel product = prodcutInfoFetcher.FetchProduct(productId, asOfDate,"股票");
                return Json(product.Portfolio.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when GetHoldings", ex);
                return Json(null);
            }
        }
        public ActionResult GetMostValuableBonds([DataSourceRequest]DataSourceRequest request, int productId, String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                ProductBriefViewModel product = prodcutInfoFetcher.FetchProduct(productId, asOfDate, "国债标准券");
                List<PorductHoldingViewModel> bonds = product.Portfolio
                    .OrderByDescending(p => p.PropotionOfTotalAssets).Take(10)
                    .ToList();

                if (bonds.Count > 0)
                {
                    var lastUpdatedDate = bonds[0].UpdatedDate.AddDays(-1);
                    var productPrev = prodcutInfoFetcher.FetchProduct(productId, lastUpdatedDate, "国债标准券");
                    bonds.ForEach(s =>
                    {
                        var prevEquity = productPrev.Portfolio.FirstOrDefault(x => x.SecurityCode == s.SecurityCode);
                        if (prevEquity != null)
                        {
                            s.PropotionOfTotalAssetsPrev = prevEquity.PropotionOfTotalAssets;
                        }
                    });
                }
                return Json(bonds.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when GetMostValuableBonds", ex);
                return Json(null);
            }
        }

        public ActionResult GetMostValuableEquities([DataSourceRequest]DataSourceRequest request, int productId, String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                ProductBriefViewModel product = prodcutInfoFetcher.FetchProduct(productId, asOfDate, "股票");
                List<PorductHoldingViewModel> bonds = product.Portfolio
                    .OrderByDescending(p => p.PropotionOfTotalAssets).Take(10)
                    .ToList();

                if (bonds.Count > 0) 
                {
                    var lastUpdatedDate = bonds[0].UpdatedDate.AddDays(-1);
                    var productPrev = prodcutInfoFetcher.FetchProduct(productId, lastUpdatedDate, "股票");
                    bonds.ForEach(s => 
                    {
                        var prevEquity = productPrev.Portfolio.FirstOrDefault(x => x.SecurityCode == s.SecurityCode);
                        if (prevEquity != null) 
                        {
                            s.PropotionOfTotalAssetsPrev = prevEquity.PropotionOfTotalAssets;
                        }
                    });
                }
                return Json(bonds.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when GetMostValuableEquities", ex);
                return Json(null);
            }
        }
        public ActionResult GetIndustryDistView([DataSourceRequest]DataSourceRequest request, int productId, String asOfDateStr)
        {
            try
            {
                DateTime asOfDate = toDate(asOfDateStr);
                ProductBriefViewModel product = prodcutInfoFetcher.FetchProduct(productId, asOfDate,"股票");
                var q = from p in product.Portfolio
                        orderby p.PropotionOfTotalAssets descending
                        group p by p.IndustryName into gr
                        select new PorductHoldingViewModel
                        {
                            IndustryName = gr.Key,
                            PropotionOfTotalAssets = gr.Sum(x => x.MarketValue)
                        };

                var list = q.ToList();
                var totalValue = q.ToList().Sum(x => x.PropotionOfTotalAssets);
                list.ForEach(p => {
                    p.PropotionOfTotalAssets = p.PropotionOfTotalAssets / totalValue;
                });

                //fetch the previous data
                if(product.Portfolio.Count>0)
                {
                    var updateDate= product.Portfolio[0].UpdatedDate.AddDays(-1);
                    ProductBriefViewModel productPrev = prodcutInfoFetcher.FetchProduct(productId, updateDate, "股票");
                    var qPrev = from p in productPrev.Portfolio
                            orderby p.PropotionOfTotalAssets descending
                            group p by p.IndustryName into gr
                            select new PorductHoldingViewModel
                            {
                                IndustryName = gr.Key,
                                PropotionOfTotalAssets = gr.Sum(x => x.MarketValue)
                            };

                    var listPre = qPrev.ToList();
                    var totalValuePre = qPrev.ToList().Sum(x => x.PropotionOfTotalAssets);
                    listPre.ForEach(p =>
                    {
                        p.PropotionOfTotalAssets = p.PropotionOfTotalAssets / totalValuePre;
                    });

                    list.ForEach(cur => {
                        var prevIndeus = listPre.FirstOrDefault(x => x.IndustryName == cur.IndustryName);
                        if (prevIndeus != null) 
                        {
                            cur.PropotionOfTotalAssetsPrev = prevIndeus.PropotionOfTotalAssets;
                        }
                    });
                }
               

                return Json(list.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("Error happend when GetHoldings", ex);
                return Json(null);
            }
        }

        //public ActionResult GetBondsDist([DataSourceRequest]DataSourceRequest request, int productId, String asOfDateStr)
        //{
        //    try
        //    {
        //        DateTime asOfDate = toDate(asOfDateStr);
        //        ProductBriefViewModel product = prodcutInfoFetcher.FetchProduct(productId, asOfDate);
        //        return Json(product.Portfolio.ToDataSourceResult(request));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Fatal("Error happend when GetHoldings", ex);
        //        return Json(null);
        //    }
        //}

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