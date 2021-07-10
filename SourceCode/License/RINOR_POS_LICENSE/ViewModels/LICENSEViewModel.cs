using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RINOR_POS;

namespace RINOR_POS.ModelLicence
{
    /// <summary>
    /// License View Model
    /// </summary>
    public class LICENSEViewModel
    {
        public LICENSEViewModel()
        {
            merchant_list = new List<pos_merchant_data>();
            brand_list = new List<pos_brand_data>();
            MasterShop_list = new List<pos_shop_data>();
            shop_list = new List<pos_shop_data>();
            licencetype_list = new List<POS_Licence_Type>();
            computer_list = new List<pos_computer_name>();
        }

        public List<pos_merchant_data> merchant_list { get; set; }
        public List<pos_brand_data> brand_list { get; set; }
        public List<pos_shop_data> MasterShop_list { get; set; }
        public List<pos_shop_data> shop_list { get; set; }
        public List<POS_Licence_Type> licencetype_list { get; set; }

        public int? LicenceDataID { get; set; }

        [Required]
        [Display(Name = "Merchant")]
        public int MerchantID { get; set; }
        public string MerchantName { get; set; }
        
        [StringLength(100)]
        [Display(Name = "Merchant Key")]
        public string MerchantKey { get; set; }

        [Required]
        [Display(Name = "Brand")]
        public int BrandID { get; set; }
        public string BrandName { get; set; }

        [StringLength(100)]
        [Display(Name = "Brand Key")]
        public string BrandKey { get; set; }

        [Required]
        [Display(Name = "Shop")]
        public int ShopID { get; set; }

        [Display(Name = "Master Shop")]
        public int MasterShopID { get; set; }

        public string ShopName { get; set; }

        [StringLength(100)]
        [Display(Name = "Shop Key")]
        public string ShopKey { get; set; }

        [Required]
        [Display(Name = "Licence Type")]
        public int? LicenceType { get; set; }
        public string LicenceTypeName { get; set; }

        [StringLength(50)]
        [Display(Name = "Device Key")]
        public string DeviceKey { get; set; }

        [Display(Name = "Device Name")]
        public string DeviceName { get; set; }
        public List<pos_computer_name> computer_list { get; set; }

        [StringLength(50)]
        [Display(Name = "Licence Key")]
        public string LicenceKey { get; set; }

        [Required]
        [Display(Name = "Licence Start")]
        public DateTime? LicenceStart { get; set; }

        [Required]
        [Display(Name = "Licence Finish")]
        public DateTime? LicenceFinish { get; set; }

        public DateTime? TenggoStart { get; set; }

        public DateTime? TenggoFinish { get; set; }

        [Display(Name = "Period")]
        public int? Period { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Buyer Email")]
        public string BuyerEmail { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Phone Number")]
        public string BuyerMobileNo { get; set; }

        public DateTime? Reminder1 { get; set; }

        public DateTime? Reminder2 { get; set; }

        public DateTime? Reminder3 { get; set; }

        public DateTime? Reminder4 { get; set; }

        public DateTime? Reminder5 { get; set; }

        [Display(Name = "Status")]
        public bool? isActive { get; set; }
    }
}