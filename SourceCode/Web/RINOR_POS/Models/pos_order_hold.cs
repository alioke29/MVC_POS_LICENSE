namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_order_hold
    {
        [Key]
        public int OrderHoldID { get; set; }

        [StringLength(50)]
        public string SessionID { get; set; }

        public int? IdUser { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

        public DateTime? OrderTime { get; set; }

        public int? Qty { get; set; }

        public decimal? Total { get; set; }

        public int? SaleModeID { get; set; }
    }
}
