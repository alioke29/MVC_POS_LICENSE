namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class crud_sample
    {
        [Key]
        public int crud_key { get; set; }

        [StringLength(50)]
        public string field_string { get; set; }

        public int? field_opsi_id { get; set; }

        public bool? field_flag { get; set; }

        public int? field_number { get; set; }

        public decimal? field_decimal { get; set; }

        public DateTime? field_date { get; set; }

        public DateTime? created_date { get; set; }

        public int? created_by { get; set; }

        public DateTime? updated_date { get; set; }

        public int? updated_by { get; set; }

        public DateTime? deleted_date { get; set; }

        public int? deleted_by { get; set; }
    }
}
