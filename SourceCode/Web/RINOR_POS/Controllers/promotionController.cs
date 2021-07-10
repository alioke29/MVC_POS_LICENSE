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
    public class promotionController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        #region Promotion
        // GET: promotion
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

            var _qry = (from a in db.pos_promotion.Where(a => a.DeletedDate == null)
                        join b in db.pos_promotion_type.Where(b => b.IsActive == true) on a.PromotionType equals b.PromotionTypeId
                        select new promotionViewModel
                        {
                            PromotionID = a.PromotionID,
                            PromotionName = a.PromotionName,
                            PromotionCode = a.PromotionCode,
                            PromotionType = a.PromotionType,
                            PromotionTypeName = b.PromotionType,
                            IsActive = a.IsActive,
                            IsAutomation = a.IsAutomation,
                            ExpiredDate = a.ExpiredDate
                        }).ToList<promotionViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        /// GET: promotion/Create
        public ActionResult Create(int? id)
        {
            promotionViewModel promoview = new promotionViewModel()
            {
                BeginDate = DateTime.Today,
                ExpiredDate = DateTime.Today,
                TimeStart = TimeSpan.Zero,
                TimeFinish = TimeSpan.Zero,
                PeriodCondition = 0,
                PromotionPrice = 0,
                PromotionVAT = 0,
                PromotionServiceCharge = 0,
                day_selected = new List<string>(),
                PromotionTypeList = db.pos_promotion_type.Where(a => a.IsActive == true).ToList(),
                IsActive = true,
                IsAutomation = false,
            };
            return View(promoview);
        }



        /// POST: crud/Create
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(promotionViewModel promotion_data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_promotion pos_promotion = new pos_promotion();
                    pos_promotion.PromotionName = promotion_data.PromotionName;
                    pos_promotion.PromotionCode = promotion_data.PromotionCode;
                    pos_promotion.PromotionType = promotion_data.PromotionType;
                    pos_promotion.BeginDate = promotion_data.BeginDate;
                    pos_promotion.ExpiredDate = promotion_data.ExpiredDate;
                    pos_promotion.TimeStart = promotion_data.TimeStart;
                    pos_promotion.TimeFinish = promotion_data.TimeFinish;
                    pos_promotion.EffectTo = promotion_data.EffectTo;
                    pos_promotion.PromotionPrice = promotion_data.PromotionPrice;
                    pos_promotion.PromotionVAT = promotion_data.PromotionVAT;
                    pos_promotion.PromotionServiceCharge = promotion_data.PromotionServiceCharge;
                    pos_promotion.NoPrintCopy = promotion_data.NoPrintCopy;
                    pos_promotion.Priority = promotion_data.Priority;

                    if (promotion_data.PeriodCondition == 0)
                    {
                        pos_promotion.WeeklyCondition = "";
                        pos_promotion.DayCondition = "";
                    }
                    else if (promotion_data.PeriodCondition == 1)
                    {
                        //enable weekly
                        string enableweekly = string.Empty;
                        if (promotion_data.Sunday)
                            enableweekly += "Sunday,";
                        if (promotion_data.Monday)
                            enableweekly += "Monday,";
                        if (promotion_data.Tuesday)
                            enableweekly += "Tuesday,";
                        if (promotion_data.Wednesday)
                            enableweekly += "Wednesday,";
                        if (promotion_data.Thursday)
                            enableweekly += "Thursday,";
                        if (promotion_data.Friday)
                            enableweekly += "Friday,";
                        if (promotion_data.Saturday)
                            enableweekly += "Saturday,";
                        pos_promotion.WeeklyCondition = enableweekly;
                        pos_promotion.DayCondition = "";
                    }
                    else if (promotion_data.PeriodCondition == 2)
                    {
                        string enableday = string.Empty;
                        foreach (string day in promotion_data.day_selected)
                        {
                            enableday += "," + day;
                        }
                        enableday += ",";
                        pos_promotion.WeeklyCondition = "";
                        pos_promotion.DayCondition = enableday;
                    }
                    pos_promotion.PrintSignature = promotion_data.PrintSignature;

                    pos_promotion.IsActive = promotion_data.IsActive;
                    if (promotion_data.IsAutomation == null)
                        pos_promotion.IsAutomation = false;
                    else
                        pos_promotion.IsAutomation = promotion_data.IsAutomation;
                    pos_promotion.CreatedDate = DateTime.Now;
                    pos_promotion.CreatedBy = UserProfile.UserId;

                    pos_promotion = db.pos_promotion.Add(pos_promotion);
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
            promotion_data.PromotionTypeList = db.pos_promotion_type.Where(a => a.IsActive == true).ToList();
            return View(promotion_data);
        }

        /// GET: crud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_promotion promotion_data = db.pos_promotion.Find(id);
            if (promotion_data == null)
            {
                return HttpNotFound("Promotion not found.");
            }

            promotionViewModel promotion_model = new promotionViewModel()
            {
                PromotionID = promotion_data.PromotionID,
                PromotionName = promotion_data.PromotionName,
                PromotionCode = promotion_data.PromotionCode,
                PromotionTypeList = db.pos_promotion_type.Where(a => a.IsActive == true).ToList(),
                PromotionType = promotion_data.PromotionType,
                BeginDate = promotion_data.BeginDate,
                ExpiredDate = promotion_data.ExpiredDate,
                TimeStart = promotion_data.TimeStart,
                TimeFinish = promotion_data.TimeFinish,
                EffectTo = promotion_data.EffectTo,
                PromotionPrice = promotion_data.PromotionPrice,
                PromotionVAT = promotion_data.PromotionVAT,
                PromotionServiceCharge = promotion_data.PromotionServiceCharge,
                NoPrintCopy = promotion_data.NoPrintCopy,
                Priority = promotion_data.Priority,
                PeriodCondition = 0,
                WeeklyCondition = promotion_data.WeeklyCondition,
                DayCondition = promotion_data.DayCondition,
                PrintSignature = promotion_data.PrintSignature,
                IsActive = promotion_data.IsActive,
                IsAutomation = promotion_data.IsAutomation
            };
            if (promotion_data.WeeklyCondition != string.Empty && promotion_data.WeeklyCondition != null)
            {
                promotion_model.PeriodCondition = 1;
                promotion_model.Sunday = (promotion_data.WeeklyCondition.Contains("Sunday"));
                promotion_model.Monday = (promotion_data.WeeklyCondition.Contains("Monday"));
                promotion_model.Tuesday = (promotion_data.WeeklyCondition.Contains("Tuesday"));
                promotion_model.Wednesday = (promotion_data.WeeklyCondition.Contains("Wednesday"));
                promotion_model.Thursday = (promotion_data.WeeklyCondition.Contains("Thursday"));
                promotion_model.Friday = (promotion_data.WeeklyCondition.Contains("Friday"));
                promotion_model.Saturday = (promotion_data.WeeklyCondition.Contains("Saturday"));
            }
            if (promotion_data.DayCondition != string.Empty && promotion_data.DayCondition != null)
            {
                promotion_model.PeriodCondition = 2;
                string[] days = promotion_data.DayCondition.Split(',');
                promotion_model.day_selected = new List<string>();
                foreach (string day in days)
                {
                    if (day.Trim() != string.Empty)
                    {
                        promotion_model.day_selected.Add(day);
                    }
                }
            }
            return View(promotion_model);
        }

        /// POST: crud/Edit
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(promotionViewModel promotion_data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_promotion pos_promotion = db.pos_promotion.Find(promotion_data.PromotionID);
                    pos_promotion.PromotionName = promotion_data.PromotionName;
                    pos_promotion.PromotionCode = promotion_data.PromotionCode;
                    pos_promotion.PromotionType = promotion_data.PromotionType;
                    pos_promotion.BeginDate = promotion_data.BeginDate;
                    pos_promotion.ExpiredDate = promotion_data.ExpiredDate;
                    pos_promotion.TimeStart = promotion_data.TimeStart;
                    pos_promotion.TimeFinish = promotion_data.TimeFinish;
                    pos_promotion.EffectTo = promotion_data.EffectTo;
                    pos_promotion.PromotionPrice = promotion_data.PromotionPrice;
                    pos_promotion.PromotionVAT = promotion_data.PromotionVAT;
                    pos_promotion.PromotionServiceCharge = promotion_data.PromotionServiceCharge;

                    pos_promotion.NoPrintCopy = promotion_data.NoPrintCopy;
                    pos_promotion.Priority = promotion_data.Priority;

                    if (promotion_data.PeriodCondition == 0)
                    {
                        pos_promotion.WeeklyCondition = "";
                        pos_promotion.DayCondition = "";
                    }
                    else if (promotion_data.PeriodCondition == 1)
                    {
                        //enable weekly
                        string enableweekly = string.Empty;
                        if (promotion_data.Sunday)
                            enableweekly += "Sunday,";
                        if (promotion_data.Monday)
                            enableweekly += "Monday,";
                        if (promotion_data.Tuesday)
                            enableweekly += "Tuesday,";
                        if (promotion_data.Wednesday)
                            enableweekly += "Wednesday,";
                        if (promotion_data.Thursday)
                            enableweekly += "Thursday,";
                        if (promotion_data.Friday)
                            enableweekly += "Friday,";
                        if (promotion_data.Saturday)
                            enableweekly += "Saturday,";
                        pos_promotion.WeeklyCondition = enableweekly;
                        pos_promotion.DayCondition = "";
                    }
                    else if (promotion_data.PeriodCondition == 2)
                    {
                        string enableday = string.Empty;
                        foreach (string day in promotion_data.day_selected)
                        {
                            enableday += "," + day;
                        }
                        enableday += ",";
                        pos_promotion.WeeklyCondition = "";
                        pos_promotion.DayCondition = enableday;
                    }
                    pos_promotion.PrintSignature = promotion_data.PrintSignature;
                    pos_promotion.IsActive = promotion_data.IsActive;
                    if (promotion_data.IsAutomation == null)
                        pos_promotion.IsAutomation = false;
                    else
                        pos_promotion.IsAutomation = promotion_data.IsAutomation;
                    pos_promotion.UpdatedDate = DateTime.Now;
                    pos_promotion.UpdatedBy = UserProfile.UserId;

                    db.Entry(pos_promotion).State = EntityState.Modified;
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

            return View(promotion_data);
        }

        /// <summary>
        /// Activate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Activate(int id)
        {
            pos_promotion promotion = db.pos_promotion.Find(id);
            if (promotion != null)
            {
                promotion.IsActive = !promotion.IsActive;
                promotion.UpdatedBy = UserProfile.employee_id;
                promotion.UpdatedDate = DateTime.Now;
                db.Entry(promotion).State = EntityState.Modified;
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
            pos_promotion pos_promotion = db.pos_promotion.Find(id);
            if (pos_promotion != null)
            {
                pos_promotion.DeletedBy = UserProfile.UserId;
                pos_promotion.DeletedDate = DateTime.Now;
                db.Entry(pos_promotion).State = EntityState.Modified;
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region PromoProduct
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

        private List<DiscountType> SetDiscountTypeList()
        {
            List<DiscountType> DiscountTypeList = new List<DiscountType>();
            DiscountType discType = new DiscountType()
            {
                DiscountTypeCode = "Percent",
                DiscountTypeName = "Discount(%)"
            };
            DiscountTypeList.Add(discType);

            discType = new DiscountType()
            {
                DiscountTypeCode = "DiscountRp",
                DiscountTypeName = "Discount(Rp)"
            };
            DiscountTypeList.Add(discType);

            discType = new DiscountType()
            {
                DiscountTypeCode = "SalePrice",
                DiscountTypeName = "Sale Price"
            };
            DiscountTypeList.Add(discType);

            return DiscountTypeList;
        }

        // GET: promotion/PromoItem/5
        public ActionResult PromoItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var promotion_model = (from a in db.pos_promotion.Where(a => a.PromotionID == id)
                                   join b in db.pos_promotion_type on a.PromotionType equals b.PromotionTypeId
                                   select new promotionproductViewModel()
                                   {
                                       PromotionID = a.PromotionID,
                                       PromotionName = a.PromotionName,
                                       PromotionTypeName = b.PromotionType,
                                       MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                                       SalemodeList = db.pos_sale_mode.Where(o => o.DeletedDate == null).ToList(),
                                       PromoProductList = db.vw_promotion_product.Where(o => o.PromotionID == id && o.ProductDeptID == 0).ToList(),
                                       ProductFrom = "Group"
                                   }).FirstOrDefault();

            promotion_model.ProductList = new List<pos_products>();
            promotion_model.DiscountTypeList = SetDiscountTypeList();
            return View(promotion_model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PromoItem(promotionproductViewModel PromotionProdData)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (PromotionProdData.flagSave == "save")
                    {
                        //Save data by btn Save
                        switch (PromotionProdData.ProductFrom)
                        {
                            case ("Group"):
                                if (PromotionProdData.ProductGroupID == 0)
                                {
                                    ModelState.AddModelError("ProductGroupID", "Product Group is Mandatory.");
                                }
                                break;
                            case ("Dept"):
                                if (PromotionProdData.ProductDeptID == 0)
                                {
                                    ModelState.AddModelError("ProductDeptID", "Product Dept is Mandatory.");
                                }
                                break;
                            case ("Product"):
                                if (PromotionProdData.product_selected.Count == 0)
                                {
                                    ModelState.AddModelError("ProductID", "Product is Mandatory.");
                                }
                                break;
                            default:
                                if (PromotionProdData.ProductCode == "")
                                {
                                    ModelState.AddModelError("ProductCode", "Product " + PromotionProdData.ProductFrom + " is Mandatory.");
                                }
                                break;
                        }
                        if (PromotionProdData.sale_mode_selected.Count == 0)
                        {
                            ModelState.AddModelError("SaleModeID", "Sale Mode is Mandatory.");
                        }
                        if (PromotionProdData.DiscountAmount == null || PromotionProdData.DiscountAmount == 0)
                        {
                            ModelState.AddModelError("DiscountAmount", "Discount Amount is Mandatory.");
                        }
                        if (PromotionProdData.DiscountType == null || PromotionProdData.DiscountType == "")
                        {
                            ModelState.AddModelError("DiscountType", "Discount Type is Mandatory.");
                        }

                        if (ModelState.IsValid)
                        {
                            foreach (string salemodeid in PromotionProdData.sale_mode_selected)
                            {
                                if (salemodeid != "")
                                {
                                    int SalemodeID_INT = Convert.ToInt32(salemodeid);
                                    pos_promotion_product productdiscount = new pos_promotion_product();
                                    if (PromotionProdData.DiscountType == "Percent")
                                    {
                                        productdiscount.DiscountPercentage = PromotionProdData.DiscountAmount;
                                        productdiscount.DiscountAmount = 0;
                                        productdiscount.DiscountSalePrice = 0;
                                    }
                                    else if (PromotionProdData.DiscountType == "DiscountRp")
                                    {
                                        productdiscount.DiscountAmount = PromotionProdData.DiscountAmount;
                                        productdiscount.DiscountPercentage = 0;
                                        productdiscount.DiscountSalePrice = 0;
                                    }
                                    else if (PromotionProdData.DiscountType == "SalePrice")
                                    {
                                        productdiscount.DiscountSalePrice = PromotionProdData.DiscountAmount;
                                        productdiscount.DiscountPercentage = 0;
                                        productdiscount.DiscountAmount = 0;
                                    }

                                    switch (PromotionProdData.ProductFrom)
                                    {
                                        case ("Group"):
                                            pos_promotion_product promo_product_grp = db.pos_promotion_product.Where(o => o.PromotionID == PromotionProdData.PromotionID && o.ProductGroupID == PromotionProdData.ProductGroupID && o.SaleModeID == SalemodeID_INT && o.ProductDeptID == 0).FirstOrDefault();

                                            bool isAdd = (promo_product_grp == null);
                                            if (isAdd)
                                                promo_product_grp = new pos_promotion_product();
                                            else
                                                promo_product_grp = db.pos_promotion_product.Find(promo_product_grp.PromotionProductID);

                                            promo_product_grp.PromotionID = PromotionProdData.PromotionID;
                                            promo_product_grp.ProductGroupID = PromotionProdData.ProductGroupID;
                                            promo_product_grp.ProductDeptID = 0;
                                            promo_product_grp.SaleModeID = Convert.ToInt32(salemodeid);
                                            promo_product_grp.DiscountAmount = productdiscount.DiscountAmount;
                                            promo_product_grp.DiscountPercentage = productdiscount.DiscountPercentage;
                                            promo_product_grp.DiscountSalePrice = productdiscount.DiscountSalePrice;
                                            promo_product_grp.CreatedBy = UserProfile.employee_id;
                                            promo_product_grp.CreatedDate = DateTime.Now;

                                            if (isAdd)
                                                promo_product_grp = db.pos_promotion_product.Add(promo_product_grp);
                                            else
                                                db.Entry(promo_product_grp).State = EntityState.Modified;
                                            db.SaveChanges();
                                            break;
                                        case ("Dept"):
                                            pos_promotion_product promo_product_dpt = db.pos_promotion_product.Where(o => o.PromotionID == PromotionProdData.PromotionID && o.ProductDeptID == PromotionProdData.ProductDeptID && o.ProductID == 0 && o.SaleModeID == SalemodeID_INT && o.ProductID == 0).FirstOrDefault();

                                            bool isAddDPT = (promo_product_dpt == null);
                                            if (isAddDPT)
                                                promo_product_dpt = new pos_promotion_product();
                                            else
                                                promo_product_dpt = db.pos_promotion_product.Find(promo_product_dpt.PromotionProductID);

                                            promo_product_dpt.PromotionID = PromotionProdData.PromotionID;
                                            promo_product_dpt.ProductGroupID = PromotionProdData.ProductGroupID;
                                            promo_product_dpt.ProductDeptID = PromotionProdData.ProductDeptID;
                                            promo_product_dpt.ProductID = 0;
                                            promo_product_dpt.SaleModeID = SalemodeID_INT;
                                            promo_product_dpt.DiscountAmount = productdiscount.DiscountAmount;
                                            promo_product_dpt.DiscountPercentage = productdiscount.DiscountPercentage;
                                            promo_product_dpt.DiscountSalePrice = productdiscount.DiscountSalePrice;
                                            promo_product_dpt.CreatedBy = UserProfile.employee_id;
                                            promo_product_dpt.CreatedDate = DateTime.Now;

                                            if (isAddDPT)
                                                promo_product_dpt = db.pos_promotion_product.Add(promo_product_dpt);
                                            else
                                                db.Entry(promo_product_dpt).State = EntityState.Modified;
                                            db.SaveChanges();

                                            break;
                                        case ("Product"):
                                            foreach (string prod in PromotionProdData.product_selected)
                                            {
                                                int prodID = Convert.ToInt32(prod);
                                                pos_promotion_product promo_products = db.pos_promotion_product.Where(o => o.PromotionID == PromotionProdData.PromotionID && o.ProductID == prodID && o.SaleModeID == SalemodeID_INT).FirstOrDefault();

                                                bool isAddPrd = (promo_products == null);
                                                if (isAddPrd)
                                                    promo_products = new pos_promotion_product();
                                                else
                                                    promo_products = db.pos_promotion_product.Find(promo_products.PromotionProductID);

                                                promo_products.PromotionID = PromotionProdData.PromotionID;
                                                promo_products.ProductGroupID = PromotionProdData.ProductGroupID;
                                                promo_products.ProductDeptID = PromotionProdData.ProductDeptID;
                                                promo_products.ProductID = prodID;
                                                promo_products.SaleModeID = SalemodeID_INT;
                                                promo_products.DiscountAmount = productdiscount.DiscountAmount;
                                                promo_products.DiscountPercentage = productdiscount.DiscountPercentage;
                                                promo_products.DiscountSalePrice = productdiscount.DiscountSalePrice;
                                                promo_products.CreatedBy = UserProfile.employee_id;
                                                promo_products.CreatedDate = DateTime.Now;

                                                if (isAddPrd)
                                                    promo_products = db.pos_promotion_product.Add(promo_products);
                                                else
                                                    db.Entry(promo_products).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }
                                            break;
                                        default:
                                            string[] products;
                                            PromotionProdData.ProductCode += ",";
                                            products = PromotionProdData.ProductCode.Split(',');

                                            foreach (string prod in products)
                                            {
                                                if (prod != "")
                                                {
                                                    pos_products product = new pos_products();

                                                    if (PromotionProdData.flagSave == "Code")
                                                        product = db.pos_products.Where(o => o.ProductCode == prod && o.ProductActivate == true && o.DeletedDate == null).FirstOrDefault();
                                                    else if (PromotionProdData.flagSave == "Barcode")
                                                        product = db.pos_products.Where(o => o.ProductName3 == prod && o.ProductActivate == true && o.DeletedDate == null).FirstOrDefault();

                                                    if (product != null)
                                                    {
                                                        pos_promotion_product promo_products = db.pos_promotion_product.Where(o => o.PromotionID == PromotionProdData.PromotionID && o.ProductID == product.ProductID && o.SaleModeID == SalemodeID_INT).FirstOrDefault();

                                                        bool isAddPrd = (promo_products == null);
                                                        if (isAddPrd)
                                                            promo_products = new pos_promotion_product();
                                                        else
                                                            promo_products = db.pos_promotion_product.Find(promo_products.PromotionProductID);

                                                        promo_products.PromotionID = PromotionProdData.PromotionID;
                                                        promo_products.ProductGroupID = product.ProductGroupID;
                                                        promo_products.ProductDeptID = product.ProductDeptID;
                                                        promo_products.ProductID = product.ProductID;
                                                        promo_products.SaleModeID = SalemodeID_INT;
                                                        promo_products.DiscountAmount = productdiscount.DiscountAmount;
                                                        promo_products.DiscountPercentage = productdiscount.DiscountPercentage;
                                                        promo_products.DiscountSalePrice = productdiscount.DiscountSalePrice;
                                                        promo_products.CreatedBy = UserProfile.employee_id;
                                                        promo_products.CreatedDate = DateTime.Now;

                                                        if (isAddPrd)
                                                            promo_products = db.pos_promotion_product.Add(promo_products);
                                                        else
                                                            db.Entry(promo_products).State = EntityState.Modified;
                                                        db.SaveChanges();
                                                    }
                                                }
                                            }

                                            break;
                                    }
                                }
                            }
                            transaction.Commit();
                        }
                    }

                    //get data by changes dropdownlist
                    var promotion_model = (from a in db.pos_promotion.Where(a => a.PromotionID == PromotionProdData.PromotionID)
                                           join b in db.pos_promotion_type on a.PromotionType equals b.PromotionTypeId
                                           select new promotionproductViewModel()
                                           {
                                               PromotionID = a.PromotionID,
                                               PromotionName = a.PromotionName,
                                               PromotionTypeName = b.PromotionType,
                                               MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                                               SalemodeList = db.pos_sale_mode.Where(o => o.DeletedDate == null).ToList(),
                                               ProductFrom = PromotionProdData.ProductFrom
                                           }).FirstOrDefault();

                    if (PromotionProdData.ProductFrom == "Group")
                        promotion_model.PromoProductList = db.vw_promotion_product.Where(o => o.PromotionID == PromotionProdData.PromotionID && o.ProductDeptID == 0).ToList();
                    else if (PromotionProdData.ProductFrom == "Dept")
                        promotion_model.PromoProductList = db.vw_promotion_product.Where(o => o.PromotionID == PromotionProdData.PromotionID && o.ProductDeptID > 0 && o.ProductID == 0).ToList();
                    else
                        promotion_model.PromoProductList = db.vw_promotion_product.Where(o => o.PromotionID == PromotionProdData.PromotionID && o.ProductID > 0).ToList();
                    promotion_model.ProductList = new List<pos_products>();
                    promotion_model.product_selected = new List<string>();
                    promotion_model.sale_mode_selected = new List<string>();
                    promotion_model.DiscountTypeList = SetDiscountTypeList();
                    promotion_model.flagSave = "";
                    PromotionProdData.DiscountAmount = 0;
                    PromotionProdData.DiscountType = "Percent";

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
            pos_promotion_product pos_promotion = db.pos_promotion_product.Find(id);
            if (pos_promotion != null)
            {
                db.pos_promotion_product.Remove(pos_promotion);
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region ShopLink
        public ActionResult ShopLink(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var promotion_model = (from a in db.pos_promotion.Where(a => a.PromotionID == id)
                                   join b in db.pos_promotion_type on a.PromotionType equals b.PromotionTypeId
                                   select new promotionshoplinkViewModel()
                                   {
                                       PromotionID = a.PromotionID,
                                       PromotionName = a.PromotionName,
                                       PromotionTypeName = b.PromotionType,
                                       PromoShopLinkList = db.vw_promotion_shoplink.Where(o => o.PromotionID == id).ToList(),
                                       MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                                   }).FirstOrDefault();

            promotion_model.ShopList = new List<pos_shop_data>();
            return View(promotion_model);
        }
        public JsonResult GetShopList(int MasterShopID = 0, int PromotionID = 0)
        {
            //var _ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == MasterShopID && o.DeletedDate == null).ToList();

            var ShopLink = (from shop in db.pos_shop_data.Where(shop => shop.MasterShopLink == MasterShopID && shop.DeletedDate == null)
                            join sl in db.pos_promotion_shop_link.Where(sl => sl.IsActive == true && sl.PromotionID == PromotionID)
                            on shop.ShopID equals sl.ShopID
                            into shl
                            from sl in shl.DefaultIfEmpty()
                            select new
                            {
                               shop.ShopID,
                               shop.ShopName,
                               sl.PromotionID
                            }
                            ).ToList();

            return Json(ShopLink, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShopLink(promotionshoplinkViewModel PromotionProdData)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (PromotionProdData.shop_selected.Count == 0)
                    {
                        ModelState.AddModelError("ShopID", "Shop is Mandatory.");
                    }

                    if (ModelState.IsValid)
                    {
                        foreach (string shopid in PromotionProdData.shop_selected)
                        {
                            if (shopid != "")
                            {
                                int shopid_INT = Convert.ToInt32(shopid);

                                pos_promotion_shop_link promo_products = db.pos_promotion_shop_link.Where(o => o.PromotionID == PromotionProdData.PromotionID && o.ShopID == shopid_INT && o.MasterShopID == PromotionProdData.MasterShopID).FirstOrDefault();

                                bool isAddPrd = (promo_products == null);
                                if (isAddPrd)
                                    promo_products = new pos_promotion_shop_link();
                                else
                                    promo_products = db.pos_promotion_shop_link.Find(promo_products.PromotionShopLinkID);

                                promo_products.PromotionID = PromotionProdData.PromotionID;
                                promo_products.MasterShopID = PromotionProdData.MasterShopID;
                                promo_products.ShopID = shopid_INT;
                                promo_products.IsActive = true;

                                if (isAddPrd)
                                {
                                    promo_products.CreatedBy = UserProfile.employee_id;
                                    promo_products.CreatedDate = DateTime.Now;
                                    promo_products = db.pos_promotion_shop_link.Add(promo_products);
                                }
                                else
                                {
                                    promo_products.UpdatedBy = UserProfile.employee_id;
                                    promo_products.UpdatedDate = DateTime.Now;
                                    db.Entry(promo_products).State = EntityState.Modified;
                                }
                                db.SaveChanges();
                            }
                        }
                        transaction.Commit();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        PromotionProdData.MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList();
                        PromotionProdData.PromoShopLinkList = db.vw_promotion_shoplink.Where(o => o.PromotionID == PromotionProdData.PromotionID).ToList();
                        return View(PromotionProdData);
                    }

                    //get data by changes dropdownlist
                    var promotion_model = (from a in db.pos_promotion.Where(a => a.PromotionID == PromotionProdData.PromotionID)
                                           join b in db.pos_promotion_type on a.PromotionType equals b.PromotionTypeId
                                           select new promotionshoplinkViewModel()
                                           {
                                               PromotionID = a.PromotionID,
                                               PromotionName = a.PromotionName,
                                               PromotionTypeName = b.PromotionType,
                                               PromoShopLinkList = db.vw_promotion_shoplink.Where(o => o.PromotionID == PromotionProdData.PromotionID).ToList(),
                                               MasterShopList = db.pos_shop_data.Where(o => o.MasterShop == true && o.DeletedDate == null).ToList(),
                                           }).FirstOrDefault();

                    promotion_model.ShopList = new List<pos_shop_data>();
                    promotion_model.IsActive = true;

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
        public JsonResult DeleteShopByID(int id)
        {
            pos_promotion_shop_link pos_promotion = db.pos_promotion_shop_link.Find(id);
            if (pos_promotion != null)
            {
                db.pos_promotion_shop_link.Remove(pos_promotion);
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