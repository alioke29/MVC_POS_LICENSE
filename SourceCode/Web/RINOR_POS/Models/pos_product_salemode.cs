namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_product_salemode
    {
        [Key]
        public int ProductSaleModeID { get; set; }

        public int ProductID { get; set; }

        public int SaleModeID { get; set; }

        public bool? Activate { get; set; }
    }
}
