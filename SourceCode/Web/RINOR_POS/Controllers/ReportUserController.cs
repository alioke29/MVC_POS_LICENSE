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
    /// Report User
    /// </summary>
    [Authorize]
    public class ReportUserController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();
        /// <summary>
        ///  GET: Report Discount
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
        public JsonResult GetIndexList(string ShopId = "", string ShopList = "")
        {
            db.Configuration.ProxyCreationEnabled = false;

            try
            {
                string constring = WebConfigurationManager.ConnectionStrings["ModelPOSDB"].ConnectionString;
                var conn = new SqlConnection(constring);

                var cmd = new SqlCommand("Report", conn);
                cmd.CommandText = "Exec usp_report_user @MasterShopID, @ShopID";

                cmd.Parameters.AddWithValue("@MasterShopID", ShopId);
                if (ShopList != "")
                    ShopList = ShopList.Substring(0, ShopList.Length - 1);
                cmd.Parameters.AddWithValue("@ShopID", ShopList);

                conn.Open();
                List<ReportUser> Users = new List<ReportUser>();
                using (SqlDataReader row = cmd.ExecuteReader())
                {
                    //var tb = new DataTable();
                    //tb.Load(dr);
                    while (row.Read())
                    {
                        ReportUser user = new ReportUser();
                        user.ShopName = row["ShopName"].ToString();
                        user.employee_nik = row["employee_nik"].ToString();
                        user.employee_name = row["employee_name"].ToString();
                        user.user_name = row["user_name"].ToString();
                        user.Role = row["Role"].ToString();
                        Users.Add(user);
                    }

                }
                conn.Close();

                return Json(new { data = Users }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}