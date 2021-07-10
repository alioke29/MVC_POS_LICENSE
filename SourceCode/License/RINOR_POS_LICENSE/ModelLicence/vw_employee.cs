namespace RINOR_POS.ModelLicence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_employee
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int employee_id { get; set; }

        [StringLength(20)]
        public string employee_nik { get; set; }

        public int? ShopId { get; set; }

        [StringLength(50)]
        public string employee_name { get; set; }

        [StringLength(50)]
        public string employee_email { get; set; }

        public bool? fl_active { get; set; }

        public DateTime? deleted_date { get; set; }

        [StringLength(10)]
        public string ShopCode { get; set; }

        [StringLength(50)]
        public string ShopKey { get; set; }

        [StringLength(50)]
        public string ShopName { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BrandID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string BrandCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string BrandName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MerchantID { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(30)]
        public string MerchantCode { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(300)]
        public string MerchantName { get; set; }
    }
}
