using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using SistemaRepuesto.Models;

namespace SistemaRepuesto.Controllers
{
    public class ReportesController : Controller
    {
        private RepuestoEntities db = new RepuestoEntities();
        public static List<Venta> ventas = new List<Venta>();

        public static DateTime RMInventariofechadesde;
        public static DateTime RMInventariofechahasta;
        public static string TipoNCF_publico;



        public ActionResult ReporteArticulos()
        {
            RMInventariofechadesde = DateTime.Parse("1901-01-01");
            RMInventariofechahasta = DateTime.Now;
            var invQuery =
                from inv in db.InventarioArticulos
                where inv.Id_Articulo >= 0
                orderby inv.Id_Articulo
                select inv;
            return View(invQuery.ToList());
        }

        [HttpPost]
        public ActionResult ReporteArticulos(string Fecha_Desde, string Fecha_Hasta)
        {
            DateTime fechadesde = DateTime.Parse("1980-01-01");
            DateTime fechahasta = DateTime.Now;

            if (Fecha_Desde != null && Fecha_Desde != "")
            {
                fechadesde = DateTime.Parse(Fecha_Desde.ToString());
            }

            if (Fecha_Hasta != null && Fecha_Hasta != "")
            {
                fechahasta = DateTime.Parse(Fecha_Hasta.ToString());
            }
            RMInventariofechadesde = fechadesde;
            RMInventariofechahasta = fechahasta;

            var invQuery =
                from inv in db.InventarioArticulos
                where inv.Id_Articulo >= 0
                where inv.Fecha_Entrada >= fechadesde
                where inv.Fecha_Entrada <= fechahasta

                orderby inv.Id_Articulo
                select inv;
            return View(invQuery.ToList());

        }

        public ActionResult ReporteArticulosP()
        {
            DateTime fechadesde = RMInventariofechadesde;
            DateTime fechahasta = RMInventariofechahasta;

            if (fechadesde == null)
            {
                fechadesde = DateTime.Parse("19800101");
            }

            if (fechahasta == null)
            {
                fechahasta = DateTime.Now;
            }
            var invQuery =
                from inv in db.InventarioArticulos
                where inv.Id_Articulo >= 0
                where inv.Fecha_Entrada >= fechadesde
                where inv.Fecha_Entrada <= fechahasta

                orderby inv.Id_Articulo
                select inv;
            return View(invQuery.ToList());

        }





        public ActionResult ReporteMovInventario()
        {

            RMInventariofechadesde = DateTime.Parse("1901-01-01");
            RMInventariofechahasta = DateTime.Now;
            var invQuery =
                from inv in db.DetalleMovInventarios
                where inv.Id >= 0
                orderby inv.Id
                select inv;
            return View(invQuery.ToList());
        }
        [HttpPost]
        public ActionResult ReporteMovInventario(string Fecha_Desde, string Fecha_Hasta)
        {
            DateTime fechadesde = DateTime.Parse("1980-01-01");
            DateTime fechahasta = DateTime.Now;

            if (Fecha_Desde != null && Fecha_Desde != "")
            {
                fechadesde = DateTime.Parse(Fecha_Desde.ToString());
            }

            if (Fecha_Hasta != null && Fecha_Hasta != "")
            {
                fechahasta = DateTime.Parse(Fecha_Hasta.ToString());
            }
            RMInventariofechadesde = fechadesde;
            RMInventariofechahasta = fechahasta;
            var invQuery =
                from inv in db.DetalleMovInventarios
                where inv.Id >= 0
                where inv.Venta.Fecha >= fechadesde
                where inv.Venta.Fecha <= fechahasta
                orderby inv.Id
                select inv;
            return View(invQuery.ToList());

        }

        public ActionResult ReporteMovInventarioP()
        {
            DateTime fechadesde = RMInventariofechadesde;
            DateTime fechahasta = RMInventariofechahasta;

            if (fechadesde == null)
            {
                fechadesde = DateTime.Parse("19800101");
            }

            if (fechahasta == null)
            {
                fechahasta = DateTime.Now;
            }
            var invQuery =
                from inv in db.DetalleMovInventarios
                where inv.Id >= 0
                where inv.Venta.Fecha >= fechadesde
                where inv.Venta.Fecha <= fechahasta
                orderby inv.Id
                select inv;
            return View(invQuery.ToList());

        }

        public ActionResult ReporteFacturas()
        {


            RMInventariofechadesde = DateTime.Parse("1980-01-01");
            RMInventariofechahasta = DateTime.Now;
            TipoNCF_publico = "";
            var ArtQuery =
                from art in db.Ventas
                where (art.ComprobanteFiscal).Contains("0")
                orderby art.ComprobanteFiscal
                select art;
            return View(ArtQuery.ToList());



        }

        [HttpPost]
        public ActionResult ReporteFacturas(string cboBox, string Fecha_Desde, string Fecha_Hasta)
        {
            string tiponcf = "";
            DateTime fechadesde = DateTime.Parse("1980-01-01");
            DateTime fechahasta = DateTime.Now;

            if (Fecha_Desde != null && Fecha_Desde != "")
            {
                fechadesde = DateTime.Parse(Fecha_Desde.ToString());
            }

            if (Fecha_Hasta != null && Fecha_Hasta != "")
            {
                fechahasta = DateTime.Parse(Fecha_Hasta.ToString());
            }

            if (cboBox != null && cboBox != "TODOS")
            {
                tiponcf = String.Format("{0:00}", int.Parse(cboBox));
                tiponcf = "B" + tiponcf;
            }

            TipoNCF_publico = tiponcf;
            RMInventariofechadesde = fechadesde;
            RMInventariofechahasta = fechahasta;

            var ArtQuery =
                    from art in db.Ventas
                    where (art.ComprobanteFiscal).Contains(tiponcf)
                    where art.Fecha >= fechadesde
                    where art.Fecha <= fechahasta
                    orderby art.ComprobanteFiscal
                    select art;
            return View(ArtQuery.ToList());
        }


        public ActionResult ReporteFacturasP()
        {
            string tiponcf = TipoNCF_publico;
            DateTime fechadesde = RMInventariofechadesde;
            DateTime fechahasta = RMInventariofechahasta;

            if (fechadesde == null)
            {
                fechadesde = DateTime.Parse("19800101");
            }

            if (fechahasta == null)
            {
                fechahasta = DateTime.Now;
            }
            if (tiponcf != "B01" && tiponcf != "B02" && tiponcf != "B14" && tiponcf != "B15")
            {
                tiponcf = "";
            }
            var ArtQuery =
                    from art in db.Ventas
                    where (art.ComprobanteFiscal).Contains(tiponcf)
                    where art.Fecha >= fechadesde
                    where art.Fecha <= fechahasta
                    orderby art.ComprobanteFiscal
                    select art;
            return View(ArtQuery.ToList());
        }


        public ActionResult ReporteInventario()
        {
            return View();
        }

        public ActionResult ReporteReposicion()
        {
            return View();
        }

        public ActionResult ReporteMovInvent()
        {
            return View();
        }
    }
}