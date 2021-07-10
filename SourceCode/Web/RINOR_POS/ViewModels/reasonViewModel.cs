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
    public class reasongroupViewModel
    {
        public reasongroupViewModel()
        {
            MasterShopList = new List<pos_shop_data>();
            ShopList = new List<pos_shop_data>();
        }

        [Key]
        public int ReasonGroupID { get; set; }

        [Display(Name = "Master Shop")]
        public int MasterShopID { get; set; }
        public List<pos_shop_data> MasterShopList { get; set; }

        [Display(Name = "Shop Name")]
        public int ShopID { get; set; }

        public string ShopName { get; set; }

        public List<pos_shop_data> ShopList { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Reason Group")]
        public string ReasonGroupName { get; set; }

        [Display(Name = "Required Amount")]
        public bool isRequiredAmount { get; set; }

        public bool Activate { get; set; }

        [Display(Name = "Open to Input Reason")]
        public bool AllowedInput { get; set; }
    }

    public class reasondetailViewModel
    {
        [Key]
        public int ReasonGroupDetailID { get; set; }

        [Display(Name = "Shop Name")]
        public int ShopID { get; set; }
        public string ShopName { get; set; }

        [Display(Name = "Reason Group")]
        public int ReasonGroupID { get; set; }
        public string ReasonGroupName { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Reason Text")]
        public string ReasonText { get; set; }

        public int Ordering { get; set; }

        public bool Activate { get; set; }
    }
}