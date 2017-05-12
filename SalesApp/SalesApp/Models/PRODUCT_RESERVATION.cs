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
    
    public partial class PRODUCT_RESERVATION
    {
        public int Id { get; set; }
        public Nullable<int> ProductCalenderId { get; set; }
        public Nullable<int> AgentId { get; set; }
        public Nullable<int> PaymentTypeId { get; set; }
        public Nullable<int> AgencyId { get; set; }
        public string Description { get; set; }
        public Nullable<int> PaxId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> PaymentStatusId { get; set; }
        public Nullable<int> ProductReservationTypeId { get; set; }
        public Nullable<System.DateTime> BloqueoDate { get; set; }
        public Nullable<int> TourDaysId { get; set; }
        public Nullable<int> DivePrice { get; set; }
        public Nullable<int> DiveId { get; set; }
        public string PickUp { get; set; }
        public string DropOff { get; set; }
        public string Restrictions { get; set; }
        public Nullable<bool> Continua { get; set; }
    
        public virtual AGENCY AGENCY { get; set; }
        public virtual AGENT AGENT { get; set; }
        public virtual PAX PAX { get; set; }
        public virtual PAYMENT_TYPE PAYMENT_TYPE { get; set; }
        public virtual PRODUCT_CALENDER PRODUCT_CALENDER { get; set; }
        public virtual PRODUCT_CALENDERHOUR PRODUCT_CALENDERHOUR { get; set; }
        public virtual PAYMENT_STATUS PAYMENT_STATUS { get; set; }
        public virtual PRODUCT_RESERVATION_TYPE PRODUCT_RESERVATION_TYPE { get; set; }
        public virtual TOUR_DAYS TOUR_DAYS { get; set; }
        public virtual DIVE DIVE { get; set; }
    }
}
