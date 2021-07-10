namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_salemode_byshop
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MasterShopID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShopID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SaleModeID { get; set; }

        [StringLength(300)]
        public string SaleModeName { get; set; }

        [StringLength(90)]
        public string PrefixText { get; set; }

        [StringLength(90)]
        public string PrefixTextPrinting { get; set; }

        [StringLength(30)]
        public string PrefixQueue { get; set; }

        [StringLength(150)]
        public string ReceiptHeaderText { get; set; }

        public int? HasServiceCharge { get; set; }

        [StringLength(3000)]
        public string PayTypeList { get; set; }

        [StringLength(600)]
        public string NOTinPayTypeList { get; set; }

        public int? IsDefault { get; set; }

        public int? PriceAtHeader { get; set; }

        [StringLength(60)]
        public string PrefixHeaderText { get; set; }

        [StringLength(765)]
        public string AutoAddProduct { get; set; }

        [StringLength(60)]
        public string KDSHeaderColor { get; set; }

        public int? SaleModeTypeID { get; set; }

        public int? NoPrintCopy { get; set; }

        public string IconButton { get; set; }
    }
}
