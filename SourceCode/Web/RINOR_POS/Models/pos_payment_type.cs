namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_payment_type
    {
        [Key]
        public int PayTypeID { get; set; }

        public int PayGroupId { get; set; }

        [Required]
        [StringLength(150)]
        public string PayTypeName { get; set; }

        [Required]
        [StringLength(20)]
        public string PayTypeCode { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        public int? Ordering { get; set; }

        [StringLength(300)]
        public string IconButtonServer { get; set; }

        [StringLength(3000)]
        public string IconButton { get; set; }

        public bool? Activate { get; set; }

        [StringLength(100)]
        public string EDCType { get; set; }

        public decimal? MinimumPay { get; set; }

        public decimal? MaximumPay { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
