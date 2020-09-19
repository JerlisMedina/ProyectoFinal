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
    public class ProductosController : Controller
    {
        private RepuestoEntities db = new RepuestoEntities();

        // GET: Productos
        public ActionResult Index()
        {
            return View(db.InventarioArticulos.ToList());
        }

        // GET: Catalogo de Productos
        public ActionResult CatalogoProductos()
        {
            return View(db.InventarioArticulos.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioArticulo inventarioArticulo = db.InventarioArticulos.Find(id);
            if (inventarioArticulo == null)
            {
                return HttpNotFound();
            }
            return View(inventarioArticulo);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            //ViewBag.Id_Articulo = new SelectList(db.CarroCompras, "Id_CarroCompras", "Id_CarroCompras");
            ViewBag.Id_Empleado = new SelectList(db.Empleados, "Id","Nombre");
            ViewBag.Id_Categoria = new SelectList(db.Categorias, "Id", "Descripcion");
            ViewBag.Id_Presentacion = new SelectList(db.Presentacions, "Id", "Descripcion");
            ViewBag.Id_Proveedor = new SelectList(db.Proveedores, "Id", "Razon_Social");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Articulo,Id_Trabajador,Id_Proveedor,Codigo,Descripcion,Marca,Modelo,Year,Id_Categoria,Id_Presentacion,Fecha_Entrada,CostoUnitario,PrecioUnitarioVenta,CantidadDisponible,CantidadReposicion,Estatus,Almacen,Tramo,ArtImagen")] InventarioArticulo inventarioArticulo, HttpPostedFileBase archivo)
        {
            if (archivo != null)
            {
                string imgName = Path.GetFileName(archivo.FileName);
                string fisiPath = Server.MapPath("~/Content/Imagenes/" + imgName);
                archivo.SaveAs(fisiPath);
                inventarioArticulo.ArtImagen = imgName;
            }

            if (ModelState.IsValid)
            {
                db.InventarioArticulos.Add(inventarioArticulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Id_Articulo = new SelectList(db.CarroCompras, "Id_CarroCompras", "Id_CarroCompras", inventarioArticulo.Id_Articulo);
            ViewBag.Id_Empleado = new SelectList(db.Empleados, "Id", "Nombre", inventarioArticulo.Id_Trabajador);
            ViewBag.Id_Categoria = new SelectList(db.Categorias, "Id", "Descripcion", inventarioArticulo.Id_Categoria);
            ViewBag.Id_Presentacion = new SelectList(db.Presentacions, "Id", "Descripcion", inventarioArticulo.Id_Presentacion);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedores, "Id", "Razon_Social", inventarioArticulo.Id_Proveedor);

            return View(inventarioArticulo);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioArticulo inventarioArticulo = db.InventarioArticulos.Find(id);
            if (inventarioArticulo == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id_Empleado = new SelectList(db.Empleados, "Id", "Nombre");
            ViewBag.Id_Categoria = new SelectList(db.Categorias, "Id", "Descripcion");
            ViewBag.Id_Presentacion = new SelectList(db.Presentacions, "Id", "Descripcion");
            ViewBag.Id_Proveedor = new SelectList(db.Proveedores, "Id", "Razon_Social");

            return View(inventarioArticulo);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Articulo,Id_Trabajador,Id_Proveedor,Codigo,Descripcion,Marca,Modelo,Year,Id_Categoria,Id_Presentacion,Fecha_Entrada,CostoUnitario,PrecioUnitarioVenta,CantidadDisponible,CantidadReposicion,Estatus,Almacen,Tramo,ArtImagen")] InventarioArticulo inventarioArticulo, HttpPostedFileBase archivo)
        {
            if (archivo != null)
            {
                string imgName = Path.GetFileName(archivo.FileName);
                string fisiPath = Server.MapPath("~/Content/Imagenes/" + imgName);
                archivo.SaveAs(fisiPath);
                inventarioArticulo.ArtImagen = imgName;
            }

            if (ModelState.IsValid)
            {
                db.Entry(inventarioArticulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Id_Articulo = new SelectList(db.CarroCompras, "Id_CarroCompras", "Id_CarroCompras", inventarioArticulo.Id_Articulo);
            ViewBag.Id_Empleado = new SelectList(db.Empleados, "Id", "Nombre", inventarioArticulo.Id_Trabajador);
            ViewBag.Id_Categoria = new SelectList(db.Categorias, "Id", "Descripcion", inventarioArticulo.Id_Categoria);
            ViewBag.Id_Presentacion = new SelectList(db.Presentacions, "Id", "Descripcion", inventarioArticulo.Id_Presentacion);
            ViewBag.Id_Proveedor = new SelectList(db.Proveedores, "Id", "Razon_Social", inventarioArticulo.Id_Proveedor);

            return View(inventarioArticulo);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioArticulo inventarioArticulo = db.InventarioArticulos.Find(id);
            if (inventarioArticulo == null)
            {
                return HttpNotFound();
            }
            return View(inventarioArticulo);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InventarioArticulo inventarioArticulo = db.InventarioArticulos.Find(id);
            db.InventarioArticulos.Remove(inventarioArticulo);
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
