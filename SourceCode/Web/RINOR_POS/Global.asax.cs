using RINOR_POS.CustomAuthentication;
using RINOR_POS.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Optimization;
using System.Web.Http;
using RINOR_POS.App_Start;

namespace RINOR_POS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application_Start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var binder = new App_Start.UTCDateTimeModelBinder();
            ModelBinders.Binders.Add(typeof(DateTime), binder);
            ModelBinders.Binders.Add(typeof(DateTime?), binder);

            var decimal_binder = new App_Start.DecimalModelBinder();
            ModelBinders.Binders.Add(typeof(Decimal), decimal_binder);
            ModelBinders.Binders.Add(typeof(Decimal?), decimal_binder);
        }

        /// <summary>
        /// Session_Start
        /// </summary>
        protected void Session_Start()
        {
            if (UserProfile.employee_id == 0)
            {
                //Redirect to Welcome Page if Session is null  
                HttpContext.Current.Response.Redirect("~/Account/Login?ReturnUrl=", false);
            }
        }

        /// <summary>
        /// Application_PostAuthenticateRequest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel = JsonConvert.DeserializeObject<CustomSerializeViewModel>(authTicket.UserData);

                CustomPrincipal principal = new CustomPrincipal(authTicket.Name);

                principal.user_id = serializeModel.user_id;
                principal.user_name = serializeModel.user_name;
                principal.user_password = serializeModel.user_password;

                principal.employee_id = serializeModel.employee_id;
                principal.employee_nik = serializeModel.employee_nik;
                principal.employee_name = serializeModel.employee_name;
                principal.employee_email = serializeModel.employee_email;
                principal.APIURLBase = serializeModel.APIURLBase;
                principal.PathFolder = serializeModel.PathFolder;

                HttpContext.Current.User = principal;

                #region setelah login isi user profile 
                UserProfile.UserId = serializeModel.user_id;
                UserProfile.UserName = serializeModel.user_name;

                UserProfile.employee_id = serializeModel.employee_id;
                UserProfile.UserFullName = serializeModel.employee_name;

                UserProfile.PathFolder = serializeModel.PathFolder;
                UserProfile.APIURLBase = serializeModel.APIURLBase;
                //UserProfile.pos_employee = _db.pos_employee.Find(userModel.employee_id);
                #endregion

            }

        }

    }
}
