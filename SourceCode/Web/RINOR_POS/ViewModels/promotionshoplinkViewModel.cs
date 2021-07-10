using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RINOR_POS.Models
{
    public class promotionshoplinkViewModel
    {
        public promotionshoplinkViewModel()
        {
            MasterShopList = new List<pos_shop_data>();
            ShopList = new List<pos_shop_data>();
            shop_selected = new List<string>();
            PromoShopLinkList = new List<vw_promotion_shoplink>();
        }

        public int PromotionShopLinkID { get; set; }
        public List<vw_promotion_shoplink> PromoShopLinkList { get; set; }
        [Display(Name = "Promotion")]
        public int? PromotionID { get; set; }
        public string PromotionName { get; set; }
        public string PromotionTypeName { get; set; }

        [Display(Name = "Master Shop")]
        public int? MasterShopID { get; set; }
        public List<pos_shop_data> MasterShopList { get; set; }

        [Display(Name = "Shop")]
        public int? ShopID { get; set; }
        public List<pos_shop_data> ShopList { get; set; }
        public List<string> shop_selected { get; set; }

        [Display(Name = "Activate")]
        public bool? IsActive { get; set; }
    }
}