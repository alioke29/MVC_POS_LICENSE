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
    public class productvatController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: productvat
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

            var _qry = (from a in db.pos_product_vat.Where(a => a.DeletedDate == null)

                        select new productvatViewModel
                        {
                            ProductVATID = a.ProductVATID,
                            ProductVATCode = a.ProductVATCode,
                            ProductVATPercent = a.ProductVATPercent,
                            VATDisplay = a.VATDisplay,
                            VATDesp = a.VATDesp
                        }).ToList<productvatViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: productvat/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        // GET: productvat/Detail/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_product_vat productvat_data = db.pos_product_vat.Find(id);

            if (productvat_data == null)
            {
                return HttpNotFound("Product VAT not found.");
            }

            productvatViewModel productvat_model = new productvatViewModel()
            {
                ProductVATID = productvat_data.ProductVATID,
                ProductVATCode = productvat_data.ProductVATCode,
                ProductVATPercent = productvat_data.ProductVATPercent,
                VATDisplay = productvat_data.VATDisplay,
                VATDesp = productvat_data.VATDesp
            };

            return View(productvat_model);
        }

        // POST: crud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductVATCode, ProductVATPercent, VATDisplay, VATDesp")] productvatViewModel productvat_data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_product_vat pos_product_vat = new pos_product_vat();
                    pos_product_vat.ProductVATCode = productvat_data.ProductVATCode;
                    pos_product_vat.ProductVATPercent = productvat_data.ProductVATPercent;
                    pos_product_vat.VATDisplay = productvat_data.VATDisplay;
                    pos_product_vat.VATDesp = productvat_data.VATDesp;
                    pos_product_vat.CreatedDate = DateTime.Now;
                    pos_product_vat.CreatedBy = UserProfile.UserId;

                    pos_product_vat = db.pos_product_vat.Add(pos_product_vat);
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

            return View(productvat_data);
        }

        // GET: crud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_product_vat productvat_data = db.pos_product_vat.Find(id);
            if (productvat_data == null)
            {
                return HttpNotFound("Product VAT not found.");
            }

            productvatViewModel productvat_model = new productvatViewModel()
            {
                ProductVATID = productvat_data.ProductVATID,
                ProductVATCode = productvat_data.ProductVATCode,
                ProductVATPercent = productvat_data.ProductVATPercent,
                VATDisplay = productvat_data.VATDisplay,
                VATDesp = productvat_data.VATDesp
            };

            return View(productvat_model);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductVATID, ProductVATCode, ProductVATPercent, VATDisplay, VATDesp")] productvatViewModel productvat_data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_product_vat pos_product_vat = db.pos_product_vat.Find(productvat_data.ProductVATID);
                    pos_product_vat.ProductVATCode = productvat_data.ProductVATCode;
                    pos_product_vat.ProductVATPercent = productvat_data.ProductVATPercent;
                    pos_product_vat.VATDisplay = productvat_data.VATDisplay;
                    pos_product_vat.VATDesp = productvat_data.VATDesp;
                    pos_product_vat.UpdatedDate = DateTime.Now;
                    pos_product_vat.UpdatedBy = UserProfile.UserId;

                    db.Entry(pos_product_vat).State = EntityState.Modified;
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

            return View(productvat_data);
        }

        // POST: crud/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_product_vat pos_product_vat = db.pos_product_vat.Find(id);
            if (pos_product_vat != null)
            {
                pos_product_vat.DeletedBy = UserProfile.UserId;
                pos_product_vat.DeletedDate = DateTime.Now;
                db.Entry(pos_product_vat).State = EntityState.Modified;
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