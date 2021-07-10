namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_initial_cash
    {
        [Key]
        public int InitialCashID { get; set; }

        public int ShopId { get; set; }

        public long Amount { get; set; }

        public DateTime? InsertedTime { get; set; }

        public int? InsertedBy { get; set; }

        public bool? isFilled { get; set; }
    }
}
