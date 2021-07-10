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
    public class promotionproductViewModel
    {
        public promotionproductViewModel()
        {
            ProductGroupList = new List<pos_product_group>();
            ProductDeptList = new List<pos_product_dept>();
            ProductList = new List<pos_products>();
            product_selected = new List<string>();
            SalemodeList = new List<pos_sale_mode>();
            sale_mode_selected = new List<string>();
            MasterShopList = new List<pos_shop_data>();
            ShopList = new List<pos_shop_data>();
            DiscountTypeList = new List<Models.DiscountType>();
            PromoProductList = new List<vw_promotion_product>();
        }

        //Additional
        public List<DiscountType> DiscountTypeList { get; set; }
        public List<vw_promotion_product> PromoProductList { get; set; }
        public string ProductFrom { get; set; }
        public string ProductCode { get; set; }
        public string DiscountType { get; set; }
        //-----//

        [Key]
        public int PromotionProductID { get; set; }

        [Display(Name = "Promotion")]
        public int PromotionID { get; set; }
        public string PromotionName { get; set; }
        public string PromotionTypeName { get; set; }

        [Display(Name = "Product Group")]
        public int ProductGroupID { get; set; }
        public List<pos_product_group> ProductGroupList { get; set; }

        [Display(Name = "Product Department")]
        public int ProductDeptID { get; set; }
        public List<pos_product_dept> ProductDeptList { get; set; }

        [Display(Name = "Product ")]
        public int ProductID { get; set; }
        public List<pos_products> ProductList { get; set; }
        public List<string> product_selected { get; set; }


        [Display(Name = "Sale Mode")]
        public int SaleModeID { get; set; }
        public List<pos_sale_mode> SalemodeList { get; set; }
        public List<string> sale_mode_selected { get; set; }

        [Display(Name = "Master Shop")]
        public int MasterShopID { get; set; }
        public List<pos_shop_data> MasterShopList { get; set; }

        [Display(Name = "Master Shop")]
        public int ShopID { get; set; }
        public List<pos_shop_data> ShopList { get; set; }

        [Display(Name = "Discount Amount")]
        public int? DiscountAmount { get; set; }

        [Display(Name = "Discount Percentage")]
        public int? DiscountPercentage { get; set; }

        [Display(Name = "Discount Sale Price")]
        public int? DiscountSalePrice { get; set; }

        public string flagSave { get; set; }

    }

    public class DiscountType
    {
        public string DiscountTypeCode { get; set; }
        public string DiscountTypeName { get; set; }
    }
}