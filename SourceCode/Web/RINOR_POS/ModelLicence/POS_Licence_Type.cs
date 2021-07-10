namespace RINOR_POS.ModelLicence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class POS_Licence_Type
    {
        public int? LicenceTypeID { get; set; }

        [StringLength(50)]
        public string LicenceName { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Duration { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Pricing { get; set; }

        public DateTime? InsertedDate { get; set; }

        [StringLength(50)]
        public string InsertedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        [StringLength(50)]
        public string DeletedBy { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int isActive { get; set; }
    }
}
