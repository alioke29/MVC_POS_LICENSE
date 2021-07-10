using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RINOR_POS.Models
{
    public class todolistViewModel
    {
        public todolistViewModel()
        { 

        }

        [Key]
        [Display(Name = "To Do List")]
        public int ToDoListID { get; set; }

        public int? ShopID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "To Do")]
        public string ToDo { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public int? PIC { get; set; }

        [Display(Name = "Target Date")]
        public DateTime? TargetDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Display(Name = "Attachment")]
        public string ImageServer { get; set; }

        [StringLength(300)]
        public string ImageClient { get; set; }

        [StringLength(500)]
        [Display(Name = "Action PIC")]
        public string ActionPIC { get; set; }
    }
}