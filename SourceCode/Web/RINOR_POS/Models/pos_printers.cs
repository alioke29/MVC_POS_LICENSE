namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_printers
    {
        [Key]
        public int PrinterID { get; set; }

        public int PrinterTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string PrinterName { get; set; }

        [StringLength(255)]
        public string PrinterDeviceName { get; set; }

        [StringLength(255)]
        public string PrinterDeviceBackup { get; set; }

        public bool? IsOposPrinter { get; set; }

        public int? PrinterStatus { get; set; }

        [StringLength(255)]
        public string PrinterNameForPrint { get; set; }

        public int? PaperWidth { get; set; }

        public int? LineSpacing { get; set; }

        public int? MarginTop { get; set; }

        public int? MarginLeft { get; set; }

        public int? MarginRight { get; set; }

        public int? MarginBottom { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
