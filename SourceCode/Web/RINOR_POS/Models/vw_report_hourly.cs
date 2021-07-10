namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_report_hourly
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idOrderDetailComplete { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string computerID { get; set; }

        public DateTime? datetime { get; set; }

        [StringLength(4000)]
        public string hour { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        public int? Qty { get; set; }

        public decimal? SubTotal { get; set; }
    }
}
