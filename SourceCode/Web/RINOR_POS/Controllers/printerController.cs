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
    public class printerController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: print
        public ActionResult Index(int id = 0)
        {
            var printtype = db.pos_printer_type.Find(id);
            printerViewModel printerView = new printerViewModel()
            {
                PrinterTypeID = id,
                PrinterTypeName = printtype.PrinterTypeName
            };
            return View(printerView);
        }

        /// <summary>
        /// list for index
        /// </summary>
        /// <returns>JSON</returns>
        public JsonResult GetIndexList(int id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var _qry = (from a in db.pos_printers.Where(a => a.DeletedDate == null && a.PrinterTypeID == id)
                        join b in db.pos_printer_type on a.PrinterTypeID equals b.PrinterTypeID

                        select new printerViewModel
                        {
                            PrinterID = a.PrinterID,
                            PrinterName = a.PrinterName,
                            PrinterTypeName = b.PrinterTypeName,
                            PrinterDeviceName = a.PrinterDeviceName,
                            PrinterNameForPrint = a.PrinterNameForPrint,
                            PaperWidth = a.PaperWidth,
                            LineSpacing = a.LineSpacing,
                            MarginTop = a.MarginTop,
                            MarginBottom = a.MarginBottom,
                            MarginLeft = a.MarginLeft,
                            MarginRight = a.MarginRight,
                        }).ToList<printerViewModel>();

            List<printerViewModel> Printers = new List<printerViewModel>();
            foreach (printerViewModel printerview in _qry)
            {
                printerViewModel printer = new printerViewModel();
                printer.PrinterID = printerview.PrinterID;
                printer.PrinterName = printerview.PrinterName;
                printer.PrinterDeviceName = printerview.PrinterDeviceName;
                printer.PrinterTypeName = printerview.PrinterTypeName;
                printer.PrinterNameForPrint = string.Format("[{0}] [Margin: {1} {2} {3} {4}] [Width: {5}] [Line-Spacing: {6}]",
                                                    printerview.PrinterNameForPrint,
                                                    printerview.MarginTop, printerview.MarginLeft, printerview.MarginRight, printerview.MarginBottom,
                                                    printerview.PaperWidth, printerview.LineSpacing);
                Printers.Add(printer);
            }
            return Json(new { data = Printers }, JsonRequestBehavior.AllowGet);
        }

        // GET: printer/Create
        public ActionResult Create(int id = 0)
        {
            printerViewModel printerView = new printerViewModel()
            {
                PrinterTypeID = id,
                PaperWidth = 0,
                LineSpacing = 0,
                MarginTop = 0,
                MarginBottom = 0,
                MarginLeft = 0,
                MarginRight = 0
            };
            return View(printerView);
        }

        // POST: crud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(printerViewModel printer_data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_printers pos_printers = new pos_printers()
                    {
                        PrinterTypeID = printer_data.PrinterTypeID,
                        PrinterName = printer_data.PrinterName,
                        PrinterDeviceName = printer_data.PrinterDeviceName,
                        PrinterDeviceBackup = printer_data.PrinterDeviceBackup,
                        PrinterNameForPrint = printer_data.PrinterNameForPrint,
                        PaperWidth = printer_data.PaperWidth,
                        LineSpacing = printer_data.LineSpacing,
                        MarginTop = printer_data.MarginTop,
                        MarginLeft = printer_data.MarginLeft,
                        MarginRight = printer_data.MarginRight,
                        MarginBottom = printer_data.MarginBottom,
                        IsOposPrinter = printer_data.IsOposPrinter,
                        PrinterStatus = printer_data.PrinterStatus,
                        CreatedDate = DateTime.Now,
                        CreatedBy = UserProfile.UserId
                    };
                    pos_printers = db.pos_printers.Add(pos_printers);
                    db.SaveChanges();

                    return RedirectToAction("Index", new { id = printer_data.PrinterTypeID });
                }
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(printer_data);
        }

        // GET: crud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_printers printer_data = db.pos_printers.Find(id);
            if (printer_data == null)
            {
                return HttpNotFound("Product VAT not found.");
            }

            printerViewModel printer_model = new printerViewModel()
            {
                PrinterID = printer_data.PrinterID,
                PrinterTypeID = printer_data.PrinterTypeID,
                PrinterName = printer_data.PrinterName,
                PrinterDeviceName = printer_data.PrinterDeviceName,
                PrinterDeviceBackup = printer_data.PrinterDeviceBackup,
                PrinterNameForPrint = printer_data.PrinterNameForPrint,
                PaperWidth = printer_data.PaperWidth,
                LineSpacing = printer_data.LineSpacing,
                MarginTop = printer_data.MarginTop,
                MarginLeft = printer_data.MarginLeft,
                MarginRight = printer_data.MarginRight,
                MarginBottom = printer_data.MarginBottom,
                IsOposPrinter = printer_data.IsOposPrinter,
                PrinterStatus = printer_data.PrinterStatus,
            };

            return View(printer_model);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(printerViewModel printer_data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_printers pos_printers = db.pos_printers.Find(printer_data.PrinterID);
                    //pos_printers.PrinterTypeID = printer_data.PrinterTypeID;
                    pos_printers.PrinterName = printer_data.PrinterName;
                    pos_printers.PrinterDeviceName = printer_data.PrinterDeviceName;
                    pos_printers.PrinterDeviceBackup = printer_data.PrinterDeviceBackup;
                    pos_printers.PrinterNameForPrint = printer_data.PrinterNameForPrint;
                    pos_printers.PaperWidth = printer_data.PaperWidth;
                    pos_printers.LineSpacing = printer_data.LineSpacing;
                    pos_printers.MarginTop = printer_data.MarginTop;
                    pos_printers.MarginLeft = printer_data.MarginLeft;
                    pos_printers.MarginRight = printer_data.MarginRight;
                    pos_printers.MarginBottom = printer_data.MarginBottom;
                    pos_printers.IsOposPrinter = printer_data.IsOposPrinter;
                    pos_printers.PrinterStatus = printer_data.PrinterStatus;
                    pos_printers.UpdatedDate = DateTime.Now;
                    pos_printers.UpdatedBy = UserProfile.UserId;

                    db.Entry(pos_printers).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", new { id = printer_data.PrinterTypeID });
                }
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(printer_data);
        }

        // POST: crud/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_printers pos_printers = db.pos_printers.Find(id);
            if (pos_printers != null)
            {
                pos_printers.DeletedBy = UserProfile.UserId;
                pos_printers.DeletedDate = DateTime.Now;
                db.Entry(pos_printers).State = EntityState.Modified;
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