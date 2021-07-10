namespace RINOR_POS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pos_employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pos_employee()
        {
            pos_user_application = new HashSet<pos_user_application>();
        }

        [Key]
        public int employee_id { get; set; }

        [StringLength(20)]
        public string employee_nik { get; set; }

        [StringLength(50)]
        public string employee_name { get; set; }

        [StringLength(50)]
        public string employee_email { get; set; }

        public int? ShopId { get; set; }

        public bool? fl_active { get; set; }

        public DateTime? created_date { get; set; }

        public int? created_by { get; set; }

        public DateTime? updated_date { get; set; }

        public int? updated_by { get; set; }

        public DateTime? deleted_date { get; set; }

        public int? deleted_by { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pos_user_application> pos_user_application { get; set; }
    }
}
