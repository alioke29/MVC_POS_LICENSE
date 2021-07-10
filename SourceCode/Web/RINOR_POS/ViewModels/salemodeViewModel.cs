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
    public class salemodeViewModel
    {
        public salemodeViewModel()
        {
        }

        [Key]
        public int SaleModeID { get; set; }

        [StringLength(300)]
        [Required]
        [Display(Name = "Sale Mode Name")]
        public string SaleModeName { get; set; }

        [Display(Name = "Position Prefix")]
        public bool? PositionPrefix { get; set; }

        [StringLength(90)]
        [Display(Name = "Prefix Text")]
        public string PrefixText { get; set; }

        [StringLength(90)]
        [Display(Name = "Prefix Text Printing")]
        public string PrefixTextPrinting { get; set; }

        [StringLength(30)]
        [Display(Name = "Prefix Queue")]
        public string PrefixQueue { get; set; }

        [StringLength(150)]
        [Display(Name = "Receipt Header Text")]
        public string ReceiptHeaderText { get; set; }

        [Display(Name = "Has Service Charge")]
        public bool? HasServiceCharge { get; set; }

        [StringLength(3000)]
        [Display(Name = "Pay Type List")]
        public string PayTypeList { get; set; }

        [StringLength(600)]
        [Display(Name = "NOT in Pay Type List")]
        public string NOTinPayTypeList { get; set; }

        [Display(Name = "Is Default")]
        public int? IsDefault { get; set; }

        [Display(Name = "Price At Header")]
        public int? PriceAtHeader { get; set; }

        [StringLength(60)]
        [Display(Name = "Prefix Header Text")]
        public string PrefixHeaderText { get; set; }

        [StringLength(765)]
        [Display(Name = "Auto Add Product")]
        public string AutoAddProduct { get; set; }

        [StringLength(60)]
        [Display(Name = "KDS Header Color")]
        public string KDSHeaderColor { get; set; }

        [Display(Name = "Sale Mode Type")]
        public int? SaleModeTypeID { get; set; }

        [Display(Name = "No Print Copy")]
        public int? NoPrintCopy { get; set; }

        [Display(Name = "Icon")]
        public string IconButtonServer { get; set; }

        [Display(Name = "Icon")]
        public string IconButton { get; set; }
    }
}