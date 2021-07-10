using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RINOR_POS.Models
{
    public class licenceViewModels
    {
        public licenceViewModels()
        {
            merchant_list = new List<pos_merchant_data>();
            brand_list = new List<pos_brand_data>();
            shop_list = new List<pos_shop_data>();
            currency_list = new List<pos_currency>();
        }

        [Display(Name = "Merchant")]
        public int MerchantID { get; set; }
        [Display(Name = "Brand")]
        public int BrandID { get; set; }
        [Display(Name = "Shop")]
        public int ShopID { get; set; }

        public List<pos_merchant_data> merchant_list { get; set; }
        public List<pos_brand_data> brand_list { get; set; }
        public List<pos_shop_data> shop_list { get; set; }
        public List<pos_currency> currency_list { get; set; }

        [Key]
        public int LicenceDataID { get; set; }

        [Display(Name = "Computer Name")]
        public int ComputerNameID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "DeviceKey")]
        public string DeviceKey { get; set; }

        [Display(Name = "Currency")]
        public int? CurrencyID { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Required]
        [Display(Name = "Licence Start")]
        public DateTime LicenceStart { get; set; }

        [Required]
        [Display(Name = "Licence End")]
        public DateTime LicenceEnd { get; set; }
    }
}