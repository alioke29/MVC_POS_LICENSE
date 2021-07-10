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
    public class productvattypeViewModel
    {
        public productvattypeViewModel()
        {
        }

        [Key]
        public int VATTypeID { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Product VAT Type")]
        public string VATTypeName { get; set; }
    }
}