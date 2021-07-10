namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_sync_log
    {
        [Key]
        public int SyncID { get; set; }

        public int? ShopID { get; set; }

        public DateTime? SyncLastDate { get; set; }

        [StringLength(20)]
        public string SyncBy { get; set; }

        [StringLength(15)]
        public string Status { get; set; }
    }
}
