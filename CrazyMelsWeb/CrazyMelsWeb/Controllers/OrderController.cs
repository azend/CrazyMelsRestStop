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

        //TODO: OrderController, DELETE, Add, Function Not Implemented
        //TODO: OrderController, PUT(Update), Add, Function Not Implemented
        //TODO: OrderController, POST(Insert), Add, Function Not Implemented


        //TODO: OrderController, General, Add, Comments
    }
}
