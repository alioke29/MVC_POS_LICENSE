namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_order_no
    {
        [Key]
        public long OrderNoID { get; set; }

        public int OrderNo { get; set; }

        [Required]
        public string SessionId { get; set; }
    }
}
