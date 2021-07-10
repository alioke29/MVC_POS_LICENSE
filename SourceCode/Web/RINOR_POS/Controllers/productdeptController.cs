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
    public class productdeptController : Controller
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: Product
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            productDeptViewModel productDeptdata = (from pg in db.pos_product_group.Where(pd => pd.ProductGroupID == id)
                                                    join s in db.pos_shop_data on pg.ShopID equals s.ShopID

                                                    select new productDeptViewModel()
                                                    {
                                                        ShopID = s.ShopID,
                                                        ShopName = s.ShopName,
                                                        ProductGroupID = pg.ProductGroupID,
                                                        ProductGroupName = pg.ProductGroupName
                                                    }
                                                ).FirstOrDefault<productDeptViewModel>();

            return View(productDeptdata);
        }

        /// <summary>
        /// list for index
        /// </summary>
        /// <returns>JSON</returns>
        public JsonResult GetIndexList(int ProductGroupID)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var _qry = (from pg in db.pos_product_group.Where(pd => pd.ProductGroupID == ProductGroupID)
                        join s in db.pos_shop_data on pg.ShopID equals s.ShopID
                        join pd in db.pos_product_dept on pg.ProductGroupID equals pd.ProductGroupID
                        //into leftpd from pd in leftpd.DefaultIfEmpty()

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
                        }).ToList<productDeptViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: product/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            productDeptViewModel productDeptdata = (from a in db.pos_product_group.Where(a => a.ProductGroupID == id)
                                                    join c in db.pos_shop_data on a.ShopID equals c.ShopID
                                                    select new productDeptViewModel()
                                                    {
                                                        ShopID = a.ShopID,
                                                        ShopName = c.ShopName,
                                                        ProductGroupID = a.ProductGroupID,
                                                        ProductGroupName = a.ProductGroupName
                                                    }).FirstOrDefault<productDeptViewModel>();

            productDeptViewModel productDept = new productDeptViewModel();
            //productDept.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null).ToList();
            productDept.ShopID = productDeptdata.ShopID;
            productDept.ShopName = productDeptdata.ShopName;
            productDept.ProductGroupID = productDeptdata.ProductGroupID;
            productDept.ProductGroupName = productDeptdata.ProductGroupName;

            return View(productDept);
        }

        // POST: product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(productDeptViewModel productDeptdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_product_dept productDept = new pos_product_dept();

                    productDept.ShopID = productDeptdata.ShopID;
                    productDept.ProductGroupID = productDeptdata.ProductGroupID;
                    productDept.ProductDeptCode = productDeptdata.ProductDeptCode;
                    productDept.ProductDeptName = productDeptdata.ProductDeptName;
                    if (productDeptdata.ProductDeptActivate == null)
                        productDept.ProductDeptActivate = false;
                    else
                        productDept.ProductDeptActivate = productDeptdata.ProductDeptActivate;

                    if (productDeptdata.DisplayMobile == null)
                        productDept.DisplayMobile = false;
                    else
                        productDept.DisplayMobile = productDeptdata.DisplayMobile;

                    productDept.ProductDeptOrdering = productDeptdata.ProductDeptOrdering;

                    productDept.CreatedDate = DateTime.Now;
                    productDept.CreatedBy = UserProfile.UserId;

                    productDept = db.pos_product_dept.Add(productDept);
                    db.SaveChanges();

                    return RedirectToAction("Index", new {id = productDept.ProductGroupID});
                }

                //productGroupdata.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null).ToList();
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(productDeptdata);
        }

        // GET: product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            productDeptViewModel productDeptdata = (from a in db.pos_product_dept.Where(a => a.ProductDeptID == id)
                                                    join b in db.pos_product_group on a.ProductGroupID equals b.ProductGroupID
                                                    join c in db.pos_shop_data on a.ShopID equals c.ShopID
                                                    select new productDeptViewModel()
                                                    {
                                                        ProductDeptID = a.ProductDeptID,
                                                        ShopID = a.ShopID,
                                                        ShopName = c.ShopName,
                                                        ProductGroupID = a.ProductGroupID,
                                                        ProductGroupName = b.ProductGroupName,
                                                        ProductDeptCode = a.ProductDeptCode,
                                                        ProductDeptName = a.ProductDeptName,
                                                        ProductDeptActivate = a.ProductDeptActivate,
                                                        DisplayMobile = a.DisplayMobile,
                                                        ProductDeptOrdering = a.ProductDeptOrdering
                                                    }).FirstOrDefault<productDeptViewModel>();
            if (productDeptdata == null)
            {
                return HttpNotFound("Product Dept not found.");
            }

            productDeptViewModel productDept = new productDeptViewModel();
            //productDept.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null).ToList();
            productDept.ProductDeptID = productDeptdata.ProductDeptID;
            productDept.ShopID = productDeptdata.ShopID;
            productDept.ShopName = productDeptdata.ShopName;
            productDept.ProductGroupID = productDeptdata.ProductGroupID;
            productDept.ProductGroupName = productDeptdata.ProductGroupName;
            productDept.ProductDeptCode = productDeptdata.ProductDeptCode;
            productDept.ProductDeptName = productDeptdata.ProductDeptName;
            if (productDeptdata.ProductDeptActivate == null)
                productDept.ProductDeptActivate = false;
            else
                productDept.ProductDeptActivate = productDeptdata.ProductDeptActivate;

            if (productDeptdata.DisplayMobile == null)
                productDept.DisplayMobile = false;
            else
                productDept.DisplayMobile = productDeptdata.DisplayMobile;

            productDept.ProductDeptOrdering = productDeptdata.ProductDeptOrdering;

            return View(productDept);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(productDeptViewModel productDeptdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_product_dept productDept = db.pos_product_dept.Find(productDeptdata.ProductDeptID);
                    productDept.ShopID = productDeptdata.ShopID;
                    productDept.ProductGroupID = productDeptdata.ProductGroupID;
                    productDept.ProductDeptCode = productDeptdata.ProductDeptCode;
                    productDept.ProductDeptName = productDeptdata.ProductDeptName;
                    if (productDeptdata.ProductDeptActivate == null)
                        productDept.ProductDeptActivate = false;
                    else
                        productDept.ProductDeptActivate = productDeptdata.ProductDeptActivate;

                    if (productDeptdata.DisplayMobile == null)
                        productDept.DisplayMobile = false;
                    else
                        productDept.DisplayMobile = productDeptdata.DisplayMobile;

                    productDept.ProductDeptOrdering = productDeptdata.ProductDeptOrdering;

                    productDept.UpdatedDate = DateTime.Now;
                    productDept.UpdatedBy = UserProfile.UserId;

                    db.Entry(productDept).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", new { id = productDept.ProductGroupID });
                }

                //productGroupdata.shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null).ToList();
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(productDeptdata);
        }

        // GET: product/Edit/5
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
            pos_product_dept productDept = db.pos_product_dept.Find(id);
            if (productDept != null)
            {
                productDept.DeletedBy = UserProfile.UserId;
                productDept.DeletedDate = DateTime.Now;
                db.Entry(productDept).State = EntityState.Modified;
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