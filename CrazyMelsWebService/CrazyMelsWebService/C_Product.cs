//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class C_Product
    {
        public C_Product()
        {
            this.C_Cart = new HashSet<C_Cart>();
        }
    
        public int prodID { get; set; }
        public string prodName { get; set; }
        public double price { get; set; }
        public double prodWeight { get; set; }
        public bool inStock { get; set; }
    
        public virtual ICollection<C_Cart> C_Cart { get; set; }
    }
}
