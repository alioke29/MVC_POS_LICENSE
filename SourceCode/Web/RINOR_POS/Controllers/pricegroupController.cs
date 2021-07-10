using System;
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
    public class pricegroupController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();
        // GET: pricetime
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Getpricegroup()
        {

            var CountryList = db.pos_product_price_group.Where(t => t.DeletedDate == null).Select(
                t => new
                {
                    t.ProductPriceGroupID,
                    t.ProductPriceName
                }).ToList();
            return Json(CountryList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult List(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {
            sord = (sord == null) ? "" : sord;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var price_group = db.pos_product_price_group.Where(t => t.DeletedDate == null).Select(
                    t => new
                    {
                        t.ProductPriceGroupID,
                        t.ProductPriceName,
                        t.CreatedBy,
                        t.CreatedDate,
                        t.UpdatedBy,
                        t.UpdatedDate,
                        t.DeletedBy,
                        t.DeletedDate,
                    });

            // search function
            if (_search)
            {
                switch (searchField)
                {
                    case "ProductPriceName":
                        price_group = price_group.Where(t => t.ProductPriceName.Contains(searchString));
                        break;

                }
            }
            //calc paging
            int totalRecords = price_group.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            //default sorting
            if (sord.ToUpper() == "DESC")
            {
                price_group = price_group.OrderByDescending(t => t.ProductPriceName);
                price_group = price_group.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                price_group = price_group.OrderBy(t => t.ProductPriceName);
                price_group = price_group.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = price_group
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private bool IsNumeric(string id)
        {
            int ID;
            return int.TryParse(id, out ID);
        }

        [HttpPost]
        public JsonResult Crud()
        {
            if (UserProfile.UserId != 0)
            {
                if (Request.Form["oper"] == "add")
                {
                    //prepare for insert data
                    pos_product_price_group pricegroup = new pos_product_price_group();
                    pricegroup.ProductPriceName = Request.Form["ProductPriceName"];

                    pricegroup.CreatedBy = UserProfile.UserId;
                    pricegroup.CreatedDate = DateTime.Now;

                    db.Entry(pricegroup).State = EntityState.Added;
                    db.SaveChanges();

                    return Json("Insert", JsonRequestBehavior.AllowGet);
                }
                else if (Request.Form["oper"] == "edit")
                {
                    if (IsNumeric(Request.Form["ProductPriceGroupID"].ToString()))
                    {
                        //prepare for update data
                        int id = Convert.ToInt32(Request.Form["ProductPriceGroupID"]);
                        pos_product_price_group pricegroup = db.pos_product_price_group.Find(id);
                        pricegroup.ProductPriceName = Request.Form["ProductPriceName"];
                        pricegroup.UpdatedBy = UserProfile.UserId;
                        pricegroup.UpdatedDate = DateTime.Now;

                        db.Entry(pricegroup).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json("Update", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //prepare for insert data
                        pos_product_price_group pricegroup = new pos_product_price_group();
                        pricegroup.ProductPriceName = Request.Form["ProductPriceName"];

                        pricegroup.CreatedBy = UserProfile.UserId;
                        pricegroup.CreatedDate = DateTime.Now;

                        db.Entry(pricegroup).State = EntityState.Added;
                        db.SaveChanges();
                        return Json("Insert", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    if (Request.Form["oper"] == "del")
                    {
                        //for delete process
                        string ids = Request.Form["Id"];
                        string[] values = ids.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            values[i] = values[i].Trim();
                            //prepare for soft delete data
                            int id = Convert.ToInt32(values[i]);
                            pos_product_price_group pricegroup = db.pos_product_price_group.Find(id);

                            pricegroup.DeletedBy = UserProfile.UserId;
                            pricegroup.DeletedDate = DateTime.Now;

                            db.Entry(pricegroup).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        return Json("Delete", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Error", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json("Session", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult MappingShop(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_product_price_group priceGroup = db.pos_product_price_group.Find(id);

            pricegroupshopViewModels priceGroupShop = new pricegroupshopViewModels();
            priceGroupShop.ProductPriceGroupID = priceGroup.ProductPriceGroupID;
            priceGroupShop.ProductPriceGroupName = priceGroup.ProductPriceName;
            priceGroupShop.shop_list = (from s in db.pos_shop_data.Where(s => s.MasterShop == true) select s).ToList<pos_shop_data>();
            priceGroupShop.shop_selected = new List<string>();
            priceGroupShop.shop_available = new List<string>();
            return View(priceGroupShop);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MappingShop([Bind(Include = "ProductPriceGroupID, ProductPriceGroupName, shop_selected, shop_available")] pricegroupshopViewModels pricegroupshop)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (string shopid in pricegroupshop.shop_selected)
                    {
                        pricegroupshop.shop_available.Remove(shopid);
                        int shopID = Convert.ToInt32(shopid);
                        pos_price_group_shop groupshopexist = (from s in db.pos_price_group_shop.Where(s => s.ShopId == shopID) select s).FirstOrDefault<pos_price_group_shop>();

                        if (groupshopexist == null)
                        {
                            //insert
                            pos_price_group_shop priceGroupShop = new pos_price_group_shop();
                            priceGroupShop.ProductPriceGroupID = pricegroupshop.ProductPriceGroupID;
                            priceGroupShop.ShopId = shopID;
                            priceGroupShop.CreatedBy = UserProfile.employee_id;
                            priceGroupShop.CreatedDate = DateTime.Now;
                            priceGroupShop = db.pos_price_group_shop.Add(priceGroupShop);
                            db.SaveChanges();
                        }
                        else
                        {
                            //update
                            groupshopexist.ProductPriceGroupID = pricegroupshop.ProductPriceGroupID;
                            groupshopexist.UpdatedBy = UserProfile.employee_id;
                            groupshopexist.UpdatedDate = DateTime.Now;
                            db.Entry(groupshopexist).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    foreach (string shopid in pricegroupshop.shop_available)
                    {
                        int shopID = Convert.ToInt32(shopid);
                        pos_price_group_shop groupshopexist = (from s in db.pos_price_group_shop.Where(s => pricegroupshop.ShopId == shopID) select s).FirstOrDefault<pos_price_group_shop>();

                        if (groupshopexist != null)
                        {
                            //Delete
                            db.Entry(groupshopexist).State = EntityState.Deleted;
                            db.SaveChanges();
                        }
                    }
                    transaction.Commit();
                    return RedirectToAction("Index");
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
            return View(pricegroupshop);
        }
        public JsonResult GetShopList(int MasterShopID = 0)
        {
            var _ShopList = new object();

            if (MasterShopID == 0)
                _ShopList = db.vw_price_group_shop.Where(o => o.MasterShop == false).ToList();
            else
                _ShopList = db.vw_price_group_shop.Where(o => o.MasterShopLink == MasterShopID).ToList();

            return Json(_ShopList, JsonRequestBehavior.AllowGet);
        }
    }
}