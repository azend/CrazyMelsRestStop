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
        public Product[] Get()
        {
           List<Product> data = new List<Product>();

           IQueryable<C_Product> returnValue = from mine in db.C_Product
                                              select mine;

            foreach (C_Product prod in returnValue)
            {
                data.Add(new Product(prod));
            }

            return data.ToArray();





        }
    }
}
