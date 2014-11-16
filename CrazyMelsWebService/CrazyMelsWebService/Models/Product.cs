using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrazyMelsWebService.Models
{
    public class Product
    {
        public Product(C_Product rawInput)
        {
        prodID = rawInput.prodID;             
        prodName = rawInput.prodName;
        price = rawInput.price;
        prodWeight = rawInput.prodWeight;
        inStock = rawInput.inStock;

        }
        public Product()
        {
            prodID = 0;
            prodName = String.Empty;
            price = 0;
            prodWeight = 0;
            inStock = false;
        }



        
        public int prodID { get; set; }
        public string prodName { get; set; }
        public double price { get; set; }
        public double prodWeight { get; set; }
        public bool inStock { get; set; }
    }
}