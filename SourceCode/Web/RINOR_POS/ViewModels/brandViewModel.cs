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
    public class brandViewModel
    {
        public brandViewModel()
        {
            merchant_list = new List<pos_merchant_data>();
        }

        public virtual pos_brand_data pos_brand_data { get; set; }

        [Key]
        public int BrandID { get; set; }

        [StringLength(100)]
        [Display(Name = "Brand Key Number")]
        public string BrandKey { get; set; }

        [Required]
        [Display(Name = "Merchant")]
        public int MerchantID { get; set; }
        public virtual pos_merchant_data pos_merchant_data { get; set; }
        //for Dropdown
        public List<pos_merchant_data> merchant_list { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Brand Code")]
        public string BrandCode { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [StringLength(50)]
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        [Display(Name = "Activate")]
        public bool? Activate { get; set; }

    }
}