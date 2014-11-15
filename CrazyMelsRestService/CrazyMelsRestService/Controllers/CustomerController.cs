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
    public class CustomerController : ApiController
    {
        private CrazyMelsRestServiceEntities db = new CrazyMelsRestServiceEntities();

        // GET api/Customer
        public IQueryable<C_Customer> GetC_Customer()
        {
            return db.C_Customer;
        }

        // GET api/Customer/5
        [ResponseType(typeof(C_Customer))]
        public IHttpActionResult GetC_Customer(int id)
        {
            C_Customer c_customer = db.C_Customer.Find(id);
            if (c_customer == null)
            {
                return NotFound();
            }

            return Ok(c_customer);
        }

        // PUT api/Customer/5
        public IHttpActionResult PutC_Customer(int id, C_Customer c_customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != c_customer.custID)
            {
                return BadRequest();
            }

            db.Entry(c_customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!C_CustomerExists(id))
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

        // POST api/Customer
        [ResponseType(typeof(C_Customer))]
        public IHttpActionResult PostC_Customer(C_Customer c_customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.C_Customer.Add(c_customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = c_customer.custID }, c_customer);
        }

        // DELETE api/Customer/5
        [ResponseType(typeof(C_Customer))]
        public IHttpActionResult DeleteC_Customer(int id)
        {
            C_Customer c_customer = db.C_Customer.Find(id);
            if (c_customer == null)
            {
                return NotFound();
            }

            db.C_Customer.Remove(c_customer);
            db.SaveChanges();

            return Ok(c_customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool C_CustomerExists(int id)
        {
            return db.C_Customer.Count(e => e.custID == id) > 0;
        }
    }
}