namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_temp_payment_detail
    {
        [Key]
        public int IdTempPaymentDetail { get; set; }

        [Required]
        [StringLength(100)]
        public string SessionId { get; set; }

        public int IdPaymentTypeItem { get; set; }

        public DateTime? datetime { get; set; }

        [StringLength(100)]
        public string CreditCardNo { get; set; }

        [StringLength(100)]
        public string CreditCardHolderName { get; set; }

        public decimal? Nominal { get; set; }

        public decimal? DoPayment { get; set; }

        public decimal? PromoPayment { get; set; }
    }
}
