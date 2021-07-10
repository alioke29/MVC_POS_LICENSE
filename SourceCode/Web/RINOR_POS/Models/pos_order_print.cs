namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_order_print
    {
        [Key]
        public int IdOrderPrint { get; set; }

        public int IdOrderComplete { get; set; }

        public int IdOrderDetailComplete { get; set; }

        public int SaleModeID { get; set; }

        public int MasterShopID { get; set; }

        public int ShopID { get; set; }

        public int PrinterID { get; set; }

        public int PrinterTypeID { get; set; }

        public int IdUser { get; set; }

        public long OrderNo { get; set; }

        [Required]
        [StringLength(50)]
        public string SessionID { get; set; }

        [Required]
        [StringLength(100)]
        public string ComputerID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        public int Qty { get; set; }

        [StringLength(100)]
        public string PrinterName { get; set; }

        [StringLength(100)]
        public string PrinterDeviceName { get; set; }

        public DateTime? InsertedDate { get; set; }

        public int? InsertedBy { get; set; }

        public bool? isPrint { get; set; }

        public bool? isPaid { get; set; }
    }
}
