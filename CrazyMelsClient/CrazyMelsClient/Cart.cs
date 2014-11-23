using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrazyMelsClient
{
    public class Cart
    {
        public Cart()
        {
            orderID = 0;
            prodID = 0;
            quantity = 0;
        }


        public int orderID { get; set; }
        public int prodID { get; set; }
        public int quantity { get; set; }
    }
}