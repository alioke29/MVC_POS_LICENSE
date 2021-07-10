namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_order_payment_detail
    {
        [Key]
        public int IdOrderPaymentDetail { get; set; }

        public int IdOrderPaymentComplete { get; set; }

        [StringLength(50)]
        public string sessionId { get; set; }

        public int IdPaymentTypeItem { get; set; }

        [StringLength(20)]
        public string PayTypeCode { get; set; }

        [StringLength(150)]
        public string PayTypeName { get; set; }

        public int MasterShopID { get; set; }

        public int ShopID { get; set; }

        public DateTime? datetime { get; set; }

        [StringLength(100)]
        public string CreditCardNo { get; set; }

        [StringLength(100)]
        public string CreditCardHolderName { get; set; }

        public decimal? Nominal { get; set; }

        public decimal? DoPayment { get; set; }

        public int? isDelete { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? isVoid { get; set; }

        public DateTime? VoidDate { get; set; }

        public int? VoidBy { get; set; }
    }
}
