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
    
    public partial class SP_Insert_PaxDefault_Result
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string Passport { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> AdressId { get; set; }
        public Nullable<int> Gender { get; set; }
        public string Email { get; set; }
        public Nullable<int> PaxCategory { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
    }
}