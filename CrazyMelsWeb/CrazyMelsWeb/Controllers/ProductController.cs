using CrazyMelsWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CrazyMelsWeb.Controllers
{
    public class ProductController : ApiController
    {
        private CrazyMelsRestServiceEntities db = new CrazyMelsRestServiceEntities();

        //TODO: CustomerController, Get, ADD, Search functions beyond get all scenario.
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

        // DELETE api/Product/5
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

            return Ok(new Product(c_product));
        }


        // PUT api/Product/5
        public IHttpActionResult PutProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (product == null)
            {
                return BadRequest();
            }

            C_Product c_product = null;

            c_product = db.C_Product.Find(product.prodID);

            if (c_product == null)
            {
                IQueryable<C_Product> products = db.C_Product.Where(p => p.prodName == product.prodName);

                if (products.Count() > 1)
                {
                    //TODO: Throw an error here
                    NotFound();
                }
                else
                {
                    c_product = products.First();
                }
            }

            if (c_product != null)
            {
                c_product.prodName = product.prodName;
                c_product.prodWeight = product.prodWeight;
                c_product.price = product.price;
                c_product.inStock = product.inStock;

                db.SaveChanges();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Product
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            C_Product c_product = product.ToC_Product();
            db.C_Product.Add(c_product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = c_product.prodID }, product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //TODO: CustomerController, General, Add, Comments
    }
}
