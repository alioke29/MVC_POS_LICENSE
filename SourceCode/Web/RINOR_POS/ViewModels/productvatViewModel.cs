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
    public class productvatViewModel
    {
        public productvatViewModel()
        {
        }

        [Key]
        [Display(Name = "Product VAT")]
        public int ProductVATID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Product VAT Code")]
        public string ProductVATCode { get; set; }

        [Required]
        [Display(Name = "Product VAT %")]
        public int? ProductVATPercent { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Product VAT Display")]
        public string VATDisplay { get; set; }

        [StringLength(300)]
        [Display(Name = "Product VAT Description")]
        public string VATDesp { get; set; }
    }
}