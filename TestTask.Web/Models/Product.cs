using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TestTask.Web.Models
{
    public class Product
    {
        [XmlAttribute("id")]
        public string Id { get; set; }


        public string Title { get; set; }


        public string Description { get; set; }


        public string Image { get; set; }


        public double Price { get; set; }

        public string Availability { get; set; }

        public Sorting Sorting { get; set; }

        [XmlArray(ElementName = "Specs")]
        [XmlArrayItem(ElementName = "Spec")]
        public List<string> Specs { get; set; }
    }



    public class Specification
    {
        public string Spec { get; set; }
    }


    [XmlRoot("Store")]
    public class Store
    {

        [XmlArray(ElementName = "Products")]
        [XmlArrayItem(ElementName = "Product")]
        public List<Product> Products { get; set; }
    }

    public class Sorting
    {
        [XmlElement("Popular")]
        public int Popular { get; set; }
    }
}