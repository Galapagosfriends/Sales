//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HR_Roles_Anticipos
    {
        public int Id { get; set; }
        public int RollDateId { get; set; }
        public int PayTypeId { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> PayDate { get; set; }
        public int Estado { get; set; }
        public string PayTypeNumber { get; set; }
    }
}
