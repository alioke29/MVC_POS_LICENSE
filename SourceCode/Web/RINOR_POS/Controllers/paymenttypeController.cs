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
    public class paymenttypeController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: paymenttype
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

            var _qry = (from a in db.pos_payment_type.Where(a => a.DeletedDate == null)
                        from b in db.pos_payment_group.Where(b => b.DeletedDate == null && a.PayGroupId == b.PayGroupID)
                        select new paymenttypeViewModel
                        {
                            PayTypeID = a.PayTypeID,
                            PayTypeName = a.PayTypeName,
                            PayTypeCode = a.PayTypeCode,
                            PayGroupId = a.PayGroupId,
                            PayGroupName = b.PayGroupName,
                            DisplayName = a.DisplayName,
                            Ordering = a.Ordering,
                            Activate = a.Activate
                        }).ToList<paymenttypeViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: paymenttype/Create
        public ActionResult Create(int? id)
        {
            paymenttypeViewModel view = new paymenttypeViewModel();
            view.Activate = false;
            view.isServiceCharge = false;
            view.isVat = false;
            view.Ordering = 0;
            view.payment_group_list = db.pos_payment_group.Where(a => a.DeletedDate == null).ToList();
            return View(view);
        }

        // POST: paymenttype/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(paymenttypeViewModel paymenttypedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_payment_type pos_payment_type = new pos_payment_type();
                    pos_payment_type.PayGroupId = paymenttypedata.PayGroupId;
                    pos_payment_type.PayTypeName = paymenttypedata.PayTypeName;
                    pos_payment_type.PayTypeCode = paymenttypedata.PayTypeCode;
                    pos_payment_type.DisplayName = paymenttypedata.DisplayName;
                    //pos_payment_type.isVat = paymenttypedata.isVat;
                    //pos_payment_type.isServiceCharge = paymenttypedata.isServiceCharge;
                    pos_payment_type.Ordering = paymenttypedata.Ordering;
                    pos_payment_type.Activate = paymenttypedata.Activate;

                    if (paymenttypedata.IconButtonServer != null && paymenttypedata.IconButtonServer != string.Empty)
                    {
                        string pathfile = ProcessImage(paymenttypedata.IconButtonServer);
                        string[] arraypath = pathfile.Split('/');
                        pos_payment_type.IconButtonServer = pathfile;
                        if (arraypath.Length > 0)
                            pos_payment_type.IconButton = arraypath[arraypath.Length - 1].ToString();
                    }

                    pos_payment_type.CreatedBy = UserProfile.employee_id;
                    pos_payment_type.CreatedDate = DateTime.Now;

                    pos_payment_type = db.pos_payment_type.Add(pos_payment_type);
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
            paymenttypedata.payment_group_list = db.pos_payment_group.Where(a => a.DeletedDate == null).ToList();
            return View(paymenttypedata);
        }

        // GET: paymenttype/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_payment_type paymenttype_data = db.pos_payment_type.Find(id);
            if (paymenttype_data == null)
            {
                return HttpNotFound("Crud not found.");
            }
            string urlbase = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            paymenttypeViewModel paymenttypeView = new paymenttypeViewModel()
            {
                PayTypeID = paymenttype_data.PayTypeID,
                PayGroupId = paymenttype_data.PayGroupId,
                PayTypeName = paymenttype_data.PayTypeName,
                PayTypeCode = paymenttype_data.PayTypeCode,
                DisplayName = paymenttype_data.DisplayName,
                //isVat = paymenttype_data.isVat,
                //isServiceCharge = paymenttype_data.isServiceCharge,
                Ordering = paymenttype_data.Ordering,
                Activate = paymenttype_data.Activate,
                IconButtonServer = urlbase + paymenttype_data.IconButtonServer,
                payment_group_list = db.pos_payment_group.Where(a => a.DeletedDate == null).ToList()
            };

            return View(paymenttypeView);
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
            
            filePath = "/Content/Pictures/Payment/pay-" + Guid.NewGuid() + DateTime.Today.ToString("yyMMdd") + ".png";
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

            return filePath;
        }
        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(paymenttypeViewModel paymenttypedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_payment_type pos_payment_type = db.pos_payment_type.Find(paymenttypedata.PayTypeID);
                    pos_payment_type.PayGroupId = paymenttypedata.PayGroupId;
                    pos_payment_type.PayTypeName = paymenttypedata.PayTypeName;
                    pos_payment_type.PayTypeCode = paymenttypedata.PayTypeCode;
                    pos_payment_type.DisplayName = paymenttypedata.DisplayName;
                    //pos_payment_type.isVat = paymenttypedata.isVat;
                    //pos_payment_type.isServiceCharge = paymenttypedata.isServiceCharge;
                    pos_payment_type.Ordering = paymenttypedata.Ordering;
                    pos_payment_type.Activate = paymenttypedata.Activate;

                    if (paymenttypedata.IconButtonServer != null && paymenttypedata.IconButtonServer != string.Empty)
                    {
                        string pathfile = ProcessImage(paymenttypedata.IconButtonServer);
                        string[] arraypath = pathfile.Split('/');
                        pos_payment_type.IconButtonServer = pathfile;
                        if (arraypath.Length > 0)
                            pos_payment_type.IconButton = arraypath[arraypath.Length - 1].ToString();
                    }

                    pos_payment_type.UpdatedBy = UserProfile.employee_id;
                    pos_payment_type.UpdatedDate = DateTime.Now;

                    db.Entry(pos_payment_type).State = EntityState.Modified;
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
            paymenttypedata.payment_group_list = db.pos_payment_group.Where(a => a.DeletedDate == null).ToList();
            return View(paymenttypedata);
        }

        // POST: paymenttype/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_payment_type pos_payment_type = db.pos_payment_type.Find(id);
            if (pos_payment_type != null)
            {
                pos_payment_type.DeletedBy = UserProfile.UserId;
                pos_payment_type.DeletedDate = DateTime.Now;
                db.Entry(pos_payment_type).State = EntityState.Modified;
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