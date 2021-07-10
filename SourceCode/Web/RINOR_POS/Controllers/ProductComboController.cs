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
    /// <summary>
    /// Controller for Product Combo
    /// </summary>
    [Authorize]
    public class ProductComboController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();
        /// GET: ProductCombo
        public ActionResult Index(int id)
        {
            var productCombo = db.vw_product_combo_list.Where(a => a.ProductGroupID == id).FirstOrDefault<vw_product_combo_list>();

            return View(productCombo);
        }

        /// <summary>
        /// list for index
        /// </summary>
        /// <returns>JSON</returns>
        public JsonResult GetIndexList(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var productCombo = db.vw_product_combo_list.Where(a => a.ProductGroupID == id && a.ProductComboID != null).ToList<vw_product_combo_list>();

            return Json(new { data = productCombo }, JsonRequestBehavior.AllowGet);
        }

        /// GET: combo/Create
        public ActionResult Create(int? id)
        {
            var productCombo = (from a in db.pos_product_group.Where(a => a.ProductGroupID == id)
                                join b in db.pos_shop_data on a.ShopID equals b.ShopID
                                select new productComboViewModel
                                {
                                    ProductGroupID = a.ProductGroupID,
                                    ProductGroupName = a.ProductGroupName,
                                    ShopID = a.ShopID,
                                    ShopName = b.ShopName
                                }).FirstOrDefault();

            return View(productCombo);
        }

        /// POST: crud/Create
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(productComboViewModel combo_data)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        pos_product_combo pos_combo = new pos_product_combo();
                        pos_combo.ProductComboName = combo_data.ProductComboName;
                        pos_combo.ShopID = combo_data.ShopID;
                        pos_combo.ProductGroupID = combo_data.ProductGroupID;
                        pos_combo.Activate = true;

                        pos_combo.CreatedDate = DateTime.Now;
                        pos_combo.CreatedBy = UserProfile.UserId;

                        pos_combo = db.pos_product_combo.Add(pos_combo);
                        db.SaveChanges();

                        transaction.Commit();
                        return RedirectToAction("Index", new { id = combo_data.ProductGroupID });
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                    if (ex.InnerException != null)
                        msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                    ModelState.AddModelError("", msgErr);
                }
            }
            combo_data.ProductDeptList = new List<pos_product_dept>();
            combo_data.ProductList = new List<pos_products>();
            combo_data.product_selected = new List<string>();
            return View(combo_data);
        }

        /// GET: crud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var combo_data = db.vw_product_combo_list.Where(a => a.ProductComboID == id).FirstOrDefault();
            if (combo_data == null)
            {
                return HttpNotFound("combo not found.");
            }

            return View(combo_data);
        }

        /// POST: crud/Edit
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(vw_product_combo_list combo_data)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        pos_product_combo pos_combo = db.pos_product_combo.Find(combo_data.ProductComboID);
                        pos_combo.ProductComboName = combo_data.ProductComboName;
                        pos_combo.ShopID = combo_data.ShopID;
                        pos_combo.Activate = combo_data.Activate;

                        pos_combo.UpdatedDate = DateTime.Now;
                        pos_combo.UpdatedBy = UserProfile.UserId;

                        db.Entry(pos_combo).State = EntityState.Modified;
                        db.SaveChanges();

                        transaction.Commit();
                        return RedirectToAction("Index", new { id = combo_data.ProductGroupID });
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                    if (ex.InnerException != null)
                        msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                    ModelState.AddModelError("", msgErr);
                }
            }
            return View(combo_data);
        }

        /// <summary>
        /// Activate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Activate(int id)
        {
            pos_product_combo combo = db.pos_product_combo.Find(id);
            if (combo != null)
            {
                combo.Activate = !combo.Activate;
                combo.UpdatedBy = UserProfile.employee_id;
                combo.UpdatedDate = DateTime.Now;
                db.Entry(combo).State = EntityState.Modified;
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        /// POST: crud/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_product_combo pos_combo = db.pos_product_combo.Find(id);
            if (pos_combo != null)
            {
                pos_combo.DeletedBy = UserProfile.UserId;
                pos_combo.DeletedDate = DateTime.Now;
                db.Entry(pos_combo).State = EntityState.Modified;
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        #region Product Combo Link
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ComboLink(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = (from b in db.pos_product_combo
                         where b.ProductComboID == id
                         join c in db.pos_product_group on b.ProductGroupID equals c.ProductGroupID
                         join d in db.pos_shop_data on b.ShopID equals d.ShopID
                         select new productComboViewModel()
                         {
                             ProductComboID = b.ProductComboID,
                             ProductComboName = b.ProductComboName,
                             ProductGroupID = c.ProductGroupID,
                             ProductGroupName = c.ProductGroupName,
                             ShopID = d.ShopID,
                             ShopName = d.ShopName
                         }).FirstOrDefault();

            model.ProductDeptList = new List<pos_product_dept>();
            model.ProductList = new List<pos_products>();
            model.product_selected = new List<string>();
            model.ProductComboList = db.vw_product_combo_link.Where(a => a.ProductComboID == id).ToList<vw_product_combo_link>();
            return View(model);
        }

        /// POST: crud/ComboLink
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ComboLink(productComboViewModel combo_data)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        foreach (string productid in combo_data.product_selected)
                        {
                            if (productid != "")
                            {
                                int prodID_INT = Convert.ToInt32(productid);

                                pos_product_combo_link combolink = db.pos_product_combo_link.Where(o => o.ProductComboID == combo_data.ProductComboID && o.ProductID == prodID_INT).FirstOrDefault();

                                if (combolink == null)
                                {
                                    combolink = new pos_product_combo_link();
                                    combolink.ProductComboID = combo_data.ProductComboID;
                                    combolink.ProductID = prodID_INT;
                                    combolink = db.pos_product_combo_link.Add(combolink);

                                    db.SaveChanges();
                                }
                            }
                        }
                        transaction.Commit();
                        return RedirectToAction("Index", new { id = combo_data.ProductGroupID });
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                    if (ex.InnerException != null)
                        msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                    ModelState.AddModelError("", msgErr);
                }
            }
            combo_data.ProductDeptList = new List<pos_product_dept>();
            combo_data.ProductList = new List<pos_products>();
            combo_data.product_selected = new List<string>();
            combo_data.ProductComboList = db.vw_product_combo_link.Where(a => a.ProductComboID == combo_data.ProductComboID).ToList<vw_product_combo_link>();
            return View(combo_data);
        }
        /// <summary>
        /// Product Dept By Group ID
        /// </summary>
        /// <param name="ProductGroupID"></param>
        /// <returns></returns>
        public JsonResult GetProductDeptList(int ProductGroupID = 0)
        {
            var _ProductDeptList = db.pos_product_dept.Where(o => o.ProductGroupID == ProductGroupID && o.DeletedDate == null && o.ProductDeptActivate == true).ToList();

            return Json(_ProductDeptList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Product List
        /// </summary>
        /// <param name="ProductDeptID"></param>
        /// <returns></returns>
        public JsonResult GetProductList(int ProductDeptID = 0)
        {
            var _ProductList = db.pos_products.Where(o => o.ProductDeptID == ProductDeptID && o.DeletedDate == null && o.ProductActivate == true).ToList();

            return Json(_ProductList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Delete Combolink by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult DeleteComboLinkByID(int id)
        {
            pos_product_combo_link pos_combolink = db.pos_product_combo_link.Find(id);
            if (pos_combolink != null)
            {
                db.pos_product_combo_link.Remove(pos_combolink);
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