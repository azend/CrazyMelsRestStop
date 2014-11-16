using CrazyMelsWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrazyMelsWebService.Controllers
{
    public class CartController : ApiController
    {
        private CrazyMelEntities db = new CrazyMelEntities();
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

           

        }
    }
}
