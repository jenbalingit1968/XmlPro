using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestTask.Web.Logics;

namespace TestTask.Web.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductServiceController : ApiController
    {

        private readonly ProductLogic productLogic;

        public ProductServiceController()
        {
            productLogic = new ProductLogic();
        }


        [HttpGet]
        [Route("GetProducts")]
        public List<Models.Product> GetProducts()
        {
            var raw = productLogic.GetProducts();
            return raw;
        }
    }
}
