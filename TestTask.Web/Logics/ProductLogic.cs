using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
using TestTask.Web.Models;

namespace TestTask.Web.Logics
{
    public class ProductLogic
    {

        public List<Product> GetProducts(bool sortByPrice = true, bool isAscending = true)
        {

            var merge = (from p in LoadProducts()
                         join d in GetProductDetails()
                         on p.Id equals d.Id
                         select new
                         {
                             p.Title,
                             p.Price,
                             d.Availability,
                             p.Id,
                             p.Image,
                             p.Description,
                             p.Sorting
                         });

            var grpResult = (from g in merge
                             group g by g.Id into grp
                             select new Product()
                             {
                                 Id = grp.Key,
                                 Title = grp.FirstOrDefault().Title,
                                 Price = grp.FirstOrDefault().Price,
                                 Availability = grp.FirstOrDefault().Availability,
                                 Image = grp.FirstOrDefault().Image,
                                 Description = grp.FirstOrDefault().Description,
                                 Sorting = grp.FirstOrDefault().Sorting
                             });



            if (sortByPrice && isAscending)
                return grpResult.OrderBy(o => o.Price).ToList();
            else if (sortByPrice && !isAscending)
                return grpResult.OrderByDescending(o => o.Price).ToList();
            else if (!sortByPrice && isAscending)
                return grpResult.OrderBy(o => o.Sorting.Popular).ToList();
            else
                return grpResult.OrderByDescending(o => o.Sorting.Popular).ToList();

        }

        public Product GetProductDetails(string Id)
        {
            var merge = from p in LoadProducts()
                        join d in GetProductDetails()
                        on p.Id equals d.Id
                        where p.Id == Id.Trim()
                        select new Product()
                        {
                            Title = p.Title,
                            Price = p.Price,
                            Specs = d.Specs,
                            Description = p.Description
                        };

            return merge.First();
        }


        private List<Product> GetProductDetails()
        {
            string xmlData = HttpContext.Current.Server.MapPath("~/Data/Details.xml");
            var data = this.ReadExcelFile<Store>(xmlData);

            return data.Products;
        }
        private List<Product> LoadProducts()
        {
            string xmlData = HttpContext.Current.Server.MapPath("~/Data/List.xml");
            var data = this.ReadExcelFile<Store>(xmlData);

            return data.Products;
        }

        private T ReadExcelFile<T>(string path)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamReader streamReader = new StreamReader(path);
            var input = (T)serializer.Deserialize(streamReader);

            return input;
        }



    }
}