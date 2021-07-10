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
    /// Report Product
    /// </summary>
    [Authorize]
    public class ReportProductController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();
        /// <summary>
        ///  GET: Report Product
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ReportsViewModel report = new ReportsViewModel();
            report.StartPeriod = DateTime.Today;
            report.EndPeriod = DateTime.Today;
            report.shop_list = (from s in db.pos_shop_data.Where(s => s.MasterShop == true) select s).ToList<pos_shop_data>();
            return View(report);
        }

        /// <summary>
        /// list for index
        /// </summary>
        /// <returns>JSON</returns>
        public JsonResult GetIndexList(string StartPeriod, string EndPeriod, string ShopId = "")
        {
            db.Configuration.ProxyCreationEnabled = false;

            try
            {
                string constring = WebConfigurationManager.ConnectionStrings["ModelPOSDB"].ConnectionString;
                var conn = new SqlConnection(constring);

                var cmd = new SqlCommand("Report", conn);
                cmd.CommandText = "Exec usp_report_product @StartDate, @EndDate, @MasterShopID";

                cmd.Parameters.AddWithValue("@StartDate", DateTime.ParseExact(StartPeriod, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@EndDate", DateTime.ParseExact(EndPeriod, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@MasterShopID", ShopId);

                conn.Open();
                List<ReportProduct> products = new List<ReportProduct>();
                using (SqlDataReader row = cmd.ExecuteReader())
                {
                    //var tb = new DataTable();
                    //tb.Load(dr);
                    while (row.Read())
                    {
                        ReportProduct product = new ReportProduct();
                        product.ShopName = row["ShopName"].ToString();
                        product.ProductGroup = row["ProductGroupName"].ToString();
                        product.ProductDept = row["ProductDeptName"].ToString();
                        product.ProductName = row["ProductName"].ToString();
                        product.ProductBarcode = row["ProductBarcode"].ToString();
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