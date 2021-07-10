namespace RINOR_POS.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using RINOR_POS.Models;

    public class menu_navigationController : Controller
    {
        private ModelPOSDB db = new ModelPOSDB();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuNavigation()
        {
            List<Models.MenuViewModel> ListMenu = new List<Models.MenuViewModel>();


            var result = (from m in db.pos_menu_application where (m.deleted_date == null && m.fl_active == true)
                          join n in db.pos_module_application on m.module_id equals n.module_id
                          join r in db.pos_role_menu.Where(r => r.CanRead == true) on m.menu_id equals r.MenuId
                          join u in db.pos_user_role on r.UserRoleID equals u.UserRoleID
                          join e in db.pos_user_application on u.UserRoleID equals e.UserRoleId
                          where e.user_id == UserProfile.UserId
                          select new
                          {
                              m.menu_id,
                              n.module_name,
                              m.module_id,
                              m.menu_code,
                              m.menu_name,
                              m.menu_url,
                              m.rec_order,
                              r.UserRoleID,
                              r.CanRead,
                              r.CanCreate,
                              r.CanUpdate,
                              r.CanApproval,
                              r.CanDelete
                          }).ToList();

            UserProfile.MenuList = new List<pos_role_menu>();
            foreach (var mn in result)
            {
                Models.MenuViewModel Menu = new Models.MenuViewModel();
                Menu.menu_id = mn.menu_id;
                Menu.menu_code = mn.menu_code;
                Menu.menu_name = mn.menu_name;
                Menu.menu_url = mn.menu_url;
                Menu.module_id = (int)mn.module_id;
                Menu.module_name = mn.module_name;
                Menu.rec_order = (int)mn.rec_order;
                ListMenu.Add(Menu);

                //session untuk menampung hak akses seluruh module
                pos_role_menu objmenu = new pos_role_menu
                {
                    MenuId = mn.menu_id,
                    UserRoleID = mn.UserRoleID,
                    CanRead = mn.CanRead,
                    CanCreate = mn.CanCreate,
                    CanUpdate = mn.CanUpdate,
                    CanApproval = mn.CanApproval,
                    CanDelete = mn.CanDelete
                };
                UserProfile.MenuList.Add(objmenu);
            }

            return PartialView("_Aside", ListMenu.OrderBy(n => n.rec_order));

        }
    }
}