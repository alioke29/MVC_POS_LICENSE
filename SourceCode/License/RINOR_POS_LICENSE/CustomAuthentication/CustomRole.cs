using RINOR_POS.ModelLicence;
using RINOR_POS.CustomAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace RINOR_POS.CustomAuthentication
{
    public class CustomRole : RoleProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string username)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var userRoles = new string[] { };

            using (ModelLicencePOSDB _db = new ModelLicencePOSDB())
            {
                var selectedUser = (from usr in _db.pos_user_application.Include("pos_employee")
                                    where string.Compare(usr.user_name, username, StringComparison.OrdinalIgnoreCase) == 0
                                    && usr.deleted_date == null && usr.fl_active == true
                                    select usr
                                    ).FirstOrDefault();

                
                if(selectedUser != null)
                {
                    //userRoles = new[] { selectedUser.ms_employee.ms_employee_detail.Select(r=>r.ms_user_type.user_type_code).ToString() };
                }

                return userRoles.ToArray();
            }


        }

                                                   

        #region Overrides of Role Provider

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }


        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}