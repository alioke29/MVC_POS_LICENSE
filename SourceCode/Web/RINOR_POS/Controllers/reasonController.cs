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
    public class reasonController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();
        // GET: Reason
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

            var _qry = (from a in db.pos_reason_group.Where(a => a.DeletedDate == null)
                        join b in db.pos_shop_data on a.ShopID equals b.ShopID
                        select new reasongroupViewModel
                        {
                            ReasonGroupID = a.ReasonGroupID,
                            ReasonGroupName = a.ReasonGroupName,
                            AllowedInput = a.AllowedInput,
                            isRequiredAmount = a.isRequiredAmount,
                            Activate = a.Activate,
                            ShopName = b.ShopName
                        }).ToList<reasongroupViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetShopList
        /// </summary>
        /// <param name="MasterShopID"></param>
        /// <returns></returns>
        public JsonResult GetShopList(int MasterShopID = 0)
        {
            var _ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == MasterShopID && o.DeletedDate == null).ToList();

            return Json(_ShopList, JsonRequestBehavior.AllowGet);
        }

        /// GET: reasongroup/Create
        public ActionResult Create(int? id)
        {
            reasongroupViewModel Reasonview = new reasongroupViewModel()
            {
                Activate = false,
                isRequiredAmount = false,
                AllowedInput = false,
                MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList()
            };
            return View(Reasonview);
        }

        // POST: crud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(reasongroupViewModel reasongroup_data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_reason_group pos_reason_group = new pos_reason_group();
                    pos_reason_group.ShopID = reasongroup_data.ShopID;
                    pos_reason_group.ReasonGroupName = reasongroup_data.ReasonGroupName;
                    pos_reason_group.isRequiredAmount = reasongroup_data.isRequiredAmount;
                    pos_reason_group.Activate = reasongroup_data.Activate;
                    pos_reason_group.AllowedInput = reasongroup_data.AllowedInput;
                    pos_reason_group.CreatedDate = DateTime.Now;
                    pos_reason_group.CreatedBy = UserProfile.UserId;

                    pos_reason_group = db.pos_reason_group.Add(pos_reason_group);
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
            reasongroup_data.MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList();
            return View(reasongroup_data);
        }

        /// GET: crud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_reason_group reasongroup_data = db.pos_reason_group.Find(id);
            if (reasongroup_data == null)
            {
                return HttpNotFound("Product VAT not found.");
            }
            pos_shop_data shop = db.pos_shop_data.Find(reasongroup_data.ShopID);
            reasongroupViewModel reasongroup_model = new reasongroupViewModel()
            {
                MasterShopID = Convert.ToInt32(shop.MasterShopLink),
                ShopID = reasongroup_data.ShopID,
                ReasonGroupID = reasongroup_data.ReasonGroupID,
                ReasonGroupName = reasongroup_data.ReasonGroupName,
                isRequiredAmount = reasongroup_data.isRequiredAmount,
                Activate = reasongroup_data.Activate,
                AllowedInput = reasongroup_data.AllowedInput,
                MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == shop.MasterShopLink && o.DeletedDate == null).ToList()
            };

            return View(reasongroup_model);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(reasongroupViewModel reasongroup_data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_reason_group pos_reason_group = db.pos_reason_group.Find(reasongroup_data.ReasonGroupID);
                    pos_reason_group.ShopID = reasongroup_data.ShopID;
                    pos_reason_group.ReasonGroupName = reasongroup_data.ReasonGroupName;
                    pos_reason_group.isRequiredAmount = reasongroup_data.isRequiredAmount;
                    pos_reason_group.Activate = reasongroup_data.Activate;
                    pos_reason_group.AllowedInput = reasongroup_data.AllowedInput;
                    pos_reason_group.UpdatedDate = DateTime.Now;
                    pos_reason_group.UpdatedBy = UserProfile.UserId;

                    db.Entry(pos_reason_group).State = EntityState.Modified;
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
            reasongroup_data.MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList();
            return View(reasongroup_data);
        }

        // POST: crud/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_reason_group pos_reason_group = db.pos_reason_group.Find(id);
            if (pos_reason_group != null)
            {
                pos_reason_group.DeletedBy = UserProfile.UserId;
                pos_reason_group.DeletedDate = DateTime.Now;
                db.Entry(pos_reason_group).State = EntityState.Modified;
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        #region Reason Detail
        /// <summary>
        /// ReasonDetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ReasonDetail(int id = 0)
        {
            var _qry = (from a in db.pos_reason_group.Where(a => a.DeletedDate == null && a.ReasonGroupID == id)
                        join c in db.pos_shop_data on a.ShopID equals c.ShopID
                        select new reasondetailViewModel
                        {
                            ReasonGroupID = a.ReasonGroupID,
                            ReasonGroupName = a.ReasonGroupName,
                            ShopID = a.ShopID,
                            ShopName = c.ShopName
                        }).FirstOrDefault<reasondetailViewModel>();

            return View(_qry);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetReasonDetailList(int id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var _qry = (from a in db.pos_reason_detail.Where(a => a.DeletedDate == null && a.ReasonGroupID == id)
                        join b in db.pos_reason_group.Where(b => b.DeletedDate == null) on a.ReasonGroupID equals b.ReasonGroupID
                        select new reasondetailViewModel
                        {
                            ReasonGroupID = a.ReasonGroupID,
                            ReasonGroupName = b.ReasonGroupName,
                            ReasonGroupDetailID = a.ReasonGroupDetailID,
                            ReasonText = a.ReasonText,
                            Ordering = a.Ordering,
                            Activate = a.Activate
                        }).ToList<reasondetailViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        /// GET: reason/dataeditdetail/5
        public JsonResult GetDataEdit(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var reasondetail = db.pos_reason_detail.Find(id);

            return Json(reasondetail, JsonRequestBehavior.AllowGet);
        }

        /// POST: crud/Edit
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult EditDetail(pos_reason_detail data)
        {
            pos_reason_detail pos_reason_detail = db.pos_reason_detail.Find(data.ReasonGroupDetailID);
            if (pos_reason_detail != null)
            {
                pos_reason_detail.ShopID = data.ShopID;
                pos_reason_detail.ReasonGroupID = data.ReasonGroupID;
                pos_reason_detail.ReasonText = data.ReasonText;
                pos_reason_detail.Ordering = data.Ordering;
                pos_reason_detail.Activate = data.Activate;
                pos_reason_detail.UpdatedBy = UserProfile.employee_id;
                pos_reason_detail.UpdatedDate = DateTime.Now;
                db.Entry(pos_reason_detail).State = EntityState.Modified;
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                pos_reason_detail reasondetail = new pos_reason_detail();
                reasondetail.ShopID = data.ShopID;
                reasondetail.ReasonGroupID = data.ReasonGroupID;
                reasondetail.ReasonText = data.ReasonText;
                reasondetail.Ordering = data.Ordering;
                reasondetail.Activate = data.Activate;
                reasondetail.CreatedBy = UserProfile.employee_id;
                reasondetail.CreatedDate = DateTime.Now;
                db.pos_reason_detail.Add(reasondetail);
                db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }

        /// POST: Reason/DeleteDetail/5
        [HttpPost]
        public JsonResult DeleteDetail(int id)
        {
            pos_reason_detail pos_reason_detail = db.pos_reason_detail.Find(id);
            if (pos_reason_detail != null)
            {
                pos_reason_detail.DeletedBy = UserProfile.employee_id;
                pos_reason_detail.DeletedDate = DateTime.Now;
                db.Entry(pos_reason_detail).State = EntityState.Modified;
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}