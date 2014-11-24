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

        //TODO: CustomerController, Get, ADD, Search functions beyond get all scenario.
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

        // PUT api/Customer/5 (UPDATE)
        public IHttpActionResult PutCustomer(Customer customer)
        {
            C_Customer updateCustomer = customer.ToC_Customer();
            C_Customer currentCustomer = searchCustomer(customer);

            if (currentCustomer == null)
            {
                return NotFound();
            }

           // C_Customer completeCustomer;

            if (!Validation.IsValid.MergeEntries(currentCustomer, updateCustomer))
            {
                return BadRequest();
            }

           // currentCustomer = completeCustomer;
            db.Entry(currentCustomer).State = EntityState.Modified;
            
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);

        }



        // POST api/Customer
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostC_Customer(Customer customer)
        {
            C_Customer newCustomer = customer.ToC_Customer();
            if(!Validation.IsValid.NewEntry(newCustomer))
            {
                return BadRequest("Invalid New Entry");
            }
                                   
            db.C_Customer.Add(newCustomer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newCustomer.custID },new Customer(newCustomer));
        }

        // DELETE api/Customer/5
        [ResponseType(typeof(Customer))]
        [Route("api/customer/{*input}")]
        public IHttpActionResult DeleteCustomer(string input)       // Customer customerToDelete)
  

{
            
            Char parameterDelimiter = '/';
            Char valueDelimiter = '=';

            String[] parameters = input.Split(new Char[] { parameterDelimiter });

            SortedList<String, String> paramValues = new SortedList<string, string>();

            foreach (String a in parameters)
            {
                String[] temp = a.Split(new Char[] { valueDelimiter });
                if (temp.Length == 2)
                {
                    paramValues.Add(temp[0], temp[1]);
                }
                else if (temp.Length == 1)
                {
                    paramValues.Add(temp[0], String.Empty);
                }
                else
                {
                    return BadRequest();
                }

            }

            
            Customer customerToDelete = new Customer();

            if(paramValues.ContainsKey("custID"))
            {
                if(String.IsNullOrWhiteSpace(paramValues["custID"]))
                {
                    customerToDelete.custID = 0;

                }
                else
                {
                    Int32 tempInt;
                    if (Int32.TryParse(paramValues["custID"], out tempInt))
                    {
                        customerToDelete.custID = tempInt;
                    }
                    else
                    {
                        return BadRequest();
                    }
                     
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
            

         /*   Customer customerToDelete = new Customer();

            customerToDelete.custID = custID;
            customerToDelete.firstName = firstName;
          
                customerToDelete.lastName = lastName;

                customerToDelete.phoneNumber = phoneNumber;
            
            */



            
            C_Customer c_customer = searchCustomer(customerToDelete);
            if (c_customer == null)
            {
                return NotFound();
            }

            IQueryable<C_Order> custOrderDate = from orders in db.C_Order
                                                where orders.custID == c_customer.custID
                                                select orders;


            foreach (C_Order a in custOrderDate)
            {
                IQueryable<C_Cart> cartData = from carts in db.C_Cart where carts.orderID == a.orderID select carts;
                foreach (C_Cart cart in cartData)
                {
                    db.C_Cart.Remove(cart);
                }
                db.C_Order.Remove(a);
            }

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
