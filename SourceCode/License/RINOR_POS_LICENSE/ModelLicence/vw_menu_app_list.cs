namespace RINOR_POS.ModelLicence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_menu_app_list
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int menu_id { get; set; }

        public int? UserRoleID { get; set; }

        public int? module_id { get; set; }

        [StringLength(10)]
        public string module_code { get; set; }

        [StringLength(50)]
        public string module_name { get; set; }

        [StringLength(20)]
        public string menu_code { get; set; }

        [StringLength(50)]
        public string menu_name { get; set; }

        [StringLength(250)]
        public string menu_url { get; set; }

        public int? rec_order { get; set; }

        public bool? fl_active { get; set; }

        public DateTime? deleted_date { get; set; }

        public int? deleted_by { get; set; }

        public bool CanRead { get; set; }

        public bool CanCreate { get; set; }

        public bool CanUpdate { get; set; }

        public bool CanApproval { get; set; }

        public bool CanDelete { get; set; }
    }
}
