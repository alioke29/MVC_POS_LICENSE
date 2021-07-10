using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RINOR_POS;
using System.Web.Mvc;

namespace RINOR_POS.Models
{
    public class ReportsViewModel
    {
        public ReportsViewModel()
        {
            shop_list = new List<pos_shop_data>();
            shop_selected = new List<string>();
            shop_available = new List<string>();
        }

        [Display(Name = "Master Shop")]
        public int ShopId { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public List<pos_shop_data> shop_list { get; set; }
        public List<string> shop_available { get; set; }
        public List<string> shop_selected { get; set; }
    }
    public class ReportSalesProduct
    {
        public int ProductId { get; set; }
        public string ProductGroup { get; set; }
        public string ProductDept { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public int QtyPercent { get; set; }
        public decimal SubTotal { get; set; }
        public decimal SubTotalPercent { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal VAT { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal NetSales { get; set; }
    }
    public class ReportPayment
    {
        public int IdPaymentTypeItem { get; set; }
        public string PayGroupName { get; set; }
        public string PayTypeCode { get; set; }
        public string DisplayName { get; set; }
        public int CountPay { get; set; }
        public decimal PayAmount { get; set; }
    }
    public class ReportCashier
    {
        public int IdUser { get; set; }
        public string employee_name { get; set; }
        public int QtyTr { get; set; }
        public decimal QtyProduct { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal PriceBeforeVAT { get; set; }
        public decimal TotalVAT { get; set; }
        public decimal TotalServiceCharge { get; set; }
        public decimal PayAmount { get; set; }
        public decimal CashChange { get; set; }
        public decimal TotalPay { get; set; }
    }
    public class ReportHourly
    {
        public string Hour { get; set; }
        public int Bill { get; set; }
        public int Customers { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalDisc { get; set; }
        public decimal TotalSC { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalVAT  { get; set; }
        public decimal Rounding { get; set; }
        public decimal TotalPayment { get; set; }
    }
    public class ReportSalemode
    {
        public int SaleModeId { get; set; }
        public string SaleModeName { get; set; }
        public string ProductGroupName { get; set; }
        public string ProductDeptName { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public int QtyPercent { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public decimal SubTotalPercent { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal VAT { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal NetSales { get; set; }
    }
    public class ReportProduct
    {
        public int ProductId { get; set; }
        public string ShopName { get; set; }
        public string ProductGroup { get; set; }
        public string ProductDept { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductBarcode { get; set; }
        public int ProductOrdering { get; set; }
    }
    public class ReportPromotion
    {
        public int PromotionId { get; set; }
        public string ShopName { get; set; }
        public string PromotionType { get; set; }
        public string PromotionCode { get; set; }
        public string PromotionName { get; set; }
        public string WeeklyCondition { get; set; }
        public string DayCondition { get; set; }
        public int Priority { get; set; }
    }
    public class ReportDiscount
    {
        public string sessionId { get; set; }
        public string ShopName { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalDiscount { get; set; }
    }
    public class ReportUser
    {
        public string ShopName { get; set; }
        public string employee_nik { get; set; }
        public string employee_name { get; set; }
        public string user_name { get; set; }
        public string Role { get; set; }
    }
    public class ReportTop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
    }
}