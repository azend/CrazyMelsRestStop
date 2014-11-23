using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrazyMelsClient
{
    public class Order
    {
        public Order()
        {
            custID = 0;
            orderDate = new DateTime();
            orderID = 0;
            poNumber = String.Empty;
        }
    
        public int orderID { get; set; }
        public int custID { get; set; }
        public string poNumber { get; set; }
        public System.DateTime orderDate { get; set; }
    
    }
}