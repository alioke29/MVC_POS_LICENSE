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
    public class shopViewModel
    {
        public shopViewModel()
        {
            brand_list = new List<pos_brand_data>();
            merchant_list = new List<pos_merchant_data>();
            shop_list = new List<pos_shop_data>();
            Province_list = new List<pos_province>();
            city_list = new List<pos_city>();
            region_list = new List<pos_region>();
        }

        [Key]
        public int ShopID { get; set; }

        [Display(Name = "Merchant Name")]
        public int MerchantID { get; set; }
        public virtual pos_merchant_data pos_merchant_data { get; set; }
        public List<pos_merchant_data> merchant_list { get; set; }

        [Display(Name = "Brand Name")]
        public int BrandID { get; set; }
        public virtual pos_brand_data pos_brand_data { get; set; }
        public List<pos_brand_data> brand_list { get; set; }

        public List<pos_province> Province_list { get; set; }
        public List<pos_city> city_list { get; set; }
        public List<pos_region> region_list { get; set; }

        [StringLength(10)]
        [Required]
        [Display(Name = "Shop Code")]
        public string ShopCode { get; set; }
        public virtual pos_shop_data pos_shop_data { get; set; }
        public List<pos_shop_data> shop_list { get; set; }

        [StringLength(150)]
        [Display(Name = "Shop Key")]
        public string ShopKey { get; set; }

        [StringLength(150)]
        [Required]
        [Display(Name = "Shop Name")]
        public string ShopName { get; set; }

        [Display(Name = "Is Shop")]
        public bool? IsShop { get; set; }

        [Display(Name = "Is Inventory")]
        public bool? IsInv { get; set; }

        [Display(Name = "Has Service Charge")]
        public bool? HasSC { get; set; }

        public int? IsSCBeforeDisc { get; set; }

        public decimal? SCPercent { get; set; }

        [StringLength(10)]
        public string VATCode { get; set; }

        public int? VATType { get; set; }

        [Display(Name = "Master Shop")]
        public bool? MasterShop { get; set; }

        [Display(Name = "Master Shop Link")]
        public int? MasterShopLink { get; set; }

        public int? ShowInReport { get; set; }

        public int? ShopTypeID { get; set; }

        public int? ShopCatID1 { get; set; }

        public int? ShopCatID2 { get; set; }

        public int? ShopCatID3 { get; set; }

        public int? ShopCatID4 { get; set; }

        public int? ShopCatID5 { get; set; }

        public int? ShopCatID6 { get; set; }

        public int? ShopCatID7 { get; set; }

        public int? ShopCatID8 { get; set; }

        public int? ShopCatID9 { get; set; }

        public int? ShopCatID10 { get; set; }

        [Display(Name = "Open Hour")]
        public TimeSpan? OpenHour { get; set; }

        [Display(Name = "Close Hour")]
        public TimeSpan? CloseHour { get; set; }

        [StringLength(100)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [StringLength(255)]
        [Display(Name = "Company Address 1")]
        public string CompanyAddress1 { get; set; }

        [StringLength(255)]
        [Display(Name = "Company Address 2")]
        public string CompanyAddress2 { get; set; }

        [Display(Name = "Company Province")]
        public int? CompanyProvince { get; set; }

        [Display(Name = "Company City")]
        public int? CompanyCity { get; set; }

        [Display(Name = "Company Region")]
        public int? CompanyRegion { get; set; }

        public int? DisplayCompanyProvinceLangID { get; set; }

        [StringLength(50)]
        [Display(Name = "Company Zip Code")]
        public string CompanyZipCode { get; set; }

        [StringLength(50)]
        [Display(Name = "Company Telephone")]
        public string CompanyTelephone { get; set; }

        [StringLength(50)]
        [Display(Name = "Company Fax")]
        public string CompanyFax { get; set; }

        [StringLength(3)]
        [Display(Name = "Company Country")]
        public string CompanyCountry { get; set; }

        [StringLength(50)]
        [Display(Name = "Company Tax ID")]
        public string CompanyTaxID { get; set; }

        [StringLength(50)]
        [Display(Name = "Company Register ID")]
        public string CompanyRegisterID { get; set; }

        [StringLength(255)]
        [Display(Name = "Branch")]
        public string BranchName { get; set; }

        [StringLength(50)]
        [Display(Name = "Branch No")]
        public string BranchNo { get; set; }

        [Display(Name = "Company Register Location")]
        public int? CompanyRegisterLocation { get; set; }

        [StringLength(255)]
        [Display(Name = "Branch Name In Full Tax Report")]
        public string BranchNameInFullTaxReport { get; set; }

        [StringLength(255)]
        public string AccountingCode { get; set; }

        [StringLength(50)]
        public string TaxPOSID { get; set; }

        [StringLength(100)]
        [Display(Name = "Delivery Name")]
        public string DeliveryName { get; set; }

        [StringLength(255)]
        [Display(Name = "Delivery Address 1")]
        public string DeliveryAddress1 { get; set; }

        [StringLength(255)]
        [Display(Name = "Delivery Address 2")]
        public string DeliveryAddress2 { get; set; }

        [Display(Name = "Delivery Province")]
        public int? DeliveryProvince { get; set; }

        [Display(Name = "Delivery City")]
        public int? DeliveryCity { get; set; }

        [Display(Name = "Delivery Region")]
        public int? DeliveryRegion { get; set; }

        [StringLength(50)]
        [Display(Name = "Delivery Zip Code")]
        public string DeliveryZipCode { get; set; }

        [StringLength(50)]
        [Display(Name = "Delivery Telephone")]
        public string DeliveryTelephone { get; set; }

        [StringLength(50)]
        [Display(Name = "Delivery Fax")]
        public string DeliveryFax { get; set; }

        [StringLength(50)]
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        [StringLength(100)]
        public string Addtional { get; set; }

        public int? ProductLevelOrder { get; set; }

        [StringLength(50)]
        public string SLOC { get; set; }

        [StringLength(20)]
        public string PTTShopCode { get; set; }

        [Display(Name = "Start Sale Date")]
        public DateTime? StartSaleDate { get; set; }

        public int? StartMonth { get; set; }

        public int? StartYear { get; set; }

        public int? Deleted { get; set; }
    }
}