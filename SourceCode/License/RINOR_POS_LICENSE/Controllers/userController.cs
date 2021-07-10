using System;
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
    public class userController : Controller
    {
        private ModelLicencePOSDB db = new ModelLicencePOSDB();

        // GET: user
        public ActionResult Index()
        {
            var ms_user = db.pos_user_application.Include(m => m.pos_employee);
            return View(ms_user.ToList());
        }


        // GET: user/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pos_user_application ms_user = db.pos_user_application.Find(id);
            if (ms_user == null)
            {
                return HttpNotFound();
            }
            return View(ms_user);
        }

        // GET: user/Create
        public ActionResult Create()
        {
            ViewBag.employee_id = new SelectList(db.pos_employee, "employee_id", "employee_nik");
            return View();
        }

        // POST: user/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,user_name,user_password,employee_id,fl_active,created_date,created_by,updated_date,updated_by,deleted_date,deleted_by,org_id")] pos_user_application ms_user)
        {
            if (ModelState.IsValid)
            {
                db.pos_user_application.Add(ms_user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employee_id = new SelectList(db.pos_employee, "employee_id", "employee_nik", ms_user.employee_id);
            return View(ms_user);
        }

        // GET: user/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pos_user_application ms_user = db.pos_user_application.Find(id);
            if (ms_user == null)
            {
                return HttpNotFound();
            }
            ViewBag.employee_id = new SelectList(db.pos_employee, "employee_id", "employee_nik", ms_user.employee_id);
            return View(ms_user);
        }

        // POST: user/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,user_name,user_password,employee_id,fl_active,created_date,created_by,updated_date,updated_by,deleted_date,deleted_by,org_id")] pos_user_application ms_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ms_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employee_id = new SelectList(db.pos_employee, "employee_id", "employee_nik", ms_user.employee_id);
            return View(ms_user);
        }

        // GET: user/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pos_user_application ms_user = db.pos_user_application.Find(id);
            if (ms_user == null)
            {
                return HttpNotFound();
            }
            return View(ms_user);
        }

        // POST: user/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pos_user_application ms_user = db.pos_user_application.Find(id);
            db.pos_user_application.Remove(ms_user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
