namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_order_complete_totaldiscount
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idOrderComplete { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string sessionId { get; set; }

        [StringLength(50)]
        public string SessionKey { get; set; }

        public int? IdUser { get; set; }

        public decimal? TotalDiscountManual { get; set; }

        public decimal? TotalPromotion { get; set; }

        public int? TotalDiscountBill { get; set; }

        public decimal? TotalPromoPayment { get; set; }
    }
}
