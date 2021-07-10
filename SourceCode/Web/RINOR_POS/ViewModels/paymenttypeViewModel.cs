using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using RINOR_POS.Models;

namespace RINOR_POS.Models
{
    public class paymenttypeViewModel
    {
        public paymenttypeViewModel()
        {
            payment_group_list = new List<pos_payment_group>();
        }

        [Key]
        public int PayTypeID { get; set; }

        [Display(Name = "Payment Group Name")]
        public int PayGroupId { get; set; }
        public string PayGroupName { get; set; }

        public List<pos_payment_group> payment_group_list { get; set; }

        [StringLength(150)]
        [Display(Name = "Payment Type")]
        public string PayTypeName { get; set; }

        [StringLength(20)]
        [Display(Name = "Payment Type Code")]
        public string PayTypeCode { get; set; }

        [StringLength(50)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        public int? Ordering { get; set; }

        [Display(Name = "VAT Available")]
        public bool? isVat { get; set; }

        [Display(Name = "Has Service Charge")]
        public bool? isServiceCharge { get; set; }

        public bool? Activate { get; set; }

        [Display(Name = "Icon")]
        public string IconButtonServer { get; set; }

        [Display(Name = "Icon")]
        public string IconButton { get; set; }
    }
}