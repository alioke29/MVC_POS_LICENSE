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
    public class billtemplateController : Controller
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: billtemplate
        public ActionResult Index(int MasterShopID = 0, int ShopID = 0)
        {
            if (MasterShopID == 0 && ShopID == 0)
            {
                billtemplateViewModel billViewModel = new billtemplateViewModel()
                {
                    MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                    flagSave = ""
                };
                return View(billViewModel);
            }
            else
            {
                pos_shop_data shopdata = db.pos_shop_data.Find(ShopID);
                int mastershopid = Convert.ToInt32(shopdata.MasterShopLink);
                billtemplateViewModel billViewModel = new billtemplateViewModel()
                {
                    MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                    ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == mastershopid && o.DeletedDate == null).ToList(),
                    MasterShopID = mastershopid,
                    ShopID = ShopID,
                    BillTemplateList = db.vw_bill_template.Where(o => o.MasterShopID == MasterShopID && o.ShopID == ShopID).OrderBy(o => o.Ordering).OrderByDescending(o => o.Position).ToList(),
                    flagSave = "",
                    billlogo = db.pos_bill_template_image.Where(o => o.MasterShopID == MasterShopID && o.ShopID == ShopID).FirstOrDefault()
                };
                ModelState.Clear();
                return View(billViewModel);
            }
        }

        public JsonResult GetShopList(int MasterShopID = 0)
        {
            var _ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == MasterShopID && o.DeletedDate == null).ToList();

            return Json(_ShopList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetShopChanges(billtemplateViewModel BillTempData)
        {
            try
            {
                BillTempData.MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList();
                BillTempData.ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == BillTempData.MasterShopID && o.DeletedDate == null).ToList();
                BillTempData.BillTemplateList = db.vw_bill_template.Where(o => o.MasterShopID == BillTempData.MasterShopID && o.ShopID == BillTempData.ShopID).ToList();
                return RedirectToAction("Index", new { id = BillTempData.ShopID });

            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(BillTempData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Index(billtemplateViewModel BillTempData)
        {

            try
            {
                if (BillTempData.flagSave == "save")
                {
                    if (BillTempData.Ordering < 1)
                    {
                        ModelState.AddModelError("Ordering", "Order Number is invalid.");
                    }
                    else if (BillTempData.MasterShopID > 0 && BillTempData.ShopID > 0)
                    {
                        var exist = db.pos_bill_template.Where(o => o.MasterShopID == BillTempData.MasterShopID
                                                                && o.ShopID == BillTempData.ShopID
                                                                && o.Ordering == BillTempData.Ordering
                                                                && o.Position == BillTempData.Position).FirstOrDefault();
                        if (exist != null)
                            ModelState.AddModelError("Ordering", "Order Number is already exist.");
                    }
                    if (ModelState.IsValid)
                    {
                        pos_bill_template BillTemplate = new pos_bill_template();

                        BillTemplate.MasterShopID = BillTempData.MasterShopID;
                        BillTemplate.ShopID = BillTempData.ShopID;
                        BillTemplate.Position = BillTempData.Position;
                        BillTemplate.Text = BillTempData.Text;
                        BillTemplate.Ordering = BillTempData.Ordering;
                        BillTemplate.isActive = true;

                        BillTemplate.CreatedBy = UserProfile.employee_id;
                        BillTemplate.CreatedDate = DateTime.Now;

                        BillTemplate = db.pos_bill_template.Add(BillTemplate);
                        db.SaveChanges();

                    }
                    else
                    {
                        BillTempData.MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList();
                        BillTempData.ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == BillTempData.MasterShopID && o.DeletedDate == null).ToList();
                        BillTempData.BillTemplateList = db.vw_bill_template.Where(o => o.MasterShopID == BillTempData.MasterShopID && o.ShopID == BillTempData.ShopID).OrderBy(o => o.Ordering).OrderByDescending(o => o.Position).ToList();
                        BillTempData.billlogo = db.pos_bill_template_image.Where(o => o.MasterShopID == BillTempData.MasterShopID && o.ShopID == BillTempData.ShopID).FirstOrDefault();
                        return View(BillTempData);
                    }
                }

                //get data by changes dropdownlist
                pos_shop_data shopdata = db.pos_shop_data.Find(BillTempData.ShopID);
                int mastershopid = Convert.ToInt32(shopdata.MasterShopLink);
                billtemplateViewModel billViewModel = new billtemplateViewModel()
                {
                    MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                    ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == mastershopid && o.DeletedDate == null).ToList(),
                    MasterShopID = mastershopid,
                    ShopID = BillTempData.ShopID,
                    Ordering = BillTempData.Ordering + 1,
                    BillTemplateList = db.vw_bill_template.Where(o => o.MasterShopID == BillTempData.MasterShopID && o.ShopID == BillTempData.ShopID).OrderBy(o => o.Ordering).OrderByDescending(o => o.Position).ToList(),
                    flagSave = "",
                    billlogo = db.pos_bill_template_image.Where(o => o.MasterShopID == BillTempData.MasterShopID && o.ShopID == BillTempData.ShopID).FirstOrDefault()
                };
                ModelState.Clear();
                return View(billViewModel);
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(BillTempData);
        }

        [HttpGet]
        public JsonResult DeleteBillByID(int id)
        {
            pos_bill_template bill = db.pos_bill_template.Find(id);
            if (bill != null)
            {
                db.pos_bill_template.Remove(bill);
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        #region LogoImage
        public ActionResult LogoImage(int MasterShopID, int ShopID = 0)
        {
            string urlbase = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            pos_bill_template_image billImage = db.pos_bill_template_image.Where(a => a.ShopID == ShopID).FirstOrDefault();
            billtemplateViewModel billViewModel = new billtemplateViewModel()
            {
                MasterShopID = MasterShopID,
                ShopID = ShopID,
                LogoHeaderFile = urlbase + billImage.LogoHeaderFile,
                QRFooterText = billImage.QRFooterText
            };
            return View(billViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogoImage(billtemplateViewModel BillTempData)
        {
            try
            {
                pos_bill_template_image bill = db.pos_bill_template_image.Where(o => o.MasterShopID == BillTempData.MasterShopID && o.ShopID == BillTempData.ShopID).FirstOrDefault();
                if (bill != null)
                {
                    pos_bill_template_image billitem = db.pos_bill_template_image.Find(bill.BillTemplateImageID);
                    if (BillTempData.LogoHeaderFile != null)
                        billitem.LogoHeaderFile = ProcessImage(BillTempData.LogoHeaderFile);
                    billitem.QRFooterText = BillTempData.QRFooterText;
                    billitem.UpdatedBy = UserProfile.employee_id;
                    billitem.UpdatedDate = DateTime.Now;
                    db.Entry(billitem).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", new { MasterShopID = BillTempData.MasterShopID, ShopID = BillTempData.ShopID });
                }
                else
                {
                    pos_bill_template_image billitem = new pos_bill_template_image();
                    billitem.MasterShopID = BillTempData.MasterShopID;
                    billitem.ShopID = BillTempData.ShopID;
                    if (BillTempData.LogoHeaderFile != null)
                        billitem.LogoHeaderFile = ProcessImage(BillTempData.LogoHeaderFile);
                    billitem.QRFooterText = BillTempData.QRFooterText;
                    billitem.CreatedBy = UserProfile.employee_id;
                    billitem.CreatedDate = DateTime.Now;
                    billitem = db.pos_bill_template_image.Add(billitem);
                    db.SaveChanges();

                    return RedirectToAction("Index", new { MasterShopID = BillTempData.MasterShopID, ShopID = BillTempData.ShopID });
                }
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }
            return View(BillTempData);
        }

        /// <summary>
        /// Process image and save in predefined path
        /// </summary>
        /// <param name="croppedImage">
        /// <returns></returns>
        private string ProcessImage(string croppedImage)
        {
            string filePath = String.Empty;
            try
            {
                string base64 = croppedImage;
                byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
                filePath = "/Content/Pictures/Shop/sbl-" + Guid.NewGuid() + DateTime.Today.ToString("yyMMdd") + ".png";
                using (FileStream stream = new FileStream(UserProfile.PathFolder + filePath, FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                string st = ex.Message;
            }

            return "." + filePath;
        }
        #endregion
    }
}