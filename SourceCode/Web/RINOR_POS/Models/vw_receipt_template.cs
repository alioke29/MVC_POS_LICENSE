namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_receipt_template
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReceiptTemplateID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MasterShopID { get; set; }

        [StringLength(50)]
        public string MasterShopName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShopID { get; set; }

        [StringLength(50)]
        public string ShopName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string Position { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(200)]
        public string Text { get; set; }

        public int? Ordering { get; set; }

        public bool? isActive { get; set; }
    }
}
