using System;
using System.IO;
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
    public class printtypeController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: printtype
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

            var _qry = (from a in db.pos_printer_type.Where(a => a.DeletedDate == null)
                        select new printertypeViewModel
                        {
                            PrinterTypeID = a.PrinterTypeID,
                            PrinterTypeName = a.PrinterTypeName
                        }).ToList<printertypeViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: printertype/Edit/5
        public JsonResult GetDataEdit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var printertype_data = db.pos_printer_type.Find(id);

            return Json(printertype_data, JsonRequestBehavior.AllowGet);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]
        public JsonResult Edit(int id, string name)
        {
            pos_printer_type pos_printer_type = db.pos_printer_type.Find(id);
            if (pos_printer_type != null)
            {
                pos_printer_type.PrinterTypeName = name;
                pos_printer_type.UpdatedBy = UserProfile.employee_id;
                pos_printer_type.UpdatedDate = DateTime.Now;
                db.Entry(pos_printer_type).State = EntityState.Modified;
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                pos_printer_type posprintertype = new pos_printer_type();
                posprintertype.PrinterTypeName = name;
                posprintertype.CreatedBy = UserProfile.employee_id;
                posprintertype.CreatedDate = DateTime.Now;
                posprintertype = db.pos_printer_type.Add(posprintertype);
                db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: printertype/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_printer_type pos_printer_type = db.pos_printer_type.Find(id);
            if (pos_printer_type != null)
            {
                pos_printer_type.DeletedBy = UserProfile.employee_id;
                pos_printer_type.DeletedDate = DateTime.Now;
                db.Entry(pos_printer_type).State = EntityState.Modified;
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}