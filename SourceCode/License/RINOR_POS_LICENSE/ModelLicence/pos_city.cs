namespace RINOR_POS.ModelLicence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_city
    {
        [Key]
        public int CityID { get; set; }

        public int ProvinceID { get; set; }

        [Required]
        [StringLength(50)]
        public string CityName { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
