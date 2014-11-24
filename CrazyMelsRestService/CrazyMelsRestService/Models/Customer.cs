using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrazyMelsRestService.Models
{
    public class Customer : CrazyMelDataModel
    {


        public Customer (C_Customer rawData)
        {
            custID = rawData.custID;
            firstName = rawData.firstName;
            lastName = rawData.lastName;
            phoneNumber = rawData.phoneNumber;
        }

        public Customer()
        {
            custID = 0;
            firstName = String.Empty;
            lastName = String.Empty;
            phoneNumber = String.Empty;
        }

        public C_Customer ToC_Customer()
        {
            C_Customer converted = new C_Customer();

            converted.custID = custID;
            converted.firstName = firstName;
            converted.lastName = lastName;
            converted.phoneNumber = phoneNumber;

            return converted;
        }

        public int custID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }


        
    }
}