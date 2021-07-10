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
    public class PromoPackageController : Controller
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: PromoPackage
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var promotion_model = (from a in db.pos_promotion.Where(a => a.PromotionID == id)
                                   join b in db.pos_promotion_type on a.PromotionType equals b.PromotionTypeId
                                   select new promotionpackageViewModel()
                                   {
                                       PromotionID = a.PromotionID,
                                       PromotionName = a.PromotionName,
                                       PromotionTypeName = b.PromotionType,
                                       PromotionPrice = a.PromotionPrice,
                                       PromotionVAT = a.PromotionVAT,
                                       PromotionServiceCharge = a.PromotionServiceCharge,
                                       MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                                       SalemodeList = db.pos_sale_mode.Where(o => o.DeletedDate == null).ToList(),
                                   }).FirstOrDefault();

            promotion_model.ProductList = new List<pos_products>();
            promotion_model.IsActive = true;
            promotion_model.PromoPackageList = db.vw_promotion_package.Where(o => o.PromotionID == id).ToList();
            return View(promotion_model);
        }

        public JsonResult GetProductGroupList(int MasterShopId = 0)
        {
            var _ProductGroupList = db.pos_product_group.Where(o => o.ShopID == MasterShopId && o.DeletedDate == null && o.ProductGroupActivate == true).ToList();

            return Json(_ProductGroupList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductDeptList(int ProductGroupID = 0)
        {
            var _ProductDeptList = db.pos_product_dept.Where(o => o.ProductGroupID == ProductGroupID && o.DeletedDate == null && o.ProductDeptActivate == true).ToList();

            return Json(_ProductDeptList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// GetProductComboList
        /// </summary>
        /// <param name="ProductGroupID"></param>
        /// <returns></returns>
        public JsonResult GetProductComboList(int ProductGroupID = 0)
        {
            var _ProductComboList = db.pos_product_combo.Where(o => o.ProductGroupID == ProductGroupID && o.DeletedDate == null && o.Activate == true).ToList();

            return Json(_ProductComboList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// GetProductList
        /// </summary>
        /// <param name="ProductDeptID"></param>
        /// <returns></returns>
        public JsonResult GetProductList(int ProductDeptID = 0)
        {
            var _ProductList = db.pos_products.Where(o => o.ProductDeptID == ProductDeptID && o.DeletedDate == null && o.ProductActivate == true).ToList();

            return Json(_ProductList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(promotionpackageViewModel PromotionProdData)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        pos_promotion_package promo_products = db.pos_promotion_package.Where(o => o.PromotionID == PromotionProdData.PromotionID && o.ProductID == PromotionProdData.ProductID && o.SaleModeID == PromotionProdData.SaleModeID).FirstOrDefault();

                        bool isAddPrd = (promo_products == null);
                        if (isAddPrd)
                            promo_products = new pos_promotion_package();
                        else
                            promo_products = db.pos_promotion_package.Find(promo_products.PromotionPackageID);

                        promo_products.PromotionID = PromotionProdData.PromotionID;
                        promo_products.MasterShopID = PromotionProdData.MasterShopID;
                        promo_products.SaleModeID = PromotionProdData.SaleModeID;
                        promo_products.ProductGroupID = PromotionProdData.ProductGroupID;
                        promo_products.ProductDeptID = PromotionProdData.ProductDeptID;
                        promo_products.ProductID = PromotionProdData.ProductID;
                        promo_products.ProductComboID = PromotionProdData.ProductComboID;

                        if (isAddPrd)
                        {
                            promo_products.CreatedBy = UserProfile.employee_id;
                            promo_products.CreatedDate = DateTime.Now;
                            promo_products = db.pos_promotion_package.Add(promo_products);
                        }
                        else
                        {
                            promo_products.UpdatedBy = UserProfile.employee_id;
                            promo_products.UpdatedDate = DateTime.Now;
                            db.Entry(promo_products).State = EntityState.Modified;
                        }
                        db.SaveChanges();

                        transaction.Commit();
                    }
                    else
                    {
                        PromotionProdData.MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList();
                        PromotionProdData.SalemodeList = db.pos_sale_mode.Where(o => o.DeletedDate == null).ToList();
                        PromotionProdData.PromoPackageList = db.vw_promotion_package.Where(o => o.PromotionID == PromotionProdData.PromotionID).ToList();
                        return View(PromotionProdData);
                    }

                    //get data by changes dropdownlist
                    var promotion_model = (from a in db.pos_promotion.Where(a => a.PromotionID == PromotionProdData.PromotionID)
                                           join b in db.pos_promotion_type on a.PromotionType equals b.PromotionTypeId
                                           select new promotionpackageViewModel()
                                           {
                                               PromotionID = a.PromotionID,
                                               PromotionName = a.PromotionName,
                                               PromotionTypeName = b.PromotionType,
                                               PromotionPrice = a.PromotionPrice,
                                               PromotionVAT = a.PromotionVAT,
                                               PromotionServiceCharge = a.PromotionServiceCharge,
                                               MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                                               SalemodeList = db.pos_sale_mode.Where(o => o.DeletedDate == null).ToList(),
                                           }).FirstOrDefault();

                    promotion_model.ProductList = new List<pos_products>();
                    promotion_model.product_selected = new List<string>();
                    promotion_model.ProductComboList = new List<pos_product_combo>();
                    promotion_model.productCombo_selected = new List<string>();
                    promotion_model.IsActive = true;
                    promotion_model.PromoPackageList = db.vw_promotion_package.Where(o => o.PromotionID == PromotionProdData.PromotionID).ToList();

                    return View(promotion_model);
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
            return View(PromotionProdData);
        }

        [HttpGet]
        public JsonResult DeletePromoByID(int id)
        {
            pos_promotion_package pos_promotion = db.pos_promotion_package.Find(id);
            if (pos_promotion != null)
            {
                db.pos_promotion_package.Remove(pos_promotion);
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