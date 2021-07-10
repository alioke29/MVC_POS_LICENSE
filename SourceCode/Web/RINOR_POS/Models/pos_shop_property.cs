namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_shop_property
    {
        [Key]
        public long ShopPropertyID { get; set; }

        public int ShopID { get; set; }

        public int PropertyID { get; set; }

        [StringLength(150)]
        public string PropertyName { get; set; }

        [StringLength(60)]
        public string PropertyParam { get; set; }

        [StringLength(765)]
        public string PropertyDesp { get; set; }

        public decimal? DefaultDecimalValue { get; set; }

        [StringLength(150)]
        public string DefaultTextValue { get; set; }

        public bool? Active { get; set; }

        public int? Ordering { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
