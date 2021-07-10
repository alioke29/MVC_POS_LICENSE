namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_pay_group_bydevice
    {
        [StringLength(70)]
        public string UniqID { get; set; }

        [StringLength(20)]
        public string DeviceCode { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PayGroupID { get; set; }

        [StringLength(50)]
        public string PayGroupName { get; set; }
    }
}
