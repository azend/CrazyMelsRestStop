using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrazyMelsWeb.Models
{
    public class Product : CrazyMelDataModel
    {
        public Product(C_Product rawInput)
        {
            prodID = rawInput.prodID;             
            prodName = rawInput.prodName;
            price = rawInput.price;
            prodWeight = rawInput.prodWeight;
            //inStock = Convert.ToBoolean( rawInput.inStock);

        }
        public Product()
        {
            prodID = 0;
            prodName = String.Empty;
            price = 0;
            prodWeight = 0;
            inStock = false;
        }

        public C_Product ToC_Product()
        {
            C_Product c_product = new C_Product();
            c_product.prodID = prodID;
            c_product.prodName = prodName;
            c_product.prodWeight = prodWeight;
            c_product.price = price;
            c_product.inStock = inStock;

            return c_product;
        }

        
        public int prodID { get; set; }
        public string prodName { get; set; }
        public double price { get; set; }
        public double prodWeight { get; set; }
        public bool inStock { get; set; }
    }
}