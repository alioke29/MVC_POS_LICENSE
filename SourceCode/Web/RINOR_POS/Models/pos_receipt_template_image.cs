namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_receipt_template_image
    {
        [Key]
        public int ReceiptTemplateImageID { get; set; }

        public int MasterShopID { get; set; }

        public int ShopID { get; set; }

        [StringLength(2000)]
        public string LogoHeaderFile { get; set; }

        [StringLength(500)]
        public string QRFooterText { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
