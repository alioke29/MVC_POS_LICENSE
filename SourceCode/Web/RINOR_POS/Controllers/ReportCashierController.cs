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
    /// Report Cashier
    /// </summary>
    [Authorize]
    public class ReportCashierController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();
        /// <summary>
        ///  GET: ReportCashier
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
                cmd.CommandText = "Exec usp_report_cashier @StartDate, @EndDate, @MasterShopID, @ShopID";

                cmd.Parameters.AddWithValue("@StartDate", DateTime.ParseExact(StartPeriod, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@EndDate", DateTime.ParseExact(EndPeriod, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@MasterShopID", ShopId);
                if (ShopList != "")
                    ShopList = ShopList.Substring(0, ShopList.Length - 1);
                cmd.Parameters.AddWithValue("@ShopID", ShopList);

                conn.Open();
                List<ReportCashier> cashierList = new List<ReportCashier>();
                using (SqlDataReader row = cmd.ExecuteReader())
                {
                    //var tb = new DataTable();
                    //tb.Load(dr);
                    while (row.Read())
                    {
                        ReportCashier cashier = new ReportCashier();
                        cashier.IdUser = Convert.ToInt32(row["IdUser"]);
                        cashier.employee_name = row["employee_name"].ToString();
                        cashier.QtyTr = int.Parse(row["QtyTr"].ToString());
                        cashier.QtyProduct = int.Parse(row["QtyProduct"].ToString());
                        cashier.SubTotal = decimal.Parse(row["SubTotal"].ToString());
                        cashier.TotalDiscount = decimal.Parse(row["TotalDiscount"].ToString());
                        cashier.PriceBeforeVAT = decimal.Parse(row["PriceBeforeVAT"].ToString());
                        cashier.TotalVAT = decimal.Parse(row["TotalVAT"].ToString());
                        cashier.TotalServiceCharge = decimal.Parse(row["TotalServiceCharge"].ToString());
                        cashier.PayAmount = decimal.Parse(row["PayAmount"].ToString());
                        cashier.TotalPay = decimal.Parse(row["TotalPay"].ToString());
                        cashier.CashChange = decimal.Parse(row["CashChange"].ToString());
                        cashierList.Add(cashier);
                    }

                }
                conn.Close();

                return Json(new { data = cashierList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}