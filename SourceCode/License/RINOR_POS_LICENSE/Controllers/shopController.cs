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
    public class shopController : Controller
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();

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
                        join b in db.pos_brand_data on a.BrandID equals b.BrandID
                        select new shopViewModel
                        {
                            BranchName = b.BrandName,
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
                    string Key = App_Helpers.SerialKey.GetHash(shopdata.ShopCode + DateTime.Today.ToString("ddMMyyyy"));
                    shop.ShopKey = Key;
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
                    //shop.MerchantID = shopdata.MerchantID;
                    //shop.BrandID = shopdata.BrandID;
                    string Key = App_Helpers.SerialKey.GetHash(shopdata.ShopCode + DateTime.Today.ToString("ddMMyyyy"));
                    shop.ShopKey = Key;
                    //shop.ShopName = shopdata.ShopName;
                    //shop.ShopCode = shopdata.ShopCode;
                    //shop.CompanyName = shopdata.CompanyName;
                    //shop.ShopName = shopdata.ShopName;
                    //shop.BranchName = shopdata.BranchName;
                    //if (shopdata.MasterShop == null)
                    //    shop.MasterShop = false;
                    //else
                    //    shop.MasterShop = shopdata.MasterShop;
                    //shop.MasterShopLink = shopdata.MasterShopLink;
                    //if (shopdata.CompanyRegisterLocation != null)
                    //    shop.CompanyRegisterLocation = shopdata.CompanyRegisterLocation;
                    //if (shopdata.IsShop == null)
                    //    shop.IsShop = false;
                    //else
                    //    shop.IsShop = shopdata.IsShop;
                    //if (shopdata.IsInv == null)
                    //    shop.IsInv = false;
                    //else
                    //    shop.IsInv = shopdata.IsInv;
                    //shop.BranchNameInFullTaxReport = shopdata.BranchNameInFullTaxReport;
                    //shop.CompanyAddress1 = shopdata.CompanyAddress1;
                    //shop.CompanyAddress2 = shopdata.CompanyAddress2;
                    //shop.CompanyProvince = shopdata.CompanyProvince;
                    //shop.CompanyCity = shopdata.CompanyCity;
                    //shop.CompanyRegion = shopdata.CompanyRegion;
                    //shop.CompanyZipCode = shopdata.CompanyZipCode;
                    //shop.CompanyTelephone = shopdata.CompanyTelephone;
                    //shop.CompanyFax = shopdata.CompanyFax;
                    //if (shopdata.HasSC == null)
                    //    shop.HasSC = false;
                    //else
                    //    shop.HasSC = shopdata.HasSC;
                    //shop.DeliveryName = shopdata.DeliveryName;
                    //shop.DeliveryAddress1 = shopdata.DeliveryAddress1;
                    //shop.DeliveryAddress2 = shopdata.DeliveryAddress2;
                    //shop.DeliveryProvince = shopdata.DeliveryProvince;
                    //shop.DeliveryCity = shopdata.DeliveryCity;
                    //shop.DeliveryRegion = shopdata.DeliveryRegion;
                    //shop.DeliveryZipCode = shopdata.DeliveryZipCode;
                    //shop.DeliveryTelephone = shopdata.DeliveryTelephone;
                    //shop.DeliveryFax = shopdata.DeliveryFax;
                    //shop.CompanyRegisterID = shopdata.CompanyRegisterID;
                    //shop.IPAddress = shopdata.IPAddress;
                    //if (shopdata.StartSaleDate != null)
                    //{
                    //    shop.StartSaleDate = shopdata.StartSaleDate;
                    //    shop.OpenHour = shopdata.OpenHour;
                    //    shop.CloseHour = shopdata.CloseHour;
                    //}
                    //shop.SLOC = shopdata.SLOC;
                    shop.UpdatedBy = UserProfile.employee_id;
                    shop.UpdatedDate = DateTime.Now;

                    db.Entry(shop).State = EntityState.Modified;
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

        private bool IsNumeric(string id)
        {
            int ID;
            return int.TryParse(id, out ID);
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