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
    public class licenceController : Controller
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: licence
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

            var _qry = (from a in db.pos_creditcard_bank.Where(a => a.DeletedDate == null)
                        select new ccbankViewModel
                        {
                            CreditCardBankID = a.CreditCardBankID,
                            CreditCardBank = a.CreditCardBank
                        }).ToList<ccbankViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: ccbank/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        // GET: ccbank/Detail/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_creditcard_bank ccBank = db.pos_creditcard_bank.Find(id);

            if (ccBank == null)
            {
                return HttpNotFound("Credit Card Bank not found.");
            }

            ccbankViewModel ccbankViewModel = new ccbankViewModel()
            {
                CreditCardBankID = ccBank.CreditCardBankID,
                CreditCardBank = ccBank.CreditCardBank
            };

            return View(ccbankViewModel);
        }

        // POST: ccbank/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CreditCardBank")] ccbankViewModel ccbankdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_creditcard_bank pos_creditcard_bank = new pos_creditcard_bank();
                    pos_creditcard_bank.CreditCardBank = ccbankdata.CreditCardBank;

                    pos_creditcard_bank = db.pos_creditcard_bank.Add(pos_creditcard_bank);
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

            return View(ccbankdata);
        }

        // GET: ccbank/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_creditcard_bank ccbank_data = db.pos_creditcard_bank.Find(id);
            if (ccbank_data == null)
            {
                return HttpNotFound("Crud not found.");
            }

            ccbankViewModel ccBankView = new ccbankViewModel()
            {
                CreditCardBankID = ccbank_data.CreditCardBankID,
                CreditCardBank = ccbank_data.CreditCardBank
            };

            return View(ccBankView);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CreditCardBankID, CreditCardBank")] ccbankViewModel ccbankdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_creditcard_bank pos_creditcard_bank = db.pos_creditcard_bank.Find(ccbankdata.CreditCardBankID);
                    pos_creditcard_bank.CreditCardBank = ccbankdata.CreditCardBank;

                    db.Entry(pos_creditcard_bank).State = EntityState.Modified;
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

            return View(ccbankdata);
        }

        // POST: ccbank/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_creditcard_bank pos_creditcard_bank = db.pos_creditcard_bank.Find(id);
            if (pos_creditcard_bank != null)
            {
                pos_creditcard_bank.DeletedBy = UserProfile.UserId;
                pos_creditcard_bank.DeletedDate = DateTime.Now;
                db.Entry(pos_creditcard_bank).State = EntityState.Modified;
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