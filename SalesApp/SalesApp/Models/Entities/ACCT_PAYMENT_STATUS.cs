//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesApp.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ACCT_PAYMENT_STATUS")]
    public class ACCT_PAYMENT_STATUS
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LangId { get; set; }
        public int PaymentStatusId { get; set; }
    }
}
