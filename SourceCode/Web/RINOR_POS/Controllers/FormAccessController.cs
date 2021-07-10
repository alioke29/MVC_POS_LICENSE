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

namespace RINOR_POS.Controllers
{
    /// <summary>
    /// Form Access Controller
    /// </summary>
    public class FormAccessController : Controller
    {
        /// GET: FormAccess
        [Authorize]
        public ActionResult Index(string MenuUrl = "", int MenuId = 0)
        {
            if (UserProfile.MenuList != null)
                if (MenuId != 0)
                    UserProfile.menu_id = MenuId;

            return RedirectToAction("Index", MenuUrl);
        }
    }
}