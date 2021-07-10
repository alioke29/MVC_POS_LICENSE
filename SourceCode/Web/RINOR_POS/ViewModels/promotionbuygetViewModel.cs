using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RINOR_POS.Models
{
    public class promotionbuygetViewModel
    {
        public promotionbuygetViewModel()
        {
            ProductGroupList = new List<pos_product_group>();
            ProductDeptList = new List<pos_product_dept>();
            ProductList = new List<pos_products>();
            product_selected = new List<string>();
            SalemodeList = new List<pos_sale_mode>();
            sale_mode_selected = new List<string>();
            MasterShopList = new List<pos_shop_data>();
            ShopList = new List<pos_shop_data>();
            PromoBuyGetList = new List<vw_promotion_buyget>();
        }

        public List<vw_promotion_buyget> PromoBuyGetList { get; set; }
        public int PromotionBuyGetID { get; set; }

        [Display(Name = "Promotion")]
        public int PromotionID { get; set; }
        public string PromotionName { get; set; }
        public string PromotionTypeName { get; set; }

        [Display(Name = "Sale Mode")]
        public int? BuySaleModeID { get; set; }

        [Display(Name = "Master Shop")]
        public int BuyMasterShopID { get; set; }

        [Display(Name = "Shop")]
        public int BuyShopID { get; set; }

        [Display(Name = "Product Group")]
        public int BuyProductGroupID { get; set; }
        public List<pos_product_group> ProductGroupList { get; set; }

        [Display(Name = "Product Dept")]
        public int BuyProductDepthID { get; set; }
        public List<pos_product_dept> ProductDeptList { get; set; }

        [Display(Name = "Product")]
        public int? BuyProductID { get; set; }
        public List<pos_products> ProductList { get; set; }
        public List<string> product_selected { get; set; }

        [Display(Name = "Quantity")]
        public int BuyQty { get; set; }

        [Display(Name = "Sale Mode")]
        public int GetSaleModeID { get; set; }
        public List<pos_sale_mode> SalemodeList { get; set; }
        public List<string> sale_mode_selected { get; set; }

        [Display(Name = "Master Shop")]
        public int GetMasterShopID { get; set; }
        public List<pos_shop_data> MasterShopList { get; set; }

        [Display(Name = "Shop")]
        public int GetShopID { get; set; }
        public List<pos_shop_data> ShopList { get; set; }

        [Display(Name = "Product Group")]
        public int GetProductGroupID { get; set; }

        [Display(Name = "Product Dept")]
        public int GetProductDeptID { get; set; }

        [Display(Name = "Product")]
        public int GetProductID { get; set; }

        [Display(Name = "Quantity")]
        public int GetQty { get; set; }

        [Display(Name = "Activate")]
        public bool? isActive { get; set; }
    }
}