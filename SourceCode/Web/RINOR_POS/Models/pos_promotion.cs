namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_promotion
    {
        [Key]
        public int PromotionID { get; set; }

        public int PromotionType { get; set; }

        public int? MasterShopID { get; set; }

        public int? ShopID { get; set; }

        [Required]
        [StringLength(30)]
        public string PromotionCode { get; set; }

        [Required]
        [StringLength(100)]
        public string PromotionName { get; set; }

        public decimal? PromotionPrice { get; set; }

        public decimal? PromotionVAT { get; set; }

        public decimal? PromotionServiceCharge { get; set; }

        [StringLength(20)]
        public string EffectTo { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime ExpiredDate { get; set; }

        public TimeSpan TimeStart { get; set; }

        public TimeSpan TimeFinish { get; set; }

        public int? NoPrintCopy { get; set; }

        public int? Priority { get; set; }

        [StringLength(200)]
        public string WeeklyCondition { get; set; }

        [StringLength(100)]
        public string DayCondition { get; set; }

        public bool? PrintSignature { get; set; }

        public bool? IsAutomation { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }
    }
}
