namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_user_register_role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int employee_id { get; set; }

        [StringLength(20)]
        public string employee_nik { get; set; }

        public int? ShopId { get; set; }

        [StringLength(50)]
        public string employee_name { get; set; }

        [StringLength(50)]
        public string employee_email { get; set; }

        public int? user_id { get; set; }

        public int? UserRoleId { get; set; }

        [StringLength(50)]
        public string user_name { get; set; }
    }
}
