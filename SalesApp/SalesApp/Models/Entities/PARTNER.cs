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

    [Table("PARTNER")]
    public class PARTNER
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> TourID { get; set; }
        public Nullable<int> AdresseID { get; set; }
        public Nullable<int> PriceID { get; set; }

        [ForeignKey("AdresseID")]
        public virtual ADRESS ADRESS { get; set; }
    }
}
