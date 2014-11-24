using CrazyMelsWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CrazyMelsWeb.Controllers
{
    public class CartController : ApiController
    {
        private CrazyMelsEntities db = new CrazyMelsEntities();

        public Cart[] Get()
        {
            List<Cart> data = new List<Cart>();

            IQueryable<C_Cart> returnValue = from mine in db.C_Cart
                                                select mine;

            foreach (C_Cart car in returnValue)
            {
                data.Add(new Cart(car));
            }

            return data.ToArray();

        }

        [ResponseType(typeof(Cart))]
        [Route("api/cart/{*input}")]
        public IHttpActionResult DeleteC_Cart(String input) //int oid, int pid)
        {
           

            Char parameterDelimiter = '/';
            Char valueDelimiter = '=';

            String[] parameters = input.Split(new Char[] { parameterDelimiter });

            SortedList<String, String> paramValues = new SortedList<string, string>();

            foreach (String a in parameters)
            {
                String[] temp = a.Split(new Char[] { valueDelimiter });
                if (temp.Length == 2)
                {
                    paramValues.Add(temp[0], temp[1]);
                }
                else if (temp.Length == 1)
                {
                    paramValues.Add(temp[0], String.Empty);
                }
                else
                {
                    return BadRequest();
                }

            }

            Int32 oid;
            Int32 pid;


            if (paramValues.ContainsKey("pID") && paramValues.ContainsKey("oID"))
            {
                if (!(Int32.TryParse(paramValues["pID"], out pid)))
                {
                    return BadRequest();
                }

                if (!(Int32.TryParse(paramValues["oID"], out oid)))
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
                        

            C_Cart c_cart = db.C_Cart.Find(oid, pid);

            if (c_cart == null)
            {
                return NotFound();
            }
            
            db.C_Cart.Remove(c_cart);
            db.SaveChanges();

            return Ok(c_cart);
        }

        public IHttpActionResult PutC_Cart(Cart cart)
        {
            C_Cart c_cart = new C_Cart();
            c_cart = cart.CartToC_Cart(cart);

            C_Product c_product = db.C_Product.Find(c_cart.prodID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (c_product.inStock == false)
            {
                return BadRequest("Cart cannot be updated because the product is currently out of stock!");
            }
            if (db.C_Cart.Find(c_cart.orderID) == null)
            {
                return NotFound();
            }

            db.Entry(c_cart).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                    throw e;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult PostC_Cart(Cart cart)
        {
            C_Cart c_cart = new C_Cart();
            c_cart = cart.CartToC_Cart(cart);

            C_Product c_product = db.C_Product.Find(c_cart.prodID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (db.C_Product.Find(c_cart.prodID) == null)
            {
                return BadRequest("Invalid ProductId");
            }
            if (c_product.inStock == false)
            {
                return BadRequest("The Product cannot be inserted into the cart because it is out of stock!");
            }

            db.Entry(c_cart).State = EntityState.Added;

            try
            {
                db.SaveChanges();
            }
            catch (DBConcurrencyException e)
            {
                throw e;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool C_CartExists(int oid)
        {
            return db.C_Cart.Count(e => e.orderID == oid) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
