namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_order_payment_complete
    {
        [Key]
        public int IdOrderPaymentComplete { get; set; }

        [StringLength(50)]
        public string sessionId { get; set; }

        public int IdPaymentTypeItem { get; set; }

        public int MasterShopID { get; set; }

        public int ShopID { get; set; }

        public int IdUser { get; set; }

        [Required]
        [StringLength(50)]
        public string CashierName { get; set; }

        [Required]
        [StringLength(100)]
        public string ComputerID { get; set; }

        public DateTime datetime { get; set; }

        public decimal PayAmount { get; set; }

        public decimal TotalPay { get; set; }

        public decimal? CashChange { get; set; }

        public bool? isPaid { get; set; }

        public DateTime? InsertedDate { get; set; }

        public int? InsertedBy { get; set; }

        public int? deleted { get; set; }
    }
}
