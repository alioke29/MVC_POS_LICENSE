using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RINOR_POS.Controllers
{
    public class errorController : Controller
    {
        // GET: error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}