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
    public class computerController : Controller
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();

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
                _qry = (from a in db.pos_computer_name.Where(a => a.DeletedDate == null && a.ShopId == ShopID)
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

        // GET: computer/Create
        public ActionResult Create(int? id)
        {
            computerViewModel view = new computerViewModel();
            //isi list
            view.mastershop_list = db.pos_shop_data.Where(a => a.DeletedDate == null && a.MasterShop == true).ToList();
    
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
               
                computertype_list = db.pos_computer_type.Where(a => a.DeletedDate == null).ToList(),
                
                ShopId = computer_data.ShopId,
                ComputerNameId = computer_data.ComputerNameId,
                ComputerName = computer_data.ComputerName,
                ComputerTypeId = computer_data.ComputerTypeId,
                RegistrationNumber = computer_data.RegistrationNumber,
                DeviceCode = computer_data.DeviceCode,
                ReceiptHeader = computer_data.ReceiptHeader,
                FullTaxHeader = computer_data.FullTaxHeader,
                IPAddress = computer_data.IPAddress,                
                Activate = computer_data.Activate
            };

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