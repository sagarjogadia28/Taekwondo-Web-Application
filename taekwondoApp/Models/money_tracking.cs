//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace taekwondoApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class money_tracking
    {
        public int student_id { get; set; }
        public int inventory_id { get; set; }
        public System.DateTime date_of_purchase { get; set; }
        public decimal amount { get; set; }
    
        public virtual inventory inventory { get; set; }
        public virtual student student { get; set; }
    }
}
