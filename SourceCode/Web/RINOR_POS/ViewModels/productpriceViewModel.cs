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
    public class productpriceViewModel
    {
        public productpriceViewModel()
        {
            merchant_list = new List<pos_merchant_data>();
            brand_list = new List<pos_brand_data>();
            shop_list = new List<pos_shop_data>();
            salemode_data = new List<pos_sale_mode>();
            pricedate_list = new List<vw_product_price_date>();
            pricegroup_list = new List<pos_product_price_group>();
            productgroup_list = new List<vw_product_group>();
            productsonly_list = new List<pos_products>();
            salemode_list = new List<vw_salemode_byshop>();
            productprice_list = new List<ProductPriceDetail>();
            productpricesave = new List<pos_product_price>();
        }
        [Display(Name = "Merchant")]
        public int MerchantId { get; set; }
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        [Display(Name = "Shop")]
        public int ShopId { get; set; }
       
        public List<pos_merchant_data> merchant_list { get; set; }
        public List<pos_brand_data> brand_list { get; set; }
        public List<pos_shop_data> shop_list { get; set; }
        public List<pos_sale_mode> salemode_data { get; set; }

        public List<vw_product_price_date> pricedate_list { get; set; }
        public List<pos_product_price_group> pricegroup_list { get; set; }
        public List<vw_product_group> productgroup_list { get; set; }
        public List<pos_products> productsonly_list { get; set; }
        public List<vw_salemode_byshop> salemode_list { get; set; }
        public List<ProductPriceDetail> productprice_list { get; set; }
        public List<pos_product_price> productpricesave { get; set; }

        [Key]
        public int ProductPriceId { get; set; }

        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Display(Name = "Price Group")]
        public int? ProductPriceGroupId { get; set; }

        [Display(Name = "Price Group Date")]
        public int? ProductPriceDateId { get; set; }

        public decimal? ProductPrice { get; set; }

        public decimal? PrepaidPrice { get; set; }

        [Display(Name = "Main Prace")]
        public bool? MainPrice { get; set; }

        public int? SaleMode { get; set; }

        [StringLength(100)]
        public string PriceRemark { get; set; }

        public bool? AddingFromBranch { get; set; }


        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }


        [StringLength(100)]
        public string ProductPriceName { get; set; }
    }

    public class ProductPriceDetail
    {
        public int ProductPriceId { get; set; }

        public int ShopID { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public int SaleMode { get; set; }

        public int ProductPriceGroupId { get; set; }

        public int ProductPriceDateId { get; set; }

        public int ProductPrice { get; set; }

        public decimal? PrepaidPrice { get; set; }

        public bool? MainPrice { get; set; }

        [StringLength(100)]
        public string PriceRemark { get; set; }

        public bool? AddingFromBranch { get; set; }
    }

    public class PriceData
    {
        public string DataID { get; set; }

        public string Price { get; set; }

        public int ShopID { get; set; }

        public int PriceGroupDateID { get; set; }

        public string PriceGroupID { get; set; }
    }
}