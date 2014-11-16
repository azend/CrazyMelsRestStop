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

            foreach(C_Customer bob in returnValue)
            {
                Customer mike = new Customer();
                mike.custID = bob.custID;
                mike.firstName = bob.firstName;
                mike.lastName = bob.lastName;
                mike.phoneNumber = bob.phoneNumber;
                data.Add(mike);
            }
            

            return data.ToArray();

        }
    }
}
