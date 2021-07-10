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

namespace RINOR_POS.Controllers
{
    [Authorize]
    public class computertypeController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: computertype
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

            var _qry = (from a in db.pos_computer_type.Where(a => a.DeletedDate == null)
                        select new computertypeViewModel
                        {
                            ComputerTypeID = a.ComputerTypeID,
                            ComputerTypeName = a.ComputerTypeName,
                            Activate = a.Activate
                        }).ToList<computertypeViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: computertype/Create
        public ActionResult Create(int? id)
        {
            computertypeViewModel view = new computertypeViewModel();
            view.Activate = false;
            return View(view);
        }

        // POST: computertype/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComputerTypeName, Ordering, Activate")] computertypeViewModel computertypedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_computer_type pos_computer_type = new pos_computer_type();
                    pos_computer_type.ComputerTypeName = computertypedata.ComputerTypeName;
                    if (computertypedata.Activate == null)
                        pos_computer_type.Activate = false;
                    else
                        pos_computer_type.Activate = computertypedata.Activate;
                    pos_computer_type.CreatedBy = UserProfile.employee_id;
                    pos_computer_type.CreatedDate = DateTime.Now;

                    pos_computer_type = db.pos_computer_type.Add(pos_computer_type);
                    db.SaveChanges();

                    //Request Register to License
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(UserProfile.APIURLBase + "/api/APIRegisterComputerType?KeyToken=uptome");

                        //HTTP POST
                        var result = client.PostAsJsonAsync("", pos_computer_type).Result;
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

            return View(computertypedata);
        }

        // GET: computertype/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_computer_type computertype_data = db.pos_computer_type.Find(id);
            if (computertype_data == null)
            {
                return HttpNotFound("Crud not found.");
            }

            computertypeViewModel computertypeView = new computertypeViewModel()
            {
                ComputerTypeID = computertype_data.ComputerTypeID,
                ComputerTypeName = computertype_data.ComputerTypeName,
                Activate = computertype_data.Activate
            };

            return View(computertypeView);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComputerTypeID, ComputerTypeName, Ordering, Activate")] computertypeViewModel computertypedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_computer_type pos_computer_type = db.pos_computer_type.Find(computertypedata.ComputerTypeID);
                    pos_computer_type.ComputerTypeName = computertypedata.ComputerTypeName;
                    if (computertypedata.Activate == null)
                        pos_computer_type.Activate = false;
                    else
                        pos_computer_type.Activate = computertypedata.Activate;
                    pos_computer_type.UpdatedBy = UserProfile.employee_id;
                    pos_computer_type.UpdatedDate = DateTime.Now;

                    db.Entry(pos_computer_type).State = EntityState.Modified;
                    db.SaveChanges();

                    //Update to License
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(UserProfile.APIURLBase + "/api/APIRegisterComputerType?KeyToken=uptome");

                        //HTTP POST
                        var result = client.PostAsJsonAsync("", pos_computer_type).Result;
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

            return View(computertypedata);
        }

        // POST: computertype/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_computer_type pos_computer_type = db.pos_computer_type.Find(id);
            if (pos_computer_type != null)
            {
                pos_computer_type.DeletedBy = UserProfile.UserId;
                pos_computer_type.DeletedDate = DateTime.Now;
                db.Entry(pos_computer_type).State = EntityState.Modified;
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