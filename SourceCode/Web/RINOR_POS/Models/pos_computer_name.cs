namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_computer_name
    {
        [Key]
        public int ComputerNameId { get; set; }

        public int? ShopId { get; set; }

        public int? ComputerTypeId { get; set; }

        [StringLength(50)]
        public string ComputerName { get; set; }

        [StringLength(250)]
        public string PayTypeList { get; set; }

        [StringLength(250)]
        public string SaleModeList { get; set; }

        [StringLength(250)]
        public string PrinterList { get; set; }

        [StringLength(20)]
        public string RegistrationNumber { get; set; }

        [StringLength(20)]
        public string DeviceCode { get; set; }

        [StringLength(250)]
        public string ReceiptHeader { get; set; }

        [StringLength(250)]
        public string FullTaxHeader { get; set; }

        [StringLength(20)]
        public string IPAddress { get; set; }

        public bool? Activate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public DateTime? ValidUntil { get; set; }
    }
}
