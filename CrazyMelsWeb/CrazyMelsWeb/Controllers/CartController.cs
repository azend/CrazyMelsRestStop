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
        public IHttpActionResult DeleteC_Cart(int oid, int pid)
        {
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

            //TODO: change instock  in the Product table to a boolean and uncomment code below
            //C_Product c_product = db.C_Product.Find(c_cart.prodID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            /*if (c_product.inStock == false)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Cart cannot be updated because the product is currently out of stock!"));
            }*/
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

            //TODO: change instock  in the Product table to a boolean and uncomment code below
            //C_Product c_product = db.C_Product.Find(c_cart.prodID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            /*if (c_product.inStock == false)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "The Product cannot be inserted into the cart because it is out of stock!"));
            }*/

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
