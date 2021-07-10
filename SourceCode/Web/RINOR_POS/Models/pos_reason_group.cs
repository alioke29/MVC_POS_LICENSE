namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_reason_group
    {
        [Key]
        public int ReasonGroupID { get; set; }

        public int ShopID { get; set; }

        [Required]
        [StringLength(50)]
        public string ReasonGroupName { get; set; }

        public bool isRequiredAmount { get; set; }

        public bool Activate { get; set; }

        public bool AllowedInput { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
