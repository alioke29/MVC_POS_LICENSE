namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_receipt_template
    {
        [Key]
        public int ReceiptTemplateID { get; set; }

        public int MasterShopID { get; set; }

        public int ShopID { get; set; }

        [Required]
        [StringLength(10)]
        public string Position { get; set; }

        [Required]
        [StringLength(200)]
        public string Text { get; set; }

        public int? Ordering { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? isActive { get; set; }
    }
}
