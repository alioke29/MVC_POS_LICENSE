namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_orderdetail_byprinter
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idOrderDetailComplete { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idOrderComplete { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string sessionId { get; set; }

        [StringLength(50)]
        public string SessionKey { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MasterShopId { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShopId { get; set; }

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

        public decimal? VAT { get; set; }

        public double? ServiceCharge { get; set; }

        [StringLength(50)]
        public string ModifierText { get; set; }

        public int? isDelete { get; set; }

        public int? PrinterTypeID { get; set; }

        [StringLength(10)]
        public string PrintDesign { get; set; }

        public int? PrinterID { get; set; }

        [StringLength(100)]
        public string PrinterName { get; set; }
    }
}
