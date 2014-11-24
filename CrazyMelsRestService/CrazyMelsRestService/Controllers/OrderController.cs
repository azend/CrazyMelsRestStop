using CrazyMelsRestService.Models;
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

namespace CrazyMelsRestService.Controllers
{
    public class OrderController : ApiController
    {

        private CrazyMelsEntities db = new CrazyMelsEntities();

        //TODO: OrderController, GET(Search), ADD, Missing search functions beyond get all scenario.


        /// <summary>
        /// Queries the database and returns all rows from the data table;
        /// </summary>
        /// <returns>Array of Order objects</returns>
       
        [Route("api/order")]
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
        } //End Get();

        [Route("api/order")]
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
        [Route("api/order")]
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

        /// <summary>
        /// Deletes an entry in the database 
        /// determined by the String input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("api/order/{*input}")]
        public IHttpActionResult DeleteOrder(String input)
        {
            SortedList<String, String> paramValues;
            Int32 oid;
            C_Order orderToDelete = null;

            try
            {
                paramValues = Parsing.parseInputValuePairs(input);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (paramValues.ContainsKey("oID") && Int32.TryParse(paramValues["oID"], out oid))
            {
                if (oid == 0)
                {
                    orderToDelete = null;
                }
                else
                {
                    orderToDelete = db.C_Order.Find(oid);
                    if (orderToDelete == null)
                    {
                        return BadRequest("Order oid does not exist");
                    }
                }
            }

            if(orderToDelete == null)
            {
                if(paramValues.ContainsKey("custID") && paramValues.ContainsKey("orderDate"))
                {
                    Int32 tempCustID;
                    DateTime tempOrderDate;
                    if(Int32.TryParse(paramValues["custID"], out tempCustID) && DateTime.TryParse(paramValues["orderDate"], out tempOrderDate) )
                    {
                        IQueryable<C_Order> tempList = db.C_Order.Where(data=> data.custID == tempCustID && data.orderDate == tempOrderDate);
                        if(tempList.Count() == 1)
                        {
                            orderToDelete = tempList.First();
                        }
                        else if( tempList.Count() == 0)
                        {
                            orderToDelete = null;
                        }
                        else
                        {
                            return BadRequest("Too many results, refine delete");
                        }
                    }
                    else
                    {
                        return BadRequest("Invalid custID or orderDate");
                    }
                }
            }
                        
            if (orderToDelete == null)
            {
                return NotFound();
            }

            IQueryable<C_Cart> cartsToDelete = db.C_Cart.Where(data => data.orderID == orderToDelete.orderID);
            foreach(C_Cart data in cartsToDelete)
            {
                db.C_Cart.Remove(data);
            }
            
            
            
            db.C_Order.Remove(orderToDelete);
            db.SaveChanges();

            return Ok(new Order(orderToDelete));
        }

        private bool C_OrderExists(int oid)
        {
            return db.C_Order.Count(e => e.orderID == oid) > 0;
        }
    }
}
