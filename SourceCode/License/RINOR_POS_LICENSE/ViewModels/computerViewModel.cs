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
    public class computertypeViewModel
    {
        public computertypeViewModel()
        {

        }

        [Key]
        public int ComputerTypeID { get; set; }

        [StringLength(50)]
        [Display(Name = "Computer Type")]
        public string ComputerTypeName { get; set; }

        public bool? Activate { get; set; }
    }

    public class computerViewModel
    {
        public computerViewModel()
        {
            computertype_list = new List<pos_computer_type>();
            mastershop_list = new List<pos_shop_data>();
        }

        [Key]
        public int ComputerNameId { get; set; }

        public int? ShopId { get; set; }

        [Display(Name = "Shop Name")]
        public string ShopName { get; set; }
        public List<pos_shop_data> mastershop_list { get; set; }

        [Display(Name = "Computer Type")]
        public int? ComputerTypeId { get; set; }
        public string ComputerTypeName { get; set; }
        public List<pos_computer_type> computertype_list { get; set; }

        [StringLength(50)]
        [Display(Name = "Computer Name")]
        public string ComputerName { get; set; }

        [StringLength(20)]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        [StringLength(20)]
        [Display(Name = "Device Code")]
        public string DeviceCode { get; set; }

        [StringLength(250)]
        [Display(Name = "ReceiptHeader")]
        public string ReceiptHeader { get; set; }

        [StringLength(250)]
        [Display(Name = "FullTaxHeader")]
        public string FullTaxHeader { get; set; }

        [StringLength(20)]
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        public bool? Activate { get; set; }
    }
}