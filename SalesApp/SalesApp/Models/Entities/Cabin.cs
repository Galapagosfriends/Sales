using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesApp.Models.Entities
{
    [Table("CABIN")]
    public class Cabin
    {
         public Cabin()
        {
            this.PRODUCT_RESERVATION = new HashSet<PRODUCT_RESERVATION>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryProductId { get; set; }

        [ForeignKey("CategoryProductId")]
        public virtual CATEGORY_PRODUCT CATEGORY_PRODUCT { get; set; }

        public virtual ICollection<PRODUCT_RESERVATION> PRODUCT_RESERVATION { get; set; }
    }
}