using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RINOR_POS;

namespace RINOR_POS.ModelLicence
{
    public class LicenseTypeViewModel
    {
        public LicenseTypeViewModel()
        {
            CurrencyList = new List<pos_currency>();
        }

        public int LicenceTypeID { get; set; }

        [StringLength(50)]
        [Display(Name = "License Type")]
        public string LicenceName { get; set; }

        [Required]
        [Display(Name = "Duration (Days)")]
        public int Duration { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int? CurrencyId { get; set; }

        public List<pos_currency> CurrencyList { get; set; }

        [Required]
        [Display(Name = "Pricing")]
        public long Pricing { get; set; }

        [Display(Name = "Activate")]
        public bool isActive { get; set; }

    }
}