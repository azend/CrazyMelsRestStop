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


    }
}
