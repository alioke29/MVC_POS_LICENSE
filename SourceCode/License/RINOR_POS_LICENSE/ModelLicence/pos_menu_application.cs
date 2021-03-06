namespace RINOR_POS.ModelLicence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_menu_application
    {
        [Key]
        public int menu_id { get; set; }

        public int? module_id { get; set; }

        [StringLength(20)]
        public string menu_code { get; set; }

        [StringLength(50)]
        public string menu_name { get; set; }

        [StringLength(250)]
        public string menu_url { get; set; }

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
