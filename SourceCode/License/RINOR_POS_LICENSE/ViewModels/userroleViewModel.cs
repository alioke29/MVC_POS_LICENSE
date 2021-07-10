using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RINOR_POS;
using System.Web.Mvc;

namespace RINOR_POS.ModelLicence
{
    public class userroleViewModel
    {
        public userroleViewModel()
        {
            merchant_list = new List<pos_merchant_data>();
            brand_list = new List<pos_brand_data>();
            shop_list = new List<pos_shop_data>();
            employee_list = new List<pos_employee>();
            rolemenu_list = new List<vw_role_menu>();
            menu_list = new List<vw_menu_app_list>();
        }

        [Key]
        public int UserRoleID { get; set; }

        [Display(Name = "Merchant")]
        public int MerchantId { get; set; }
        public string MerchantName { get; set; }
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        [Required]
        [Display(Name = "Shop")]
        public int ShopID { get; set; }
        public string ShopName { get; set; }
        public List<pos_merchant_data> merchant_list { get; set; }
        public List<pos_brand_data> brand_list { get; set; }
        public List<pos_shop_data> shop_list { get; set; }
        public List<pos_employee> employee_list { get; set; }
        public List<vw_role_menu> rolemenu_list { get; set; }
        public List<vw_menu_app_list> menu_list { get; set; }

        [StringLength(50)]
        public string Role { get; set; }
    }
}