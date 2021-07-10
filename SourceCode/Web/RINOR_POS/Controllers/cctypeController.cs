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
    public class cctypeController : BaseController
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

            var _qry = (from a in db.pos_creditcard_type.Where(a => a.DeletedDate == null)
                        select new cctypeViewModel
                        {
                            CreditCardTypeID = a.CreditCardTypeID,
                            CreditCardType = a.CreditCardType
                        }).ToList<cctypeViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: cctype/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        // GET: cctype/Detail/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_creditcard_type ccType = db.pos_creditcard_type.Find(id);

            if (ccType == null)
            {
                return HttpNotFound("Credit Card Type not found.");
            }

            cctypeViewModel cctypeViewModel = new cctypeViewModel()
            {
                CreditCardTypeID = ccType.CreditCardTypeID,
                CreditCardType = ccType.CreditCardType
            };

            return View(cctypeViewModel);
        }

        // POST: cctype/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CreditCardType")] cctypeViewModel cctypedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_creditcard_type pos_creditcard_type = new pos_creditcard_type();
                    pos_creditcard_type.CreditCardType = cctypedata.CreditCardType;
                    pos_creditcard_type.CreatedBy = UserProfile.employee_id;
                    pos_creditcard_type.CreatedDate = DateTime.Now;
                    pos_creditcard_type = db.pos_creditcard_type.Add(pos_creditcard_type);
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

            return View(cctypedata);
        }

        // GET: cctype/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_creditcard_type cctype_data = db.pos_creditcard_type.Find(id);
            if (cctype_data == null)
            {
                return HttpNotFound("Crud not found.");
            }

            cctypeViewModel ccTypeView = new cctypeViewModel()
            {
                CreditCardTypeID = cctype_data.CreditCardTypeID,
                CreditCardType = cctype_data.CreditCardType
            };

            return View(ccTypeView);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CreditCardTypeID, CreditCardType")] cctypeViewModel cctypedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_creditcard_type pos_creditcard_type = db.pos_creditcard_type.Find(cctypedata.CreditCardTypeID);
                    pos_creditcard_type.CreditCardType = cctypedata.CreditCardType;
                    pos_creditcard_type.UpdatedBy = UserProfile.employee_id;
                    pos_creditcard_type.UpdatedDate = DateTime.Now;
                    db.Entry(pos_creditcard_type).State = EntityState.Modified;
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

            return View(cctypedata);
        }

        // POST: cctype/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_creditcard_type pos_creditcard_type = db.pos_creditcard_type.Find(id);
            if (pos_creditcard_type != null)
            {
                pos_creditcard_type.DeletedBy = UserProfile.UserId;
                pos_creditcard_type.DeletedDate = DateTime.Now;
                db.Entry(pos_creditcard_type).State = EntityState.Modified;
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