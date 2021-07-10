namespace RINOR_POS.ModelLicence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class POS_Licence_Type
    {
        [Key]
        public int LicenceTypeID { get; set; }

        [StringLength(50)]
        public string LicenceName { get; set; }

        public int Duration { get; set; }

        public int? CurrencyId { get; set; }

        public long Pricing { get; set; }

        public DateTime? InsertedDate { get; set; }

        public int? InsertedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool isActive { get; set; }
    }
}
