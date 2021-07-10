namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_todo_list
    {
        [Key]
        public int ToDoListID { get; set; }

        public int? ShopID { get; set; }

        [Required]
        [StringLength(100)]
        public string ToDo { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? PIC { get; set; }

        public DateTime? TargetDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(300)]
        public string ImageServer { get; set; }

        [StringLength(300)]
        public string ImageClient { get; set; }

        [StringLength(500)]
        public string ActionPIC { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
