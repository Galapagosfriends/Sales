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
    
    public class HR_Roles_DateRole
    {
        public int Id { get; set; }
        public System.DateTime DateMonth { get; set; }
        public int WorkerId { get; set; }
        public int Estado { get; set; }
        public string Autorizado_Por { get; set; }
        public string CerradoPor { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public System.DateTime Updated { get; set; }
    }
}
