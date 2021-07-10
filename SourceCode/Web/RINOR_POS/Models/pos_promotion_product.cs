namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_promotion_product
    {
        [Key]
        public int PromotionProductID { get; set; }

        public int PromotionID { get; set; }

        public int? PromotionTypeID { get; set; }

        public int ProductGroupID { get; set; }

        public int? ProductDeptID { get; set; }

        public int? ProductID { get; set; }

        public int SaleModeID { get; set; }

        public int MasterShopID { get; set; }

        public int? ShopID { get; set; }

        public int? DiscountAmount { get; set; }

        public int? DiscountPercentage { get; set; }

        public int? DiscountSalePrice { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool? IsActive { get; set; }
    }
}
