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
    public class salemodeController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AutoAddProduct()
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

            var _qry = (from a in db.pos_sale_mode.Where(a => a.DeletedDate == null)
                        select new salemodeViewModel
                        {
                            SaleModeID = a.SaleModeID,
                            SaleModeName = a.SaleModeName
                        }).ToList<salemodeViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: salemode/Create
        public ActionResult Create(int? id)
        {
            salemodeViewModel salemodeView = new salemodeViewModel();
            salemodeView.PositionPrefix = false;
            salemodeView.HasServiceCharge = false;
            return View(salemodeView);
        }

        // POST: salemode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleModeName, PrefixText, PrefixTextPrinting, PrefixQueue, ReceiptHeaderText, NoPrintCopy, PayTypeList, PositionPrefix, HasServiceCharge, IconButtonServer")] salemodeViewModel salemodedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_sale_mode pos_sale_mode = new pos_sale_mode();
                    pos_sale_mode.SaleModeName = salemodedata.SaleModeName;
                    pos_sale_mode.PrefixText = salemodedata.PrefixText;
                    pos_sale_mode.PrefixTextPrinting = salemodedata.PrefixTextPrinting;
                    pos_sale_mode.ReceiptHeaderText = salemodedata.ReceiptHeaderText;
                    pos_sale_mode.NoPrintCopy = salemodedata.NoPrintCopy;
                    pos_sale_mode.PayTypeList = salemodedata.PayTypeList;
                    pos_sale_mode.PositionPrefix = salemodedata.PositionPrefix;
                    pos_sale_mode.HasServiceCharge = salemodedata.HasServiceCharge;


                    var files = Request.Files;

                    if (files.Count > 0)
                    {
                        string rootPathFile = Server.MapPath("~/Content/Pictures/Salesmode/");

                        string pathFile = "/Content/Pictures/Salesmode/";
                        string filename = "Prd-" + Guid.NewGuid() + DateTime.Today.ToString("yyMMdd") + ".png";

                        var myFile = Request.Files[0];
                        myFile.SaveAs(rootPathFile + filename);

                        string[] arraypath = (pathFile + filename).Split('/');
                        pos_sale_mode.IconButtonServer = pathFile + filename;

                        if (arraypath.Length > 0)
                            pos_sale_mode.IconButton = arraypath[arraypath.Length - 1].ToString();
                    }
                    else
                    {

                        if (salemodedata.IconButtonServer != null && salemodedata.IconButtonServer != string.Empty)
                        {
                            string pathfile = ProcessImage(salemodedata.IconButtonServer);
                            string[] arraypath = pathfile.Split('/');
                            pos_sale_mode.IconButtonServer = pathfile;
                            if (arraypath.Length > 0)
                                pos_sale_mode.IconButton = arraypath[arraypath.Length - 1].ToString();
                        }
                    }

                    pos_sale_mode = db.pos_sale_mode.Add(pos_sale_mode);
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

            return View(salemodedata);
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

                //filePath = "/Content/Pictures/Salesmode/sm-" + Guid.NewGuid() + DateTime.Today.ToString("yyMMdd") + ".png";
                filePath = Server.MapPath("~/Content/Pictures/Salesmode/") + "sm-" + Guid.NewGuid() + DateTime.Today.ToString("yyMMdd") + ".png";

                //using (FileStream stream = new FileStream(UserProfile.PathFolder + filePath, FileMode.Create))
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                string st = ex.Message;
            }

            return filePath;
        }
        /// GET: salemode/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_sale_mode salemode_data = db.pos_sale_mode.Find(id);
            if (salemode_data == null)
            {
                return HttpNotFound("Crud not found.");
            }
            string urlbase = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            salemodeViewModel salemodeView = new salemodeViewModel()
            {
                SaleModeID = salemode_data.SaleModeID,
                SaleModeName = salemode_data.SaleModeName,
                PrefixText = salemode_data.PrefixText,
                PrefixTextPrinting = salemode_data.PrefixTextPrinting,
                ReceiptHeaderText = salemode_data.ReceiptHeaderText,
                NoPrintCopy = salemode_data.NoPrintCopy,
                PayTypeList = salemode_data.PayTypeList,
                IconButtonServer = urlbase + salemode_data.IconButtonServer
            };

            if (salemode_data.PositionPrefix == null)
                salemodeView.PositionPrefix = false;
            else
                salemodeView.PositionPrefix = salemode_data.PositionPrefix;
            if (salemode_data.HasServiceCharge == null)
                salemodeView.HasServiceCharge = false;
            else
                salemodeView.HasServiceCharge = salemode_data.HasServiceCharge;

            return View(salemodeView);
        }

        /// POST: crud/Edit
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleModeID, SaleModeName, PrefixText, PrefixTextPrinting, PrefixQueue, ReceiptHeaderText, NoPrintCopy, PayTypeList, PositionPrefix, HasServiceCharge, IconButtonServer")] salemodeViewModel salemodedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_sale_mode pos_sale_mode = db.pos_sale_mode.Find(salemodedata.SaleModeID);
                    pos_sale_mode.SaleModeName = salemodedata.SaleModeName;
                    pos_sale_mode.PrefixText = salemodedata.PrefixText;
                    pos_sale_mode.PrefixTextPrinting = salemodedata.PrefixTextPrinting;
                    pos_sale_mode.ReceiptHeaderText = salemodedata.ReceiptHeaderText;
                    pos_sale_mode.NoPrintCopy = salemodedata.NoPrintCopy;
                    pos_sale_mode.PayTypeList = salemodedata.PayTypeList;
                    pos_sale_mode.PositionPrefix = salemodedata.PositionPrefix;
                    pos_sale_mode.HasServiceCharge = salemodedata.HasServiceCharge;

                    var files = Request.Files;

                    if (files.Count > 0)
                    {
                        string rootPathFile = Server.MapPath("~/Content/Pictures/Salesmode/");

                        string pathFile = "/Content/Pictures/Salesmode/";
                        string filename = "Prd-" + Guid.NewGuid() + DateTime.Today.ToString("yyMMdd") + ".png";

                        var myFile = Request.Files[0];
                        myFile.SaveAs(rootPathFile + filename);

                        string[] arraypath = (pathFile + filename).Split('/');
                        pos_sale_mode.IconButtonServer = pathFile + filename;

                        if (arraypath.Length > 0)
                            pos_sale_mode.IconButton = arraypath[arraypath.Length - 1].ToString();
                    }
                    else
                    {
                        if (salemodedata.IconButtonServer != null && salemodedata.IconButtonServer != string.Empty)
                        {
                            string pathfile = ProcessImage(salemodedata.IconButtonServer);
                            string[] arraypath = pathfile.Split('/');
                            pos_sale_mode.IconButtonServer = pathfile;
                            if (arraypath.Length > 0)
                                pos_sale_mode.IconButton = arraypath[arraypath.Length - 1].ToString();
                        }
                    }

                    db.Entry(pos_sale_mode).State = EntityState.Modified;
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

            return View(salemodedata);
        }

        // POST: salemode/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_sale_mode pos_sale_mode = db.pos_sale_mode.Find(id);
            if (pos_sale_mode != null)
            {
                pos_sale_mode.DeletedBy = UserProfile.UserId;
                pos_sale_mode.DeletedDate = DateTime.Now;
                db.Entry(pos_sale_mode).State = EntityState.Modified;
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