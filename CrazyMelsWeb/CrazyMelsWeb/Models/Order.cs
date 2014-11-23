using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrazyMelsWeb.Models
{
    public class Order : CrazyMelDataModel
    {
        public Order (C_Order rawInput)
        {
            custID = (int)rawInput.custID;
            orderDate = rawInput.orderDate;
            orderID = rawInput.orderID;
            poNumber = rawInput.poNumber;
        }
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