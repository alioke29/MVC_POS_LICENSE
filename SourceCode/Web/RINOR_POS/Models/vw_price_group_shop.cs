namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_price_group_shop
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShopID { get; set; }

        public bool? ShopSelected { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BrandID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MerchantID { get; set; }

        [StringLength(10)]
        public string ShopCode { get; set; }

        [StringLength(50)]
        public string ShopName { get; set; }

        public bool? MasterShop { get; set; }

        public int? MasterShopLink { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? ProductPriceGroupID { get; set; }

        [StringLength(100)]
        public string ProductPriceName { get; set; }
    }
}
