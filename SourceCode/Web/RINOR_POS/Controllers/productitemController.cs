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
using System.Data.Common;

namespace RINOR_POS.Controllers
{
    [Authorize]
    public class productitemController : Controller
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: Product
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            productItemViewModel productItemdata = (from pd in db.pos_product_dept.Where(pd => pd.ProductDeptID == id)
                                                    join s in db.pos_shop_data on pd.ShopID equals s.ShopID
                                                    join pg in db.pos_product_group on pd.ProductGroupID equals pg.ProductGroupID
                                                    //join p in db.pos_products on pd.ProductDeptID equals p.ProductDeptID
                                                    select new productItemViewModel()
                                                    {
                                                        //ProductID = p.ProductID,
                                                        ShopID = s.ShopID,
                                                        ShopName = s.ShopName,
                                                        ProductGroupID = pg.ProductGroupID,
                                                        ProductGroupName = pg.ProductGroupName,
                                                        ProductDeptID = pd.ProductDeptID,
                                                        ProductDeptName = pd.ProductDeptName
                                                    }
                                                ).FirstOrDefault<productItemViewModel>();

            return View(productItemdata);
        }

        /// <summary>
        /// list for index
        /// </summary>
        /// <returns>JSON</returns>
        public JsonResult GetIndexList(int ProductDeptID)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var _qry = (from pd in db.pos_product_dept.Where(pd => pd.ProductDeptID == ProductDeptID)
                        join pg in db.pos_product_group on pd.ProductGroupID equals pg.ProductGroupID
                        join s in db.pos_shop_data on pd.ShopID equals s.ShopID
                        join p in db.pos_products.Where(p => p.DeletedDate == null) on pd.ProductDeptID equals p.ProductDeptID
                        //into leftpd
                        //from p in leftpd.DefaultIfEmpty()

                        select new productItemViewModel()
                        {
                            ProductID = p.ProductID,
                            ProductDeptID = pd.ProductDeptID,
                            ShopID = s.ShopID,
                            ShopName = s.ShopName,
                            ProductGroupID = pg.ProductGroupID,
                            ProductGroupName = pg.ProductGroupName,
                            ProductCode = p.ProductCode,
                            ProductName = p.ProductName,
                            ProductActivate = p.ProductActivate,
                            ProductOrdering = p.ProductOrdering
                        }).ToList();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        private productItemViewModel getInitDataAdd(productItemViewModel productitem)
        {
            productItemViewModel productitemdata = (from pd in db.pos_product_dept.Where(pd => pd.ProductDeptID == productitem.ProductDeptID)
                                                    join s in db.pos_shop_data on pd.ShopID equals s.ShopID
                                                    join pg in db.pos_product_group on pd.ProductGroupID equals pg.ProductGroupID
                                                    //join p in db.pos_products on b.ProductDeptID equals p.ProductDeptID
                                                    select new productItemViewModel()
                                                    {
                                                        ShopID = s.ShopID,
                                                        ShopName = s.ShopName,
                                                        ProductGroupID = pg.ProductGroupID,
                                                        ProductGroupName = pg.ProductGroupName,
                                                        ProductDeptID = pd.ProductDeptID,
                                                        ProductDeptName = pd.ProductDeptName
                                                    }).FirstOrDefault<productItemViewModel>();

            productitem.product_type_list = db.pos_product_type.Where(o => o.DeletedDate == null).ToList();
            productitem.sale_mode_list = db.pos_sale_mode.Where(o => o.DeletedDate == null).ToList();
            productitem.sale_mode_selected = new List<string>();

            productitem.productsize_list = db.pos_product_size.Where(o => o.DeletedDate == null).ToList();
            productitem.productsize_selected = new List<pos_product_size>();

            productitem.product_salemode_list = db.pos_product_salemode.Where(o => o.ProductID == 0).ToList();
            productitem.product_vattype_list = db.pos_product_vat_type.Where(o => o.DeletedDate == null).ToList();
            productitem.product_vat_list = db.pos_product_vat.Where(o => o.DeletedDate == null).ToList();
            productitem.ShopID = productitemdata.ShopID;
            productitem.ShopName = productitemdata.ShopName;
            productitem.ProductGroupID = productitemdata.ProductGroupID;
            productitem.ProductGroupName = productitemdata.ProductGroupName;
            productitem.ProductDeptID = productitemdata.ProductDeptID;
            productitem.ProductDeptName = productitemdata.ProductDeptName;
            productitem.printer_list = db.pos_printers.Where(o => o.DeletedDate == null).ToList();
            productitem.printer_selected = new List<string>();

            productitem.Sunday = false;
            productitem.Monday = false;
            productitem.Tuesday = false;
            productitem.Wednesday = false;
            productitem.Friday = false;
            productitem.Thursday = false;
            productitem.Saturday = false;

            //request mas roby
            productitem.ProductActivate = true;
            productitem.DisplayMobile = true;

            return productitem;
        }

        // GET: product/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            productItemViewModel product = new productItemViewModel();
            product.ProductDeptID = Convert.ToInt32(id);
            product.isFavorite = false;
            product = getInitDataAdd(product);
            return View(product);
        }

        // POST: product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(productItemViewModel productItemdata)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        pos_products productItem = new pos_products();

                        productItem.ShopID = productItemdata.ShopID;
                        productItem.ProductGroupID = productItemdata.ProductGroupID;
                        productItem.ProductDeptID = productItemdata.ProductDeptID;
                        productItem.ProductCode = productItemdata.ProductCode;
                        productItem.ProductTypeID = productItemdata.ProductTypeID;
                        productItem.ProductName = productItemdata.ProductName;
                        productItem.ProductName1 = productItemdata.ProductName1;
                        productItem.ProductName2 = productItemdata.ProductName2;
                        productItem.ProductName3 = productItemdata.ProductName3;
                        productItem.ProductName4 = productItemdata.ProductName4;
                        productItem.ProductName5 = productItemdata.ProductName5;

                        productItem.ProductEnableDateTime = productItemdata.ProductEnableDateTime;
                        productItem.ProductExpireDateTime = productItemdata.ProductExpireDateTime;
                        productItem.WarningTime = productItemdata.WarningTime;
                        productItem.CriticalTime = productItemdata.CriticalTime;
                        productItem.TimeCriteriaStart = productItemdata.TimeCriteriaStart;
                        productItem.TimeCriteriaEnd = productItemdata.TimeCriteriaEnd;

                        //enable weekly
                        string enableweekly = string.Empty;
                        if (productItemdata.Sunday)
                            enableweekly += "Sunday,";
                        if (productItemdata.Monday)
                            enableweekly += "Monday,";
                        if (productItemdata.Tuesday)
                            enableweekly += "Tuesday,";
                        if (productItemdata.Wednesday)
                            enableweekly += "Wednesday,";
                        if (productItemdata.Thursday)
                            enableweekly += "Thursday,";
                        if (productItemdata.Friday)
                            enableweekly += "Friday,";
                        if (productItemdata.Saturday)
                            enableweekly += "Saturday,";
                        productItem.EnableWeekly = enableweekly;

                        productItem.PrintGroup = productItemdata.PrintGroup;
                        if (productItemdata.printer_selected != null)
                        {
                            string printlist = "";
                            foreach (string print in productItemdata.printer_selected)
                            {
                                printlist += print + ",";
                            }
                            productItem.PrintList = printlist;
                        }

                        productItem.AllowMinusStock = productItemdata.AllowMinusStock;
                        productItem.DiscountAllow = productItemdata.DiscountAllow;
                        productItem.ZeroPriceAllow = productItemdata.ZeroPriceAllow;
                        productItem.AutoComment = productItemdata.AutoComment;
                        productItem.HasServiceCharge = productItemdata.HasServiceCharge;
                        if(productItemdata.ServiceChargePercent != null)
                            productItem.ServiceChargePercent = productItemdata.ServiceChargePercent;

                        productItem.CanReturnProduct = productItemdata.CanReturnProduct;

                        if (productItemdata.ProductActivate == null)
                            productItem.ProductActivate = false;
                        else
                            productItem.ProductActivate = productItemdata.ProductActivate;

                        if (productItemdata.DisplayMobile == null)
                            productItem.DisplayMobile = false;
                        else
                            productItem.DisplayMobile = productItemdata.DisplayMobile;

                        productItem.isFavorite = productItemdata.isFavorite;

                        productItem.ProductOrdering = productItemdata.ProductOrdering;
                        productItem.PrintOrdering = productItemdata.PrintOrdering;

                        productItem.VATCode = productItemdata.VATCode;
                        productItem.VATType = productItemdata.VATType;

                        var files = Request.Files;

                        if (files.Count > 0)
                        {

                            string rootPathFile = Server.MapPath("~/Content/Pictures/Products/");

                            string pathFile = "/Content/Pictures/Products/";
                            string filename = "Prd-" + Guid.NewGuid() + DateTime.Today.ToString("yyMMdd") + ".png";

                            var myFile = Request.Files[0];
                            myFile.SaveAs(rootPathFile + filename);

                            string[] arraypath = (pathFile + filename).Split('/');
                            productItem.ProductPictureServer = pathFile + filename;

                            if (arraypath.Length > 0)
                                productItem.ProductPictureClient = arraypath[arraypath.Length - 1].ToString();
                        }
                        else
                        {
                            if (productItemdata.ProductPictureServer != null && productItemdata.ProductPictureServer != string.Empty)
                            {
                                string pathfile = ProcessImage(productItemdata.ProductPictureServer);
                                string[] arraypath = pathfile.Split('/');
                                productItem.ProductPictureServer = pathfile;
                                if (arraypath.Length > 0)
                                    productItem.ProductPictureClient = arraypath[arraypath.Length - 1].ToString();
                            }
                        }

                        productItem.CreatedBy = UserProfile.UserId;
                        productItem.CreatedDate = DateTime.Now;
                        productItem = db.pos_products.Add(productItem);
                        db.SaveChanges();

                        foreach (string salemodeid in productItemdata.sale_mode_selected)
                        {
                            //insert to pos_product_salemode
                            pos_product_salemode productsalemode = new pos_product_salemode();
                            productsalemode.ProductID = productItem.ProductID;
                            productsalemode.SaleModeID = Convert.ToInt32(salemodeid);
                            productsalemode.Activate = true;
                            productsalemode = db.pos_product_salemode.Add(productsalemode);
                            db.SaveChanges();
                        }
                        transaction.Commit();
                        return RedirectToAction("Index", new { id = productItem.ProductDeptID });
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
            productItemdata = getInitDataAdd(productItemdata);
            return View(productItemdata);
        }

        /// <summary>
        /// Process image and save in predefined path
        /// </summary>
        /// <param name="croppedImage">
        /// <returns></returns>
        private string ProcessImage(string croppedImage)
        {
            string filePath = String.Empty;
            try
            {
                string base64 = croppedImage;
                byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);

                //filePath = "/Content/Pictures/Products/Prd-" + Guid.NewGuid() + DateTime.Today.ToString("yyMMdd") + ".png";
                filePath = Server.MapPath("~/Content/Pictures/Products/") + "Prd-" + Guid.NewGuid() + DateTime.Today.ToString("yyMMdd") + ".png";

                //using (FileStream stream = new FileStream(UserProfile.PathFolder + filePath, FileMode.Create))
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                string st = ex.Message;
            }

            return filePath;
        }

        // GET: product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string urlbase = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            productItemViewModel productdata = (from p in db.pos_products.Where(p => p.ProductID == id)
                                                join a in db.pos_product_dept on p.ProductDeptID equals a.ProductDeptID
                                                join b in db.pos_product_group on a.ProductGroupID equals b.ProductGroupID
                                                join c in db.pos_shop_data on a.ShopID equals c.ShopID
                                                select new productItemViewModel()
                                                {
                                                    ProductID = p.ProductID,
                                                    ShopID = p.ShopID,
                                                    ShopName = c.ShopName,
                                                    ProductGroupID = p.ProductGroupID,
                                                    ProductGroupName = b.ProductGroupName,
                                                    ProductDeptID = a.ProductDeptID,
                                                    ProductDeptName = a.ProductDeptName,

                                                    ProductTypeID = p.ProductTypeID,
                                                    ProductCode = p.ProductCode,
                                                    ProductName = p.ProductName,
                                                    ProductName1 = p.ProductName1,
                                                    ProductName2 = p.ProductName2,
                                                    ProductName3 = p.ProductName3,
                                                    ProductName4 = p.ProductName4,
                                                    ProductName5 = p.ProductName5,
                                                    ProductDesp = p.ProductDesp,
                                                    EnableWeekly = p.EnableWeekly,
                                                    PrintGroup = p.PrintGroup,
                                                    PrintList = p.PrintList,
                                                    AllowMinusStock = p.AllowMinusStock,
                                                    DiscountAllow = p.DiscountAllow,
                                                    ZeroPriceAllow = p.ZeroPriceAllow,
                                                    AutoComment = p.AutoComment,
                                                    HasServiceCharge = p.HasServiceCharge,
                                                    ServiceChargePercent = p.ServiceChargePercent,
                                                    CanReturnProduct = p.CanReturnProduct,
                                                    VATType = p.VATType,
                                                    VATCode = p.VATCode,
                                                    ProductEnableDateTime = p.ProductEnableDateTime,
                                                    ProductExpireDateTime = p.ProductExpireDateTime,
                                                    WarningTime = p.WarningTime,
                                                    CriticalTime = p.CriticalTime,
                                                    TimeCriteriaStart = p.TimeCriteriaStart,
                                                    TimeCriteriaEnd = p.TimeCriteriaEnd,
                                                    ProductActivate = p.ProductActivate,
                                                    DisplayMobile = p.DisplayMobile,
                                                    isFavorite = p.isFavorite,
                                                    ProductOrdering = p.ProductOrdering,
                                                    PrintOrdering = p.PrintOrdering,
                                                    ProductPictureServer = urlbase + p.ProductPictureServer
                                                }).FirstOrDefault<productItemViewModel>();
            if (productdata == null)
            {
                return HttpNotFound("Product not found.");
            }

            productItemViewModel product = new productItemViewModel();

            product.product_type_list = db.pos_product_type.Where(o => o.DeletedDate == null).ToList();
            product.sale_mode_list = db.pos_sale_mode.Where(o => o.DeletedDate == null).ToList();
            product.sale_mode_selected = new List<string>();
            product.product_salemode_list = db.pos_product_salemode.Where(o => o.ProductID == productdata.ProductID).ToList();
            product.product_vattype_list = db.pos_product_vat_type.Where(o => o.DeletedDate == null).ToList();
            product.product_vat_list = db.pos_product_vat.Where(o => o.DeletedDate == null).ToList();
            product.printer_list = db.pos_printers.Where(o => o.DeletedDate == null).ToList();
            product.printer_selected = new List<string>();

            product.ProductID = productdata.ProductID;
            product.ShopID = productdata.ShopID;
            product.ShopName = productdata.ShopName;
            product.ProductGroupID = productdata.ProductGroupID;
            product.ProductGroupName = productdata.ProductGroupName;
            product.ProductDeptID = productdata.ProductDeptID;
            product.ProductDeptName = productdata.ProductDeptName;

            product.ProductTypeID = productdata.ProductTypeID;
            product.ProductCode = productdata.ProductCode;
            product.ProductName = productdata.ProductName;
            product.ProductName1 = productdata.ProductName1;
            product.ProductName2 = productdata.ProductName2;
            product.ProductName3 = productdata.ProductName3;
            product.ProductName4 = productdata.ProductName4;
            product.ProductName5 = productdata.ProductName5;
            product.ProductDesp = productdata.ProductDesp;
            product.EnableWeekly = productdata.EnableWeekly;

            product.Sunday = (productdata.EnableWeekly.Contains("Sunday"));
            product.Monday = (productdata.EnableWeekly.Contains("Monday"));
            product.Tuesday = (productdata.EnableWeekly.Contains("Tuesday"));
            product.Wednesday = (productdata.EnableWeekly.Contains("Wednesday"));
            product.Thursday = (productdata.EnableWeekly.Contains("Thursday"));
            product.Friday = (productdata.EnableWeekly.Contains("Friday"));
            product.Saturday = (productdata.EnableWeekly.Contains("Saturday"));

            product.PrintGroup = productdata.PrintGroup;
            product.PrintList = productdata.PrintList;
            product.AllowMinusStock = productdata.AllowMinusStock;
            product.DiscountAllow = productdata.DiscountAllow;
            product.ZeroPriceAllow = productdata.ZeroPriceAllow;
            product.AutoComment = productdata.AutoComment;
            product.HasServiceCharge = productdata.HasServiceCharge;
            product.ServiceChargePercent = productdata.ServiceChargePercent;
            product.CanReturnProduct = productdata.CanReturnProduct;
            product.VATType = productdata.VATType;
            product.VATCode = productdata.VATCode;
            product.ProductEnableDateTime = productdata.ProductEnableDateTime;
            product.ProductExpireDateTime = productdata.ProductExpireDateTime;
            product.WarningTime = productdata.WarningTime;
            product.CriticalTime = productdata.CriticalTime;
            product.TimeCriteriaStart = productdata.TimeCriteriaStart;
            product.TimeCriteriaEnd = productdata.TimeCriteriaEnd;
            product.ProductActivate = productdata.ProductActivate;
            product.DisplayMobile = productdata.DisplayMobile;
            product.ProductOrdering = productdata.ProductOrdering;
            product.PrintOrdering = productdata.PrintOrdering;
            product.ProductPictureServer = productdata.ProductPictureServer;
            product.isFavorite = productdata.isFavorite;

            return View(product);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(productItemViewModel productItemdata)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        pos_products productItem = db.pos_products.Find(productItemdata.ProductID);
                        productItem.ShopID = productItemdata.ShopID;
                        productItem.ProductGroupID = productItemdata.ProductGroupID;
                        productItem.ProductDeptID = productItemdata.ProductDeptID;
                        productItem.ProductCode = productItemdata.ProductCode;
                        productItem.ProductTypeID = productItemdata.ProductTypeID;
                        productItem.ProductName = productItemdata.ProductName;
                        productItem.ProductName1 = productItemdata.ProductName1;
                        productItem.ProductName2 = productItemdata.ProductName2;
                        productItem.ProductName3 = productItemdata.ProductName3;
                        productItem.ProductName4 = productItemdata.ProductName4;
                        productItem.ProductName5 = productItemdata.ProductName5;

                        productItem.ProductEnableDateTime = productItemdata.ProductEnableDateTime;
                        productItem.ProductExpireDateTime = productItemdata.ProductExpireDateTime;
                        productItem.WarningTime = productItemdata.WarningTime;
                        productItem.CriticalTime = productItemdata.CriticalTime;
                        productItem.TimeCriteriaStart = productItemdata.TimeCriteriaStart;
                        productItem.TimeCriteriaEnd = productItemdata.TimeCriteriaEnd;

                        //enable weekly
                        string enableweekly = string.Empty;
                        if (productItemdata.Sunday)
                            enableweekly += "Sunday,";
                        if (productItemdata.Monday)
                            enableweekly += "Monday,";
                        if (productItemdata.Tuesday)
                            enableweekly += "Tuesday,";
                        if (productItemdata.Wednesday)
                            enableweekly += "Wednesday,";
                        if (productItemdata.Thursday)
                            enableweekly += "Thursday,";
                        if (productItemdata.Friday)
                            enableweekly += "Friday,";
                        if (productItemdata.Saturday)
                            enableweekly += "Saturday,";
                        productItem.EnableWeekly = enableweekly;

                        productItem.PrintGroup = productItemdata.PrintGroup;
                        if (productItemdata.printer_selected != null)
                        {
                            string printlist = "";
                            foreach (string print in productItemdata.printer_selected)
                            {
                                printlist += print + ",";
                            }
                            productItem.PrintList = printlist;
                        }
                        productItem.AllowMinusStock = productItemdata.AllowMinusStock;
                        productItem.DiscountAllow = productItemdata.DiscountAllow;
                        productItem.ZeroPriceAllow = productItemdata.ZeroPriceAllow;
                        productItem.AutoComment = productItemdata.AutoComment;
                        productItem.HasServiceCharge = productItemdata.HasServiceCharge;
                        if (productItemdata.ServiceChargePercent != null)
                            productItem.ServiceChargePercent = productItemdata.ServiceChargePercent;
                        productItem.CanReturnProduct = productItemdata.CanReturnProduct;

                        if (productItemdata.ProductActivate == null)
                            productItem.ProductActivate = false;
                        else
                            productItem.ProductActivate = productItemdata.ProductActivate;

                        if (productItemdata.DisplayMobile == null)
                            productItem.DisplayMobile = false;
                        else
                            productItem.DisplayMobile = productItemdata.DisplayMobile;

                        productItem.ProductOrdering = productItemdata.ProductOrdering;
                        productItem.PrintOrdering = productItemdata.PrintOrdering;

                        productItem.VATCode = productItemdata.VATCode;
                        productItem.VATType = productItemdata.VATType;
                        productItem.isFavorite = productItemdata.isFavorite;

                        var files = Request.Files;

                        if (files.Count > 0)
                        {

                            string rootPathFile = Server.MapPath("~/Content/Pictures/Products/");

                            string pathFile = "/Content/Pictures/Products/";
                            string filename = "Prd-" + Guid.NewGuid() + DateTime.Today.ToString("yyMMdd") + ".png";

                            var myFile = Request.Files[0];
                            myFile.SaveAs(rootPathFile + filename);

                            string[] arraypath = (pathFile + filename).Split('/');
                            productItem.ProductPictureServer = pathFile + filename;

                            if (arraypath.Length > 0)
                                productItem.ProductPictureClient = arraypath[arraypath.Length - 1].ToString();
                        }
                        else
                        {
                            if (productItemdata.ProductPictureServer != null && productItemdata.ProductPictureServer != string.Empty)
                            {
                                string pathfile = ProcessImage(productItemdata.ProductPictureServer);
                                string[] arraypath = pathfile.Split('/');
                                productItem.ProductPictureServer = pathfile;
                                if (arraypath.Length > 0)
                                    productItem.ProductPictureClient = arraypath[arraypath.Length - 1].ToString();
                            }
                        }

                        productItem.UpdatedDate = DateTime.Now;
                        productItem.UpdatedBy = UserProfile.UserId;

                        db.Entry(productItem).State = EntityState.Modified;
                        db.SaveChanges();

                        //Delete pos_product_salemode
                        List<pos_product_salemode> prodsalemodesdel = db.pos_product_salemode.Where(o => o.ProductID == productItemdata.ProductID).ToList<pos_product_salemode>();
                        foreach (pos_product_salemode datadelete in prodsalemodesdel)
                        {
                            db.Entry(datadelete).State = EntityState.Deleted;
                            db.SaveChanges();
                        }
                        foreach (string salemodeid in productItemdata.sale_mode_selected)
                        {
                            //insert to pos_product_salemode
                            pos_product_salemode productsalemode = new pos_product_salemode();
                            productsalemode.ProductID = productItemdata.ProductID;
                            productsalemode.SaleModeID = Convert.ToInt32(salemodeid);
                            productsalemode.Activate = true;
                            productsalemode = db.pos_product_salemode.Add(productsalemode);
                            db.SaveChanges();
                        }
                        transaction.Commit();

                        return RedirectToAction("Index", new { id = productItemdata.ProductDeptID });
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
            productItemdata.product_type_list = db.pos_product_type.Where(o => o.DeletedDate == null).ToList();
            productItemdata.sale_mode_list = db.pos_sale_mode.Where(o => o.DeletedDate == null).ToList();
            productItemdata.sale_mode_selected = new List<string>();
            productItemdata.product_salemode_list = db.pos_product_salemode.Where(o => o.ProductID == productItemdata.ProductID).ToList();
            productItemdata.product_vattype_list = db.pos_product_vat_type.Where(o => o.DeletedDate == null).ToList();
            productItemdata.product_vat_list = db.pos_product_vat.Where(o => o.DeletedDate == null).ToList();
            productItemdata.printer_list = db.pos_printers.Where(o => o.DeletedDate == null).ToList();
            productItemdata.printer_selected = new List<string>();

            return View(productItemdata);
        }
        // GET: product/Size/5
        public ActionResult Size(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var productsize = (from p in db.pos_products.Where(p => p.ProductID == id)
                               join ps in db.pos_product_size.Where(ps => ps.DeletedDate == null) on p.ProductID equals ps.ProductID
                               into ps_left
                               from ps in ps_left.DefaultIfEmpty()

                               select new productItemViewModel()
                               {
                                   ProductID = p.ProductID,
                                   ProductDeptID = p.ProductDeptID,
                                   ShopID = p.ShopID,
                                   ProductTypeID = p.ProductTypeID,
                                   ProductCode = p.ProductCode,
                                   ProductName = p.ProductName,
                                   ProductSizeId = ps.ProductSizeID,
                                   Size = ps.Size,
                                   AdditionalPrice = ps.AdditionalPrice
                               }).ToList<productItemViewModel>();
           
            return View(productsize);
        }
        /// POST: crud/Size/
        [HttpGet]
        public JsonResult SizeChange(int ProductId, String Size, decimal Price = 0)
        {
            var productsizelist = db.pos_product_size.Where(a => a.ProductID == ProductId && a.Size == Size && a.DeletedDate == null).ToList();
            if (productsizelist.Count == 0)
            {
                //insert
                pos_product_size data = new pos_product_size();
                data.ProductID = ProductId;
                data.Size = Size;
                data.AdditionalPrice = Price;
                data.CreatedBy = UserProfile.employee_id;
                data.CreatedDate = DateTime.Now;
                data = db.pos_product_size.Add(data);
                db.SaveChanges();
                return Json(data.ProductSizeID, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        /// POST: Size/Delete/5
        [HttpGet]
        public JsonResult SizeDelete(int ProductSizeID)
        {
            //delete
            pos_product_size data = db.pos_product_size.Find(ProductSizeID);

            if (data != null)
            {
                data.DeletedBy = UserProfile.employee_id;
                data.DeletedDate = DateTime.Now;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: product/Edit/5
        public ActionResult Department(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            productDeptViewModel productDeptdata = (from pd in db.pos_product_dept.Where(pd => pd.ProductGroupID == id)
                                                    join s in db.pos_shop_data on pd.ShopID equals s.ShopID
                                                    join pg in db.pos_product_group on pd.ProductGroupID equals pg.ProductGroupID

                                                    select new productDeptViewModel()
                                                    {
                                                        ProductDeptID = pd.ProductDeptID,
                                                        ShopID = s.ShopID,
                                                        ShopName = s.ShopName,
                                                        ProductGroupID = pg.ProductGroupID,
                                                        ProductGroupName = pg.ProductGroupName,
                                                        ProductDeptCode = pd.ProductDeptCode,
                                                        ProductDeptName = pd.ProductDeptName,
                                                        ProductDeptActivate = pd.ProductDeptActivate,
                                                        ProductDeptOrdering = pd.ProductDeptOrdering
                                                    }
                                                ).FirstOrDefault<productDeptViewModel>();
            if (productDeptdata == null)
            {
                return HttpNotFound("Product department not found.");
            }

            return View(productDeptdata);
        }

        // POST: crud/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_products products = db.pos_products.Find(id);
            if (products != null)
            {
                products.DeletedBy = UserProfile.UserId;
                products.DeletedDate = DateTime.Now;
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();

                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}