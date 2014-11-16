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
        public Cart Get()
        {
            IQueryable<C_Cart> returnValue = from mine in db.C_Cart
                                                select mine;

            C_Cart bob = returnValue.First();

            Cart mike = new Cart();

            mike.orderID = bob.orderID;
            mike.prodID = bob.prodID;
            mike.quantity = bob.quantity;
            return mike;

        }
    }
}
