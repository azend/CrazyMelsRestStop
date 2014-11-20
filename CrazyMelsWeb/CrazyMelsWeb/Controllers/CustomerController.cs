using CrazyMelsWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CrazyMelsWeb.Controllers
{
    public class CustomerController : ApiController
    {
       // private CrazyMelEntities db = new CrazyMelEntities();
        private CrazyMelsRestServiceEntities db = new CrazyMelsRestServiceEntities();
        public Customer[] Get()
        {
            List<Customer> data = new List<Customer>();
            IQueryable<C_Customer> returnValue = from mine in db.C_Customer
                              select mine;

            foreach(C_Customer cust in returnValue)
            {
                data.Add(new Customer(cust));
            }
            return data.ToArray();

        }

        // PUT api/Customer/5
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.custID)
            {
                return BadRequest();
            }

            C_Customer c_customer = new C_Customer();
            c_customer.custID = customer.custID;
            c_customer.firstName = customer.firstName;
            c_customer.lastName = customer.lastName;
            c_customer.phoneNumber = customer.phoneNumber;

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
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostC_Customer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            C_Customer c_customer = new C_Customer();
            c_customer.custID = customer.custID;
            c_customer.firstName = customer.firstName;
            c_customer.lastName = customer.lastName;
            c_customer.phoneNumber = customer.phoneNumber;

            db.C_Customer.Add(c_customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = c_customer.custID }, customer);
        }

        // DELETE api/Customer/5
        [ResponseType(typeof(C_Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            C_Customer c_customer = db.C_Customer.Find(id);
            if (c_customer == null)
            {
                return NotFound();
            }

            IQueryable<C_Order> custOrderDate = from orders in db.C_Order
                                                where orders.custID == id
                                                select orders;

            
            foreach(C_Order a in custOrderDate)
            {
                IQueryable<C_Cart> cartData = from carts in db.C_Cart where carts.orderID == a.orderID select carts;
                foreach (C_Cart cart in cartData)
                {
                    db.C_Cart.Remove(cart);
                }
                db.C_Order.Remove(a);
            }

            db.C_Customer.Remove(c_customer);
            db.SaveChanges();

            return Ok(c_customer);
        }

        private bool C_CustomerExists(int id)
        {
            return db.C_Customer.Count(e => e.custID == id) > 0;
        }


    }
}
