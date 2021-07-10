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
    public class PromoBuyGetController : Controller
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: PromoBuyGet
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var promotion_model = (from a in db.pos_promotion.Where(a => a.PromotionID == id)
                                   join b in db.pos_promotion_type on a.PromotionType equals b.PromotionTypeId
                                   select new promotionbuygetViewModel()
                                   {
                                       PromotionID = a.PromotionID,
                                       PromotionName = a.PromotionName,
                                       PromotionTypeName = b.PromotionType,
                                       MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                                       SalemodeList = db.pos_sale_mode.Where(o => o.DeletedDate == null).ToList(),
                                   }).FirstOrDefault();

            promotion_model.ProductList = new List<pos_products>();
            promotion_model.BuyQty = 1;
            promotion_model.GetQty = 1;
            promotion_model.isActive = true;
            promotion_model.PromoBuyGetList = db.vw_promotion_buyget.Where(o => o.PromotionID == id).ToList();
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

        public JsonResult GetProductList(int ProductDeptID = 0)
        {
            var _ProductList = db.pos_products.Where(o => o.ProductDeptID == ProductDeptID && o.DeletedDate == null && o.ProductActivate == true).ToList();

            return Json(_ProductList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(promotionbuygetViewModel PromotionProdData)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {

                    if (PromotionProdData.sale_mode_selected.Count == 0)
                    {
                        ModelState.AddModelError("BuySaleModeID", "Sale Mode is Mandatory.");
                    }
                    if (PromotionProdData.product_selected.Count == 0)
                    {
                        ModelState.AddModelError("BuyProductID", "Product is Mandatory.");
                    }

                    if (ModelState.IsValid)
                    {
                        foreach (string salemodeid in PromotionProdData.sale_mode_selected)
                        {
                            if (salemodeid != "")
                            {
                                int SalemodeID_INT = Convert.ToInt32(salemodeid);
                                pos_promotion_buy_get productdiscount = new pos_promotion_buy_get();

                                foreach (string prod in PromotionProdData.product_selected)
                                {
                                    int prodID = Convert.ToInt32(prod);
                                    pos_promotion_buy_get promo_products = db.pos_promotion_buy_get.Where(o => o.PromotionID == PromotionProdData.PromotionID && o.BuyProductID == prodID && o.BuySaleModeID == SalemodeID_INT).FirstOrDefault();

                                    bool isAddPrd = (promo_products == null);
                                    if (isAddPrd)
                                        promo_products = new pos_promotion_buy_get();
                                    else
                                        promo_products = db.pos_promotion_buy_get.Find(promo_products.PromotionBuyGetID);

                                    promo_products.PromotionID = PromotionProdData.PromotionID;
                                    promo_products.BuyQty = PromotionProdData.BuyQty;
                                    promo_products.BuyMasterShopID = PromotionProdData.BuyMasterShopID;
                                    promo_products.BuyProductGroupID = PromotionProdData.BuyProductGroupID;
                                    promo_products.BuyProductDepthID = PromotionProdData.BuyProductDepthID;
                                    promo_products.BuyProductID = prodID;
                                    promo_products.BuySaleModeID = SalemodeID_INT;
                                    promo_products.GetQty = PromotionProdData.GetQty;
                                    promo_products.GetMasterShopID = PromotionProdData.GetMasterShopID;
                                    promo_products.GetSaleModeID = PromotionProdData.GetSaleModeID;
                                    promo_products.GetProductGroupID = PromotionProdData.GetProductGroupID;
                                    promo_products.GetProductDeptID = PromotionProdData.GetProductDeptID;
                                    promo_products.GetProductID = PromotionProdData.GetProductID;

                                    if (isAddPrd)
                                    {
                                        promo_products.CreatedBy = UserProfile.employee_id;
                                        promo_products.CreatedDate = DateTime.Now;
                                        promo_products = db.pos_promotion_buy_get.Add(promo_products);
                                    }
                                    else
                                    {
                                        promo_products.UpdateBy = UserProfile.employee_id;
                                        promo_products.UpdatedDate = DateTime.Now;
                                        db.Entry(promo_products).State = EntityState.Modified;
                                    }
                                    db.SaveChanges();
                                }

                            }
                        }
                        transaction.Commit();
                    }
                    else
                    {
                        PromotionProdData.MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList();
                        PromotionProdData.SalemodeList = db.pos_sale_mode.Where(o => o.DeletedDate == null).ToList();
                        PromotionProdData.PromoBuyGetList = db.vw_promotion_buyget.Where(o => o.PromotionID == PromotionProdData.PromotionID).ToList();
                        return View(PromotionProdData);
                    }

                    //get data by changes dropdownlist
                    var promotion_model = (from a in db.pos_promotion.Where(a => a.PromotionID == PromotionProdData.PromotionID)
                                           join b in db.pos_promotion_type on a.PromotionType equals b.PromotionTypeId
                                           select new promotionbuygetViewModel()
                                           {
                                               PromotionID = a.PromotionID,
                                               PromotionName = a.PromotionName,
                                               PromotionTypeName = b.PromotionType,
                                               MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                                               SalemodeList = db.pos_sale_mode.Where(o => o.DeletedDate == null).ToList(),
                                           }).FirstOrDefault();

                    promotion_model.ProductList = new List<pos_products>();
                    promotion_model.product_selected = new List<string>();
                    promotion_model.sale_mode_selected = new List<string>();
                    promotion_model.BuyQty = 1;
                    promotion_model.GetQty = 1;
                    promotion_model.isActive = true;
                    promotion_model.PromoBuyGetList = db.vw_promotion_buyget.Where(o => o.PromotionID == PromotionProdData.PromotionID).ToList();

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
            pos_promotion_buy_get pos_promotion = db.pos_promotion_buy_get.Find(id);
            if (pos_promotion != null)
            {
                db.pos_promotion_buy_get.Remove(pos_promotion);
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