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
    public class PromoPaymentController : Controller
    {
        private ModelPOSDB db = new ModelPOSDB();

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var promotion_model = (from a in db.pos_promotion.Where(a => a.PromotionID == id)
                                   join b in db.pos_promotion_type on a.PromotionType equals b.PromotionTypeId
                                   select new promotionpaymentViewModel()
                                   {
                                       PromotionID = a.PromotionID,
                                       PromotionName = a.PromotionName,
                                       PromotionTypeName = b.PromotionType,
                                       DiscountAmount = 0,
                                       DiscountPercentage = 0,
                                       MinimumSubTotalBeforeVAT = 0,
                                       MinimumPayAmountAfterVAT = 0,
                                       MaximumDiscountAmount = 0,
                                       PaymentTypeList = db.pos_payment_type.Where(o => o.Activate == true && o.DeletedDate == null).ToList(),
                                   }).FirstOrDefault();

            List<pos_promotion_payment> promoPayment = db.pos_promotion_payment.Where(o => o.PromotionID == promotion_model.PromotionID).ToList();

            if (promoPayment.Count > 0)
            {
                foreach (pos_promotion_payment pay in promoPayment)
                {
                    promotion_model.payment_selected.Add(pay.PayTypeID.ToString());
                }
                promotion_model.DiscountAmount = promoPayment[0].DiscountAmount;
                promotion_model.DiscountPercentage = promoPayment[0].DiscountPercentage;
                promotion_model.MinimumSubTotalBeforeVAT = promoPayment[0].MinimumSubTotalBeforeVAT;
                promotion_model.MinimumPayAmountAfterVAT = promoPayment[0].MinimumPayAmountAfterVAT;
                promotion_model.MaximumDiscountAmount = promoPayment[0].MaximumDiscountAmount;
                promotion_model.MinimumPcs = promoPayment[0].MinimumPcs;
                promotion_model.MaximumPcs = promoPayment[0].MaximumPcs;
                promotion_model.IsActive = promoPayment[0].IsActive;
            }

            return View(promotion_model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(promotionpaymentViewModel PromotionProdData)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (PromotionProdData.payment_selected.Count == 0)
                    {
                        ModelState.AddModelError("PayTypeID", "Payment is Mandatory.");
                    }

                    if (ModelState.IsValid)
                    {
                        foreach (string paymentselect in PromotionProdData.payment_selected)
                        {
                            if (paymentselect != "")
                            {
                                int paymentselect_INT = Convert.ToInt32(paymentselect);

                                pos_promotion_payment promo_products = db.pos_promotion_payment.Where(o => o.PromotionID == PromotionProdData.PromotionID && o.PayTypeID == paymentselect_INT).FirstOrDefault();

                                bool isAddPrd = (promo_products == null);
                                if (isAddPrd)
                                    promo_products = new pos_promotion_payment();
                                else
                                    promo_products = db.pos_promotion_payment.Find(promo_products.PromotionPaymentID);

                                promo_products.PromotionID = PromotionProdData.PromotionID;
                                promo_products.PayTypeID = paymentselect_INT;
                                promo_products.DiscountAmount = PromotionProdData.DiscountAmount;
                                promo_products.DiscountPercentage = PromotionProdData.DiscountPercentage;
                                promo_products.MinimumSubTotalBeforeVAT = PromotionProdData.MinimumSubTotalBeforeVAT;
                                promo_products.MinimumPayAmountAfterVAT = PromotionProdData.MinimumPayAmountAfterVAT;
                                promo_products.MaximumDiscountAmount = PromotionProdData.MaximumDiscountAmount;
                                promo_products.MinimumPcs = PromotionProdData.MinimumPcs;
                                promo_products.MaximumPcs = PromotionProdData.MaximumPcs;
                                promo_products.IsActive = PromotionProdData.IsActive;

                                if (isAddPrd)
                                {
                                    promo_products.CreatedBy = UserProfile.employee_id;
                                    promo_products.CreatedDate = DateTime.Now;
                                    promo_products = db.pos_promotion_payment.Add(promo_products);
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
                        return RedirectToAction("Index", "promotion");
                    }
                    else
                    {
                        PromotionProdData.PaymentTypeList = db.pos_payment_type.Where(o => o.Activate == true && o.DeletedDate == null).ToList();
                        return View(PromotionProdData);
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
            return View(PromotionProdData);
        }
    }
}