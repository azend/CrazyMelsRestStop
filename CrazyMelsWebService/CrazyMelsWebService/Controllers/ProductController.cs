using CrazyMelsWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrazyMelsWebService.Controllers
{
    public class ProductController : ApiController
    {

        private CrazyMelEntities db = new CrazyMelEntities();
        public Product Get()
        {
            IQueryable<C_Product> returnValue = from mine in db.C_Product
                                              select mine;

            C_Product bob = returnValue.First();

            Product mike = new Product();

            mike.inStock = bob.inStock;
            mike.price = bob.price;
            mike.prodID = bob.prodID;
            mike.prodName = bob.prodName;
            mike.prodWeight = bob.prodWeight;
            return mike;

        }
    }
}
