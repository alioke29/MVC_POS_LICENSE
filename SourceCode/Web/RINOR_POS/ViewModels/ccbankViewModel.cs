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
    public class ccbankViewModel
    {
        public ccbankViewModel()
        {
        }

        [Key]
        public int CreditCardBankID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Credit Card Bank")]
        public string CreditCardBank { get; set; }
    }
}