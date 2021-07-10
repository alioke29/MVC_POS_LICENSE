using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace RINOR_POS.Models
{
    public class UserProfile
    {
        public UserProfile()
        {
            MenuList = new List<pos_role_menu>();
        }

        #region diisi dari FormLogin di depan

        public static int UserId { get; set; }

        public static string UserName { get; set; }

        #endregion

        public static string UserFullName { get; set; }

        public static int employee_id { get; set; }

        public static pos_employee pos_employee { get; set; }

        public static int menu_id { get; set; }

        public static string PathFolder { get; set; }

        public static string APIURLBase { get; set; }

        public static List<pos_role_menu> MenuList { get; set; }

    }
}