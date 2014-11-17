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
    public class CustomerController : ApiController
    {
        private CrazyMelEntities db = new CrazyMelEntities();

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

        // DELETE api/Customer/5
        [ResponseType(typeof(C_Customer))]
        public IHttpActionResult DeleteCustomer(int id)
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


    }
}
