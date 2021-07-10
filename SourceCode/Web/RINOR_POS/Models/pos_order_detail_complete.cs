namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_order_detail_complete
    {
        [Key]
        public int idOrderDetailComplete { get; set; }

        public int idOrderComplete { get; set; }

        [Required]
        [StringLength(50)]
        public string sessionId { get; set; }

        [StringLength(50)]
        public string SessionKey { get; set; }

        public int MasterShopId { get; set; }

        public int ShopId { get; set; }

        public int? SaleModeID { get; set; }

        public DateTime? Datetime { get; set; }

        public int? ProductId { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        public int? Qty { get; set; }

        public decimal? Price { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? SubTotalVAT { get; set; }

        [StringLength(50)]
        public string DiscountType { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public decimal? DiscountValue { get; set; }

        public int? PromotionID { get; set; }

        public decimal? PromotionValue { get; set; }

        public decimal? PromotionPercentage { get; set; }

        public decimal? VAT { get; set; }

        public decimal? ServiceCharge { get; set; }

        [StringLength(50)]
        public string ModifierText { get; set; }

        public int? isDelete { get; set; }

        public int? isVoid { get; set; }

        public int? VoidBy { get; set; }

        [StringLength(100)]
        public string Reason { get; set; }

        [StringLength(100)]
        public string RowPackageProduct { get; set; }
    }
}
