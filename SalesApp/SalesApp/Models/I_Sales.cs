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
    
    public partial class I_Sales
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int SalesGroupId { get; set; }
        public int PaxId { get; set; }
        public int ItineraryId { get; set; }
        public string FlightIn { get; set; }
        public string FlightOut { get; set; }
        public Nullable<System.DateTime> Dates { get; set; }
        public string Comment { get; set; }
        public Nullable<decimal> PrecioNett { get; set; }
        public Nullable<decimal> PrecioNettTotalPax { get; set; }
    
        public virtual I_Agent I_Agent { get; set; }
        public virtual I_Itinerary I_Itinerary { get; set; }
        public virtual PAX PAX { get; set; }
        public virtual I_SalesGroup I_SalesGroup { get; set; }
    }
}