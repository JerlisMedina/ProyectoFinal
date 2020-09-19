using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaRepuesto.Models;
using System.Web.Helpers;

namespace SistemaRepuesto.Controllers
{
    public class UsuarioController : Controller
    {
        private RepuestoEntities db = new RepuestoEntities();

        // GET: Usuario
        public ActionResult Index()
        {
            var logins = db.Logins.Include(l => l.Role);
            return View(logins.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.LoginRoleID = new SelectList(db.Roles, "RoleID", "RoleDescripcion");
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginID,UserName,Password,ConfirmPassword,PersonalName,Email,LoginRoleID,LoginEstatus")] Login login)
        {
            if (ModelState.IsValid)
            {
                //string pw = Utils.Encriptar(login.Password);
                //login.Password = pw;
                db.Logins.Add(login);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoginRoleID = new SelectList(db.Roles, "RoleID", "RoleDescripcion", login.LoginRoleID);
            return View(login);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginRoleID = new SelectList(db.Roles, "RoleID", "RoleDescripcion", login.LoginRoleID);
            return View(login);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginID,UserName,Password,ConfirmPassword,PersonalName,Email,LoginRoleID,LoginEstatus")] Login login)
        {
            if (ModelState.IsValid)
            {
                //string pw = Utils.Encriptar(login.Password);
                //login.Password = pw;
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginRoleID = new SelectList(db.Roles, "RoleID", "RoleDescripcion", login.LoginRoleID);
            return View(login);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Login login = db.Logins.Find(id);
            db.Logins.Remove(login);
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
