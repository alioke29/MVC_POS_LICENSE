namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_merchant_data
    {
        [Key]
        public int MerchantID { get; set; }

        [Required]
        [StringLength(150)]
        public string MerchantKey { get; set; }

        [Required]
        [StringLength(30)]
        public string MerchantCode { get; set; }

        [Required]
        [StringLength(300)]
        public string MerchantName { get; set; }

        [StringLength(300)]
        public string IPaddress { get; set; }

        [StringLength(100)]
        public string DatabaseName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }
    }
}
