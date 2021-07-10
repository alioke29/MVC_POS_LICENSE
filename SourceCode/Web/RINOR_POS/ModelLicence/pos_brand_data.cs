namespace RINOR_POS.ModelLicence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_brand_data
    {
        [Key]
        public int BrandID { get; set; }

        [Required]
        [StringLength(100)]
        public string BrandKey { get; set; }

        public int MerchantID { get; set; }

        [Required]
        [StringLength(20)]
        public string BrandCode { get; set; }

        [Required]
        [StringLength(50)]
        public string BrandName { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }

        public bool? Activate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }
    }
}
