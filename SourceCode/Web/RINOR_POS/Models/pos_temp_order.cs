namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_temp_order
    {
        [Key]
        public int idTempOrder { get; set; }

        [Required]
        [StringLength(50)]
        public string sessionId { get; set; }

        [Required]
        [StringLength(50)]
        public string computerID { get; set; }

        public int MasterShopID { get; set; }

        public int ShopID { get; set; }

        public int SaleModeId { get; set; }

        public int UserID { get; set; }

        public long? OrderNo { get; set; }

        [StringLength(100)]
        public string AcceptedListPromotionID { get; set; }

        public DateTime? datetime { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

        [StringLength(50)]
        public string CustomerTableNo { get; set; }

        public int? TotalCustomer { get; set; }

        public int? TotalQty { get; set; }

        public decimal? TotalDiscount { get; set; }

        public decimal? TotalPromotion { get; set; }

        public int? BillDiscSaleModeID { get; set; }

        public decimal? BillDiscount { get; set; }

        [StringLength(50)]
        public string BillDiscountType { get; set; }

        public decimal? TotalServiceCharge { get; set; }

        public decimal? OtherIncome { get; set; }

        public decimal? TotalVAT { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? GrandTotal { get; set; }

        public decimal? Rounding { get; set; }

        public decimal? PayAmount { get; set; }

        public decimal? PriceBeforeVAT { get; set; }

        public int? isPrint { get; set; }

        public int? isPay { get; set; }
    }
}
