namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_printers_bysession
    {
        [StringLength(150)]
        public string PrintID { get; set; }

        [Key]
        [StringLength(50)]
        public string sessionId { get; set; }

        [StringLength(50)]
        public string SessionKey { get; set; }

        public int? PrinterTypeID { get; set; }

        [StringLength(10)]
        public string PrintDesign { get; set; }
    }
}
