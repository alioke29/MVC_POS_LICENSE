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
    /// License Controller
    /// </summary>
    [Authorize]
    public class LicenseController : Controller
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();

        // GET: License
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

            var _qry = (from com in db.pos_computer_name

                        join a in db.GCS_LICENSE_DEV.Where(a => a.DeletedDate == null)
                        on com.DeviceCode equals a.DeviceKey into aleft
                        from a in aleft.DefaultIfEmpty()

                        join b in db.POS_Licence_Type.Where(b => b.DeletedDate == null)
                        on a.LicenceType equals b.LicenceTypeID into bleft
                        from b in bleft.DefaultIfEmpty()

                        join e in db.pos_shop_data.Where(e => e.DeletedDate == null)
                        on com.ShopId equals e.ShopID

                        join d in db.pos_brand_data.Where(d => d.DeletedDate == null && d.Activate == true)
                        on e.BrandID equals d.BrandID

                        join c in db.pos_merchant_data.Where(c => c.DeletedDate == null)
                        on d.MerchantID equals c.MerchantID

                        select new LICENSEViewModel
                        {
                            LicenceDataID = a.LicenceDataID,
                            MerchantKey = a.MerchantKey,
                            MerchantName = c.MerchantName,
                            BrandKey = d.BrandKey,
                            BrandName = d.BrandName,
                            ShopKey = e.ShopKey,
                            ShopName = e.ShopName,
                            LicenceType = a.LicenceType,
                            LicenceTypeName = b.LicenceName,
                            LicenceKey = a.LicenceKey,
                            DeviceKey = com.DeviceCode,
                            DeviceName = com.ComputerName,
                            LicenceStart = a.LicenceStart,
                            LicenceFinish = a.LicenceFinish,
                            isActive = a.isActive
                        }).ToList<LICENSEViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// License List by MerchantID
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        public JsonResult GetBrandList(int MerchantID = 0)
        {
            var _BrandList = db.pos_brand_data.Where(o => o.DeletedDate == null && o.Activate == true && o.MerchantID == MerchantID).ToList();

            return Json(_BrandList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Master Shop By BrandID
        /// </summary>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        public JsonResult GetMasterShopList(int BrandID = 0)
        {
            var _ShopList = db.pos_shop_data.Where(o => o.DeletedDate == null && o.BrandID == BrandID && o.MasterShop == true).ToList();

            return Json(_ShopList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Shop By MasterShopID
        /// </summary>
        /// <param name="MasterShopID"></param>
        /// <returns></returns>
        public JsonResult GetShopList(int MasterShopID = 0)
        {
            var _ShopList = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShopLink == MasterShopID).ToList();

            return Json(_ShopList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Device By ShopID
        /// </summary>
        /// <param name="ShopID"></param>
        /// <returns></returns>
        public JsonResult GetDeviceList(int ShopID = 0)
        {
            var _ShopList = db.pos_computer_name.Where(o => o.DeletedDate == null && o.ShopId == ShopID).ToList();

            return Json(_ShopList, JsonRequestBehavior.AllowGet);
        }

        /// GET: license/Create
        //public ActionResult Create(int? id)
        //{
        //    LICENSEViewModel data = new LICENSEViewModel()
        //    {
        //        merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList(),
        //        licencetype_list = db.POS_Licence_Type.Where(o => o.DeletedDate == null).ToList()
        //    };

        //    return View(data);
        //}

        //// POST: License/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        ///// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(LICENSEViewModel data)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            GCS_LICENSE_DEV license = new GCS_LICENSE_DEV();
        //            license.MerchantID = data.MerchantID;
        //            pos_merchant_data merchant = db.pos_merchant_data.Find(data.MerchantID);
        //            license.MerchantKey = merchant.MerchantKey;

        //            license.BrandID = data.BrandID;
        //            pos_brand_data brand = db.pos_brand_data.Find(data.BrandID);
        //            license.BrandKey = brand.BrandKey;

        //            license.ShopID = data.ShopID;
        //            pos_shop_data shop = db.pos_shop_data.Find(data.ShopID);
        //            license.ShopKey = shop.ShopKey;

        //            license.LicenceType = data.LicenceType;
        //            string Key = App_Helpers.SerialKey.GetHash(data.DeviceKey + DateTime.Today.ToString("ddMMyyyy"));
        //            license.LicenceKey = Key;
        //            license.DeviceKey = data.DeviceKey;
        //            license.LicenceStart = data.LicenceStart;
        //            license.LicenceFinish = data.LicenceFinish;
        //            license.BuyerEmail = data.BuyerEmail;
        //            license.BuyerMobileNo = data.BuyerMobileNo;

        //            if (data.isActive == null)
        //                license.isActive = false;
        //            else
        //                license.isActive = data.isActive;

        //            license.CreatedDate = DateTime.Now;
        //            license.CreatedBy = UserProfile.UserId;

        //            license = db.GCS_LICENSE_DEV.Add(license);
        //            db.SaveChanges();

        //            return RedirectToAction("Index");
        //        }

        //        data.merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList();
        //        if (data.MerchantID > 0)
        //            data.brand_list = db.pos_brand_data.Where(o => o.DeletedDate == null && o.MerchantID == data.MerchantID).ToList();
        //        if (data.BrandID > 0)
        //            data.MasterShop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.BrandID == data.BrandID && o.MasterShop == true).ToList();
        //        if (data.ShopID > 0)
        //        {
        //            pos_shop_data pos_shop = db.pos_shop_data.Find(data.ShopID);
        //            data.MasterShopID = Convert.ToInt32(pos_shop.MasterShopLink);
        //            data.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShopLink == pos_shop.MasterShopLink).ToList();
        //            data.computer_list = db.pos_computer_name.Where(o => o.DeletedDate == null && o.ShopId == data.ShopID).ToList();
        //        }
        //        data.licencetype_list = db.POS_Licence_Type.Where(o => o.DeletedDate == null).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
        //        if (ex.InnerException != null)
        //            msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
        //        ModelState.AddModelError("", msgErr);
        //    }

        //    return View(data);
        //}

        // GET: brand/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_computer_name device = db.pos_computer_name.Where(a => a.DeviceCode == id).FirstOrDefault();

            if (device == null)
            {
                return HttpNotFound("Device not found.");
            }

            var model = (from com in db.pos_computer_name.Where(com => com.DeviceCode == id)

                         join a in db.GCS_LICENSE_DEV.Where(a => a.DeletedDate == null)
                         on com.DeviceCode equals a.DeviceKey into aleft
                         from a in aleft.DefaultIfEmpty()

                         join b in db.POS_Licence_Type.Where(b => b.DeletedDate == null)
                         on a.LicenceType equals b.LicenceTypeID into bleft
                         from b in bleft.DefaultIfEmpty()

                         join e in db.pos_shop_data.Where(e => e.DeletedDate == null)
                         on com.ShopId equals e.ShopID

                         join d in db.pos_brand_data.Where(d => d.DeletedDate == null && d.Activate == true)
                         on e.BrandID equals d.BrandID

                         join c in db.pos_merchant_data.Where(c => c.DeletedDate == null)
                         on d.MerchantID equals c.MerchantID

                         select new LICENSEViewModel
                         {
                             LicenceDataID = a.LicenceDataID,
                             MerchantID = c.MerchantID,
                             MerchantKey = c.MerchantKey,
                             MerchantName = c.MerchantName,
                             BrandID = d.BrandID,
                             BrandKey = d.BrandKey,
                             BrandName = d.BrandName,
                             ShopID = e.ShopID,
                             ShopKey = e.ShopKey,
                             ShopName = e.ShopName,
                             LicenceType = a.LicenceType,
                             LicenceTypeName = b.LicenceName,
                             LicenceKey = a.LicenceKey,
                             DeviceKey = com.DeviceCode,
                             LicenceStart = a.LicenceStart,
                             LicenceFinish = a.LicenceFinish,
                             Period = a.Period,
                             BuyerEmail = a.BuyerEmail,
                             BuyerMobileNo = a.BuyerMobileNo,
                             isActive = a.isActive
                         }).FirstOrDefault();

            model.merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList();
            if (model.MerchantID > 0)
                model.brand_list = db.pos_brand_data.Where(o => o.DeletedDate == null && o.MerchantID == model.MerchantID).ToList();
            if (model.BrandID > 0)
                model.MasterShop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.BrandID == model.BrandID && o.MasterShop == true).ToList();
            if (model.ShopID > 0)
            {
                pos_shop_data pos_shop = db.pos_shop_data.Find(model.ShopID);
                model.MasterShopID = Convert.ToInt32(pos_shop.MasterShopLink);
                model.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShopLink == pos_shop.MasterShopLink).ToList();
                model.computer_list = db.pos_computer_name.Where(o => o.DeletedDate == null && o.ShopId == model.ShopID).ToList();
            }
            model.licencetype_list = db.POS_Licence_Type.Where(o => o.DeletedDate == null).ToList();

            return View(model);
        }

        // POST: License/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LICENSEViewModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GCS_LICENSE_DEV devicelicense = db.GCS_LICENSE_DEV.Where(a => a.DeviceKey == data.DeviceKey).FirstOrDefault();

                    if (devicelicense != null)
                    {
                        //update
                        GCS_LICENSE_DEV license = db.GCS_LICENSE_DEV.Find(data.LicenceDataID);
                        license.MerchantID = data.MerchantID;
                        pos_merchant_data merchant = db.pos_merchant_data.Find(data.MerchantID);
                        license.MerchantKey = merchant.MerchantKey;

                        license.BrandID = data.BrandID;
                        pos_brand_data brand = db.pos_brand_data.Find(data.BrandID);
                        license.BrandKey = brand.BrandKey;

                        license.ShopID = data.ShopID;
                        pos_shop_data shop = db.pos_shop_data.Find(data.ShopID);
                        license.ShopKey = shop.ShopKey;

                        license.LicenceType = Convert.ToInt32(data.LicenceType);
                        license.LicenceKey = data.LicenceKey;
                        license.DeviceKey = data.DeviceKey;
                        license.LicenceStart = data.LicenceStart;
                        license.LicenceFinish = data.LicenceFinish;
                        license.BuyerEmail = data.BuyerEmail;
                        license.BuyerMobileNo = data.BuyerMobileNo;

                        if (data.isActive == null)
                            license.isActive = false;
                        else
                            license.isActive = data.isActive;

                        license.UpdatedDate = DateTime.Now;
                        license.UpdatedBy = UserProfile.UserId;

                        db.Entry(license).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        //add
                        GCS_LICENSE_DEV license = new GCS_LICENSE_DEV();
                        license.MerchantID = data.MerchantID;
                        pos_merchant_data merchant = db.pos_merchant_data.Find(data.MerchantID);
                        license.MerchantKey = merchant.MerchantKey;

                        license.BrandID = data.BrandID;
                        pos_brand_data brand = db.pos_brand_data.Find(data.BrandID);
                        license.BrandKey = brand.BrandKey;

                        license.ShopID = data.ShopID;
                        pos_shop_data shop = db.pos_shop_data.Find(data.ShopID);
                        license.ShopKey = shop.ShopKey;

                        license.LicenceType = Convert.ToInt32(data.LicenceType);
                        string Key = App_Helpers.SerialKey.GetHash(data.DeviceKey + DateTime.Today.ToString("ddMMyyyy"));
                        license.LicenceKey = Key;
                        license.DeviceKey = data.DeviceKey;
                        license.LicenceStart = data.LicenceStart;
                        license.LicenceFinish = data.LicenceFinish;
                        license.BuyerEmail = data.BuyerEmail;
                        license.BuyerMobileNo = data.BuyerMobileNo;

                        if (data.isActive == null)
                            license.isActive = false;
                        else
                            license.isActive = data.isActive;

                        license.CreatedDate = DateTime.Now;
                        license.CreatedBy = UserProfile.UserId;

                        license = db.GCS_LICENSE_DEV.Add(license);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }

                data.merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList();
                if (data.MerchantID > 0)
                    data.brand_list = db.pos_brand_data.Where(o => o.DeletedDate == null && o.MerchantID == data.MerchantID).ToList();
                if (data.BrandID > 0)
                    data.MasterShop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.BrandID == data.BrandID && o.MasterShop == true).ToList();
                if (data.ShopID > 0)
                {
                    pos_shop_data pos_shop = db.pos_shop_data.Find(data.ShopID);
                    data.MasterShopID = Convert.ToInt32(pos_shop.MasterShopLink);
                    data.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShopLink == pos_shop.MasterShopLink).ToList();
                    data.computer_list = db.pos_computer_name.Where(o => o.DeletedDate == null && o.ShopId == data.ShopID).ToList();
                }
                data.licencetype_list = db.POS_Licence_Type.Where(o => o.DeletedDate == null).ToList();

            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(data);
        }

        /// POST: License/Activate/5
        [HttpGet]
        public JsonResult ActivateLicense(int id)
        {
            GCS_LICENSE_DEV license = db.GCS_LICENSE_DEV.Find(id);
            if (license != null)
            {
                license.isActive = !license.isActive;
                license.UpdatedBy = UserProfile.employee_id;
                license.UpdatedDate = DateTime.Now;
                db.Entry(license).State = EntityState.Modified;
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: license/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            GCS_LICENSE_DEV license = db.GCS_LICENSE_DEV.Find(id);
            if (license != null)
            {
                license.DeletedBy = UserProfile.UserId;
                license.DeletedDate = DateTime.Now;
                db.Entry(license).State = EntityState.Modified;
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