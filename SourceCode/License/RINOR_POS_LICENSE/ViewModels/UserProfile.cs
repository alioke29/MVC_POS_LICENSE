using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace RINOR_POS.ModelLicence
{
    public class UserProfile
    {
        #region diisi dari FormLogin di depan

        public static int UserId { get; set; }

        public static string UserName { get; set; }

        #endregion

        public static string UserFullName { get; set; }

        public static int employee_id { get; set; }

        public static pos_employee pos_employee { get; set; }        

    }
}