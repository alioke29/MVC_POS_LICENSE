using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RINOR_POS.Models;

namespace RINOR_POS.Controllers
{
    [Authorize]
    public class preferencesController : Controller
    {
        private ModelPOSDB db = new ModelPOSDB();
        // GET: preferences
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult List(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {
            sord = (sord == null) ? "" : sord;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var PreferencesList = from pre in db.pos_program_property
                                  where (pre.Active == true)
                                  select pre;

            // search function
            if (_search)
            {
                switch (searchField)
                {
                    case "PropertyName":
                        PreferencesList = PreferencesList.Where(t => t.PropertyName.Contains(searchString));
                        break;
                    case "PropertyDesp":
                        PreferencesList = PreferencesList.Where(t => t.PropertyDesp.Contains(searchString));
                        break;
                    case "DefaultTextValue":
                        PreferencesList = PreferencesList.Where(t => t.DefaultTextValue.Contains(searchString));
                        break;
                }
            }
            //calc paging
            int totalRecords = PreferencesList.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            //default sorting
            if (sord.ToUpper() == "DESC")
            {
                PreferencesList = PreferencesList.OrderByDescending(t => t.Ordering);
                PreferencesList = PreferencesList.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                PreferencesList = PreferencesList.OrderBy(t => t.Ordering);
                PreferencesList = PreferencesList.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = PreferencesList
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private bool IsNumeric(string id)
        {
            int ID;
            return int.TryParse(id, out ID);
        }

        [HttpPost]
        public JsonResult Crud()
        {
            if (Request.Form["oper"] == "edit")
            {
                if (IsNumeric(Request.Form["PropertyID"].ToString()))
                {
                    //prepare for update data
                    int id = Convert.ToInt32(Request.Form["PropertyID"]);
                    pos_program_property pre = db.pos_program_property.Find(id);
                    pre.DefaultDecimalValue = Convert.ToDecimal(Request.Form["DefaultDecimalValue"]);
                    pre.DefaultTextValue = Request.Form["DefaultTextValue"];
                    db.SaveChanges();
                    
                }
            }
            return Json("Preference successfully saved", JsonRequestBehavior.AllowGet);
        }
    }
}