namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_licence_data
    {
        [Key]
        public int LicenceDataID { get; set; }

        public int MerchantID { get; set; }

        public int BrandID { get; set; }

        public int ShopID { get; set; }

        public int ComputerNameID { get; set; }

        [Required]
        [StringLength(20)]
        public string DeviceKey { get; set; }

        public int? CurrencyID { get; set; }

        public decimal? Price { get; set; }

        public DateTime LicenceStart { get; set; }

        public DateTime LicenceEnd { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
