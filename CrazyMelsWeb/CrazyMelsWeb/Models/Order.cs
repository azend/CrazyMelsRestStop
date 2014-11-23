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

        public C_Order ToC_Order(Order order)
        {
            C_Order c_order = new C_Order();

            c_order.orderID = order.orderID;
            c_order.custID = order.custID;
            c_order.orderDate = order.orderDate;
            c_order.poNumber = order.poNumber;

            return c_order;
        }

        public int orderID { get; set; }
        public int custID { get; set; }
        public string poNumber { get; set; }
        public System.DateTime orderDate { get; set; }
    
    }
}