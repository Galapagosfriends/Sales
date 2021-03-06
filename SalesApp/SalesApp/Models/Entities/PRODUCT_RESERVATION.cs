//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations.Schema;
using SalesApp.Common;

namespace SalesApp.Models.Entities
{
    
    [Table("PRODUCT_RESERVATION")]
    public class PRODUCT_RESERVATION
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
        public int ProductReservationTypeId { get; set; }
        public Nullable<System.DateTime> BloqueoDate { get; set; }
        public Nullable<int> TourDaysId { get; set; }
        public Nullable<decimal> DivePrice { get; set; }
        public Nullable<int> DiveId { get; set; }
        public string PickUp { get; set; }
        public string DropOff { get; set; }
        public int? CabinId { get; set; }

        public string Restrictions { get; set; }
        public Nullable<bool> Continua { get; set; }

        [ForeignKey("CabinId")]
        public virtual Cabin CABIN { get; set; }

        [ForeignKey("AgencyId")]
        public virtual AGENCY AGENCY { get; set; }

        [ForeignKey("AgentId")]
        public virtual AGENT AGENT { get; set; }

        [ForeignKey("PaxId")]
        public virtual Pax PAX { get; set; }

        [ForeignKey("PaymentTypeId")]
        public virtual PAYMENT_TYPE PAYMENT_TYPE { get; set; }

        [ForeignKey("ProductCalenderId")]
        public virtual PRODUCT_CALENDER PRODUCT_CALENDER { get; set; }

        [ForeignKey("ProductCalenderId")]
        public virtual PRODUCT_CALENDERHOUR PRODUCT_CALENDERHOUR { get; set; }

        [ForeignKey("PaymentStatusId")]
        public virtual PAYMENT_STATUS PAYMENT_STATUS { get; set; }

        public string FacturaNr { get; set; }

        public Nullable<decimal> NetoPrice { get; set; }

        public string UserName { get; set; }

        public string PC_ID { get; set; }

       //[NotMapped]
       //private PRODUCT_RESERVATION_TYPE _PRODUCT_RESERVATION_TYPE;

       //public virtual PRODUCT_RESERVATION_TYPE PRODUCT_RESERVATION_TYPE
       //{
       //    get
       //    {
       //        if (_PRODUCT_RESERVATION_TYPE == null) { return new _PRODUCT_RESERVATION_TYPE() { Id = 1 }; }
       //        return _PRODUCT_RESERVATION_TYPE;
       //    }
       //    set { _PRODUCT_RESERVATION_TYPE = value; }
       //  }

       [ForeignKey("ProductReservationTypeId")]
        public virtual PRODUCT_RESERVATION_TYPE PRODUCT_RESERVATION_TYPE {get; set;}

        [ForeignKey("TourDaysId")]
        public virtual TOUR_DAYS TOUR_DAYS { get; set; }

        [ForeignKey("DiveId")]
        public virtual DIVE DIVE { get; set; }
        [NotMapped]
        public virtual CuantaVeces CuantaVeces { get; set; }
    }
}
