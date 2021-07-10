namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_user_role
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserRoleID { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShopID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(300)]
        public string MerchantName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string BrandName { get; set; }

        [StringLength(50)]
        public string ShopName { get; set; }

        public int? TotalUser { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MerchantID { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BrandID { get; set; }
    }
}
