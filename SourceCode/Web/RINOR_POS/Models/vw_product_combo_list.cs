namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_product_combo_list
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductComboID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShopID { get; set; }

        [StringLength(50)]
        public string ShopName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductGroupID { get; set; }

        [StringLength(765)]
        public string ProductGroupName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string ProductComboName { get; set; }

        public int? CountProduct { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool Activate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
