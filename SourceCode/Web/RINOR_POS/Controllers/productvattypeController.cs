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
    public class productvattypeController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: crud
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

            var _qry = (from a in db.pos_product_vat_type.Where(a => a.DeletedDate == null)
                        select new productvattypeViewModel
                        {
                            VATTypeID = a.VATTypeID,
                            VATTypeName = a.VATTypeName
                        }).ToList<productvattypeViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: productvattype/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        // GET: productvattype/Detail/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_product_vat_type ccType = db.pos_product_vat_type.Find(id);

            if (ccType == null)
            {
                return HttpNotFound("Credit Card Type not found.");
            }

            productvattypeViewModel productvattypeViewModel = new productvattypeViewModel()
            {
                VATTypeID = ccType.VATTypeID,
                VATTypeName = ccType.VATTypeName
            };

            return View(productvattypeViewModel);
        }

        // POST: productvattype/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VATTypeName")] productvattypeViewModel productvattypedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_product_vat_type pos_product_vat_type = new pos_product_vat_type();
                    pos_product_vat_type.VATTypeName = productvattypedata.VATTypeName;

                    pos_product_vat_type = db.pos_product_vat_type.Add(pos_product_vat_type);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(productvattypedata);
        }

        // GET: productvattype/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_product_vat_type productvattype_data = db.pos_product_vat_type.Find(id);
            if (productvattype_data == null)
            {
                return HttpNotFound("Crud not found.");
            }

            productvattypeViewModel ccTypeView = new productvattypeViewModel()
            {
                VATTypeID = productvattype_data.VATTypeID,
                VATTypeName = productvattype_data.VATTypeName
            };

            return View(ccTypeView);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VATTypeID, VATTypeName")] productvattypeViewModel productvattypedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_product_vat_type pos_product_vat_type = db.pos_product_vat_type.Find(productvattypedata.VATTypeID);
                    pos_product_vat_type.VATTypeName = productvattypedata.VATTypeName;

                    db.Entry(pos_product_vat_type).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(productvattypedata);
        }

        // POST: productvattype/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_product_vat_type pos_product_vat_type = db.pos_product_vat_type.Find(id);
            if (pos_product_vat_type != null)
            {
                pos_product_vat_type.DeletedBy = UserProfile.UserId;
                pos_product_vat_type.DeletedDate = DateTime.Now;
                db.Entry(pos_product_vat_type).State = EntityState.Modified;
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