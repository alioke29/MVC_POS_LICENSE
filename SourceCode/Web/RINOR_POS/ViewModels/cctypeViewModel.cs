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
    public class cctypeViewModel
    {
        public cctypeViewModel()
        {
        }

        [Key]
        public int CreditCardTypeID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Credit Card Type")]
        public string CreditCardType { get; set; }
    }
}