using CrazyMelsWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrazyMelsWebService.Controllers
{
    public class CustomerController : ApiController
    {
        private CrazyMelEntities db = new CrazyMelEntities();

        public Customer[] Get()
        {
            List<Customer> data = new List<Customer>();
            IQueryable<C_Customer> returnValue = from mine in db.C_Customer
                              select mine;

            foreach(C_Customer cust in returnValue)
            {
                data.Add(new Customer(cust));
            }
            return data.ToArray();

        }
    }
}
