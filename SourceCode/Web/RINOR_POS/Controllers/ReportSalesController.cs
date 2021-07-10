using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Report Sales By Product
    /// </summary>
    [Authorize]
    public class ReportSalesController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();
        /// <summary>
        ///  GET: ReportSales
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ReportsViewModel report = new ReportsViewModel();
            report.StartPeriod = DateTime.Today;
            report.EndPeriod = DateTime.Today;
            report.shop_list = (from s in db.pos_shop_data.Where(s => s.MasterShop == true) select s).ToList<pos_shop_data>();
            report.shop_selected = new List<string>();
            report.shop_available = new List<string>();
            return View(report);
        }

        /// <summary>
        /// Get List Shop by MasterShopID
        /// </summary>
        /// <param name="MasterShopID"></param>
        /// <returns></returns>
        public JsonResult GetShopList(int MasterShopID = 0)
        {
            var _ShopList = new object();

            if (MasterShopID == 0)
                _ShopList = db.pos_shop_data.Where(o => o.ShopID == 0).ToList();
            else
                _ShopList = db.pos_shop_data.Where(o => o.MasterShopLink == MasterShopID).ToList();

            return Json(_ShopList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// list for index
        /// </summary>
        /// <returns>JSON</returns>
        public JsonResult GetIndexList(string StartPeriod, string EndPeriod, string ShopId = "", string ShopList = "")
        {
            db.Configuration.ProxyCreationEnabled = false;

            try
            {
                string constring = WebConfigurationManager.ConnectionStrings["ModelPOSDB"].ConnectionString;
                var conn = new SqlConnection(constring);

                var cmd = new SqlCommand("Report", conn);
                cmd.CommandText = "Exec usp_report_salesbyproduct @StartDate, @EndDate, @MasterShopID, @ShopID";

                cmd.Parameters.AddWithValue("@StartDate", DateTime.ParseExact(StartPeriod, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@EndDate", DateTime.ParseExact(EndPeriod, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@MasterShopID", ShopId);
                if (ShopList != "")
                    ShopList = ShopList.Substring(0, ShopList.Length - 1);
                cmd.Parameters.AddWithValue("@ShopID", ShopList);

                conn.Open();
                List<ReportSalesProduct> products = new List<ReportSalesProduct>();
                using (SqlDataReader row = cmd.ExecuteReader())
                {
                    //var tb = new DataTable();
                    //tb.Load(dr);
                    while (row.Read())
                    {
                        ReportSalesProduct product = new ReportSalesProduct();
                        product.ProductId = Convert.ToInt32(row["ProductId"]);
                        product.ProductGroup = row["ProductGroupName"].ToString();
                        product.ProductDept = row["ProductDeptName"].ToString();
                        product.ProductName = row["ProductName"].ToString();
                        product.Qty = !string.IsNullOrEmpty(row["Qty"].ToString()) ?  int.Parse(row["Qty"].ToString()) : 0;
                        product.QtyPercent = !string.IsNullOrEmpty(row["QtyPercent"].ToString()) ? int.Parse(row["QtyPercent"].ToString()) : 0; 
                        product.SubTotal = !string.IsNullOrEmpty(row["SubTotal"].ToString()) ? decimal.Parse(row["SubTotal"].ToString()) : 0; 
                        product.SubTotalPercent = !string.IsNullOrEmpty(row["SubTotalPercent"].ToString()) ? decimal.Parse(row["SubTotalPercent"].ToString()) : 0; 
                        product.DiscountValue = !string.IsNullOrEmpty(row["DiscountValue"].ToString()) ? decimal.Parse(row["DiscountValue"].ToString()) : 0; 
                        product.VAT = !string.IsNullOrEmpty(row["VAT"].ToString()) ? decimal.Parse(row["VAT"].ToString()) : 0; 
                        product.ServiceCharge = !string.IsNullOrEmpty(row["ServiceCharge"].ToString()) ? decimal.Parse(row["ServiceCharge"].ToString()) : 0; 
                        product.NetSales = !string.IsNullOrEmpty(row["NetSales"].ToString()) ? decimal.Parse(row["NetSales"].ToString()) : 0; 
                        products.Add(product);
                    }

                }
                conn.Close();

                return Json(new { data = products }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}