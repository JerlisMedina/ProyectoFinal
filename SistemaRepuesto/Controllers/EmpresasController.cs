using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaRepuesto.Models;

namespace SistemaRepuesto.Controllers
{
    public class EmpresasController : Controller
    {
        private RepuestoEntities db = new RepuestoEntities();

        // GET: Empresas
        public ActionResult Index()
        {
            return View(db.EmpresaConfigs.ToList());
        }

        // GET: Empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpresaConfig empresaConfig = db.EmpresaConfigs.Find(id);
            if (empresaConfig == null)
            {
                return HttpNotFound();
            }
            return View(empresaConfig);
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpresaID,NombreEmpresa,RNC,NCF01_Proximo,NCF02_Proximo,NCF03_Proximo,NCF04_Proximo,NCF14_Proximo,NCF15_Proximo,TasaItbisActual,ArchivoLogo,ArchivoTipografia")] EmpresaConfig empresaConfig, HttpPostedFileBase archivo1, HttpPostedFileBase archivo2)
        {
            if (archivo1 != null)
            {
                string imgName = Path.GetFileName(archivo1.FileName);
                string fisiPath = Server.MapPath("~/Content/Imagenes/" + imgName);
                archivo1.SaveAs(fisiPath);
                empresaConfig.ArchivoLogo = imgName;
            }

            if (archivo2 != null)
            {
                string imgName = Path.GetFileName(archivo1.FileName);
                string fisiPath = Server.MapPath("~/Content/Imagenes/" + imgName);
                archivo1.SaveAs(fisiPath);
                empresaConfig.ArchivoTipografia = imgName;
            }

            if (ModelState.IsValid)
            {
                db.EmpresaConfigs.Add(empresaConfig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empresaConfig);
        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpresaConfig empresaConfig = db.EmpresaConfigs.Find(id);
            if (empresaConfig == null)
            {
                return HttpNotFound();
            }
            return View(empresaConfig);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpresaID,NombreEmpresa,RNC,NCF01_Proximo,NCF02_Proximo,NCF03_Proximo,NCF04_Proximo,NCF14_Proximo,NCF15_Proximo,TasaItbisActual,ArchivoLogo,ArchivoTipografia")] EmpresaConfig empresaConfig, HttpPostedFileBase archivo1, HttpPostedFileBase archivo2)
        {
            if (archivo1 != null)
            {
                string imgName = Path.GetFileName(archivo1.FileName);
                string fisiPath = Server.MapPath("~/Content/Imagenes/" + imgName);
                archivo1.SaveAs(fisiPath);
                empresaConfig.ArchivoLogo = imgName;
            }

            if (archivo2 != null)
            {
                string imgName = Path.GetFileName(archivo1.FileName);
                string fisiPath = Server.MapPath("~/Content/Imagenes/" + imgName);
                archivo1.SaveAs(fisiPath);
                empresaConfig.ArchivoTipografia = imgName;
            }

            if (ModelState.IsValid)
            {
                db.Entry(empresaConfig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empresaConfig);
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpresaConfig empresaConfig = db.EmpresaConfigs.Find(id);
            if (empresaConfig == null)
            {
                return HttpNotFound();
            }
            return View(empresaConfig);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpresaConfig empresaConfig = db.EmpresaConfigs.Find(id);
            db.EmpresaConfigs.Remove(empresaConfig);
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
