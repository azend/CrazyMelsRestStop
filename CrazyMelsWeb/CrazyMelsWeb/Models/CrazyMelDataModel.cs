using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CrazyMelsWeb.Models
{
    [KnownType(typeof(Cart))]
    [KnownType(typeof(Order))]
    [KnownType(typeof(Product))]
    [KnownType(typeof(Customer))]
    public abstract class CrazyMelDataModel
    {
        
        public CrazyMelDataModel()
        {

        }
    }
}