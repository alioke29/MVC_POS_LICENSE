namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_sale_mode
    {
        [Key]
        public int SaleModeID { get; set; }

        [StringLength(300)]
        public string SaleModeName { get; set; }

        public bool? PositionPrefix { get; set; }

        [StringLength(90)]
        public string PrefixText { get; set; }

        [StringLength(90)]
        public string PrefixTextPrinting { get; set; }

        [StringLength(30)]
        public string PrefixQueue { get; set; }

        [StringLength(150)]
        public string ReceiptHeaderText { get; set; }

        public bool? HasServiceCharge { get; set; }

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

        [StringLength(300)]
        public string IconButtonServer { get; set; }

        [StringLength(3000)]
        public string IconButton { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
