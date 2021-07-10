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
    public class productViewModel
    {
        public productViewModel()
        {
            shop_list = new List<pos_shop_data>();
        }

        [Key]
        public int ProductGroupID { get; set; }

        [Required]
        [Display(Name = "Master Shop")]
        public int ShopID { get; set; }
        public string ShopName { get; set; }
        public virtual pos_shop_data pos_shop_data { get; set; }

        public List<pos_shop_data> shop_list { get; set; }

        [StringLength(150)]
        [Required]
        [Display(Name = "Product Group Code")]
        public string ProductGroupCode { get; set; }

        [StringLength(765)]
        [Required]
        [Display(Name = "Product Group Name")]
        public string ProductGroupName { get; set; }

        [Display(Name = "Activate")]
        public bool? ProductGroupActivate { get; set; }

        [Display(Name = "Display On Mobile")]
        public bool? DisplayMobile { get; set; }

        [Display(Name = "Is Comment")]
        public bool? IsComment { get; set; }

        public int? Deleted { get; set; }
    }

    public class productDeptViewModel
    {
        public productDeptViewModel()
        {
        }

        [Key]
        [Display(Name = "Product Department")]
        public int ProductDeptID { get; set; }

        [Display(Name = "Product Group")]
        public int ProductGroupID { get; set; }

        [Display(Name = "Product Group")]
        public string ProductGroupName { get; set; }

        public int ShopID { get; set; }

        [Display(Name = "Shop")]
        public string ShopName { get; set; }

        [StringLength(150)]
        [Display(Name = "Product Department Code")]
        public string ProductDeptCode { get; set; }

        [StringLength(765)]
        [Display(Name = "Product Department Name")]
        public string ProductDeptName { get; set; }

        [Display(Name = "Activate")]
        public bool? ProductDeptActivate { get; set; }

        [Display(Name = "Ordering")]
        public int? ProductDeptOrdering { get; set; }

        [Display(Name = "Display on Mobile")]
        public bool? DisplayMobile { get; set; }
    }

    public class productItemViewModel
    {
        public productItemViewModel()
        {
            product_type_list = new List<pos_product_type>();
            sale_mode_list = new List<pos_sale_mode>();
            sale_mode_selected = new List<string>();

            product_salemode_list = new List<pos_product_salemode>();
            product_vattype_list = new List<pos_product_vat_type>();
            product_vat_list = new List<pos_product_vat>();

            productsize_list = new List<pos_product_size>();
            productsize_selected = new List<pos_product_size>();
        }

        [Key]
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Shop")]
        public int ShopID { get; set; }
        public string ShopName { get; set; }

        [Required]
        [Display(Name = "Product Group")]
        public int ProductGroupID { get; set; }
        public string ProductGroupName { get; set; }

        [Required]
        [Display(Name = "Product Department")]
        public int ProductDeptID { get; set; }
        public string ProductDeptName { get; set; }

        [Display(Name = "Product Type")]
        public int? ProductTypeID { get; set; }

        public virtual pos_product_type pos_product_type { get; set; }

        public List<pos_product_type> product_type_list { get; set; }

        public List<pos_sale_mode> sale_mode_list { get; set; }

        public List<pos_product_salemode> product_salemode_list { get; set; }

        public List<string> sale_mode_selected { get; set; }

        public List<pos_product_vat_type> product_vattype_list { get; set; }

        public List<pos_product_vat> product_vat_list { get; set; }

        public List<pos_product_size> productsize_list { get; set; }
        public List<pos_product_size> productsize_selected { get; set; }
        public int? ProductSizeId { get; set; }
        public string Size { get; set; }
        public decimal? AdditionalPrice { get; set; }

        [Required]
        [Display(Name = "Product Code")]
        [StringLength(150)]
        public string ProductCode { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [StringLength(13)]
        public string ProductName { get; set; }

        [StringLength(300)]
        [Display(Name = "Product Name 1")]
        public string ProductName1 { get; set; }

        [StringLength(300)]
        [Display(Name = "Product Name 2")]
        public string ProductName2 { get; set; }

        [StringLength(300)]
        [Display(Name = "Product Barcode 1")]
        public string ProductName3 { get; set; }

        [StringLength(300)]
        [Display(Name = "Product Barcode 2")]
        public string ProductName4 { get; set; }

        [StringLength(300)]
        [Display(Name = "Product Barcode 3")]
        public string ProductName5 { get; set; }

        [Display(Name = "Product Picture Server")]
        public string ProductPictureServer { get; set; }

        [StringLength(300)]
        [Display(Name = "Product Picture Client")]
        public string ProductPictureClient { get; set; }

        [StringLength(150)]
        [Display(Name = "Printer")]
        public string PrinterID { get; set; }

        [Display(Name = "Print Option")]
        public int? PrintGroup { get; set; }
        [Display(Name = "Printer List")]
        public string PrintList { get; set; }
        public List<pos_printers> printer_list { get; set; }
        public List<string> printer_selected { get; set; }

        [StringLength(765)]
        [Display(Name = "KDS")]
        public string KDSID { get; set; }

        [StringLength(765)]
        [Display(Name = "KDS Summary")]
        public string KDSSummaryID { get; set; }

        [Display(Name = "Duration Time")]
        public int? DurationTime { get; set; }

        [Display(Name = "Has Service Charge")]
        public bool? HasServiceCharge { get; set; }

        public double? ServiceChargePercent { get; set; }

        [Display(Name = "Price Include SC")]
        public bool? PriceIncludeSC { get; set; }

        [Display(Name = "Out Of Stock")]
        public bool? IsOutOfStock { get; set; }

        [Display(Name = "Allow Minus Stock")]
        public bool? AllowMinusStock { get; set; }

        [Display(Name = "Auto Comment")]
        public bool? AutoComment { get; set; }

        [Display(Name = "Display Bill")]
        public bool? IsDisplayBill { get; set; }

        [Display(Name = "Print Check")]
        public bool? IsPrintCheck { get; set; }

        [Display(Name = "Print Receipt")]
        public bool? IsPrintReceipt { get; set; }

        [Display(Name = "Can Return Product")]
        public bool? CanReturnProduct { get; set; }

        [Display(Name = "Display At Checker System")]
        public bool? DisplayAtCheckerSystem { get; set; }

        [Display(Name = "Display on Mobile")]
        public bool? DisplayMobile { get; set; }

        [Display(Name = "Begin Date")]
        public DateTime? ProductEnableDateTime { get; set; }

        [Display(Name = "Expired Date")]
        public DateTime? ProductExpireDateTime { get; set; }

        [Display(Name = "Warning Time (in Minutes)")]
        public int? WarningTime { get; set; }

        [Display(Name = "Critical Time (in Minutes)")]
        public int? CriticalTime { get; set; }

        public TimeSpan? TimeCriteriaStart { get; set; }

        public TimeSpan? TimeCriteriaEnd { get; set; }

        [StringLength(100)]
        [Display(Name = "Enable Weekly")]
        public string EnableWeekly { get; set; }

        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }

        [Display(Name = "Product VAT Type")]
        public int? VATType { get; set; }

        [StringLength(30)]
        [Display(Name = "VAT Code")]
        public string VATCode { get; set; }

        [StringLength(150)]
        [Display(Name = "Product Unit Name")]
        public string ProductUnitName { get; set; }

        [Display(Name = "DiscountAllow")]
        public bool? DiscountAllow { get; set; }

        [Display(Name = "Zero Price Allow")]
        public bool? ZeroPriceAllow { get; set; }

        [Display(Name = "Limit Discount Amount")]
        public decimal? LimitDiscountAmount { get; set; }

        [Display(Name = "Limit Discount Percent")]
        public decimal? LimitDiscountPercent { get; set; }

        [Display(Name = "Comm Rate")]
        public int? CommRate { get; set; }

        [StringLength(1500)]
        [Display(Name = "Product Description")]
        public string ProductDesp { get; set; }

        [Display(Name = "Product Display")]
        public bool? ProductDisplay { get; set; }

        [Display(Name = "Product Activate")]
        public bool? ProductActivate { get; set; }

        [Display(Name = "Product Ordering")]
        public int? ProductOrdering { get; set; }

        [Display(Name = "Print Ordering")]
        public int? PrintOrdering { get; set; }

        [Display(Name = "KDS Ordering")]
        public int? KDSOrdering { get; set; }

        [Display(Name = "Require Add Amount")]
        public bool? RequireAddAmount { get; set; }

        [Display(Name = "Adding From Branch")]
        public bool? AddingFromBranch { get; set; }

        [Display(Name = "Is Favorite")]
        public bool? isFavorite { get; set; }
    }

    public class productComboViewModel
    {
        public productComboViewModel()
        {
            ProductList = new List<pos_products>();
            product_selected = new List<string>();
            ProductComboList = new List<vw_product_combo_link>();
        }
        [Key]
        
        public int ProductComboID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Product Combo Name")]
        public string ProductComboName { get; set; }

        [Display(Name = "Master Shop")]
        public int ShopID { get; set; }
        public string ShopName { get; set; }

        [Display(Name = "Activate")]
        public bool Activate { get; set; }

        [Display(Name = "Product Group")]
        public int ProductGroupID { get; set; }
        public string ProductGroupName { get; set; }

        [Display(Name = "Product Department")]
        public int ProductDeptID { get; set; }
        public List<pos_product_dept> ProductDeptList { get; set; }

        [Required]
        public int ProductID { get; set; }
        public List<pos_products> ProductList { get; set; }
        public List<string> product_selected { get; set; }
        public List<vw_product_combo_link> ProductComboList { get; set; }

        [Display(Name = "Product Code")]
        [StringLength(150)]
        public string ProductCode { get; set; }

        [Display(Name = "Product Name")]
        [StringLength(300)]
        public string ProductName { get; set; }
    }
}