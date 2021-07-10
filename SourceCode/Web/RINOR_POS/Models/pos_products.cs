namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_products
    {
        [Key]
        public int ProductID { get; set; }

        public int ShopID { get; set; }

        public int? InventoryID { get; set; }

        public int ProductGroupID { get; set; }

        public int ProductDeptID { get; set; }

        public int? ProductTypeID { get; set; }

        [StringLength(150)]
        public string ProductCode { get; set; }

        [StringLength(300)]
        public string ProductName { get; set; }

        [StringLength(300)]
        public string ProductName1 { get; set; }

        [StringLength(300)]
        public string ProductName2 { get; set; }

        [StringLength(300)]
        public string ProductName3 { get; set; }

        [StringLength(300)]
        public string ProductName4 { get; set; }

        [StringLength(300)]
        public string ProductName5 { get; set; }

        [StringLength(300)]
        public string ProductPictureServer { get; set; }

        [StringLength(300)]
        public string ProductPictureClient { get; set; }

        [StringLength(150)]
        public string PrinterID { get; set; }

        public int? PrintGroup { get; set; }

        [StringLength(150)]
        public string PrintList { get; set; }

        [StringLength(765)]
        public string KDSID { get; set; }

        [StringLength(765)]
        public string KDSSummaryID { get; set; }

        public int? DurationTime { get; set; }

        public bool? HasServiceCharge { get; set; }

        public double? ServiceChargePercent { get; set; }

        public bool? IsOutOfStock { get; set; }

        public bool? AllowMinusStock { get; set; }

        public bool? AutoComment { get; set; }

        public bool? IsDisplayBill { get; set; }

        public bool? IsPrintCheck { get; set; }

        public bool? IsPrintReceipt { get; set; }

        public bool? CanReturnProduct { get; set; }

        public bool? DisplayAtCheckerSystem { get; set; }

        public bool? DisplayMobile { get; set; }

        public DateTime? ProductEnableDateTime { get; set; }

        public DateTime? ProductExpireDateTime { get; set; }

        public int? WarningTime { get; set; }

        public int? CriticalTime { get; set; }

        public TimeSpan? TimeCriteriaStart { get; set; }

        public TimeSpan? TimeCriteriaEnd { get; set; }

        [StringLength(100)]
        public string EnableWeekly { get; set; }

        public int? VATType { get; set; }

        [StringLength(30)]
        public string VATCode { get; set; }

        [StringLength(150)]
        public string ProductUnitName { get; set; }

        public bool? DiscountAllow { get; set; }

        public bool? ZeroPriceAllow { get; set; }

        public decimal? LimitDiscountAmount { get; set; }

        public decimal? LimitDiscountPercent { get; set; }

        public int? CommRate { get; set; }

        [StringLength(1500)]
        public string ProductDesp { get; set; }

        public bool? ProductDisplay { get; set; }

        public bool? ProductActivate { get; set; }

        public int? ProductOrdering { get; set; }

        public int? PrintOrdering { get; set; }

        public int? KDSOrdering { get; set; }

        public int? RequireAddAmount { get; set; }

        public int? AddingFromBranch { get; set; }

        public bool? isFavorite { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
