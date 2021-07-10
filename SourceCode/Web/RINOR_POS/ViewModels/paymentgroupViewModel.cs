using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RINOR_POS.Models
{
    public class paymentgroupViewModel
    {
        public paymentgroupViewModel()
        {

        }

        [Key]
        public int PayGroupID { get; set; }

        [StringLength(50)]
        [Display(Name = "Payment Group Name")]
        public string PayGroupName { get; set; }

        public int? Ordering { get; set; }

        public bool? Activate { get; set; }

        [Display(Name = "Icon Button")]
        public string IconButtonServer { get; set; }

        [Display(Name = "Icon Button")]
        public string IconButton { get; set; }
    }
}