using CrazyMelsWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CrazyMelsWeb.Controllers
{
    public class CustomerController : ApiController
    {

        private CrazyMelsEntities db = new CrazyMelsEntities();

        /// <summary>
        /// Queries the database and returns all rows from the customer datatable;
        /// </summary>
        /// <returns>Array of Customer objects</returns>
        /// GET api/Customer (GET All)
        [Route("api/customer")]
        public Customer[] Get()
        {
            List<Customer> data = new List<Customer>();
            IQueryable<C_Customer> returnValue = from mine in db.C_Customer
                                                 select mine;

            foreach (C_Customer cust in returnValue)
            {
                data.Add(new Customer(cust));
            }
            return data.ToArray();
        }

        
        /// <summary>
        /// Updates an instance of customer.
        /// </summary>
        /// <param name="customer">The customer to update</param>
        /// <returns>httpActionResult</returns>
        /// PUT api/Customer/5 (UPDATE)
         [Route("api/customer")]
        public IHttpActionResult PutCustomer(Customer customer)
        {
             //TODO: Only works if we have customer ID or phone number or (first and last name)
            C_Customer updateCustomer = customer.ToC_Customer();
            C_Customer currentCustomer = searchCustomer(customer);

            if (currentCustomer == null)
            {
                return NotFound();
            }
                    
            if (!Validation.IsValid.MergeEntries(currentCustomer, updateCustomer))
            {
                return BadRequest();
            }

            db.Entry(currentCustomer).State = EntityState.Modified;

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }



        // POST api/Customer
        [ResponseType(typeof(Customer))]
        [Route("api/customer")]
        public IHttpActionResult PostC_Customer(Customer customer)
        {
            C_Customer newCustomer = customer.ToC_Customer();
            if (!Validation.IsValid.NewEntry(newCustomer))
            {
                return BadRequest("Invalid New Entry");
            }

            db.C_Customer.Add(newCustomer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newCustomer.custID }, new Customer(newCustomer));
        }

        // DELETE api/Customer/5
        [ResponseType(typeof(Customer))]

        [Route("api/customer/{*input}")]

        public IHttpActionResult DeleteCustomer(string input)
        {
            SortedList<String, String> paramValues;
            try
            {
                paramValues = Parsing.parseInputValuePairs(input);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            Customer customerToDelete = new Customer();

            if (paramValues.ContainsKey("custID"))
            {
                Int32 tempCustID;
                if (Int32.TryParse(paramValues["custID"], out tempCustID))
                {
                    customerToDelete.custID = tempCustID;
                }
                else
                {
                    return BadRequest("Invalid custID indicated");
                }
            }

            if (paramValues.ContainsKey("firstName"))
            {
                customerToDelete.firstName = paramValues["firstName"];
            }
            if (paramValues.ContainsKey("lastName"))
            {
                customerToDelete.lastName = paramValues["lastName"];
            }
            if (paramValues.ContainsKey("phoneNumber"))
            {
                customerToDelete.phoneNumber = paramValues["phoneNumber"];
            }

            C_Customer c_customer = searchCustomer(customerToDelete);
            if (c_customer == null)
            {
                return NotFound();
            }

            IQueryable<C_Order> custOrderData = db.C_Order.Where(orders => orders.custID == c_customer.custID);

            foreach (C_Order a in custOrderData)
            {
                IQueryable<C_Cart> cartData = db.C_Cart.Where(carts => carts.orderID == a.orderID);
                db.C_Cart.RemoveRange(cartData);
            }

            db.C_Order.RemoveRange(custOrderData);
            db.C_Customer.Remove(c_customer);
            db.SaveChanges();

            return Ok(new Customer(c_customer));
        }


        private C_Customer searchCustomer(Customer userInput)
        {
            IQueryable<C_Customer> searchResults = null;
            if (userInput.custID > 0)
            {
                return db.C_Customer.Find(userInput.custID);
            }

            if (userInput.phoneNumber != null && userInput.phoneNumber != String.Empty)
            {
                searchResults = db.C_Customer.Where(Cust => Cust.phoneNumber == userInput.phoneNumber);
                if (searchResults.Count() == 1)
                {
                    return searchResults.First();
                }
                else if (searchResults.Count() > 1)
                {
                    IQueryable<C_Customer> secondarySearch = null;

                    if (userInput.firstName != null && userInput.firstName != String.Empty)
                    {
                        secondarySearch = searchResults.Where(Cust => Cust.firstName == userInput.firstName);

                        if (secondarySearch.Count() == 1)
                        {
                            return secondarySearch.First();
                        }
                    }

                    if (userInput.lastName != null && userInput.lastName != String.Empty)
                    {
                        secondarySearch = searchResults.Where(Cust => Cust.lastName == userInput.lastName);
                        if (secondarySearch.Count() == 1)
                        {
                            return secondarySearch.First();
                        }
                    }
                }
            }

            if (userInput.firstName != null && userInput.firstName != String.Empty && userInput.lastName != null && userInput.lastName != String.Empty)
            {
                searchResults = db.C_Customer.Where(Cust => Cust.firstName == userInput.firstName && Cust.lastName == userInput.lastName);
                if (searchResults.Count() == 1)
                {
                    return searchResults.First();
                }
            }

            return null;
        }

        private bool C_CustomerExists(int id)
        {
            return db.C_Customer.Count(e => e.custID == id) > 0;
        }


    }
}
