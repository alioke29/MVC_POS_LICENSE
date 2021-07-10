namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_promotion_payment
    {
        [Key]
        public int PromotionPaymentID { get; set; }

        public int PromotionID { get; set; }

        public int PromotionTypeID { get; set; }

        public int PayTypeID { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public decimal? MinimumSubTotalBeforeVAT { get; set; }

        public decimal? MinimumPayAmountAfterVAT { get; set; }

        public decimal? MaximumDiscountAmount { get; set; }

        public int? MinimumPcs { get; set; }

        public int? MaximumPcs { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }
    }
}
