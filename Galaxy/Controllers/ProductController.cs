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
        Random R = new Random();

        //
        // GET: /Product/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail() 
        {
            return View();
        }

        public ActionResult GetNetValues(int productId)
        {
            List<Models.TimeSeriesDataViewModel> models = new List<Models.TimeSeriesDataViewModel>();
            for (int i = 0; i < 100; i++)
            {
                TimeSeriesDataViewModel model = new TimeSeriesDataViewModel();
                model.Name = "净值";
                model.ReportedDataTime = DateTime.Today.AddDays(i);
                model.ReportedValue = R.NextDouble();
                models.Add(model);
            }
            var jsonResult= Json(models,JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
        }
	}
}