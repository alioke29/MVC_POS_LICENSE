using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RINOR_POS.ModelLicence;

namespace RINOR_POS.Controllers
{
    [Authorize]
    public class employeeController : Controller
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// list for index
        /// </summary>
        /// <returns>JSON</returns>
        public JsonResult GetIndexList()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var _qry = db.vw_employee.ToList();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: roleUser/Create
        public ActionResult Create()
        {
            employeeViewModel Employee = new employeeViewModel()
            {
                merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList(),
                brand_list = new List<pos_brand_data>(),
                shop_list = new List<pos_shop_data>(),
            };

            return View(Employee);
        }

        // POST: roleUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employee_nik, ShopID, employee_name, employee_email, fl_active")] employeeViewModel employeedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_employee pos_employee = new pos_employee();
                    pos_employee.employee_nik = employeedata.employee_nik;
                    pos_employee.ShopId = employeedata.ShopID;
                    pos_employee.employee_name = employeedata.employee_name;
                    pos_employee.employee_email = employeedata.employee_email;
                    pos_employee.fl_active = employeedata.fl_active;
                    pos_employee.created_by = UserProfile.employee_id;
                    pos_employee.created_date = DateTime.Now;
                    pos_employee = db.pos_employee.Add(pos_employee);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(employeedata);
        }

        // GET: roleUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            vw_employee emp_data = (from a in db.vw_employee.Where(a => a.employee_id == id)
                                    select a).FirstOrDefault<vw_employee>();
            if (emp_data == null)
            {
                return HttpNotFound("Crud not found.");
            }

            employeeViewModel employeeView = new employeeViewModel()
            {
                merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList(),
                brand_list = db.pos_brand_data.Where(o => o.DeletedDate == null).ToList(),
                shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null).ToList(),
                employee_id = emp_data.employee_id,
                employee_nik = emp_data.employee_nik,
                employee_name = emp_data.employee_name,
                employee_email = emp_data.employee_email,
                MerchantId = emp_data.MerchantID,
                BrandId = emp_data.BrandID,
                ShopID = Convert.ToInt32(emp_data.ShopId),
                fl_active = emp_data.fl_active
            };

            return View(employeeView);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "employee_id, employee_nik, ShopID, employee_name, employee_email, fl_active")] employeeViewModel employeedata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_employee pos_employee = db.pos_employee.Find(employeedata.employee_id);
                    pos_employee.employee_nik = employeedata.employee_nik;
                    pos_employee.ShopId = employeedata.ShopID;
                    pos_employee.employee_name = employeedata.employee_name;
                    pos_employee.employee_email = employeedata.employee_email;
                    pos_employee.fl_active = employeedata.fl_active;
                    pos_employee.updated_by = UserProfile.employee_id;
                    pos_employee.updated_date = DateTime.Now;
                    db.Entry(pos_employee).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(employeedata);
        }

        // GET: roleUser/Setting/5
        public ActionResult Setting(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var checkUser = db.pos_user_application.Where(a => a.employee_id == id).ToList();

            user_accountViewModel user_app = new user_accountViewModel();
            if (checkUser.Count > 0)
                user_app = (from a in db.pos_employee.Where(a => a.employee_id == id)
                            join usr in db.pos_user_application on a.employee_id equals usr.employee_id into usrleft
                            from b in usrleft.DefaultIfEmpty()

                            select new user_accountViewModel
                            {
                                employee_id = a.employee_id,
                                employee_name = a.employee_name,
                                employee_nik = a.employee_nik,
                                user_id = b.user_id,
                                user_name = b.user_name,
                                user_password = b.user_password,
                                fl_active = b.fl_active,
                                UserRoleId = b.UserRoleId
                            }).FirstOrDefault<user_accountViewModel>();
            else
                user_app = (from a in db.pos_employee.Where(a => a.employee_id == id)
                            select new user_accountViewModel
                            {
                                employee_id = a.employee_id,
                                employee_name = a.employee_name,
                                employee_nik = a.employee_nik,
                                user_id = 0,
                                user_name = "",
                                fl_active = false,
                                UserRoleId = 0
                            }).FirstOrDefault<user_accountViewModel>();
            if (user_app == null)
            {
                return HttpNotFound("Crud not found.");
            }

            user_accountViewModel userView = new user_accountViewModel()
            {
                employee_id = user_app.employee_id,
                employee_name = user_app.employee_name,
                employee_nik = user_app.employee_nik,
                user_id = user_app.user_id,
                user_name = user_app.user_name,
                user_password = "",
                fl_active = user_app.fl_active,
                UserRoleId = user_app.UserRoleId,
                userrole_list = (from a in db.pos_user_role.Where(a => a.DeletedDate == null)
                                 select a).ToList<pos_user_role>()
            };

            return View(userView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setting([Bind(Include = "employee_id, user_id, user_name, user_password, fl_active, UserRoleId")] user_accountViewModel UserAccountData)
        {
            try
            {
                var checkUserName = db.pos_user_application.Where(a => a.user_name == UserAccountData.user_name && a.employee_id != UserAccountData.employee_id).ToList();
                if (checkUserName.Count > 0)
                    ModelState.AddModelError("user_name", "User Name Already Exist.");

                if (ModelState.IsValid)
                {
                    var checkexist = db.pos_user_application.Where(a => a.user_name == UserAccountData.user_name && a.employee_id == UserAccountData.employee_id).ToList();
                    if (checkexist.Count > 0)
                    {
                        //update
                        pos_user_application user_app = db.pos_user_application.Find(UserAccountData.user_id);
                        user_app.fl_active = UserAccountData.fl_active;
                        user_app.UserRoleId = UserAccountData.UserRoleId;
                        user_app.updated_by = UserProfile.employee_id;
                        user_app.updated_date = DateTime.Now;
                        db.Entry(user_app).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        //add
                        pos_user_application user_app = new pos_user_application();
                        user_app.employee_id = UserAccountData.employee_id;
                        user_app.user_id = UserAccountData.user_id;
                        user_app.user_name = UserAccountData.user_name;
                        user_app.user_password = UserAccountData.user_password;
                        if (UserAccountData.fl_active == null)
                            user_app.fl_active = false;
                        else
                            user_app.fl_active = UserAccountData.fl_active;
                        user_app.UserRoleId = UserAccountData.UserRoleId;
                        user_app.created_by = UserProfile.employee_id;
                        user_app.created_date = DateTime.Now;
                        user_app = db.pos_user_application.Add(user_app);
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string msgErr = string.Format("An unknown error has occurred , Please contact your system administrator. {0}", ex.Message);
                if (ex.InnerException != null)
                    msgErr += string.Format(" Inner Exception: {0}", ex.InnerException.Message);
                ModelState.AddModelError("", msgErr);
            }

            return View(UserAccountData);
        }

        // POST: roleUser/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_employee pos_employee = db.pos_employee.Find(id);
            if (pos_employee != null)
            {
                pos_employee.deleted_by = UserProfile.UserId;
                pos_employee.deleted_date = DateTime.Now;
                db.Entry(pos_employee).State = EntityState.Modified;
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