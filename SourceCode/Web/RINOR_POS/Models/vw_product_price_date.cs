namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_product_price_date
    {
        [Key]
        public int ProductPriceDateID { get; set; }

        [StringLength(63)]
        public string ValidDate { get; set; }
    }
}
