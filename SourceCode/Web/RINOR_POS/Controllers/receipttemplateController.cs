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
    public class receipttemplateController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: receipttemplate
        public ActionResult Index(int MasterShopID = 0, int ShopID = 0)
        {
            if (MasterShopID == 0 && ShopID == 0)
            {
                receipttemplateViewModel receiptViewModel = new receipttemplateViewModel()
                {
                    MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                    flagSave = ""
                };
                return View(receiptViewModel);
            }
            else
            {
                pos_shop_data shopdata = db.pos_shop_data.Find(ShopID);
                int mastershopid = Convert.ToInt32(shopdata.MasterShopLink);
                receipttemplateViewModel receiptViewModel = new receipttemplateViewModel()
                {
                    MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                    ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == mastershopid && o.DeletedDate == null).ToList(),
                    MasterShopID = mastershopid,
                    ShopID = ShopID,
                    ReceiptTemplateList = db.vw_receipt_template.Where(o => o.MasterShopID == MasterShopID && o.ShopID == ShopID).OrderBy(o => o.Ordering).OrderByDescending(o => o.Position).ToList(),
                    flagSave = "",
                    receiptlogo = db.pos_receipt_template_image.Where(o => o.MasterShopID == MasterShopID && o.ShopID == ShopID).FirstOrDefault()
                };
                ModelState.Clear();
                return View(receiptViewModel);
            }
        }

        public JsonResult GetShopList(int MasterShopID = 0)
        {
            var _ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == MasterShopID && o.DeletedDate == null).ToList();

            return Json(_ShopList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetShopChanges(receipttemplateViewModel ReceiptTempData)
        {
            try
            {
                ReceiptTempData.MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList();
                ReceiptTempData.ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == ReceiptTempData.MasterShopID && o.DeletedDate == null).ToList();
                ReceiptTempData.ReceiptTemplateList = db.vw_receipt_template.Where(o => o.MasterShopID == ReceiptTempData.MasterShopID && o.ShopID == ReceiptTempData.ShopID).ToList();
                return RedirectToAction("Index", new { id = ReceiptTempData.ShopID });

            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(ReceiptTempData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Index(receipttemplateViewModel ReceiptTempData)
        {

            try
            {
                if (ReceiptTempData.flagSave == "save")
                {
                    if (ReceiptTempData.Ordering < 1)
                    {
                        ModelState.AddModelError("Ordering", "Order Number is invalid.");
                    }
                    else if (ReceiptTempData.MasterShopID > 0 && ReceiptTempData.ShopID > 0)
                    {
                        var exist = db.pos_receipt_template.Where(o => o.MasterShopID == ReceiptTempData.MasterShopID
                                                                && o.ShopID == ReceiptTempData.ShopID
                                                                && o.Ordering == ReceiptTempData.Ordering
                                                                && o.Position == ReceiptTempData.Position).FirstOrDefault();
                        if (exist != null)
                            ModelState.AddModelError("Ordering", "Order Number is already exist.");
                    }
                    if (ModelState.IsValid)
                    {
                        pos_receipt_template ReceiptTemplate = new pos_receipt_template();

                        ReceiptTemplate.MasterShopID = ReceiptTempData.MasterShopID;
                        ReceiptTemplate.ShopID = ReceiptTempData.ShopID;
                        ReceiptTemplate.Position = ReceiptTempData.Position;
                        ReceiptTemplate.Text = ReceiptTempData.Text;
                        ReceiptTemplate.Ordering = ReceiptTempData.Ordering;
                        ReceiptTemplate.isActive = true;

                        ReceiptTemplate.CreatedBy = UserProfile.employee_id;
                        ReceiptTemplate.CreatedDate = DateTime.Now;

                        ReceiptTemplate = db.pos_receipt_template.Add(ReceiptTemplate);
                        db.SaveChanges();

                    }
                    else
                    {
                        ReceiptTempData.MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList();
                        ReceiptTempData.ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == ReceiptTempData.MasterShopID && o.DeletedDate == null).ToList();
                        ReceiptTempData.ReceiptTemplateList = db.vw_receipt_template.Where(o => o.MasterShopID == ReceiptTempData.MasterShopID && o.ShopID == ReceiptTempData.ShopID).OrderBy(o => o.Ordering).OrderByDescending(o => o.Position).ToList();
                        ReceiptTempData.receiptlogo = db.pos_receipt_template_image.Where(o => o.MasterShopID == ReceiptTempData.MasterShopID && o.ShopID == ReceiptTempData.ShopID).FirstOrDefault();
                        return View(ReceiptTempData);
                    }
                }

                //get data by changes dropdownlist
                pos_shop_data shopdata = db.pos_shop_data.Find(ReceiptTempData.ShopID);
                int mastershopid = Convert.ToInt32(shopdata.MasterShopLink);
                receipttemplateViewModel receiptViewModel = new receipttemplateViewModel()
                {
                    MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                    ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == mastershopid && o.DeletedDate == null).ToList(),
                    MasterShopID = mastershopid,
                    ShopID = ReceiptTempData.ShopID,
                    Ordering = ReceiptTempData.Ordering + 1,
                    ReceiptTemplateList = db.vw_receipt_template.Where(o => o.MasterShopID == ReceiptTempData.MasterShopID && o.ShopID == ReceiptTempData.ShopID).OrderBy(o => o.Ordering).OrderByDescending(o => o.Position).ToList(),
                    flagSave = "",
                    receiptlogo = db.pos_receipt_template_image.Where(o => o.MasterShopID == ReceiptTempData.MasterShopID && o.ShopID == ReceiptTempData.ShopID).FirstOrDefault()
                };
                ModelState.Clear();
                return View(receiptViewModel);
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(ReceiptTempData);
        }

        [HttpGet]
        public JsonResult DeleteReceiptByID(int id)
        {
            pos_receipt_template receipt = db.pos_receipt_template.Find(id);
            if (receipt != null)
            {
                db.pos_receipt_template.Remove(receipt);
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
            pos_receipt_template_image receiptImage = db.pos_receipt_template_image.Where(a => a.ShopID == ShopID).FirstOrDefault();
            receipttemplateViewModel receiptViewModel = new receipttemplateViewModel()
            {
                MasterShopID = MasterShopID,
                ShopID = ShopID,
                LogoHeaderFile = urlbase + receiptImage.LogoHeaderFile,
                QRFooterText = receiptImage.QRFooterText
            };
            return View(receiptViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogoImage(receipttemplateViewModel ReceiptTempData)
        {
            try
            {
                pos_receipt_template_image receipt = db.pos_receipt_template_image.Where(o => o.MasterShopID == ReceiptTempData.MasterShopID && o.ShopID == ReceiptTempData.ShopID).FirstOrDefault();
                if (receipt != null)
                {
                    pos_receipt_template_image receiptitem = db.pos_receipt_template_image.Find(receipt.ReceiptTemplateImageID);
                    if (ReceiptTempData.LogoHeaderFile != null)
                        receiptitem.LogoHeaderFile = ProcessImage(ReceiptTempData.LogoHeaderFile);
                    receiptitem.QRFooterText = ReceiptTempData.QRFooterText;
                    receiptitem.UpdatedBy = UserProfile.employee_id;
                    receiptitem.UpdatedDate = DateTime.Now;
                    db.Entry(receiptitem).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", new { MasterShopID = ReceiptTempData.MasterShopID, ShopID = ReceiptTempData.ShopID });
                }
                else
                {
                    pos_receipt_template_image receiptitem = new pos_receipt_template_image();
                    receiptitem.MasterShopID = ReceiptTempData.MasterShopID;
                    receiptitem.ShopID = ReceiptTempData.ShopID;
                    if (ReceiptTempData.LogoHeaderFile != null)
                        receiptitem.LogoHeaderFile = ProcessImage(ReceiptTempData.LogoHeaderFile);
                    receiptitem.QRFooterText = ReceiptTempData.QRFooterText;
                    receiptitem.CreatedBy = UserProfile.employee_id;
                    receiptitem.CreatedDate = DateTime.Now;
                    receiptitem = db.pos_receipt_template_image.Add(receiptitem);
                    db.SaveChanges();

                    return RedirectToAction("Index", new { MasterShopID = ReceiptTempData.MasterShopID, ShopID = ReceiptTempData.ShopID });
                }
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }
            return View(ReceiptTempData);
        }

        /// <summary>
        /// Process image and save in predefined path
        /// </summary>
        /// <param name="croppedImage">
        /// <returns></returns>
        private string ProcessImage(string croppedImage)
        {
            string filePath = String.Empty;
            //try
            //{
                string base64 = croppedImage;
                byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
                filePath = "/Content/Pictures/Shop/sbl-" + Guid.NewGuid() + DateTime.Today.ToString("yyMMdd") + ".png";
                using (FileStream stream = new FileStream(UserProfile.PathFolder + filePath, FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
            //}
            //catch (Exception ex)
            //{
            //    string st = ex.Message;
            //}

            return "." + filePath;
        }
        #endregion
    }
}