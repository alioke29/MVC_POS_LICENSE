﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RINOR_POS.ModelLicence;

namespace RINOR_POS.Controllers
{
    public class homeController : Controller
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();

        [Authorize]
        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// list for index
        /// </summary>
        /// <returns>JSON</returns>
        public JsonResult GetIndexList()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var _qry = db.vw_pendingkey.Where(a => a.CountData > 0).ToList();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}