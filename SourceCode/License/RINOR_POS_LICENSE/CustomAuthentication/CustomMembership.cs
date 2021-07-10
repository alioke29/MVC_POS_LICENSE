using RINOR_POS.ModelLicence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace RINOR_POS.CustomAuthentication
{
    public class CustomMembership : MembershipProvider
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username">ini sebenarnya username OR NIK</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            using (ModelLicencePOSDB _db = new ModelLicencePOSDB())
            {
                //sesuai request user : bisa userid atau nik
                //date: 2019-08-24
                var user = (from _usr in _db.pos_user_application
                            join _emp in _db.pos_employee on _usr.employee_id equals _emp.employee_id
                            where (string.Compare(username, _usr.user_name, StringComparison.OrdinalIgnoreCase) == 0
                            || string.Compare(username, _emp.employee_nik, StringComparison.OrdinalIgnoreCase) == 0)
                            && string.Compare(password, _usr.user_password, StringComparison.OrdinalIgnoreCase) == 0
                            && _usr.fl_active == true
                            && _usr.deleted_date == null
                            select _usr).FirstOrDefault();


                return (user != null) ? true : false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="passwordQuestion"></param>
        /// <param name="passwordAnswer"></param>
        /// <param name="isApproved"></param>
        /// <param name="providerUserKey"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userIsOnline"></param>
        /// <returns></returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object loginview, bool userIsOnline)
        {
            using (ModelLicencePOSDB _db = new ModelLicencePOSDB())
            {
                AccountLoginViewModel accountloginview = loginview as AccountLoginViewModel;
                string username = accountloginview.UserName;

                var user = (from _usr in _db.pos_user_application
                            join _emp in _db.pos_employee on _usr.employee_id equals _emp.employee_id
                            where (string.Compare(username, _usr.user_name, StringComparison.OrdinalIgnoreCase) == 0
                            || string.Compare(username, _emp.employee_nik, StringComparison.OrdinalIgnoreCase) == 0)
                            && _usr.fl_active == true
                            && _usr.deleted_date == null
                            
                            select _usr).FirstOrDefault();

                if (user == null)
                {
                    return null;
                }

                //var selectedUser = new CustomMembershipUser(user);
                var selectedUser = new CustomMembershipUser(
                    new User
                    {
                        user_id = user.user_id,
                        user_name = user.user_name,
                        user_password = user.user_password,

                        employee_id = user.employee_id,
                        employee_nik = user.pos_employee.employee_nik,
                        employee_name = user.pos_employee.employee_name,
                        employee_email = user.pos_employee.employee_email,

                        fl_active = (bool)user.fl_active,
                    }
                    );

                return selectedUser;
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            using (ModelLicencePOSDB dbContext = new ModelLicencePOSDB())
            {
                string username = (from u in dbContext.pos_user_application
                                   where string.Compare(email, u.pos_employee.employee_email) == 0
                                   select u.user_name).FirstOrDefault();

                return !string.IsNullOrEmpty(username) ? username : string.Empty;
            }
        }


        #region Overrides of Membership Provider

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

        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}