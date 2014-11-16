using CrazyMelsWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrazyMelsWebService.Controllers
{
    public class OrderController : ApiController
    {

        private CrazyMelEntities db = new CrazyMelEntities();
        public Order Get()
        {
            IQueryable<C_Order> returnValue = from mine in db.C_Order
                                                 select mine;

            C_Order bob = returnValue.First();

            Order mike = new Order();

            mike.custID = (int)bob.custID;
            mike.orderDate = bob.orderDate;
            mike.orderID = bob.orderID;
            mike.poNumber = bob.poNumber;
            return mike;

        }
    }
}
