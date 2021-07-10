namespace RINOR_POS.ModelLicence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_module_application
    {
        [Key]
        public int module_id { get; set; }

        [StringLength(10)]
        public string module_code { get; set; }

        [StringLength(50)]
        public string module_name { get; set; }

        public int? rec_order { get; set; }

        public bool? fl_active { get; set; }

        public DateTime? created_date { get; set; }

        public int? created_by { get; set; }

        public DateTime? updated_date { get; set; }

        public int? updated_by { get; set; }

        public DateTime? deleted_date { get; set; }

        public int? deleted_by { get; set; }
    }
}
