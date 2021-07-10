namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_temp_order_promo
    {
        [Key]
        public int TempOrderPromoID { get; set; }

        [Required]
        [StringLength(50)]
        public string SessionID { get; set; }

        public int? ProductID { get; set; }

        public int PromotionID { get; set; }

        [StringLength(50)]
        public string PromotionCode { get; set; }

        public int? DiscountPercent { get; set; }

        public decimal? DiscountPrice { get; set; }

        [StringLength(20)]
        public string CountOn { get; set; }
    }
}
