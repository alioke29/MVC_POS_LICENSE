namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_payment_bydevice
    {
        [StringLength(170)]
        public string UniqId { get; set; }

        [StringLength(20)]
        public string DeviceCode { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PayGroupID { get; set; }

        [StringLength(50)]
        public string PayGroupName { get; set; }

        [StringLength(250)]
        public string PayTypeID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string PayTypeCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(150)]
        public string PayTypeName { get; set; }
    }
}
