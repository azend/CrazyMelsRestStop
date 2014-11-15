using CrazyMelsRestService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CrazyMelsRestService.DAL
{
    public class CrazyMelsRestServiceContext : DbContext
    {
        public CrazyMelsRestServiceContext()
            : base("CrazyMelsRestServiceContext")
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}