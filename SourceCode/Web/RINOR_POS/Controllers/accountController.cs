using RINOR_POS.CustomAuthentication;
using RINOR_POS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RINOR_POS.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private ModelPOSDB _db = new ModelPOSDB();

        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Account", null);
            //return View();
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }

            string controllerName = RouteData.Values["controller"].ToString().ToLower();
            string actionName = RouteData.Values["action"].ToString().ToLower();
            ReturnUrl = (controllerName.Equals("account") && actionName.Equals("login")) ? "~/" : ReturnUrl;
            var loginView = new AccountLoginViewModel()
            {
                remember_me = true,
                return_url = ReturnUrl,
            };
            ViewBag.ReturnUrl = loginView.return_url;
            return View(loginView);
        }

        [HttpPost]
        public ActionResult Login(AccountLoginViewModel loginView, string ReturnUrl = "")
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Membership.ValidateUser(loginView.UserName, loginView.Password))
                    {
                        var user = (CustomMembershipUser)Membership.GetUser(loginView, false);
                        if (user != null)
                        {
                            RINOR_POS.Models.CustomSerializeViewModel userModel = new RINOR_POS.Models.CustomSerializeViewModel()
                            {
                                user_id = user.user_id,
                                user_name = user.user_name,
                                user_password = user.user_password,

                                employee_id = user.employee_id,
                                employee_nik = user.employee_nik,
                                employee_name = user.employee_name,
                                employee_email = user.employee_email,

                                fl_active = user.fl_active,
                                PathFolder = _db.pos_config.Where(a => a.ConfigCode == "PATH_FOLDER").FirstOrDefault().Value,
                                APIURLBase = _db.pos_config.Where(a => a.ConfigCode == "API_LICENSE").FirstOrDefault().Value

                            };

                            string userData = JsonConvert.SerializeObject(userModel);
                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                                (
                                1, loginView.UserName, DateTime.Now, DateTime.Now.AddHours(2), true, userData
                                );

                            string enTicket = FormsAuthentication.Encrypt(authTicket);
                            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, enTicket);
                            //faCookie.Name = userModel.user_id.ToString();
                            Response.Cookies.Add(faCookie);
                        }
                        else
                            throw new Exception("Something Wrong : User not Found.");

                        string controllerName = RouteData.Values["controller"].ToString().ToLower();
                        string actionName = RouteData.Values["action"].ToString().ToLower();
                        ReturnUrl = (controllerName.Equals("account") && actionName.Equals("login")) ? "~/" : ReturnUrl;

                        UserProfile.PathFolder = _db.pos_config.Where(a => a.ConfigCode == "PATH_FOLDER").FirstOrDefault().Value;
                        UserProfile.APIURLBase = _db.pos_config.Where(a => a.ConfigCode == "API_LICENSE").FirstOrDefault().Value;

                        //if (Url.IsLocalUrl(ReturnUrl))
                        if (!string.IsNullOrWhiteSpace(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                        ModelState.AddModelError("", "Something Wrong : Username/nik or Password invalid.");
                }
                else
                    ModelState.AddModelError("", "Something Wrong : Username/nik or Password invalid.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Invalid Login." + ex.Message);
            }

            return View(loginView);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(AccountRegistrationViewModel registrationView)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;

            if (ModelState.IsValid)
            {
                // Email Verification
                string userName = Membership.GetUserNameByEmail(registrationView.employee_email);
                if (!string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError("Warning Email", "Sorry: Email already Exists");
                    return View(registrationView);
                }

                //Save User Data 
                using (ModelPOSDB _db = new ModelPOSDB())
                {
                    //var user = new User()
                    //{
                    //    Username = registrationView.Username,
                    //    FirstName = registrationView.FirstName,
                    //    LastName = registrationView.LastName,
                    //    Email = registrationView.Email,
                    //    Password = registrationView.Password,
                    //    ActivationCode = Guid.NewGuid(),
                    //};
                    //dbContext.Users.Add(user);
                    var emp = new pos_employee()
                    {
                        employee_email = registrationView.employee_email,
                        employee_nik = registrationView.employee_nik,
                        employee_name = registrationView.employee_name,

                        fl_active = true,
                        created_by = UserProfile.UserId,
                        created_date = DateTime.Now,
                        updated_by = UserProfile.UserId,
                        updated_date = DateTime.Now,
                        deleted_by = null,
                        deleted_date = null
                    };

                    emp = _db.pos_employee.Add(emp);
                    //int emp_id = emp.employee_id;

                    var user = new pos_user_application()
                    {
                        user_name = registrationView.user_name,
                        user_password = registrationView.user_password,
                        employee_id = emp.employee_id,
                        fl_active = true,
                        created_by = UserProfile.UserId,
                        created_date = DateTime.Now,
                        updated_by = UserProfile.UserId,
                        updated_date = DateTime.Now,
                        deleted_by = null,
                        deleted_date = null
                    };
                    _db.pos_user_application.Add(user);

                    _db.SaveChanges();
                }

                messageRegistration = "Your account has been created successfully. ^_^";
                statusRegistration = true;
            }
            else
            {
                messageRegistration = "Something Wrong!";
            }
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;

            return View(registrationView);
        }

        [HttpGet]
        public ActionResult ActivationAccount(string id)
        {
            //bool statusAccount = false;
            //using (ModelPOSDB dbContext = new ModelPOSDB())
            //{
            //    var userAccount = dbContext.ms_user.Where(u => u.ActivationCode.ToString().Equals(id)).FirstOrDefault();

            //    if (userAccount != null)
            //    {
            //        userAccount.IsActive = true;
            //        dbContext.SaveChanges();
            //        statusAccount = true;
            //    }
            //    else
            //    {
            //        ViewBag.Message = "Something Wrong !!";
            //    }

            //}
            //ViewBag.Status = statusAccount;
            ViewBag.Status = true;
            return View();
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        [NonAction]
        public void VerificationEmail(string email, string activationCode)
        {

        }


        [HttpGet]
        public ActionResult ForgotPassword()
        {

            return View();
        }


        [HttpPost]
        public ActionResult ForgotPassword(AccountForgotPasswordViewModel forgotPassView)
        {

            return RedirectToAction("Login", "Account", null);
            //return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            AccountChangePasswordViewModel changePwd = new AccountChangePasswordViewModel()
            {
                UserName = UserProfile.UserName,
                user_id = UserProfile.UserId
            };

            ViewBag.Status = true;
            return View(changePwd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(AccountChangePasswordViewModel model)
        {
            pos_user_application _user = (from t in _db.pos_user_application
                                          where t.user_name == model.UserName && t.user_id == model.user_id
                                          select t).SingleOrDefault<pos_user_application>();

            if (_user != null)
            {
                if (model.NewPassword.ToLower().Equals(model.ConfirmPassword.ToLower()))
                {
                    _user.user_password = model.NewPassword;
                    _user.fl_active = true;
                    _user.updated_by = UserProfile.UserId;
                    _user.updated_date = DateTime.Now;
                    _user.deleted_by = null;
                    _user.deleted_date = null;
                    _db.Entry(_user).State = EntityState.Modified;
                    _db.SaveChanges();
                    ViewBag.ErrMessage = "Your password has been successfully changed.";
                    RedirectToAction("Index", "Account", null);
                    //RedirectToAction("Logout", "Account", null);
                }
                else
                {
                    //beda confirm
                    ViewBag.ErrMessage = "[New Password] not matched to [Confirm Password].";
                }

            }
            else
            {
                ViewBag.ErrMessage = "User " + _user.user_name + " not found...";
            }
            return View(model);
        }
    }
}