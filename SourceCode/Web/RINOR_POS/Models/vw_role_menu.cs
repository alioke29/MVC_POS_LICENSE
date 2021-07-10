namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_role_menu
    {
        public int? RoleMenuAccessId { get; set; }

        public int? UserRoleID { get; set; }

        public int? ShopID { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        public int? MenuId { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int module_id { get; set; }

        [StringLength(10)]
        public string module_code { get; set; }

        [StringLength(50)]
        public string module_name { get; set; }

        public int? rec_order_module { get; set; }

        [StringLength(20)]
        public string menu_code { get; set; }

        [StringLength(50)]
        public string menu_name { get; set; }

        public int? rec_order_menu { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool CanRead { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool CanCreate { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool CanUpdate { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool CanDelete { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
