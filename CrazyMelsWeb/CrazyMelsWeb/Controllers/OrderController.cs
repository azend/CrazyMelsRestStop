using CrazyMelsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrazyMelsWeb.Controllers
{
    public class OrderController : ApiController
    {

        //private CrazyMelEntities db = new CrazyMelEntities();
        private CrazyMelsRestServiceEntities db = new CrazyMelsRestServiceEntities();
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
    }
}
