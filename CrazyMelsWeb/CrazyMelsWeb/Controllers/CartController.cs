using CrazyMelsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrazyMelsWeb.Controllers
{
    public class CartController : ApiController
    {
        private CrazyMelsRestServiceEntities db = new CrazyMelsRestServiceEntities();

        //TODO: CustomerController, GET(Search), ADD, Missing search functions beyond get all scenario.
        public Cart[] Get()
        {
            List<Cart> data = new List<Cart>();

            IQueryable<C_Cart> returnValue = from mine in db.C_Cart
                                                select mine;

            foreach (C_Cart car in returnValue)
            {
                data.Add(new Cart(car));
            }

            return data.ToArray();

           //TODO: CartController, DELETE, Add, Function Not Implemented
            //TODO: CartController, PUT(Update), Add, Function Not Implemented
            //TODO: CartController, POST(Insert), Add, Function Not Implemented

        }
    }
}
