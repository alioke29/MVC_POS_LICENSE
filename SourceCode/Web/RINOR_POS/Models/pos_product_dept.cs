namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_product_dept
    {
        [Key]
        public int ProductDeptID { get; set; }

        public int ProductGroupID { get; set; }

        public int ShopID { get; set; }

        [StringLength(150)]
        public string ProductDeptCode { get; set; }

        [StringLength(765)]
        public string ProductDeptName { get; set; }

        public bool? ProductDeptActivate { get; set; }

        public int? ProductDeptOrdering { get; set; }

        public bool? DisplayMobile { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
