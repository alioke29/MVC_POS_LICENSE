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
    public class paymentgroupController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: paymentgroup
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

            var _qry = (from a in db.pos_payment_group.Where(a => a.DeletedDate == null)
                        select new paymentgroupViewModel
                        {
                            PayGroupID = a.PayGroupID,
                            PayGroupName = a.PayGroupName,
                            Ordering = a.Ordering,
                            Activate = a.Activate
                        }).ToList<paymentgroupViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: paymentgroup/Create
        public ActionResult Create(int? id)
        {
            paymentgroupViewModel view = new paymentgroupViewModel();
            view.Activate = false;
            view.Ordering = 0;
            return View(view);
        }

        // POST: paymentgroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PayGroupName, Ordering, Activate, IconButtonServer")] paymentgroupViewModel paymentgroupdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_payment_group pos_payment_group = new pos_payment_group();
                    pos_payment_group.PayGroupName = paymentgroupdata.PayGroupName;
                    pos_payment_group.Ordering = paymentgroupdata.Ordering;
                    pos_payment_group.Activate = paymentgroupdata.Activate;
                    pos_payment_group.CreatedBy = UserProfile.employee_id;
                    pos_payment_group.CreatedDate = DateTime.Now;

                    if (paymentgroupdata.IconButtonServer != null && paymentgroupdata.IconButtonServer != string.Empty)
                    {
                        string pathfile = ProcessImage(paymentgroupdata.IconButtonServer);
                        string[] arraypath = pathfile.Split('/');
                        pos_payment_group.IconButtonServer = pathfile;
                        if (arraypath.Length > 0)
                            pos_payment_group.IconButton = arraypath[arraypath.Length - 1].ToString();
                    }

                    pos_payment_group = db.pos_payment_group.Add(pos_payment_group);
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

            return View(paymentgroupdata);
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
                filePath = "/Content/Pictures/Payment/pay-" + Guid.NewGuid() + DateTime.Today.ToString("yyMMdd") + ".png";
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

            return filePath;
        }
        // GET: paymentgroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_payment_group paymentgroup_data = db.pos_payment_group.Find(id);
            if (paymentgroup_data == null)
            {
                return HttpNotFound("Crud not found.");
            }
            string urlbase = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            paymentgroupViewModel paymentgroupView = new paymentgroupViewModel()
            {
                PayGroupID = paymentgroup_data.PayGroupID,
                PayGroupName = paymentgroup_data.PayGroupName,
                IconButtonServer = urlbase + paymentgroup_data.IconButtonServer,
                Ordering = paymentgroup_data.Ordering,
                Activate = paymentgroup_data.Activate
            };

            return View(paymentgroupView);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PayGroupID, PayGroupName, Ordering, Activate, IconButtonServer")] paymentgroupViewModel paymentgroupdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_payment_group pos_payment_group = db.pos_payment_group.Find(paymentgroupdata.PayGroupID);
                    pos_payment_group.PayGroupName = paymentgroupdata.PayGroupName;
                    pos_payment_group.Ordering = paymentgroupdata.Ordering;
                    pos_payment_group.Activate = paymentgroupdata.Activate;

                    if (paymentgroupdata.IconButtonServer != null && paymentgroupdata.IconButtonServer != string.Empty)
                    {
                        string pathfile = ProcessImage(paymentgroupdata.IconButtonServer);
                        string[] arraypath = pathfile.Split('/');
                        pos_payment_group.IconButtonServer = pathfile;
                        if (arraypath.Length > 0)
                            pos_payment_group.IconButton = arraypath[arraypath.Length - 1].ToString();
                    }

                    pos_payment_group.UpdatedBy = UserProfile.employee_id;
                    pos_payment_group.UpdatedDate = DateTime.Now;

                    db.Entry(pos_payment_group).State = EntityState.Modified;
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

            return View(paymentgroupdata);
        }

        // POST: paymentgroup/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_payment_group pos_payment_group = db.pos_payment_group.Find(id);
            if (pos_payment_group != null)
            {
                pos_payment_group.DeletedBy = UserProfile.UserId;
                pos_payment_group.DeletedDate = DateTime.Now;
                db.Entry(pos_payment_group).State = EntityState.Modified;
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