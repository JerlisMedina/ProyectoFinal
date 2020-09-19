using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaRepuesto.Models;

namespace SistemaRepuesto.Controllers
{
    public class SubMenuController : Controller
    {
        private RepuestoEntities db = new RepuestoEntities();

        // GET: SubMenu
        public ActionResult Index()
        {
            var subMenus = db.SubMenus.Include(s => s.MainMenu).Include(s => s.Role);
            return View(subMenus.ToList());
        }

        // GET: SubMenu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenu subMenu = db.SubMenus.Find(id);
            if (subMenu == null)
            {
                return HttpNotFound();
            }
            return View(subMenu);
        }

        // GET: SubMenu/Create
        public ActionResult Create()
        {
            ViewBag.SubMenuID = new SelectList(db.MainMenus, "MainMenuID", "MainDescripcion");
            ViewBag.SubRoleID = new SelectList(db.Roles, "RoleID", "RoleDescripcion");
            return View();
        }

        // POST: SubMenu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubID,SubDescripcion,SubController,SubAction,SubMenuID,SubRoleID")] SubMenu subMenu)
        {
            if (ModelState.IsValid)
            {
                db.SubMenus.Add(subMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubMenuID = new SelectList(db.MainMenus, "MainMenuID", "MainDescripcion", subMenu.SubMenuID);
            ViewBag.SubRoleID = new SelectList(db.Roles, "RoleID", "RoleDescripcion", subMenu.SubRoleID);
            return View(subMenu);
        }

        // GET: SubMenu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenu subMenu = db.SubMenus.Find(id);
            if (subMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubMenuID = new SelectList(db.MainMenus, "MainMenuID", "MainDescripcion", subMenu.SubMenuID);
            ViewBag.SubRoleID = new SelectList(db.Roles, "RoleID", "RoleDescripcion", subMenu.SubRoleID);
            return View(subMenu);
        }

        // POST: SubMenu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubID,SubDescripcion,SubController,SubAction,SubMenuID,SubRoleID")] SubMenu subMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubMenuID = new SelectList(db.MainMenus, "MainMenuID", "MainDescripcion", subMenu.SubMenuID);
            ViewBag.SubRoleID = new SelectList(db.Roles, "RoleID", "RoleDescripcion", subMenu.SubRoleID);
            return View(subMenu);
        }

        // GET: SubMenu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenu subMenu = db.SubMenus.Find(id);
            if (subMenu == null)
            {
                return HttpNotFound();
            }
            return View(subMenu);
        }

        // POST: SubMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubMenu subMenu = db.SubMenus.Find(id);
            db.SubMenus.Remove(subMenu);
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
