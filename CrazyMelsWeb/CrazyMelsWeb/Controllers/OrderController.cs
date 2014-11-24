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
    public class OrderController : ApiController
    {

        private CrazyMelsEntities db = new CrazyMelsEntities();

        //TODO: OrderController, GET(Search), ADD, Missing search functions beyond get all scenario.
        public Order[] Get()
        {
            List<Order> data = new List<Order>();

            IQueryable<C_Order> returnValue = from mine in db.C_Order
                                                 select mine;

            foreach (C_Order ord in returnValue)
            {
                data.Add(new Order(ord));
            }

            return data.ToArray();


        }

        public IHttpActionResult PutOrder(Order order)
        {
            C_Order c_order = order.ToC_Order(order);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (db.C_Order.Find(c_order.orderID) == null)
            {
                return NotFound();
            }

            db.Entry(c_order).State = EntityState.Modified;

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            C_Order c_order = order.ToC_Order(order);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (db.C_Customer.Find(c_order.custID) == null)
            {
                return BadRequest("Invalid CustomerId");
            }
          
            db.Entry(c_order).State = EntityState.Added;

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteOrder(int oid)
        {
            C_Order c_order = db.C_Order.Find(oid);

            if (c_order == null)
            {
                return NotFound();
            }

            db.C_Order.Remove(c_order);
            db.SaveChanges();

            return Ok(c_order);
        }

        private bool C_OrderExists(int oid)
        {
            return db.C_Order.Count(e => e.orderID == oid) > 0;
        }
    }
}
