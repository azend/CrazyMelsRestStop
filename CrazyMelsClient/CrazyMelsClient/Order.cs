using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrazyMelsClient
{
    public class Order : CrazyMelDataModel
    {
        public int orderID { get; set; }
        public int custID { get; set; }
        public string poNumber { get; set; }
        public System.DateTime orderDate { get; set; }
    
    }
}