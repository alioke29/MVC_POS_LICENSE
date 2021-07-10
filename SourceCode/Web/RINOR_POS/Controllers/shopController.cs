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
using System.Net.Http;
using Newtonsoft.Json;

namespace RINOR_POS.Controllers
{
    [Authorize]
    public class shopController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: shop
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

            var _qry = (from a in db.pos_shop_data.Where(a => a.DeletedDate == null)

                        select new shopViewModel
                        {
                            ShopID = a.ShopID,
                            ShopCode = a.ShopCode,
                            ShopName = a.ShopName,
                            ShopKey = a.ShopKey,
                            IsShop = a.IsShop
                            //crud_sample = a
                        }).ToList<shopViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCityList(int ProvinceID = 0)
        {
            var _CityList = (from cty in db.pos_city
                             where cty.DeletedDate == null && cty.ProvinceID == ProvinceID
                             select cty).ToList();

            return Json(_CityList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRegionList(int CityID = 0)
        {
            var _RegionList = (from rgn in db.pos_region
                               where rgn.DeletedDate == null && rgn.CityID == CityID
                               select rgn).ToList();

            return Json(_RegionList, JsonRequestBehavior.AllowGet);
        }
        // GET: crud/Create
        public ActionResult Create(int? id)
        {
            shopViewModel shopdata = new shopViewModel()
            {
                merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList(),
                brand_list = new List<pos_brand_data>(),
                shop_list = new List<pos_shop_data>(),
                Province_list = db.pos_province.Where(o => o.DeletedDate == null).ToList()
            };

            return View(shopdata);
        }

        // POST: shop/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(shopViewModel shopdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //User Transaction
                    pos_shop_data shop = new pos_shop_data();
                    shop.MerchantID = shopdata.MerchantID;
                    shop.BrandID = shopdata.BrandID;
                    //string Key = App_Helpers.SerialKey.GetHash(shopdata.ShopCode + DateTime.Today.ToString("ddMMyyyy"));
                    shop.ShopKey = "NA";
                    shop.ShopName = shopdata.ShopName;
                    shop.ShopCode = shopdata.ShopCode;
                    shop.CompanyName = shopdata.CompanyName;
                    shop.ShopName = shopdata.ShopName;
                    shop.BranchName = shopdata.BranchName;
                    if (shopdata.MasterShop == null)
                        shop.MasterShop = false;
                    else
                        shop.MasterShop = shopdata.MasterShop;
                    shop.MasterShopLink = shopdata.MasterShopLink;
                    if (shopdata.CompanyRegisterLocation != null)
                        shop.CompanyRegisterLocation = shopdata.CompanyRegisterLocation;
                    if (shopdata.IsShop == null)
                        shop.IsShop = false;
                    else
                        shop.IsShop = shopdata.IsShop;
                    if (shopdata.IsInv == null)
                        shop.IsInv = false;
                    else
                        shop.IsInv = shopdata.IsInv;
                    shop.BranchNameInFullTaxReport = shopdata.BranchNameInFullTaxReport;
                    shop.CompanyAddress1 = shopdata.CompanyAddress1;
                    shop.CompanyAddress2 = shopdata.CompanyAddress2;
                    shop.CompanyProvince = shopdata.CompanyProvince;
                    shop.CompanyCity = shopdata.CompanyCity;
                    shop.CompanyRegion = shopdata.CompanyRegion;
                    shop.CompanyZipCode = shopdata.CompanyZipCode;
                    shop.CompanyTelephone = shopdata.CompanyTelephone;
                    shop.CompanyFax = shopdata.CompanyFax;
                    if (shopdata.HasSC == null)
                        shop.HasSC = false;
                    else
                        shop.HasSC = shopdata.HasSC;
                    shop.DeliveryName = shopdata.DeliveryName;
                    shop.DeliveryAddress1 = shopdata.DeliveryAddress1;
                    shop.DeliveryAddress2 = shopdata.DeliveryAddress2;
                    shop.DeliveryProvince = shopdata.DeliveryProvince;
                    shop.DeliveryCity = shopdata.DeliveryCity;
                    shop.DeliveryRegion = shopdata.DeliveryRegion;
                    shop.DeliveryZipCode = shopdata.DeliveryZipCode;
                    shop.DeliveryTelephone = shopdata.DeliveryTelephone;
                    shop.DeliveryFax = shopdata.DeliveryFax;
                    shop.CompanyRegisterID = shopdata.CompanyRegisterID;
                    shop.IPAddress = shopdata.IPAddress;
                    if (shopdata.StartSaleDate != null)
                    {
                        shop.StartSaleDate = shopdata.StartSaleDate;
                        shop.OpenHour = shopdata.OpenHour;
                        shop.CloseHour = shopdata.CloseHour;
                    }
                    shop.SLOC = shopdata.SLOC;
                    shop.CreatedBy = UserProfile.employee_id;
                    shop.CreatedDate = DateTime.Now;
                    shop = db.pos_shop_data.Add(shop);
                    db.SaveChanges();

                    //Request Register to License
                    using (var client = new HttpClient())
                    {
                        
                        client.BaseAddress = new Uri(UserProfile.APIURLBase + "/api/APIRegisterShop?KeyToken=uptome");

                        //HTTP POST
                        var result = client.PostAsJsonAsync("", shop).Result;

                        if (result.IsSuccessStatusCode)
                        {
                            shop.ShopKey = "Waiting for registration key";
                            db.Entry(shop).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    List<pos_program_property> PreferencesList = (from pre in db.pos_program_property
                                                                  where (pre.Active == true)
                                                                  select pre).ToList<pos_program_property>();

                    foreach (pos_program_property Pre in PreferencesList)
                    {
                        pos_shop_property shopProperty = new pos_shop_property()
                        {
                            ShopID = shop.ShopID,
                            PropertyID = Pre.PropertyID,
                            PropertyName = Pre.PropertyName,
                            PropertyParam = Pre.PropertyParam,
                            PropertyDesp = Pre.PropertyDesp,
                            DefaultDecimalValue = Pre.DefaultDecimalValue,
                            DefaultTextValue = Pre.DefaultTextValue,
                            Active = Pre.Active,
                            Ordering = Pre.Ordering
                        };
                        shopProperty = db.pos_shop_property.Add(shopProperty);
                        db.SaveChanges();
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

            shopdata.merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList();
            shopdata.brand_list = db.pos_brand_data.Where(o => o.DeletedDate == null).ToList();
            shopdata.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShop == true).ToList();
            shopdata.Province_list = db.pos_province.Where(o => o.DeletedDate == null).ToList();

            return View(shopdata);
        }

        // GET: shop/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_shop_data shopdata = db.pos_shop_data.Find(id);
            if (shopdata == null)
            {
                return HttpNotFound("Crud not found.");
            }

            shopViewModel shop = new shopViewModel();
            shop.merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList();
            shop.brand_list = db.pos_brand_data.Where(o => o.DeletedDate == null).ToList();
            shop.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShop == true && o.BrandID == shopdata.BrandID).ToList();
            shop.Province_list = db.pos_province.Where(o => o.DeletedDate == null).ToList();
            if (shopdata.CompanyProvince != null)
                shop.city_list = db.pos_city.Where(o => o.DeletedDate == null && o.ProvinceID == shopdata.CompanyProvince).ToList();
            if (shopdata.CompanyCity != null)
                shop.region_list = db.pos_region.Where(o => o.DeletedDate == null && o.CityID == shopdata.CompanyCity).ToList();

            shop.MerchantID = shopdata.MerchantID;
            shop.BrandID = shopdata.BrandID;
            shop.ShopID = shopdata.ShopID;
            shop.ShopKey = shopdata.ShopKey;
            shop.ShopName = shopdata.ShopName;
            shop.ShopCode = shopdata.ShopCode;
            shop.CompanyName = shopdata.CompanyName;
            shop.ShopName = shopdata.ShopName;
            shop.BranchName = shopdata.BranchName;
            if (shopdata.MasterShop == null)
                shop.MasterShop = false;
            else
                shop.MasterShop = shopdata.MasterShop;
            shop.MasterShopLink = shopdata.MasterShopLink;
            if (shopdata.CompanyRegisterLocation != null)
                shop.CompanyRegisterLocation = shopdata.CompanyRegisterLocation;
            if (shopdata.IsShop == null)
                shop.IsShop = false;
            else
                shop.IsShop = shopdata.IsShop;
            if (shopdata.IsInv == null)
                shop.IsInv = false;
            else
                shop.IsInv = shopdata.IsInv;
            shop.BranchNameInFullTaxReport = shopdata.BranchNameInFullTaxReport;
            shop.CompanyAddress1 = shopdata.CompanyAddress1;
            shop.CompanyAddress2 = shopdata.CompanyAddress2;
            shop.CompanyProvince = shopdata.CompanyProvince;
            shop.CompanyCity = shopdata.CompanyCity;
            shop.CompanyRegion = shopdata.CompanyRegion;
            shop.CompanyZipCode = shopdata.CompanyZipCode;
            shop.CompanyTelephone = shopdata.CompanyTelephone;
            shop.CompanyFax = shopdata.CompanyFax;
            if (shopdata.HasSC == null)
                shop.HasSC = false;
            else
                shop.HasSC = shopdata.HasSC;
            shop.DeliveryName = shopdata.DeliveryName;
            shop.DeliveryAddress1 = shopdata.DeliveryAddress1;
            shop.DeliveryAddress2 = shopdata.DeliveryAddress2;
            shop.DeliveryProvince = shopdata.DeliveryProvince;
            shop.DeliveryCity = shopdata.DeliveryCity;
            shop.DeliveryRegion = shopdata.DeliveryRegion;
            shop.DeliveryZipCode = shopdata.DeliveryZipCode;
            shop.DeliveryTelephone = shopdata.DeliveryTelephone;
            shop.DeliveryFax = shopdata.DeliveryFax;
            shop.CompanyRegisterID = shopdata.CompanyRegisterID;
            shop.IPAddress = shopdata.IPAddress;
            if (shopdata.StartSaleDate != null)
            {
                shop.StartSaleDate = shopdata.StartSaleDate;
                shop.OpenHour = shopdata.OpenHour;
                shop.CloseHour = shopdata.CloseHour;
            }
            shop.SLOC = shopdata.SLOC;

            return View(shop);
        }

        // POST: shop/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(shopViewModel shopdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_shop_data shop = db.pos_shop_data.Find(shopdata.ShopID);
                    shop.MerchantID = shopdata.MerchantID;
                    shop.BrandID = shopdata.BrandID;
                    //shop.ShopKey = shopdata.ShopKey;
                    shop.ShopName = shopdata.ShopName;
                    shop.ShopCode = shopdata.ShopCode;
                    shop.CompanyName = shopdata.CompanyName;
                    shop.BranchName = shopdata.BranchName;
                    if (shopdata.MasterShop == null)
                        shop.MasterShop = false;
                    else
                        shop.MasterShop = shopdata.MasterShop;
                    shop.MasterShopLink = shopdata.MasterShopLink;
                    if (shopdata.CompanyRegisterLocation != null)
                        shop.CompanyRegisterLocation = shopdata.CompanyRegisterLocation;
                    if (shopdata.IsShop == null)
                        shop.IsShop = false;
                    else
                        shop.IsShop = shopdata.IsShop;
                    if (shopdata.IsInv == null)
                        shop.IsInv = false;
                    else
                        shop.IsInv = shopdata.IsInv;
                    shop.BranchNameInFullTaxReport = shopdata.BranchNameInFullTaxReport;
                    shop.CompanyAddress1 = shopdata.CompanyAddress1;
                    shop.CompanyAddress2 = shopdata.CompanyAddress2;
                    shop.CompanyProvince = shopdata.CompanyProvince;
                    shop.CompanyCity = shopdata.CompanyCity;
                    shop.CompanyRegion = shopdata.CompanyRegion;
                    shop.CompanyZipCode = shopdata.CompanyZipCode;
                    shop.CompanyTelephone = shopdata.CompanyTelephone;
                    shop.CompanyFax = shopdata.CompanyFax;
                    if (shopdata.HasSC == null)
                        shop.HasSC = false;
                    else
                        shop.HasSC = shopdata.HasSC;
                    shop.DeliveryName = shopdata.DeliveryName;
                    shop.DeliveryAddress1 = shopdata.DeliveryAddress1;
                    shop.DeliveryAddress2 = shopdata.DeliveryAddress2;
                    shop.DeliveryProvince = shopdata.DeliveryProvince;
                    shop.DeliveryCity = shopdata.DeliveryCity;
                    shop.DeliveryRegion = shopdata.DeliveryRegion;
                    shop.DeliveryZipCode = shopdata.DeliveryZipCode;
                    shop.DeliveryTelephone = shopdata.DeliveryTelephone;
                    shop.DeliveryFax = shopdata.DeliveryFax;
                    shop.CompanyRegisterID = shopdata.CompanyRegisterID;
                    shop.IPAddress = shopdata.IPAddress;
                    if (shopdata.StartSaleDate != null)
                    {
                        shop.StartSaleDate = shopdata.StartSaleDate;
                        shop.OpenHour = shopdata.OpenHour;
                        shop.CloseHour = shopdata.CloseHour;
                    }
                    shop.SLOC = shopdata.SLOC;
                    shop.UpdatedBy = UserProfile.employee_id;
                    shop.UpdatedDate = DateTime.Now;

                    db.Entry(shop).State = EntityState.Modified;
                    db.SaveChanges();

                    //Update to License
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(UserProfile.APIURLBase + "/api/APIRegisterShop?KeyToken=uptome");

                        //HTTP POST
                        var result = client.PostAsJsonAsync("", shop).Result;
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

            shopdata.merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList();
            shopdata.brand_list = db.pos_brand_data.Where(o => o.DeletedDate == null).ToList();
            shopdata.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShop == true && o.BrandID == shopdata.BrandID).ToList();
            shopdata.Province_list = db.pos_province.Where(o => o.DeletedDate == null).ToList();
            if (shopdata.CompanyProvince != null)
                shopdata.city_list = db.pos_city.Where(o => o.DeletedDate == null && o.ProvinceID == shopdata.CompanyProvince).ToList();
            if (shopdata.CompanyCity != null)
                shopdata.region_list = db.pos_region.Where(o => o.DeletedDate == null && o.CityID == shopdata.CompanyCity).ToList();


            return View(shopdata);
        }

        [HttpGet]
        public JsonResult preferencesList(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString, int id)
        {
            sord = (sord == null) ? "" : sord;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var PreferencesList = from pre in db.pos_shop_property
                                  where (pre.Active == true && pre.ShopID == id)
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
                    pos_shop_property pre = db.pos_shop_property.Find(id);
                    pre.DefaultDecimalValue = Convert.ToDecimal(Request.Form["DefaultDecimalValue"]);
                    pre.DefaultTextValue = Request.Form["DefaultTextValue"];
                    db.SaveChanges();

                }
            }
            return Json("Preference successfully saved", JsonRequestBehavior.AllowGet);
        }

        /// POST: shop/SendShop/5
        [HttpGet]
        public JsonResult ResendShop(int id)
        {
            pos_shop_data shopData = db.pos_shop_data.Find(id);
            if (shopData != null)
            {
                //Request Register to License
                using (var client = new HttpClient())
                {
                    
                    client.BaseAddress = new Uri(UserProfile.APIURLBase + "/api/APIRegisterShop?KeyToken=uptome");

                    //HTTP POST
                    var result = client.PostAsJsonAsync("", shopData).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        shopData.ShopKey = "Waiting for registration key";
                        db.Entry(shopData).State = EntityState.Modified;
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

        /// POST: shop/GetShopKey/5
        [HttpGet]
        public JsonResult GetShopKey(int id)
        {
            pos_shop_data shopData = db.pos_shop_data.Find(id);
            if (shopData != null)
            {
                //Request Register to License
                using (var client = new HttpClient())
                {
                    
                    client.BaseAddress = new Uri(UserProfile.APIURLBase + "/api/APIShopByID?KeyToken=uptome&ShopID=" + id);

                    //HTTP GET
                    var responseTask = client.GetAsync("");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var data = readTask.Result;
                        pos_shop_data APIData = JsonConvert.DeserializeObject<pos_shop_data>(data);

                        if (APIData != null)
                        {
                            pos_shop_data shop = db.pos_shop_data.Find(APIData.ShopID);
                            shop.ShopKey = APIData.ShopKey;
                            db.Entry(shop).State = EntityState.Modified;
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

        // POST: shop/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_shop_data Shop = db.pos_shop_data.Find(id);
            if (Shop != null)
            {
                Shop.DeletedBy = UserProfile.UserId;
                Shop.DeletedDate = DateTime.Now;
                db.Entry(Shop).State = EntityState.Modified;
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