using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RINOR_POS.Models
{
    public class billtemplateViewModel
    {
        public billtemplateViewModel()
        {
            MasterShopList = new List<pos_shop_data>();
            ShopList = new List<pos_shop_data>();
            BillTemplateList = new List<vw_bill_template>();
        }

        public pos_bill_template_image billlogo { get; set; }
        public List<vw_bill_template> BillTemplateList { get; set; }
        public string flagSave { get; set; }

        public int BillTemplateID { get; set; }

        [Display(Name = "Master Shop")]
        public int MasterShopID { get; set; }
        public List<pos_shop_data> MasterShopList { get; set; }

        [Display(Name = "Shop")]
        public int ShopID { get; set; }
        public List<pos_shop_data> ShopList { get; set; }

        [Required]
        [StringLength(10)]
        public string Position { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Text Template")]
        public string Text { get; set; }

        public int? Ordering { get; set; }

        [StringLength(2000)]
        [Display(Name = "Logo Header")]
        public string LogoHeaderFile { get; set; }

        [StringLength(500)]
        [Display(Name = "QR Footer")]
        public string QRFooterText { get; set; }
    }
}