using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SalesApp.Models.Entities
{
    [Table("V_GetPaxList")]
    public class V_GetPaxList
    {
        [Key]
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Passport { get; set; }
        public string Agencia { get; set; }
        public string Agente { get; set; }
        public string PAGO { get; set; }
        public Nullable<int> ProductCalenderId { get; set; }
    }
}
