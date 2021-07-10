namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_currency
    {
        [Key]
        public int CurrencyId { get; set; }

        [Required]
        [StringLength(3)]
        public string CurrencyCode { get; set; }

        [StringLength(20)]
        public string CurrencyName { get; set; }

        [StringLength(50)]
        public string CurrencyCountry { get; set; }
    }
}
