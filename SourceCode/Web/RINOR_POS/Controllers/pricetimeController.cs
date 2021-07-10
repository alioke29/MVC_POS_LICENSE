using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RINOR_POS.Models;
using System.Globalization;

namespace RINOR_POS.Controllers
{
    [Authorize]
    public class pricetimeController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();
        // GET: pricetime
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetPriceDate()
        {

            var CountryList = db.pos_product_price_date.Where(t => t.DeletedDate == null).Select(
                t => new
                {
                    t.ProductPriceDateID,
                    t.FromDate,
                    t.ToDate
                }).ToList();
            return Json(CountryList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult List(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {
            sord = (sord == null) ? "" : sord;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var price_date = db.pos_product_price_date.Where(t => t.DeletedDate == null).Select(
                    t => new
                    {
                        t.ProductPriceDateID,
                        t.FromDate,
                        t.ToDate,
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
                    case "FromDate":
                        //price_date = price_date.Where(t => t.FromDate.Contains(searchString));
                        break;
                    case "ToDate":
                        //price_date = price_date.Where(t => t.ToDate.Contains(searchString));
                        break;
                }
            }
            //calc paging
            int totalRecords = price_date.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            //default sorting
            if (sord.ToUpper() == "DESC")
            {
                price_date = price_date.OrderByDescending(t => t.FromDate);
                price_date = price_date.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                price_date = price_date.OrderBy(t => t.FromDate);
                price_date = price_date.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = price_date
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
                CultureInfo MyCultureInfo = new CultureInfo("en-US");
                if (Request.Form["oper"] == "add")
                {
                    //prepare for insert data
                    pos_product_price_date pricedate = new pos_product_price_date();
                    pricedate.FromDate = DateTime.ParseExact(Request.Form["FromDate"], "dd-MM-yyyy", MyCultureInfo);
                    pricedate.ToDate = DateTime.ParseExact(Request.Form["ToDate"], "dd-MM-yyyy", MyCultureInfo);

                    pricedate.CreatedBy = UserProfile.UserId;
                    pricedate.CreatedDate = DateTime.Now;

                    db.Entry(pricedate).State = EntityState.Added;
                    db.SaveChanges();

                    return Json("Insert", JsonRequestBehavior.AllowGet);
                }
                else if (Request.Form["oper"] == "edit")
                {
                    if (IsNumeric(Request.Form["ProductPriceDateID"].ToString()))
                    {
                        //prepare for update data
                        int id = Convert.ToInt32(Request.Form["ProductPriceDateID"]);
                        pos_product_price_date pricedate = db.pos_product_price_date.Find(id);
                        pricedate.FromDate = DateTime.ParseExact(Request.Form["FromDate"], "dd-MM-yyyy", MyCultureInfo);
                        pricedate.ToDate = DateTime.ParseExact(Request.Form["ToDate"], "dd-MM-yyyy", MyCultureInfo);
                        pricedate.UpdatedBy = UserProfile.UserId;
                        pricedate.UpdatedDate = DateTime.Now;

                        db.Entry(pricedate).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json("Update", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //prepare for insert data
                        pos_product_price_date pricedate = new pos_product_price_date();
                        pricedate.FromDate = DateTime.ParseExact(Request.Form["FromDate"], "dd-MM-yyyy", MyCultureInfo);
                        pricedate.ToDate = DateTime.ParseExact(Request.Form["ToDate"], "dd-MM-yyyy", MyCultureInfo);

                        pricedate.CreatedBy = UserProfile.UserId;
                        pricedate.CreatedDate = DateTime.Now;

                        db.Entry(pricedate).State = EntityState.Added;
                        db.SaveChanges();

                        return Json("Insert", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    if (Request.Form["oper"] == "del")
                    {
                        //for delete process
                        string ids = Request.Form["id"];
                        string[] values = ids.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            values[i] = values[i].Trim();
                            //prepare for soft delete data
                            int id = Convert.ToInt32(values[i]);
                            pos_product_price_date pricedate = db.pos_product_price_date.Find(id);

                            pricedate.DeletedBy = UserProfile.UserId;
                            pricedate.DeletedDate = DateTime.Now;

                            db.Entry(pricedate).State = EntityState.Modified;
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
    }
}