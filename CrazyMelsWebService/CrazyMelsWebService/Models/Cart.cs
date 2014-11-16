using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrazyMelsWebService.Models
{
    public class Cart
    {
        public int orderID { get; set; }
        public int prodID { get; set; }
        public int quantity { get; set; }
    }
}