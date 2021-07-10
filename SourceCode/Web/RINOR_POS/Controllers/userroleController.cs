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

namespace RINOR_POS.Controllers
{
    [Authorize]
    public class userroleController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        // GET: userrole
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

            var _qry = db.vw_user_role.ToList();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: roleUser/Create
        public ActionResult Create()
        {
            userroleViewModel UserRole = new userroleViewModel()
            {
                merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList(),
                brand_list = new List<pos_brand_data>(),
                shop_list = new List<pos_shop_data>(),
            };

            return View(UserRole);
        }

        // POST: roleUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Role, ShopID")] userroleViewModel roleUserdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_user_role pos_user_role = new pos_user_role();
                    pos_user_role.Role = roleUserdata.Role;
                    pos_user_role.ShopID = roleUserdata.ShopID;
                    pos_user_role.CreatedBy = UserProfile.employee_id;
                    pos_user_role.CreatedDate = DateTime.Now;
                    pos_user_role = db.pos_user_role.Add(pos_user_role);
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

            return View(roleUserdata);
        }

        // GET: roleUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            vw_user_role roleUser_data = (from a in db.vw_user_role.Where(a => a.UserRoleID == id)
                                          select a).FirstOrDefault<vw_user_role>();
            if (roleUser_data == null)
            {
                return HttpNotFound("Crud not found.");
            }

            userroleViewModel roleUserView = new userroleViewModel()
            {
                merchant_list = db.pos_merchant_data.Where(o => o.DeletedDate == null).ToList(),
                brand_list = db.pos_brand_data.Where(o => o.DeletedDate == null).ToList(),
                shop_list = db.pos_shop_data.Where(o => o.DeletedDate == null).ToList(),
                UserRoleID = roleUser_data.UserRoleID,
                Role = roleUser_data.Role,
                MerchantId = roleUser_data.MerchantID,
                BrandId = roleUser_data.BrandID,
                ShopID = roleUser_data.ShopID,
            };

            return View(roleUserView);
        }

        // POST: crud/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserRoleID, Role, ShopID")] userroleViewModel roleUserdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_user_role pos_user_role = db.pos_user_role.Find(roleUserdata.UserRoleID);
                    pos_user_role.Role = roleUserdata.Role;
                    pos_user_role.ShopID = roleUserdata.ShopID;
                    pos_user_role.UpdatedBy = UserProfile.employee_id;
                    pos_user_role.UpdatedDate = DateTime.Now;
                    db.Entry(pos_user_role).State = EntityState.Modified;
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

            return View(roleUserdata);
        }

        // GET: roleUser/Setting/5
        public ActionResult Setting(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            vw_user_role roleUser_data = (from a in db.vw_user_role.Where(a => a.UserRoleID == id)
                                          select a).FirstOrDefault<vw_user_role>();
            if (roleUser_data == null)
            {
                return HttpNotFound("Crud not found.");
            }

            var menulist = (from a in db.vw_menu_app_list.Where(a => a.UserRoleID == 0) select a).ToList();

            List<vw_menu_app_list> MenuList = new List<vw_menu_app_list>();
            foreach (var menudefault in menulist)
            {
                db = new ModelPOSDB();
                var menubyid = (from m in db.vw_menu_app_list.Where(m => m.UserRoleID == id && m.menu_id == menudefault.menu_id) select m).FirstOrDefault();

                if (menubyid != null)
                    MenuList.Add(menubyid);
                else
                    MenuList.Add(menudefault);
            };

            userroleViewModel roleUserView = new userroleViewModel()
            {
                UserRoleID = roleUser_data.UserRoleID,
                Role = roleUser_data.Role,
                MerchantId = roleUser_data.MerchantID,
                MerchantName = roleUser_data.MerchantName,
                BrandId = roleUser_data.BrandID,
                BrandName = roleUser_data.BrandName,
                ShopID = roleUser_data.ShopID,
                ShopName = roleUser_data.ShopName,
                menu_list = MenuList
            };

            return View(roleUserView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setting([Bind(Include = "UserRoleID, menu_list")] userroleViewModel roleUserdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (vw_menu_app_list menulist in roleUserdata.menu_list)
                    {
                        pos_role_menu dataexist = (from a in db.pos_role_menu.Where(a => a.UserRoleID == roleUserdata.UserRoleID && a.MenuId == menulist.menu_id) select a).FirstOrDefault<pos_role_menu>();

                        if (dataexist == null)
                        {
                            pos_role_menu pos_role_menu = new pos_role_menu();
                            pos_role_menu.UserRoleID = roleUserdata.UserRoleID;
                            pos_role_menu.MenuId = menulist.menu_id;
                            pos_role_menu.CanRead = menulist.CanRead;
                            pos_role_menu.CanCreate = menulist.CanCreate;
                            pos_role_menu.CanUpdate = menulist.CanUpdate;
                            pos_role_menu.CanApproval = menulist.CanApproval;
                            pos_role_menu.CanDelete = menulist.CanDelete;
                            pos_role_menu.CreatedBy = UserProfile.employee_id;
                            pos_role_menu.CreatedDate = DateTime.Now;
                            pos_role_menu = db.pos_role_menu.Add(pos_role_menu);
                            db.SaveChanges();
                        }
                        else
                        {
                            pos_role_menu pos_role_menu = db.pos_role_menu.Find(dataexist.RoleMenuAccessId);
                            pos_role_menu.UserRoleID = roleUserdata.UserRoleID;
                            pos_role_menu.MenuId = menulist.menu_id;
                            pos_role_menu.CanRead = menulist.CanRead;
                            pos_role_menu.CanCreate = menulist.CanCreate;
                            pos_role_menu.CanUpdate = menulist.CanUpdate;
                            pos_role_menu.CanApproval = menulist.CanApproval;
                            pos_role_menu.CanDelete = menulist.CanDelete;
                            pos_role_menu.UpdatedBy = UserProfile.employee_id;
                            pos_role_menu.UpdatedDate = DateTime.Now;
                            pos_role_menu = db.pos_role_menu.Add(pos_role_menu);
                            db.Entry(pos_role_menu).State = EntityState.Modified;
                            db.SaveChanges();
                        }
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

            return View(roleUserdata);
        }

        // POST: roleUser/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_user_role pos_user_role = db.pos_user_role.Find(id);
            if (pos_user_role != null)
            {
                pos_user_role.DeletedBy = UserProfile.UserId;
                pos_user_role.DeletedDate = DateTime.Now;
                db.Entry(pos_user_role).State = EntityState.Modified;
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