namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_promotion_product
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PromotionProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PromotionID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductGroupID { get; set; }

        [StringLength(150)]
        public string ProductGroupCode { get; set; }

        [StringLength(765)]
        public string ProductGroupName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductDeptID { get; set; }

        [StringLength(150)]
        public string ProductDeptCode { get; set; }

        [StringLength(765)]
        public string ProductDeptName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [StringLength(150)]
        public string ProductCode { get; set; }

        [StringLength(300)]
        public string ProductName { get; set; }

        [StringLength(300)]
        public string ProductName4 { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MasterShopID { get; set; }

        [StringLength(10)]
        public string ShopCode { get; set; }

        [StringLength(50)]
        public string ShopName { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SaleModeID { get; set; }

        [StringLength(300)]
        public string SaleModeName { get; set; }

        public int? DiscountAmount { get; set; }

        public int? DiscountPercentage { get; set; }

        public int? DiscountSalePrice { get; set; }

        [StringLength(12)]
        public string DiscountType { get; set; }
    }
}
