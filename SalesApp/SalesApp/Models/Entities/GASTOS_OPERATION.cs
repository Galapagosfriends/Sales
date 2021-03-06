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
    using System.Linq;

    [Table("GASTOS_OPERATION")]
    public class GASTOS_OPERATION
    {
        public int Id { get; set; }
        public Nullable<int> ProductCalenderId { get; set; }
        public string Description { get; set; }
        public Nullable<int> GastosTypeProviderId { get; set; }
        public Nullable<System.DateTime> PayDate { get; set; }
        public string BillingPath { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<bool> Closed { get; set; }
        public Nullable<decimal> Price { get; set; }

        [ForeignKey("GastosTypeProviderId")]
        public virtual GASTOS_TYPE_PROVIDER GASTOS_TYPE_PROVIDER { get; set; }

      
        public PRODUCT_CALENDER getProductCalender( int? ProductCalenderId)
        {
            return new Galadventure_TrabajosEntities().PRODUCT_CALENDER.Where(s => s.Id == ProductCalenderId).First();
        }
    }
}
