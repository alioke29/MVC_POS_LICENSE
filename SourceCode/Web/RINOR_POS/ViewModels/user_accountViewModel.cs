using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RINOR_POS;

namespace RINOR_POS.Models
{
    public class user_accountViewModel
    {
        public user_accountViewModel()
        {
            userrole_list = new List<pos_user_role>();
        }
        public List<pos_user_role> userrole_list { get; set; }

        [Key]
        public int user_id { get; set; }

        [Display(Name = "User Role")]
        [Required]
        public int? UserRoleId { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "User Name")]
        public string user_name { get; set; }

        [StringLength(100)]
        [Display(Name = "User Password")]
        public string user_password { get; set; }

        [Display(Name = "Employee")]
        public int employee_id { get; set; }

        [StringLength(20)]
        [Display(Name = "NIK")]
        public string employee_nik { get; set; }

        [StringLength(50)]
        [Display(Name = "Employee Name")]
        public string employee_name { get; set; }

        [StringLength(50)]
        [Display(Name = "Email Address")]
        public string employee_email { get; set; }

        [Display(Name = "Active")]
        public string rec_isactive { get; set; }

        [Display(Name = "Activate")]
        public bool? fl_active { get; set; }

        [Display(Name = "Last Modified Date")]
        public DateTime? rec_modified_date { get; set; }

        [Display(Name = "Last Modified By")]
        public string rec_modified_by { get; set; }

    }

}