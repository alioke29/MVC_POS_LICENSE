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
    /// <summary>
    /// To Do List
    /// </summary>
    [Authorize]
    public class todolistController : BaseController
    {
        private ModelPOSDB db = new ModelPOSDB();

        /// GET: To Do List
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

            var _qry = (from a in db.pos_todo_list.Where(a => a.DeletedDate == null)
                        select new todolistViewModel
                        {
                            ToDoListID = a.ToDoListID,
                            ShopID = a.ShopID,
                            ToDo = a.ToDo,
                            Description = a.Description,
                            TargetDate = a.TargetDate,
                            PIC = a.PIC,
                            ActionPIC = a.ActionPIC,
                            Status = a.Status
                        }).ToList<todolistViewModel>();

            return Json(new { data = _qry }, JsonRequestBehavior.AllowGet);
        }

        // GET: todolist/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        /// GET: todolist/Detail/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_todo_list ToDoList = db.pos_todo_list.Find(id);

            if (ToDoList == null)
            {
                return HttpNotFound("Credit Card Bank not found.");
            }

            todolistViewModel todolistViewModel = new todolistViewModel()
            {
                ToDoListID = ToDoList.ToDoListID,
                ShopID = ToDoList.ShopID,
                ToDo = ToDoList.ToDo,
                Description = ToDoList.Description,
                TargetDate = ToDoList.TargetDate,
                PIC = ToDoList.PIC,
                ActionPIC = ToDoList.ActionPIC,
                Status = ToDoList.Status
            };

            return View(todolistViewModel);
        }

        /// POST: todolist/Create
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(todolistViewModel ToDoList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_todo_list pos_todo_list = new pos_todo_list();
                    pos_todo_list.ToDoListID = ToDoList.ToDoListID;
                    pos_todo_list.ShopID = ToDoList.ShopID;
                    pos_todo_list.ToDo = ToDoList.ToDo;
                    pos_todo_list.Description = ToDoList.Description;
                    pos_todo_list.TargetDate = ToDoList.TargetDate;
                    pos_todo_list.PIC = ToDoList.PIC;
                    pos_todo_list.ActionPIC = ToDoList.ActionPIC;
                    pos_todo_list.Status = ToDoList.Status;
                    pos_todo_list.CreatedBy = UserProfile.employee_id;
                    pos_todo_list.CreatedDate = DateTime.Now;
                    pos_todo_list = db.pos_todo_list.Add(pos_todo_list);
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

            return View(ToDoList);
        }

        /// GET: todolist/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            pos_todo_list ToDoList = db.pos_todo_list.Find(id);
            if (ToDoList == null)
            {
                return HttpNotFound("Crud not found.");
            }

            todolistViewModel ToDoListView = new todolistViewModel()
            {
                ToDoListID = ToDoList.ToDoListID,
                ShopID = ToDoList.ShopID,
                ToDo = ToDoList.ToDo,
                Description = ToDoList.Description,
                TargetDate = ToDoList.TargetDate,
                PIC = ToDoList.PIC,
                ActionPIC = ToDoList.ActionPIC,
                Status = ToDoList.Status,
                ImageServer = ToDoList.ImageServer,
            };

            return View(ToDoListView);
        }

        /// POST: crud/Edit
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        /// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(todolistViewModel ToDoList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pos_todo_list pos_todo_list = db.pos_todo_list.Find(ToDoList.ToDoListID);
                    pos_todo_list.ToDoListID = ToDoList.ToDoListID;
                    pos_todo_list.ShopID = ToDoList.ShopID;
                    pos_todo_list.ToDo = ToDoList.ToDo;
                    pos_todo_list.Description = ToDoList.Description;
                    pos_todo_list.TargetDate = ToDoList.TargetDate;
                    pos_todo_list.PIC = ToDoList.PIC;
                    pos_todo_list.ActionPIC = ToDoList.ActionPIC;
                    pos_todo_list.Status = ToDoList.Status;
                    pos_todo_list.UpdatedBy = UserProfile.employee_id;
                    pos_todo_list.UpdatedDate = DateTime.Now;
                    db.Entry(pos_todo_list).State = EntityState.Modified;
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

            return View(ToDoList);
        }

        /// POST: todolist/Delete/5
        [HttpGet]
        public JsonResult DeleteItem(int id)
        {
            pos_todo_list pos_todo_list = db.pos_todo_list.Find(id);
            if (pos_todo_list != null)
            {
                pos_todo_list.DeletedBy = UserProfile.UserId;
                pos_todo_list.DeletedDate = DateTime.Now;
                db.Entry(pos_todo_list).State = EntityState.Modified;
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