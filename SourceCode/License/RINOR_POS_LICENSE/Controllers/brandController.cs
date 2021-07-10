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
    [Authorize]
    public class brandController : Controller
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();

        // GET: brand
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

            var _qry = (from a in db.pos_brand_data.Where(a => a.DeletedDate == null)
                        
                        join b in db.pos_merchant_data.Where(b => b.DeletedDate == null)
                        on a.MerchantID equals b.MerchantID

                        select new brandViewModel
                        {
                            BrandID = a.BrandID,
                            BrandKey = a.BrandKey,
                            MerchantID = a.MerchantID,
                            MerchantName = b.MerchantName,
                            pos_merchant_data = b,
                            BrandCode = a.BrandCode,
                            BrandName = a.BrandName,
                            IPAddress = a.IPAddress,
                            Activate = a.Activate
                        }).ToList<brandViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: brand/Create
        public ActionResult Create(int? id)
        {
            brandViewModel branddata = new brandViewModel()
            {
                merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList()
            };

            return View(branddata);
        }

        // GET: brand/Detail/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_brand_data brand_data = db.pos_brand_data.Find(id);

            if (brand_data == null)
            {
                return HttpNotFound("Brand not found.");
            }

            pos_merchant_data merchantdata = db.pos_merchant_data.Find(brand_data.MerchantID);

            brandViewModel brand_model = new brandViewModel()
            {
                BrandID = brand_data.BrandID,
                BrandKey = brand_data.BrandKey,
                MerchantID = brand_data.MerchantID,
                pos_merchant_data = merchantdata,
                BrandCode = brand_data.BrandCode,
                BrandName = brand_data.BrandName,
                IPAddress = brand_data.IPAddress,
                Activate = brand_data.Activate
            };

            return View(brand_model);
        }

        // POST: brand/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrandID, BrandKey, MerchantID, BrandCode, BrandName, IPAddress, Activate, ValidDateStart, ValidDateEnd")] brandViewModel branddata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string Key = App_Helpers.SerialKey.GetHash(branddata.BrandCode + DateTime.Today.ToString("ddMMyyyy"));

                    pos_brand_data brand = new pos_brand_data();
                    brand.MerchantID = branddata.MerchantID;
                    brand.BrandKey = Key;
                    brand.BrandCode = branddata.BrandCode;
                    brand.BrandName = branddata.BrandName;
                    brand.IPAddress = branddata.IPAddress;
                    if (branddata.Activate == null)
                        brand.Activate = false;
                    else
                        brand.Activate = branddata.Activate;
                    
                    brand.CreatedDate = DateTime.Now;
                    brand.CreatedBy = UserProfile.UserId;

                    brand = db.pos_brand_data.Add(brand);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                branddata.merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList();
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(branddata);
        }

        // GET: brand/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_brand_data brand_data = db.pos_brand_data.Find(id);
            if (brand_data == null)
            {
                return HttpNotFound("Crud not found.");
            }

            brandViewModel brand_model = new brandViewModel()
            {
                merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList(),
                BrandID = brand_data.BrandID,
                BrandKey = brand_data.BrandKey,
                MerchantID = brand_data.MerchantID,
                BrandCode = brand_data.BrandCode,
                BrandName = brand_data.BrandName,
                IPAddress = brand_data.IPAddress,
                Activate = brand_data.Activate
            };

            return View(brand_model);
        }

        // POST: brand/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrandID, BrandKey, MerchantID, BrandCode, BrandName, IPAddress, Activate, ValidDateStart, ValidDateEnd")] brandViewModel branddata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_brand_data brand = db.pos_brand_data.Find(branddata.BrandID);
                    string Key = App_Helpers.SerialKey.GetHash(branddata.BrandCode + DateTime.Today.ToString("ddMMyyyy"));
                    brand.BrandKey = Key;
                    //brand.MerchantID = branddata.MerchantID;
                    //brand.BrandCode = branddata.BrandCode;
                    //brand.BrandName = branddata.BrandName;
                    //brand.IPAddress = branddata.IPAddress;
                    //if (branddata.Activate == null)
                    //    brand.Activate = false;
                    //else
                    //    brand.Activate = branddata.Activate;

                    brand.UpdatedDate = DateTime.Now;
                    brand.UpdatedBy = UserProfile.UserId;

                    db.Entry(brand).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                branddata.merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList();
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(branddata);
        }

        // POST: brand/Delete/5
        [HttpGet]
        public JsonResult ActivateBrand(int id)
        {
            pos_brand_data brand = db.pos_brand_data.Find(id);
            if (brand != null)
            {
                brand.Activate = !brand.Activate;
                brand.UpdatedBy = UserProfile.employee_id;
                brand.UpdatedDate = DateTime.Now;
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: brand/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_brand_data brand = db.pos_brand_data.Find(id);
            if (brand != null)
            {
                brand.DeletedBy = UserProfile.UserId;
                brand.DeletedDate = DateTime.Now;
                db.Entry(brand).State = EntityState.Modified;
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