namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_promotion_shoplink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PromotionShopLinkID { get; set; }

        public int? PromotionID { get; set; }

        public int? MasterShopID { get; set; }

        [StringLength(50)]
        public string MasterShopName { get; set; }

        public int? ShopID { get; set; }

        [StringLength(50)]
        public string ShopName { get; set; }

        public bool? IsActive { get; set; }
    }
}
