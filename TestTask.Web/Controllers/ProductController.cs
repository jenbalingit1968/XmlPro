using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTask.Web.Logics;

namespace TestTask.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductLogic productLogic;
        public ProductController()
        {
            productLogic = new ProductLogic();
        }

        [HttpGet]
        public ActionResult GetProducts(bool sortByPrice, bool isAscending)
        {
            var raw = productLogic.GetProducts(sortByPrice, isAscending);
            return Json(raw, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDetails(string id)
        {
            var raw = productLogic.GetProductDetails(id);
            return Json(raw, JsonRequestBehavior.AllowGet);
        }

    }
}