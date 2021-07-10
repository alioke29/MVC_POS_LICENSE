using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RINOR_POS;

namespace RINOR_POS.Models
{
    public class printertypeViewModel
    {
        [Key]
        public int PrinterTypeID { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Printer Type")]
        public string PrinterTypeName { get; set; }
    }
    public class printerViewModel
    {
        public printerViewModel()
        {
            printer_type_list = new List<pos_printer_type>();
        }

        [Key]
        public int PrinterID { get; set; }

        [Required]
        [Display(Name = "Printer Type")]
        public int PrinterTypeID { get; set; }

        public string PrinterTypeName { get; set; }

        public List<pos_printer_type> printer_type_list { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Printer Name")]
        public string PrinterName { get; set; }

        [StringLength(255)]
        [Display(Name = "Printer Device")]
        public string PrinterDeviceName { get; set; }

        [StringLength(255)]
        [Display(Name = "Printer Device Backup")]
        public string PrinterDeviceBackup { get; set; }

        [Display(Name = "Is Opos Printer")]
        public bool? IsOposPrinter { get; set; }

        [Display(Name = "Printer Status")]
        public int? PrinterStatus { get; set; }

        [StringLength(255)]
        [Display(Name = "Printer Name For Print")]
        public string PrinterNameForPrint { get; set; }

        [Display(Name = "Paper Width")]
        public int? PaperWidth { get; set; }

        [Display(Name = "Line Spacing")]
        public int? LineSpacing { get; set; }

        [Display(Name = "Margin Top")]
        public int? MarginTop { get; set; }

        [Display(Name = "Margin Left")]
        public int? MarginLeft { get; set; }

        [Display(Name = "Margin Right")]
        public int? MarginRight { get; set; }

        [Display(Name = "Margin Bottom")]
        public int? MarginBottom { get; set; }
    }
}