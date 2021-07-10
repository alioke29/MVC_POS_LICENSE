namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_promotion_buy_get
    {
        [Key]
        public int PromotionBuyGetID { get; set; }

        public int PromotionID { get; set; }

        public int BuySaleModeID { get; set; }

        public int BuyMasterShopID { get; set; }

        public int BuyShopID { get; set; }

        public int BuyProductGroupID { get; set; }

        public int BuyProductDepthID { get; set; }

        public int BuyProductID { get; set; }

        public int BuyQty { get; set; }

        public int GetSaleModeID { get; set; }

        public int GetMasterShopID { get; set; }

        public int GetShopID { get; set; }

        public int GetProductGroupID { get; set; }

        public int GetProductDeptID { get; set; }

        public int GetProductID { get; set; }

        public int GetQty { get; set; }

        public bool? isActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }
    }
}
