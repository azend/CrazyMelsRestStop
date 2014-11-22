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
    public class OrderController : ApiController
    {
        private CrazyMelsRestServiceEntities db = new CrazyMelsRestServiceEntities();

        // GET api/Order
        public IQueryable<C_Order> GetC_Order()
        {
            return db.C_Order;
        }

        // GET api/Order/5
        [ResponseType(typeof(C_Order))]
        public IHttpActionResult GetC_Order(int id)
        {
            C_Order c_order = db.C_Order.Find(id);
            if (c_order == null)
            {
                return NotFound();
            }

            return Ok(c_order);
        }

        // PUT api/Order/5
        public IHttpActionResult PutC_Order(int id, C_Order c_order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != c_order.orderID)
            {
                return BadRequest();
            }

            db.Entry(c_order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!C_OrderExists(id))
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

        // POST api/Order
        [ResponseType(typeof(C_Order))]
        public IHttpActionResult PostC_Order(C_Order c_order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.C_Order.Add(c_order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = c_order.orderID }, c_order);
        }

        // DELETE api/Order/5
        [ResponseType(typeof(C_Order))]
        public IHttpActionResult DeleteC_Order(int id)
        {
            C_Order c_order = db.C_Order.Find(id);
            if (c_order == null)
            {
                return NotFound();
            }

            db.C_Order.Remove(c_order);
            db.SaveChanges();

            return Ok(c_order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool C_OrderExists(int id)
        {
            return db.C_Order.Count(e => e.orderID == id) > 0;
        }
    }
}