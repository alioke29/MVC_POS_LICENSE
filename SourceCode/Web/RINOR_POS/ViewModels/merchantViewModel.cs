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
    public class merchantViewModel
    {
        [Key]
        public int MerchantID { get; set; }

        [StringLength(150)]
        [Display(Name = "Merchant Key Number")]
        public string MerchantKey { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "Merchant Code")]
        public string MerchantCode { get; set; }

        [StringLength(300)]
        [Required]
        [Display(Name = "Merchant Name")]
        public string MerchantName { get; set; }

        [StringLength(300)]
        [Display(Name = "IP Address")]
        public string IPaddress { get; set; }

        [StringLength(100)]
        [Display(Name = "Database Name")]
        public string DatabaseName { get; set; }
    }
}