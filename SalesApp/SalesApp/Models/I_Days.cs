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
    
    public partial class I_Days
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public I_Days()
        {
            this.I_Itinerary = new HashSet<I_Itinerary>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lang { get; set; }
        public int DayCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<I_Itinerary> I_Itinerary { get; set; }
    }
}
