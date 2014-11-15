using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrazyMelsRestService.Models
{
    public class Customer
    {
        public int custId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }

        public Customer()
        {
            custId = -1;
            firstName = String.Empty;
            lastName = String.Empty;
            phoneNumber = String.Empty;
        }

        public Customer(int custId, string firstName, string lastName, string phoneNumber)
        {
            this.custId = custId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
        }
    }

    public class CustomerRepository
    {
        public List<Customer> customers { get; set; }

        public CustomerRepository()
        {
            customers = new List<Customer>();

            GenerateSampleData();
        }

        private void GenerateSampleData() {
            for (int i = 0; i < 30; i++)
            {
                customers.Add(new Customer(i, "First", "Last", "555-555-5555"));
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return customers;
        }

        public Customer GetCustomer(int customerId)
        {
            Customer cust = null;

            if (customerId >= 0 && customerId <= customers.Count())
            {
                cust = customers[customerId];
            }

            return cust;
        }

    }
}