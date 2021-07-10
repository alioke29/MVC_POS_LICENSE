using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RINOR_POS.Models
{
    public class promotionpackageViewModel
    {
        public promotionpackageViewModel()
        {
            PromoPackageList = new List<vw_promotion_package>();
            ProductGroupList = new List<pos_product_group>();
            ProductDeptList = new List<pos_product_dept>();
            ProductList = new List<pos_products>();
            product_selected = new List<string>();
            ProductComboList = new List<pos_product_combo>();
            productCombo_selected = new List<string>();
            SalemodeList = new List<pos_sale_mode>();
            MasterShopList = new List<pos_shop_data>();
        }

        public List<vw_promotion_package> PromoPackageList { get; set; }

        public int PromotionPackageID { get; set; }

        [Display(Name = "Promotion")]
        public int PromotionID { get; set; }
        public string PromotionName { get; set; }
        public string PromotionTypeName { get; set; }

        [Display(Name = "Promotion Price")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public decimal? PromotionPrice { get; set; }
        [Display(Name = "Promotion VAT (%)")]
        public decimal? PromotionVAT { get; set; }
        [Display(Name = "Promotion Service Charge (%)")]
        public decimal? PromotionServiceCharge { get; set; }

        [Display(Name = "Sale Mode")]
        public int SaleModeID { get; set; }
        public List<pos_sale_mode> SalemodeList { get; set; }

        [Display(Name = "Master Shop")]
        public int MasterShopID { get; set; }
        public List<pos_shop_data> MasterShopList { get; set; }

        public int ShopID { get; set; }

        [Display(Name = "Product Group")]
        public int ProductGroupID { get; set; }
        public List<pos_product_group> ProductGroupList { get; set; }

        [Display(Name = "Product Dept")]
        public int ProductDeptID { get; set; }
        public List<pos_product_dept> ProductDeptList { get; set; }

        [Display(Name = "Product")]
        public int ProductID { get; set; }
        public List<pos_products> ProductList { get; set; }
        public List<string> product_selected { get; set; }

        [Display(Name = "Product Combo")]
        public int? ProductComboID { get; set; }
        public List<pos_product_combo> ProductComboList { get; set; }
        public List<string> productCombo_selected { get; set; }

        [Display(Name = "Activate")]
        public bool? IsActive { get; set; }
    }
}