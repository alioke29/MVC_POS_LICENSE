using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RINOR_POS;

namespace RINOR_POS.Models
{
    public class promotionViewModel
    {
        public promotionViewModel()
        {
            PromotionTypeList = new List<pos_promotion_type>();
            day_selected = new List<string>();
        }

        [Key]
        public int PromotionID { get; set; }
        [Display(Name = "Promotion Type")]
        public int PromotionType { get; set; }
        public string PromotionTypeName { get; set; }
        public List<pos_promotion_type> PromotionTypeList { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Promotion Code")]
        public string PromotionCode { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Promotion Name")]
        public string PromotionName { get; set; }

        [Display(Name = "Begin Date")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Expired Date")]
        public DateTime ExpiredDate { get; set; }

        [Display(Name = "Promotion Price")]
        public decimal? PromotionPrice { get; set; }

        [Display(Name = "Promotion VAT")]
        public decimal? PromotionVAT { get; set; }

        [Display(Name = "Promotion Service Charge")]
        public decimal? PromotionServiceCharge { get; set; }

        [StringLength(20)]
        [Display(Name = "Effecte To")]
        public string EffectTo { get; set; }

        [Display(Name = "Time Criteria")]
        public TimeSpan TimeStart { get; set; }

        public TimeSpan TimeFinish { get; set; }

        [Display(Name = "No Print Receipt Copy")]
        public int? NoPrintCopy { get; set; }

        [Display(Name = "Priority")]
        public int? Priority { get; set; }

        [Display(Name = "Period Condition")]
        public int? PeriodCondition { get; set; }

        [StringLength(200)]
        [Display(Name = "Weekly Condition")]
        public string WeeklyCondition { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }

        [StringLength(100)]
        [Display(Name = "Day Condition")]
        public string DayCondition { get; set; }
        public List<string> day_selected { get; set; }

        [Display(Name = "Print Signature In Receipt")]
        public bool? PrintSignature { get; set; }

        [Display(Name = "Discount Automation")]
        public bool? IsAutomation { get; set; }

        [Display(Name = "Activate")]
        public bool IsActive { get; set; }
    }
}