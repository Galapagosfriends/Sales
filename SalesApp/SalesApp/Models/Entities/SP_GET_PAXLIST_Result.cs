//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace SalesApp.Models.Entities
{

    public class SP_GET_PAXLIST_Result
    {
        [Key]
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string First_Name { get; set; }
        public string Passport { get; set; }
        public string Agencia { get; set; }
        public string Agente { get; set; }
        public string PAGO { get; set; }
        public string Last_Name { get; set; }
    }
}