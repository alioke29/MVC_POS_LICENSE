namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_kds_front_end
    {
        [Key]
        [Column(Order = 0)]
        public int IdKDSFrontEnd { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdKDSBackEnd { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string OrderNo { get; set; }

        public int? Ordering { get; set; }

        public DateTime? InsertedWaitingListDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public bool? isWaitingList { get; set; }

        public bool? isFinish { get; set; }

        [StringLength(50)]
        public string Status { get; set; }
    }
}
