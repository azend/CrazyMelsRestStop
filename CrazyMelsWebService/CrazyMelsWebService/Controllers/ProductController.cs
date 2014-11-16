using CrazyMelsWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CrazyMelsWebService.Controllers
{
    public class ProductController : ApiController
    {

        private CrazyMelEntities db = new CrazyMelEntities();
        public Product[] Get()
        {
           List<Product> data = new List<Product>();

           IQueryable<C_Product> returnValue = from mine in db.C_Product
                                              select mine;

            foreach (C_Product prod in returnValue)
            {
                data.Add(new Product(prod));
            }

            return data.ToArray();

        }

        // DELETE api/Order/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            C_Product c_product = db.C_Product.Find(id);
            if (c_product == null)
            {
                return NotFound();
            }

            db.C_Product.Remove(c_product);

            foreach (C_Cart cart in db.C_Cart.Where(c => c.prodID == id))
            {
                db.C_Cart.Remove(cart);
            }

            db.SaveChanges();

            Product p = new Product();
            p.prodID = c_product.prodID;
            p.prodName = c_product.prodName;
            p.prodWeight = c_product.prodWeight;
            p.price = p.price;
            p.inStock = c_product.inStock;

            return Ok(p);
        }
    }
}
