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

        [ResponseType(typeof(C_Cart))]
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

        public IHttpActionResult PutC_Cart(int oid, C_Cart c_cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (oid != c_cart.orderID)
            {
                return BadRequest();
            }

            db.Entry(c_cart).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!C_CartExists(oid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult PostC_Cart(C_Cart c_cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
