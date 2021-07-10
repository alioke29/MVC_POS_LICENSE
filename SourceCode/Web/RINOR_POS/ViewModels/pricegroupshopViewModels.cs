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
    public class pricegroupshopViewModels
    {
        public pricegroupshopViewModels()
        {
            shop_list = new List<pos_shop_data>();
            shop_selected = new List<string>();
            shop_available = new List<string>();
        }
        [Key]
        public int ProductPriceGroupShopID { get; set; }

        public int ProductPriceGroupID { get; set; }

        [Display(Name = "Product Price Group")]
        public string ProductPriceGroupName { get; set; }

        [Display(Name = "Master Shop")]
        public int ShopId { get; set; }
        public List<pos_shop_data> shop_list { get; set; }
        public List<string> shop_available { get; set; }
        public List<string> shop_selected { get; set; }
    }
}