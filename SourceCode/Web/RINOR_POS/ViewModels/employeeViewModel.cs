using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RINOR_POS;
using System.Web.Mvc;

namespace RINOR_POS.Models
{
    public class employeeViewModel
    {
        public employeeViewModel()
        {
            merchant_list = new List<pos_merchant_data>();
            brand_list = new List<pos_brand_data>();
            shop_list = new List<pos_shop_data>();
        }

        [Key]
        [Display(Name = "Employee")]
        public int employee_id { get; set; }

        [StringLength(20)]
        [Display(Name = "Employee NIK")]
        public string employee_nik { get; set; }

        [Display(Name = "Merchant")]
        public int MerchantId { get; set; }
        public string MerchantName { get; set; }
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
        [Required]
        [Display(Name = "Shop")]
        public int ShopID { get; set; }
        [Display(Name = "Shop Name")]
        public string ShopName { get; set; }
        public List<pos_merchant_data> merchant_list { get; set; }
        public List<pos_brand_data> brand_list { get; set; }
        public List<pos_shop_data> shop_list { get; set; }

        [StringLength(50)]
        [Display(Name = "Employee Name")]
        public string employee_name { get; set; }

        [StringLength(50)]
        [Display(Name = "Employee Email")]
        public string employee_email { get; set; }

        [Display(Name = "Activate")]
        public bool? fl_active { get; set; }
    }
}