﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrazyMelsWebService
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CrazyMelEntities : DbContext
    {
        public CrazyMelEntities()
            : base("name=CrazyMelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<C_Cart> C_Cart { get; set; }
        public DbSet<C_Customer> C_Customer { get; set; }
        public DbSet<C_Order> C_Order { get; set; }
        public DbSet<C_Product> C_Product { get; set; }
    }
}
