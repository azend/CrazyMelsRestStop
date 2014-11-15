using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrazyMelsRestService.Models
{
    public class Product
    {
        public int prodId { get; set; }
        public string prodName { get; set; }
        public float price { get; set; }
        public float prodWeight { get; set; }
        public bool inStock { get; set; }
    }
}