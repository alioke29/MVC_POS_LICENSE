namespace RINOR_POS.ModelLicence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GCS_LICENSE_DEV
    {
        [Key]
        public int LicenceDataID { get; set; }

        public int MerchantID { get; set; }

        [Required]
        [StringLength(100)]
        public string MerchantKey { get; set; }

        public int BrandID { get; set; }

        [Required]
        [StringLength(100)]
        public string BrandKey { get; set; }

        public int ShopID { get; set; }

        [Required]
        [StringLength(100)]
        public string ShopKey { get; set; }

        public int LicenceType { get; set; }

        [StringLength(50)]
        public string DeviceKey { get; set; }

        [StringLength(50)]
        public string LicenceKey { get; set; }

        public DateTime? LicenceStart { get; set; }

        public DateTime? LicenceFinish { get; set; }

        public DateTime? TenggoStart { get; set; }

        public DateTime? TenggoFinish { get; set; }

        public int? Period { get; set; }

        [Required]
        [StringLength(50)]
        public string BuyerEmail { get; set; }

        [Required]
        [StringLength(50)]
        public string BuyerMobileNo { get; set; }

        public DateTime? Reminder1 { get; set; }

        public DateTime? Reminder2 { get; set; }

        public DateTime? Reminder3 { get; set; }

        public DateTime? Reminder4 { get; set; }

        public DateTime? Reminder5 { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool? isActive { get; set; }
    }
}
