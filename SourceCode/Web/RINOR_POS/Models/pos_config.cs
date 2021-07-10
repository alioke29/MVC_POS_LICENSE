namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_config
    {
        [Key]
        public int ConfigID { get; set; }

        [StringLength(20)]
        public string ConfigCode { get; set; }

        [StringLength(50)]
        public string ConfigName { get; set; }

        [StringLength(500)]
        public string Value { get; set; }
    }
}
