namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_product_combo_link
    {
        [Key]
        public int ProductComboLinkID { get; set; }

        public int ProductID { get; set; }

        public int ProductComboID { get; set; }
    }
}
