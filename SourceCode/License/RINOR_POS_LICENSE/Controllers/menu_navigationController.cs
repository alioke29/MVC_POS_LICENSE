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
    using RINOR_POS.ModelLicence;

    public class menu_navigationController : Controller
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuNavigation()
        {
            List<ModelLicence.MenuViewModel> ListMenu = new List<ModelLicence.MenuViewModel>();


            var result = (from m in db.pos_menu_application
                          where (m.deleted_date == null && m.fl_active == true)
                          join n in db.pos_module_application on m.module_id equals n.module_id
                          select new
                          {
                              m.menu_id,
                              n.module_name,
                              m.module_id,
                              m.menu_code,
                              m.menu_name,
                              m.menu_url,
                              m.rec_order
                          }).ToList();

            foreach (var mn in result)
            {
                ModelLicence.MenuViewModel Menu = new ModelLicence.MenuViewModel();
                Menu.menu_id = mn.menu_id;
                Menu.menu_code = mn.menu_code;
                Menu.menu_name = mn.menu_name;
                Menu.menu_url = mn.menu_url;
                Menu.module_id = (int)mn.module_id;
                Menu.module_name = mn.module_name;
                Menu.rec_order = (int)mn.rec_order;
                ListMenu.Add(Menu);
            }

            return PartialView("_Aside", ListMenu.OrderBy(n => n.rec_order));

        }
    }
}