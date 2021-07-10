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
    public class productController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: Product
        public ActionResult Index()
        {
            productViewModel ProductGroup = new productViewModel()
            {
                shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShop == true).ToList()
            };

            return View(ProductGroup);
        }

        /// <summary>
        /// list for index
        /// </summary>
        /// <returns>JSON</returns>
        public JsonResult GetIndexList(int ShopID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ShopID != 0)
            {
                var _qry = (from a in db.pos_product_group.Where(a => a.DeletedDate == null && a.ShopID == ShopID)
                            join b in db.pos_shop_data on a.ShopID equals b.ShopID
                            select new productViewModel
                            {
                                ProductGroupID = a.ProductGroupID,
                                ShopID = a.ShopID,
                                ShopName = b.ShopName,
                                ProductGroupCode = a.ProductGroupCode,
                                ProductGroupName = a.ProductGroupName,
                                ProductGroupActivate = a.ProductGroupActivate
                            }).ToList<productViewModel>();

                return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var _qry = (from a in db.pos_product_group.Where(a => a.DeletedDate == null)
                            join b in db.pos_shop_data on a.ShopID equals b.ShopID
                            select new productViewModel
                            {
                                ProductGroupID = a.ProductGroupID,
                                ShopID = a.ShopID,
                                ShopName = b.ShopName,
                                ProductGroupCode = a.ProductGroupCode,
                                ProductGroupName = a.ProductGroupName,
                                ProductGroupActivate = a.ProductGroupActivate
                            }).ToList<productViewModel>();

                return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: product/Create
        public ActionResult Create(int? id)
        {
            productViewModel ProductGroup = new productViewModel()
            {
                shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShop == true).ToList()
            };

            return View(ProductGroup);
        }

        // POST: product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(productViewModel productGroupdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_product_group productGroup = new pos_product_group();

                    productGroup.ShopID = productGroupdata.ShopID;
                    productGroup.ProductGroupCode = productGroupdata.ProductGroupCode;
                    productGroup.ProductGroupName = productGroupdata.ProductGroupName;
                    if (productGroupdata.ProductGroupActivate == null)
                        productGroup.ProductGroupActivate = false;
                    else
                        productGroup.ProductGroupActivate = productGroupdata.ProductGroupActivate;

                    if (productGroupdata.DisplayMobile == null)
                        productGroup.DisplayMobile = false;
                    else
                        productGroup.DisplayMobile = productGroupdata.DisplayMobile;

                    if (productGroupdata.IsComment == null)
                        productGroup.IsComment = false;
                    else
                        productGroup.IsComment = productGroupdata.IsComment;

                    productGroup.CreatedDate = DateTime.Now;
                    productGroup.CreatedBy = UserProfile.UserId;

                    productGroup = db.pos_product_group.Add(productGroup);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                productGroupdata.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShop == true).ToList();
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(productGroupdata);
        }

        // GET: product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_product_group productGroupdata = db.pos_product_group.Find(id);
            if (productGroupdata == null)
            {
                return HttpNotFound("Product Group not found.");
            }

            productViewModel productGroup = new productViewModel();
            productGroup.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShop == true).ToList();
            productGroup.ShopID = productGroupdata.ShopID;
            productGroup.ProductGroupID = productGroupdata.ProductGroupID;
            productGroup.ProductGroupCode = productGroupdata.ProductGroupCode;
            productGroup.ProductGroupName = productGroupdata.ProductGroupName;
            if (productGroupdata.ProductGroupActivate == null)
                productGroup.ProductGroupActivate = false;
            else
                productGroup.ProductGroupActivate = productGroupdata.ProductGroupActivate;

            if (productGroupdata.DisplayMobile == null)
                productGroup.DisplayMobile = false;
            else
                productGroup.DisplayMobile = productGroupdata.DisplayMobile;

            if (productGroupdata.IsComment == null)
                productGroup.IsComment = false;
            else
                productGroup.IsComment = productGroupdata.IsComment;

            return View(productGroup);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(productViewModel productGroupdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_product_group productGroup = db.pos_product_group.Find(productGroupdata.ProductGroupID);
                    productGroup.ShopID = productGroupdata.ShopID;
                    productGroup.ProductGroupCode = productGroupdata.ProductGroupCode;
                    productGroup.ProductGroupName = productGroupdata.ProductGroupName;
                    if (productGroupdata.ProductGroupActivate == null)
                        productGroup.ProductGroupActivate = false;
                    else
                        productGroup.ProductGroupActivate = productGroupdata.ProductGroupActivate;

                    if (productGroupdata.DisplayMobile == null)
                        productGroup.DisplayMobile = false;
                    else
                        productGroup.DisplayMobile = productGroupdata.DisplayMobile;

                    if (productGroupdata.IsComment == null)
                        productGroup.IsComment = false;
                    else
                        productGroup.IsComment = productGroupdata.IsComment;

                    productGroup.UpdatedDate = DateTime.Now;
                    productGroup.UpdatedBy = UserProfile.UserId;

                    db.Entry(productGroup).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                productGroupdata.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null && o.MasterShop == true).ToList();
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(productGroupdata);
        }

        // GET: product/Department/5
        public ActionResult Department(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            productDeptViewModel productDeptdata = (from pd in db.pos_product_dept.Where(pd => pd.ProductGroupID == id)
                                                    join s in db.pos_shop_data on pd.ShopID equals s.ShopID
                                                    join pg in db.pos_product_group on pd.ProductGroupID equals pg.ProductGroupID

                                                    select new productDeptViewModel()
                                                    {
                                                        ProductDeptID = pd.ProductDeptID,
                                                        ShopID = s.ShopID,
                                                        ShopName = s.ShopName,
                                                        ProductGroupID = pg.ProductGroupID,
                                                        ProductGroupName = pg.ProductGroupName,
                                                        ProductDeptCode = pd.ProductDeptCode,
                                                        ProductDeptName = pd.ProductDeptName,
                                                        ProductDeptActivate = pd.ProductDeptActivate,
                                                        ProductDeptOrdering = pd.ProductDeptOrdering
                                                    }
                                                ).FirstOrDefault<productDeptViewModel>();
            if (productDeptdata == null)
            {
                return HttpNotFound("Product department not found.");
            }

            return View(productDeptdata);
        }

        // POST: crud/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_product_group productGroup = db.pos_product_group.Find(id);
            if (productGroup != null)
            {
                productGroup.DeletedBy = UserProfile.UserId;
                productGroup.DeletedDate = DateTime.Now;
                db.Entry(productGroup).State = EntityState.Modified;
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