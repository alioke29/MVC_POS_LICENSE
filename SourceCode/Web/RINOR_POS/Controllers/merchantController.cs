﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RINOR_POS.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace RINOR_POS.Controllers
{
    /// <summary>
    /// merchant
    /// </summary>
    [Authorize]
    public class merchantController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        /// GET: Merchant
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

            var _qry = (from a in db.pos_merchant_data.Where(a => a.DeletedDate == null)
                        select new merchantViewModel
                        {
                            MerchantID = a.MerchantID,
                            MerchantKey = a.MerchantKey,
                            MerchantCode = a.MerchantCode,
                            MerchantName = a.MerchantName,
                            IPaddress = a.IPaddress,
                            DatabaseName = a.DatabaseName
                        }).ToList<merchantViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        /// GET: merchant/Create
        public ActionResult Create(int? id)
        {
            merchantViewModel merchantdata = new merchantViewModel();

            return View(merchantdata);
        }

        /// GET: merchant/Detail/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_merchant_data merchant_data = db.pos_merchant_data.Find(id);

            if (merchant_data == null)
            {
                return HttpNotFound("Crud not found.");
            }

            merchantViewModel merchant_model = new merchantViewModel()
            {
                MerchantID = merchant_data.MerchantID,
                MerchantKey = merchant_data.MerchantKey,
                MerchantCode = merchant_data.MerchantCode,
                MerchantName = merchant_data.MerchantName,
                IPaddress = merchant_data.IPaddress,
                DatabaseName = merchant_data.DatabaseName
            };

            return View(merchant_model);
        }

        /// POST: merchant/Create
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MerchantCode, MerchantName, IPaddress, DatabaseName, ValidDateStart, ValidDateEnd")] merchantViewModel merchantdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_merchant_data merchantData = new pos_merchant_data();
                    //string Key = App_Helpers.SerialKey.GetHash(merchantdata.MerchantCode + DateTime.Today.ToString("ddMMyyyy"));
                    merchantData.MerchantKey = "NA";
                    merchantData.MerchantCode = merchantdata.MerchantCode;
                    merchantData.MerchantName = merchantdata.MerchantName;
                    merchantData.IPaddress = merchantdata.IPaddress;
                    merchantData.DatabaseName = merchantdata.DatabaseName;
                    merchantData.CreatedBy = UserProfile.employee_id;
                    merchantData.CreatedDate = DateTime.Now;

                    merchantData = db.pos_merchant_data.Add(merchantData);
                    db.SaveChanges();

                    //Request Register to License
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(UserProfile.APIURLBase + "/api/APIRegisterMerchant?KeyToken=uptome");

                        //HTTP POST
                        var result = client.PostAsJsonAsync("", merchantData).Result;

                        if (result.IsSuccessStatusCode)
                        {
                            merchantData.MerchantKey = "Waiting for registration key";
                            db.Entry(merchantData).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

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

            return View(merchantdata);
        }

        /// GET: merchant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_merchant_data merchant_data = db.pos_merchant_data.Find(id);

            if (merchant_data == null)
            {
                return HttpNotFound("Crud not found.");
            }

            merchantViewModel merchant_model = new merchantViewModel()
            {
                MerchantID = merchant_data.MerchantID,
                MerchantKey = merchant_data.MerchantKey,
                MerchantCode = merchant_data.MerchantCode,
                MerchantName = merchant_data.MerchantName,
                IPaddress = merchant_data.IPaddress,
                DatabaseName = merchant_data.DatabaseName
            };

            return View(merchant_model);
        }

        /// POST: merchant/Edit
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MerchantID, MerchantKey, MerchantCode, MerchantName, IPaddress, DatabaseName, ValidDateStart, ValidDateEnd")] merchantViewModel merchantdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_merchant_data merchantData = db.pos_merchant_data.Find(merchantdata.MerchantID);
                    //merchantData.MerchantKey = merchantdata.MerchantKey;
                    merchantData.MerchantCode = merchantdata.MerchantCode;
                    merchantData.MerchantName = merchantdata.MerchantName;
                    merchantData.IPaddress = merchantdata.IPaddress;
                    merchantData.DatabaseName = merchantdata.DatabaseName;
                    merchantData.UpdatedBy = UserProfile.employee_id;
                    merchantData.UpdatedDate = DateTime.Now;

                    db.Entry(merchantData).State = EntityState.Modified;
                    db.SaveChanges();

                    //Update data to License
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(UserProfile.APIURLBase + "/api/APIRegisterMerchant?KeyToken=uptome");

                        //HTTP POST
                        var result = client.PostAsJsonAsync("", merchantData).Result;
                    }

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

            return View(merchantdata);
        }

        /// POST: merchant/SendMerchant/5
        [HttpGet]
        public JsonResult ResendMerchant(int id)
        {
            pos_merchant_data merchantData = db.pos_merchant_data.Find(id);
            if (merchantData != null)
            {
                //Request Register to License
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UserProfile.APIURLBase + "/api/APIRegisterMerchant?KeyToken=uptome");

                    //HTTP POST
                    var result = client.PostAsJsonAsync("", merchantData).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        merchantData.MerchantKey = "Waiting for registration key";
                        db.Entry(merchantData).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    else if (result.StatusCode.ToString() == "NotModified")
                        return Json(2, JsonRequestBehavior.AllowGet);
                }

                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        /// POST: merchant/GetMerchantKey/5
        [HttpGet]
        public JsonResult GetMerchantKey(int id)
        {
            pos_merchant_data merchantData = db.pos_merchant_data.Find(id);
            if (merchantData != null)
            {
                //Request Register to License
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UserProfile.APIURLBase + "/api/APIMerchantByID?KeyToken=uptome&MerchantID=" + id);

                    //HTTP GET
                    var responseTask = client.GetAsync("");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var data = readTask.Result;
                        pos_merchant_data APIData = JsonConvert.DeserializeObject<pos_merchant_data>(data);

                        if (APIData != null)
                        {
                            pos_merchant_data merchant = db.pos_merchant_data.Find(APIData.MerchantID);
                            merchant.MerchantKey = APIData.MerchantKey;
                            db.Entry(merchant).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        /// POST: merchant/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_merchant_data merchant = db.pos_merchant_data.Find(id);
            if (merchant != null)
            {
                merchant.DeletedBy = UserProfile.UserId;
                merchant.DeletedDate = DateTime.Now;
                db.Entry(merchant).State = EntityState.Modified;
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