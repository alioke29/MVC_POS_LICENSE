namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SessionID { get; set; }

        public int? ShopID { get; set; }

        public int? ComputerID { get; set; }

        [StringLength(50)]
        public string SessionKey { get; set; }

        [StringLength(50)]
        public string ComputerName { get; set; }

        public int? OpenStaffID { get; set; }

        [StringLength(100)]
        public string OpenStaff { get; set; }

        public int? CloseStaffID { get; set; }

        [StringLength(100)]
        public string CloseStaff { get; set; }

        public DateTime? OpenSessionDate { get; set; }

        public DateTime? CloseSessionDate { get; set; }

        public DateTime? SessionDate { get; set; }

        public decimal? OpenSessionAmount { get; set; }

        public decimal? CloseSessionAmount { get; set; }
    }
}
