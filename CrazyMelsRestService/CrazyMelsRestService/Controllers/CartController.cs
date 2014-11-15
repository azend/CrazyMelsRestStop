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
using CrazyMelsRestService;

namespace CrazyMelsRestService.Controllers
{
    public class CartController : ApiController
    {
        private CrazyMelsRestServiceEntities db = new CrazyMelsRestServiceEntities();

        // GET api/Cart
        public IQueryable<C_Cart> GetC_Cart()
        {
            return db.C_Cart;
        }

        // GET api/Cart/5
        [ResponseType(typeof(C_Cart))]
        public IHttpActionResult GetC_Cart(int id)
        {
            C_Cart c_cart = db.C_Cart.Find(id);
            if (c_cart == null)
            {
                return NotFound();
            }

            return Ok(c_cart);
        }

        // PUT api/Cart/5
        public IHttpActionResult PutC_Cart(int id, C_Cart c_cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != c_cart.orderID)
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
                if (!C_CartExists(id))
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

        // POST api/Cart
        [ResponseType(typeof(C_Cart))]
        public IHttpActionResult PostC_Cart(C_Cart c_cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.C_Cart.Add(c_cart);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (C_CartExists(c_cart.orderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = c_cart.orderID }, c_cart);
        }

        // DELETE api/Cart/5
        [ResponseType(typeof(C_Cart))]
        public IHttpActionResult DeleteC_Cart(int id)
        {
            C_Cart c_cart = db.C_Cart.Find(id);
            if (c_cart == null)
            {
                return NotFound();
            }

            db.C_Cart.Remove(c_cart);
            db.SaveChanges();

            return Ok(c_cart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool C_CartExists(int id)
        {
            return db.C_Cart.Count(e => e.orderID == id) > 0;
        }
    }
}