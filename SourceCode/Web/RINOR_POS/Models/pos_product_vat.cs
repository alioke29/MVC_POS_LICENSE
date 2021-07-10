namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_product_vat
    {
        [Key]
        public int ProductVATID { get; set; }

        [Required]
        [StringLength(30)]
        public string ProductVATCode { get; set; }

        public int? ProductVATPercent { get; set; }

        [StringLength(30)]
        public string VATDisplay { get; set; }

        [StringLength(300)]
        public string VATDesp { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
