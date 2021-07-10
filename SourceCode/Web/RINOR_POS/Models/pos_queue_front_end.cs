namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_queue_front_end
    {
        [Key]
        [Column(Order = 0)]
        public int IdQueueFrontEnd { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdOrderComplete { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SaleModeID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MasterShopID { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShopID { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUser { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string OrderNo { get; set; }

        public DateTime? InsertedQueueDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public bool? isWaitingList { get; set; }

        public bool? isFinish { get; set; }

        [StringLength(50)]
        public string Status { get; set; }
    }
}
