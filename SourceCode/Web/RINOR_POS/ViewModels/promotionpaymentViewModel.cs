using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RINOR_POS.Models
{
    public class promotionpaymentViewModel
    {
        public promotionpaymentViewModel()
        {
            PaymentTypeList = new List<pos_payment_type>();
            payment_selected = new List<string>();
        }

        public int PromotionPaymentID { get; set; }

        [Display(Name = "Promotion")]
        public int PromotionID { get; set; }
        public string PromotionName { get; set; }
        public string PromotionTypeName { get; set; }

        public int PromotionTypeID { get; set; }

        [Display(Name = "Payment Type")]
        public int PayTypeID { get; set; }
        public List<pos_payment_type> PaymentTypeList { get; set; }
        public List<string> payment_selected { get; set; }

        [Display(Name = "Discount Amount")]
        public decimal? DiscountAmount { get; set; }

        [Display(Name = "Discount Percentage")]
        public decimal? DiscountPercentage { get; set; }

        [Display(Name = "Minimum Sub Total Before VAT")]
        public decimal? MinimumSubTotalBeforeVAT { get; set; }

        [Display(Name = "Minimum Pay Amount After VAT")]
        public decimal? MinimumPayAmountAfterVAT { get; set; }

        [Display(Name = "Maximum Discount Amount")]
        public decimal? MaximumDiscountAmount { get; set; }

        [Display(Name = "Minimum Pcs")]
        public int? MinimumPcs { get; set; }

        [Display(Name = "Maximum Pcs")]
        public int? MaximumPcs { get; set; }

        [Display(Name = "Activate")]
        public bool? IsActive { get; set; }
    }
}