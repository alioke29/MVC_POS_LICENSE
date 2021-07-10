namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_promotion_package
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PromotionPackageID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PromotionID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string PromotionCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string PromotionName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SaleModeID { get; set; }

        [StringLength(300)]
        public string SaleModeName { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MasterShopID { get; set; }

        [StringLength(50)]
        public string ShopName { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductGroupID { get; set; }

        [StringLength(765)]
        public string ProductGroupName { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductDeptID { get; set; }

        [StringLength(765)]
        public string ProductDeptName { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [StringLength(300)]
        public string ProductName { get; set; }

        public bool? IsActive { get; set; }

        public int? ProductComboID { get; set; }

        [StringLength(50)]
        public string ProductComboName { get; set; }
    }
}
