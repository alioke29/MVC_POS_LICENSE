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
    /// Report Salemode
    /// </summary>
    [Authorize]
    public class ReportSalemodeController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();
        /// <summary>
        ///  GET: Report Salemode
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
                cmd.CommandText = "Exec usp_report_salemodebyproduct @StartDate, @EndDate, @MasterShopID, @ShopID";

                cmd.Parameters.AddWithValue("@StartDate", DateTime.ParseExact(StartPeriod, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@EndDate", DateTime.ParseExact(EndPeriod, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@MasterShopID", ShopId);
                if (ShopList != "")
                    ShopList = ShopList.Substring(0, ShopList.Length - 1);
                cmd.Parameters.AddWithValue("@ShopID", ShopList);

                conn.Open();
                List<ReportSalemode> salesproducts = new List<ReportSalemode>();
                using (SqlDataReader row = cmd.ExecuteReader())
                {
                    //var tb = new DataTable();
                    //tb.Load(dr);
                    while (row.Read())
                    {
                        ReportSalemode salesproduct = new ReportSalemode();
                        salesproduct.SaleModeId = Convert.ToInt32(row["SaleModeId"]);
                        salesproduct.SaleModeName = row["SaleModeName"].ToString();
                        salesproduct.ProductGroupName = row["ProductGroupName"].ToString();
                        salesproduct.ProductDeptName = row["ProductDeptName"].ToString();
                        salesproduct.ProductName = row["ProductName"].ToString();
                        salesproduct.Qty = int.Parse(row["Qty"].ToString());
                        salesproduct.QtyPercent = int.Parse(row["QtyPercent"].ToString());
                        salesproduct.SubTotal = decimal.Parse(row["Subtotal"].ToString());
                        salesproduct.SubTotalPercent = decimal.Parse(row["SubtotalPercent"].ToString());
                        salesproduct.DiscountValue = decimal.Parse(row["DiscountValue"].ToString());
                        salesproduct.VAT = decimal.Parse(row["VAT"].ToString());
                        salesproduct.ServiceCharge = decimal.Parse(row["ServiceCharge"].ToString());
                        salesproduct.NetSales = decimal.Parse(row["NetSales"].ToString());
                        salesproducts.Add(salesproduct);
                    }
                }
                conn.Close();

                return Json(new { data = salesproducts }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}