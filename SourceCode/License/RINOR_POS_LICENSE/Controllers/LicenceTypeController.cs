using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RINOR_POS.ModelLicence;

namespace RINOR_POS.Controllers
{
    /// <summary>
    /// Licence Type
    /// </summary>
    [Authorize]
    public class LicenceTypeController : Controller
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();
        /// GET: LicenceType
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

            var _qry = (from a in db.POS_Licence_Type.Where(a => a.DeletedDate == null)
                        select new LicenseTypeViewModel
                        {
                            LicenceTypeID = a.LicenceTypeID,
                            LicenceName = a.LicenceName,
                            Duration = a.Duration,
                            isActive = a.isActive
                        }).ToList<LicenseTypeViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        /// GET: licencetype/Create
        public ActionResult Create(int? id)
        {
            LicenseTypeViewModel view = new LicenseTypeViewModel();
            view.isActive = false;
            view.CurrencyList = db.pos_currency.ToList();
            return View(view);
        }

        /// POST: licencetype/Create
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LicenseTypeViewModel licencetypedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    POS_Licence_Type Pos_Licence_Type = new POS_Licence_Type();
                    Pos_Licence_Type.LicenceName = licencetypedata.LicenceName;
                    Pos_Licence_Type.Duration = licencetypedata.Duration;
                    Pos_Licence_Type.CurrencyId = licencetypedata.CurrencyId;
                    Pos_Licence_Type.Pricing = licencetypedata.Pricing;
                    Pos_Licence_Type.isActive = licencetypedata.isActive;
                    Pos_Licence_Type.InsertedBy = UserProfile.employee_id;
                    Pos_Licence_Type.InsertedDate = DateTime.Now;

                    Pos_Licence_Type = db.POS_Licence_Type.Add(Pos_Licence_Type);
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
            licencetypedata.CurrencyList = db.pos_currency.ToList();
            return View(licencetypedata);
        }

        /// GET: licencetype/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            POS_Licence_Type licencetype_data = db.POS_Licence_Type.Find(id);
            if (licencetype_data == null)
            {
                return HttpNotFound("Crud not found.");
            }

            LicenseTypeViewModel licencetypeView = new LicenseTypeViewModel()
            {
                LicenceTypeID = licencetype_data.LicenceTypeID,
                LicenceName = licencetype_data.LicenceName,
                Duration = licencetype_data.Duration,
                CurrencyId = licencetype_data.CurrencyId,
                Pricing = licencetype_data.Pricing,
                isActive = licencetype_data.isActive,
                CurrencyList = db.pos_currency.ToList()
            };

            return View(licencetypeView);
        }

        /// POST: crud/Edit
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LicenseTypeViewModel licencetypedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    POS_Licence_Type Pos_Licence_Type = db.POS_Licence_Type.Find(licencetypedata.LicenceTypeID);
                    Pos_Licence_Type.LicenceName = licencetypedata.LicenceName;
                    Pos_Licence_Type.LicenceName = licencetypedata.LicenceName;
                    Pos_Licence_Type.Duration = licencetypedata.Duration;
                    Pos_Licence_Type.CurrencyId = licencetypedata.CurrencyId;
                    Pos_Licence_Type.Pricing = licencetypedata.Pricing;
                    Pos_Licence_Type.isActive = licencetypedata.isActive;
                    Pos_Licence_Type.UpdatedBy = UserProfile.employee_id;
                    Pos_Licence_Type.UpdatedDate = DateTime.Now;

                    db.Entry(Pos_Licence_Type).State = EntityState.Modified;
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
            licencetypedata.CurrencyList = db.pos_currency.ToList();
            return View(licencetypedata);
        }

        /// POST: licencetype/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            POS_Licence_Type Pos_Licence_Type = db.POS_Licence_Type.Find(id);
            if (Pos_Licence_Type != null)
            {
                Pos_Licence_Type.DeletedBy = UserProfile.UserId;
                Pos_Licence_Type.DeletedDate = DateTime.Now;
                db.Entry(Pos_Licence_Type).State = EntityState.Modified;
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