using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RINOR_POS.Models;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace RINOR_POS.Controllers
{
    [Authorize]
    public class productpriceController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: productprice
        public ActionResult Index()
        {
            productpriceViewModel productprice = new productpriceViewModel()
            {
                pricegroup_list = db.pos_product_price_group.Where(o => o.DeletedDate == null).ToList(),
                pricedate_list = db.vw_product_price_date.ToList(),
                merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList(),
                brand_list = new List<pos_brand_data>(),
                shop_list = new List<pos_shop_data>(),
                salemode_data = db.pos_sale_mode.ToList()
            };

            return View(productprice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Index(productpriceViewModel ProductPriceData)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ProductPriceData.productpricesave.Count == 0)
                    {
                        ModelState.AddModelError("", "Product is Mandatory.");
                    }

                    if (ModelState.IsValid)
                    {
                        foreach (pos_product_price productprice in ProductPriceData.productpricesave)
                        {

                        }
                        transaction.Commit();
                    }

                    return View(ProductPriceData);
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
            return View(ProductPriceData);
        }

        public JsonResult GetTablePriceList(int ShopId = 0, int maxRecords = 0, string productName = "", string priceGroupList = "", string dateGroupList = "", string SalemodeList = "", string productGroupList = "")
        {
            try
            {
                productpriceViewModel productprice = new productpriceViewModel();

                string constring = WebConfigurationManager.ConnectionStrings["ModelPOSDB"].ConnectionString;
                var conn = new SqlConnection(constring);

                var cmd = new SqlCommand("ProductPrice", conn);
                cmd.CommandText = "Exec usp_productprice @ShopId, @MaxRecord, @ProductName, @PriceGroupList, @DateGroupList, @SaleModeList, @ProductGroupList";

                cmd.Parameters.AddWithValue("@ShopId", ShopId);
                cmd.Parameters.AddWithValue("@MaxRecord", maxRecords);
                cmd.Parameters.AddWithValue("@ProductName", productName);
                cmd.Parameters.AddWithValue("@PriceGroupList", priceGroupList);
                cmd.Parameters.AddWithValue("@DateGroupList", dateGroupList);
                cmd.Parameters.AddWithValue("@SaleModeList", SalemodeList);
                cmd.Parameters.AddWithValue("@ProductGroupList", productGroupList);

                conn.Open();
                using (SqlDataReader row = cmd.ExecuteReader())
                {
                    //var tb = new DataTable();
                    //tb.Load(dr);
                    productprice.productsonly_list = new List<pos_products>();
                    productprice.productprice_list = new List<ProductPriceDetail>();
                    productprice.salemode_data = new List<pos_sale_mode>();
                    int salemodeidlast = 0;
                    while (row.Read())
                    {
                        if (salemodeidlast == 0)
                        {
                            salemodeidlast = int.Parse(row["SaleModeID"].ToString());
                            pos_sale_mode salemode = new pos_sale_mode();
                            salemode.SaleModeID = int.Parse(row["SaleModeID"].ToString());
                            salemode.SaleModeName = row["SaleModeName"].ToString();
                            productprice.salemode_data.Add(salemode);
                        }

                        ProductPriceDetail posProduct = new ProductPriceDetail();
                        posProduct.ProductId = int.Parse(row["ProductId"].ToString());
                        posProduct.ProductName = row["ProductName"].ToString();
                        posProduct.ProductCode = row["ProductCode"].ToString();
                        posProduct.ShopID = int.Parse(row["ShopID"].ToString());
                        posProduct.SaleMode = int.Parse(row["SaleModeID"].ToString());
                        if (row["ProductPriceDateId"].ToString() != "" )
                            posProduct.ProductPriceDateId = int.Parse(row["ProductPriceDateId"].ToString());
                        if (row["ProductPrice"].ToString() == "")
                            posProduct.ProductPrice = 0;
                        else
                            posProduct.ProductPrice = int.Parse(row["ProductPrice"].ToString());

                        productprice.productprice_list.Add(posProduct);

                        if (salemodeidlast != int.Parse(row["SaleModeID"].ToString()))
                        {
                            salemodeidlast = int.Parse(row["SaleModeID"].ToString());
                            pos_sale_mode salemode = new pos_sale_mode();
                            salemode.SaleModeID = int.Parse(row["SaleModeID"].ToString());
                            salemode.SaleModeName = row["SaleModeName"].ToString();
                            productprice.salemode_data.Add(salemode);
                        }
                    }

                    row.NextResult();

                    while (row.Read())
                    {
                        pos_products posProduct = new pos_products();
                        posProduct.ProductID = int.Parse(row["ProductId"].ToString());
                        posProduct.ProductName = row["ProductName"].ToString();
                        posProduct.ProductCode = row["ProductCode"].ToString();

                        productprice.productsonly_list.Add(posProduct);
                    }
                }
                conn.Close();

                return Json(productprice, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetPrdGroupByMasterShop(int MasterShopId = 0)
        {
            var _ProductGroupList = db.pos_product_group.Where(o => o.ShopID == MasterShopId && o.DeletedDate == null && o.ProductGroupActivate == true).ToList();

            return Json(_ProductGroupList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductGroupList(int ShopId = 0, int SaleModeId = 0)
        {
            var _ProductGroupList = db.vw_product_group.Where(o => o.ShopID == ShopId && o.SaleModeID == SaleModeId).ToList();

            return Json(_ProductGroupList, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetProductPriceList(int ProductId = 0)
        //{
        //    var _ProductPriceList = db.pos_product_price.Where(o => o.ProductId == ProductId && o.MainPrice == false).ToList();

        //    return Json(_ProductPriceList, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetMainPriceList(int ProductId = 0)
        //{
        //    var _ProductPriceList = db.pos_product_price.Where(o => o.ProductId == ProductId && o.MainPrice == true).ToList();

        //    return Json(_ProductPriceList, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetBrandList(int MerchantID = 0)
        {
            var _BrandList = db.pos_brand_data.Where(o => o.DeletedDate == null && o.Activate == true && o.MerchantID == MerchantID).ToList();

            return Json(_BrandList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMasterShopList(int BrandID = 0)
        {
            var _ShopList = db.pos_shop_data.Where(o => o.DeletedDate == null && o.BrandID == BrandID && o.MasterShop == true).ToList();

            return Json(_ShopList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetShopList(int BrandID = 0)
        {
            var _ShopList = db.pos_shop_data.Where(o => o.DeletedDate == null && o.BrandID == BrandID).ToList();

            return Json(_ShopList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCityList(int ProvinceID = 0)
        {
            var _CityList = (from cty in db.pos_city
                             where cty.DeletedDate == null && cty.ProvinceID == ProvinceID
                             select cty).ToList();

            return Json(_CityList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRegionList(int CityID = 0)
        {
            var _RegionList = (from rgn in db.pos_region
                               where rgn.DeletedDate == null && rgn.CityID == CityID
                               select rgn).ToList();

            return Json(_RegionList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveAllPrice(PriceData[] priceData)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (PriceData data in priceData)
                    {
                        if (Convert.ToInt32(data.Price) > 0)
                        {
                            string[] priceGroups = data.PriceGroupID.Split(',');

                            for (int i = 0; i < priceGroups.Length - 1; i++)
                            {
                                string[] ids = data.DataID.Split('-');
                                int ProductID = Convert.ToInt32(ids[0]);
                                int SalemodeID = Convert.ToInt32(ids[1]);
                                int PriceGrpID = Convert.ToInt32(priceGroups[i]);
                                //cek already exist
                                pos_product_price prodprice = db.pos_product_price.Where(o => o.ShopID == data.ShopID
                                                                                        && o.ProductId == ProductID
                                                                                        && o.SaleMode == SalemodeID
                                                                                        && o.ProductPriceDateId == data.PriceGroupDateID
                                                                                        && o.ProductPriceGroupId == PriceGrpID).FirstOrDefault();

                                if (prodprice == null)
                                {
                                    //Insert
                                    pos_product_price datasave = new pos_product_price();
                                    datasave.ShopID = data.ShopID;
                                    datasave.ProductId = ProductID;
                                    datasave.SaleMode = SalemodeID;
                                    datasave.SaleMode = SalemodeID;
                                    datasave.ProductPriceGroupId = PriceGrpID;
                                    datasave.ProductPriceDateId = data.PriceGroupDateID;
                                    datasave.ProductPrice = Convert.ToInt32(data.Price);
                                    datasave.CreatedDate = DateTime.Now;
                                    datasave.CreatedBy = UserProfile.employee_id;

                                    datasave = db.pos_product_price.Add(datasave);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    //Update
                                    pos_product_price datasave = db.pos_product_price.Find(prodprice.ProductPriceId);
                                    datasave.ShopID = data.ShopID;
                                    datasave.ProductId = ProductID;
                                    datasave.SaleMode = SalemodeID;
                                    datasave.SaleMode = SalemodeID;
                                    datasave.ProductPriceGroupId = PriceGrpID;
                                    datasave.ProductPriceDateId = data.PriceGroupDateID;
                                    datasave.ProductPrice = Convert.ToInt32(data.Price);
                                    datasave.UpdatedDate = DateTime.Now;
                                    datasave.UpdatedBy = UserProfile.employee_id;
                                    db.Entry(datasave).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                    transaction.Commit();
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}