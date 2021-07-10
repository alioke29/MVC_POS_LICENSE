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

using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace RINOR_POS.Controllers
{
    [Authorize]
    public class computerController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();
        
        // GET: computer
        public ActionResult Index()
        {
            computerViewModel computer = new computerViewModel()
            {
                mastershop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShop == true).ToList()
            };

            return View(computer);
        }

        /// <summary>
        /// list for index
        /// </summary>
        /// <returns>JSON</returns>
        public JsonResult GetIndexList(int ShopID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var _qry = new object();
            if (ShopID != 0)
            {
                _qry = (from a in db.pos_computer_name.Where(a => a.DeletedDate == null)
                        join b in db.pos_shop_data.Where(b => b.DeletedDate == null && b.MasterShopLink == ShopID)
                                on a.ShopId equals b.ShopID
                        join c in db.pos_computer_type.Where(c => c.DeletedDate == null && c.Activate == true)
                                on a.ComputerTypeId equals c.ComputerTypeID
                        select new computerViewModel
                        {
                            ComputerNameId = a.ComputerNameId,
                            ComputerName = a.ComputerName,
                            ShopId = a.ShopId,
                            ShopName = b.ShopName,
                            ComputerTypeId = a.ComputerTypeId,
                            ComputerTypeName = c.ComputerTypeName,
                            Activate = a.Activate
                        }).ToList<computerViewModel>();
            }
            else
            {
                _qry = (from a in db.pos_computer_name.Where(a => a.DeletedDate == null)
                        join b in db.pos_shop_data.Where(b => b.DeletedDate == null)
                                on a.ShopId equals b.ShopID
                        join c in db.pos_computer_type.Where(c => c.DeletedDate == null && c.Activate == true)
                                on a.ComputerTypeId equals c.ComputerTypeID
                        select new computerViewModel
                        {
                            ComputerNameId = a.ComputerNameId,
                            ComputerName = a.ComputerName,
                            ShopId = a.ShopId,
                            ShopName = b.ShopName,
                            ComputerTypeId = a.ComputerTypeId,
                            ComputerTypeName = c.ComputerTypeName,
                            Activate = a.Activate
                        }).ToList<computerViewModel>();
            }


            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
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

        // GET: computer/Create
        public ActionResult Create(int? id)
        {
            computerViewModel view = new computerViewModel();
            //isi list
            view.mastershop_list = db.pos_shop_data.Where(a => a.DeletedDate == null && a.MasterShop == true).ToList();
            view.salemode_list = db.pos_sale_mode.Where(a => a.DeletedDate == null).ToList();
            view.sale_mode_selected = new List<string>();
            view.paytype_list = db.pos_payment_type.Where(a => a.DeletedDate == null).ToList();
            view.pay_type_selected = new List<string>();
            view.computertype_list = db.pos_computer_type.Where(a => a.DeletedDate == null).ToList();
            view.Activate = false;
            return View(view);
        }

        // POST: computer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(computerViewModel computerdata)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    pos_computer_name pos_computer_name = new pos_computer_name();

                    pos_computer_name.ShopId = computerdata.ShopId;
                    pos_computer_name.ComputerName = computerdata.ComputerName;
                    pos_computer_name.ComputerTypeId = computerdata.ComputerTypeId;
                    pos_computer_name.RegistrationNumber = computerdata.RegistrationNumber;
                    pos_computer_name.DeviceCode = computerdata.DeviceCode;

                    string paytypevalue = "";
                    foreach (string paytypeid in computerdata.pay_type_selected)
                    {
                        paytypevalue += paytypeid + ",";
                    }
                    pos_computer_name.PayTypeList = paytypevalue.Substring(0, paytypevalue.Length - 1);

                    string salemodevalue = "";
                    foreach (string salemodeid in computerdata.sale_mode_selected)
                    {
                        salemodevalue += salemodeid + ",";
                    }
                    pos_computer_name.SaleModeList = salemodevalue.Substring(0, salemodevalue.Length - 1);

                    pos_computer_name.ReceiptHeader = computerdata.ReceiptHeader;
                    pos_computer_name.FullTaxHeader = computerdata.FullTaxHeader;
                    pos_computer_name.IPAddress = computerdata.IPAddress;

                    if (computerdata.Activate == null)
                        pos_computer_name.Activate = false;
                    else
                        pos_computer_name.Activate = computerdata.Activate;
                    pos_computer_name.CreatedBy = UserProfile.employee_id;
                    pos_computer_name.CreatedDate = DateTime.Now;

                    pos_computer_name = db.pos_computer_name.Add(pos_computer_name);
                    db.SaveChanges();


                    // INSERT DATA pos_computer_name (LICENSE)
                    int newID = pos_computer_name.ComputerNameId;
                    pos_computer_name.ComputerNameId = newID;

                    ModelLicence.ModelLicencePOSDB dbLicense = new ModelLicence.ModelLicencePOSDB();

                    int activate = Convert.ToBoolean(pos_computer_name.Activate) ? 1 : 0;

                    string sql = "INSERT INTO [dbo].[pos_computer_name] ([ComputerNameId], [ShopId], [ComputerTypeId], [ComputerName],[PayTypeList],[SaleModeList],[PrinterList],[RegistrationNumber],[DeviceCode],[ReceiptHeader],[FullTaxHeader],[IPAddress],[Activate],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy],[DeletedDate],[ValidUntil])" +
                                 " VALUES" +
                                 " (" + pos_computer_name.ComputerNameId +
                                 " , " + pos_computer_name.ShopId +
                                 " , " + pos_computer_name.ComputerTypeId +
                                 " , '" + pos_computer_name.ComputerName + "'" +
                                 " , '" + pos_computer_name.PayTypeList + "'" +
                                 " , '" + pos_computer_name.SaleModeList + "'" +
                                 " , '" + pos_computer_name.PrinterList + "'" +
                                 " , '" + pos_computer_name.RegistrationNumber + "'" +
                                 " , '" + pos_computer_name.DeviceCode + "'" +
                                 " , '" + pos_computer_name.ReceiptHeader + "'" +
                                 " , '" + pos_computer_name.FullTaxHeader + "'" +
                                 " , '" + pos_computer_name.IPAddress + "'" +
                                 " , " + activate +
                                 " , " + pos_computer_name.CreatedBy +
                                 " , '" + pos_computer_name.CreatedDate + "'" +
                                 " , null" +
                                 " , null" +
                                 " , null" +
                                 " , null" +
                                 " , null" +
                                 ")";

                    dbLicense.Database.ExecuteSqlCommand(sql);

                    //ModelLicence.pos_computer_name license_computer_name = new ModelLicence.pos_computer_name();

                    //license_computer_name.ComputerNameId = pos_computer_name.ComputerNameId;
                    //license_computer_name.ShopId = pos_computer_name.ShopId;
                    //license_computer_name.ComputerName = pos_computer_name.ComputerName;
                    //license_computer_name.ComputerTypeId = pos_computer_name.ComputerTypeId;
                    //license_computer_name.RegistrationNumber = pos_computer_name.RegistrationNumber;
                    //license_computer_name.DeviceCode = pos_computer_name.DeviceCode;

                    //license_computer_name.ReceiptHeader = pos_computer_name.ReceiptHeader;
                    //license_computer_name.FullTaxHeader = pos_computer_name.FullTaxHeader;
                    //license_computer_name.IPAddress = pos_computer_name.IPAddress;

                    //if (pos_computer_name.Activate == null)
                    //    license_computer_name.Activate = false;
                    //else
                    //    license_computer_name.Activate = pos_computer_name.Activate;

                    //license_computer_name.CreatedBy = UserProfile.employee_id;
                    //license_computer_name.CreatedDate = DateTime.Now;

                    //license_computer_name = dbLicense.pos_computer_name.Add(license_computer_name);
                    //dbLicense.SaveChanges();


                    // INSERT DATA GCS_LICENSE_DEV (LICENSE)
                    ModelLicence.GCS_LICENSE_DEV license = new ModelLicence.GCS_LICENSE_DEV();

                    pos_shop_data pos_shop_data = db.pos_shop_data.Find(pos_computer_name.ShopId);
                    license.MerchantID = pos_shop_data.MerchantID;

                    pos_merchant_data merchant = db.pos_merchant_data.Find(pos_shop_data.MerchantID);
                    license.MerchantKey = merchant.MerchantKey;

                    license.BrandID = pos_shop_data.BrandID;

                    pos_brand_data brand = db.pos_brand_data.Find(pos_shop_data.BrandID);
                    license.BrandKey = brand.BrandKey;

                    license.ShopID = pos_shop_data.ShopID;
                    license.ShopKey = pos_shop_data.ShopKey;

                    license.LicenceType = Convert.ToInt32(0);

                    string Key = App_Helpers.SerialKey.GetHash(pos_computer_name.DeviceCode + DateTime.Today.ToString("ddMMyyyy"));

                    license.LicenceKey = Key;
                    license.DeviceKey = pos_computer_name.DeviceCode;
                    license.LicenceStart = (DateTime?)null;
                    license.LicenceFinish = (DateTime?)null;
                    license.BuyerEmail = "-";
                    license.BuyerMobileNo = "-";

                    if (pos_computer_name.Activate == null)
                        license.isActive = false;
                    else
                        license.isActive = pos_computer_name.Activate;

                    license.CreatedDate = DateTime.Now;
                    license.CreatedBy = UserProfile.UserId;


                    //Request Register to License
                    using (var client = new HttpClient())
                    {

                        //if (Request.Url.Scheme.ToLower().Contains("122101223439.ip-dynamic.com"))
                        //{
                        //    string baseUrl = Url.Action("", "", null, "");
                        //    client.BaseAddress = new Uri(baseUrl);
                        //    var result = client.PostAsJsonAsync("http://122101223439.ip-dynamic.com/gcs_hq_dev/api/APIRegisterComputerName?KeyToken=uptome", pos_computer_name).Result;
                        //}
                        //else
                        //{
                            string baseUrl = Url.Action("", "", null, Request.Url.Scheme);
                            client.BaseAddress = new Uri(baseUrl);
                            var result = client.PostAsJsonAsync("/api/APIRegisterComputerName?KeyToken=uptome", license).Result;
                        //}


                        //client.BaseAddress = new Uri(UserProfile.APIURLBase + "/api/APIRegisterComputerName?KeyToken=uptome&DeviceKey=" + computerdata.DeviceCode);

                        //HTTP POST
                        //var result = client.PostAsJsonAsync("", pos_computer_name).Result;

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

            computerdata.mastershop_list = db.pos_shop_data.Where(a => a.DeletedDate == null && a.MasterShop == true).ToList();
            computerdata.salemode_list = db.pos_sale_mode.Where(a => a.DeletedDate == null).ToList();
            computerdata.sale_mode_selected = new List<string>();
            computerdata.paytype_list = db.pos_payment_type.Where(a => a.DeletedDate == null).ToList();
            computerdata.pay_type_selected = new List<string>();
            computerdata.computertype_list = db.pos_computer_type.Where(a => a.DeletedDate == null).ToList();
            return View(computerdata);
        }

        // GET: computer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_computer_name computer_data = db.pos_computer_name.Find(id);
            if (computer_data == null)
            {
                return HttpNotFound("Crud not found.");
            }

            computerViewModel computerView = new computerViewModel()
            {
                mastershop_list = db.pos_shop_data.Where(a => a.DeletedDate == null && a.MasterShop == true).ToList(),
                salemode_list = db.pos_sale_mode.Where(a => a.DeletedDate == null).ToList(),
                sale_mode_selected = new List<string>(),
                paytype_list = db.pos_payment_type.Where(a => a.DeletedDate == null).ToList(),
                pay_type_selected = new List<string>(),
                computertype_list = db.pos_computer_type.Where(a => a.DeletedDate == null).ToList(),
                ShopId = computer_data.ShopId,
                ComputerNameId = computer_data.ComputerNameId,
                ComputerName = computer_data.ComputerName,
                ComputerTypeId = computer_data.ComputerTypeId,
                RegistrationNumber = computer_data.RegistrationNumber,
                DeviceCode = computer_data.DeviceCode,
                PayTypeList = computer_data.PayTypeList,
                SaleModeList = computer_data.SaleModeList,
                ReceiptHeader = computer_data.ReceiptHeader,
                FullTaxHeader = computer_data.FullTaxHeader,
                IPAddress = computer_data.IPAddress,
                Activate = computer_data.Activate
            };

            if (computerView.ShopId != null)
            {
                pos_shop_data shop = db.pos_shop_data.Find(computerView.ShopId);
                computerView.MasterShopId = shop.MasterShopLink;

                computerView.shop_list = db.pos_shop_data.Where(a => a.DeletedDate == null && a.MasterShopLink == shop.MasterShopLink).ToList();
            }

            return View(computerView);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(computerViewModel computerdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_computer_name pos_computer_name = db.pos_computer_name.Find(computerdata.ComputerNameId);
                    pos_computer_name.ShopId = computerdata.ShopId;
                    pos_computer_name.ComputerName = computerdata.ComputerName;
                    pos_computer_name.ComputerTypeId = computerdata.ComputerTypeId;
                    pos_computer_name.RegistrationNumber = computerdata.RegistrationNumber;
                    pos_computer_name.DeviceCode = computerdata.DeviceCode;

                    string paytypevalue = "";
                    foreach (string paytypeid in computerdata.pay_type_selected)
                    {
                        paytypevalue += paytypeid + ",";
                    }
                    pos_computer_name.PayTypeList = paytypevalue.Substring(0, paytypevalue.Length - 1);

                    string salemodevalue = "";
                    foreach (string salemodeid in computerdata.sale_mode_selected)
                    {
                        salemodevalue += salemodeid + ",";
                    }
                    pos_computer_name.SaleModeList = salemodevalue.Substring(0, salemodevalue.Length - 1);

                    pos_computer_name.ReceiptHeader = computerdata.ReceiptHeader;
                    pos_computer_name.FullTaxHeader = computerdata.FullTaxHeader;
                    pos_computer_name.IPAddress = computerdata.IPAddress;
                    if (computerdata.Activate == null)
                        pos_computer_name.Activate = false;
                    else
                        pos_computer_name.Activate = computerdata.Activate;
                    pos_computer_name.UpdatedBy = UserProfile.employee_id;
                    pos_computer_name.UpdatedDate = DateTime.Now;

                    db.Entry(pos_computer_name).State = EntityState.Modified;
                    db.SaveChanges();

                    //Update to License
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(UserProfile.APIURLBase + "/api/APIRegisterComputerName?KeyToken=uptome");

                        //HTTP POST
                        var result = client.PostAsJsonAsync("", pos_computer_name).Result;
                    }

                    return RedirectToAction("Index");
                }
                computerdata.mastershop_list = db.pos_shop_data.Where(a => a.DeletedDate == null && a.MasterShop == true).ToList();

                if (computerdata.MasterShopId != null)
                    computerdata.shop_list = db.pos_shop_data.Where(a => a.DeletedDate == null && a.MasterShopLink == computerdata.MasterShopId).ToList();
                computerdata.salemode_list = db.pos_sale_mode.Where(a => a.DeletedDate == null).ToList();
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(computerdata);
        }

        // POST: computer/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_computer_name pos_computer_name = db.pos_computer_name.Find(id);
            if (pos_computer_name != null)
            {
                pos_computer_name.DeletedBy = UserProfile.UserId;
                pos_computer_name.DeletedDate = DateTime.Now;
                db.Entry(pos_computer_name).State = EntityState.Modified;
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