using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RINOR_POS.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace RINOR_POS
{
    /// <summary>
    /// Base of Controller
    /// </summary>
    [Authorize]
    public class BaseController : Controller
    {
        /// GET: Base
        public BaseController()
        {
            if (UserProfile.MenuList != null)
            {
                pos_role_menu menu = UserProfile.MenuList.Find(a => a.MenuId == UserProfile.menu_id);
                if (menu != null)
                {
                    ViewBag.CanRead = menu.CanRead;
                    ViewBag.CanCreate = menu.CanCreate;
                    ViewBag.CanUpdate = menu.CanUpdate;
                    ViewBag.CanApproval = menu.CanApproval;
                    ViewBag.CanDelete = menu.CanDelete;
                }
                else
                {
                    ViewBag.CanRead = false;
                    ViewBag.CanCreate = false;
                    ViewBag.CanUpdate = false;
                    ViewBag.CanApproval = false;
                    ViewBag.CanDelete = false;
                }
            }
        }
    }
}