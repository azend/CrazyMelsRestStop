//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrazyMelsWeb
{
    using System;
    using System.Collections.Generic;
    
    public partial class C_Cart
    {
        public int orderID { get; set; }
        public int prodID { get; set; }
        public int quantity { get; set; }
    
        public virtual C_Order C_Order { get; set; }
        public virtual C_Product C_Product { get; set; }
    }
}
