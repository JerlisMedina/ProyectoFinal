using SistemaRepuesto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Rotativa;

namespace SistemaRepuesto.Controllers
{
    public class VentasController : Controller
    {

        private RepuestoEntities db = new RepuestoEntities();

        private List<InventarioArticulo> inventarioArticulos = new List<InventarioArticulo> { };

        public static List<VentasCarrito> ventasCarrito = new List<VentasCarrito>();
        public static VentasCarrito ventasCart = new VentasCarrito();

        public static int ID_Cliente;
        public static bool FacturaRegistrada = false;
        public static String NCF;

        private void CalculaTotales()
        {

            int cantidad = ventasCarrito.Count();
            decimal itbis = 0;
            decimal subtotal = 0;
            decimal Total = 0;
            var query = (from a in ventasCarrito
                         select a).ToList();

            foreach (var item in query)
            {
                subtotal = subtotal + (decimal.Parse(item.Cantidad.ToString()) * decimal.Parse(item.PrecioUnitarioVenta.ToString()));
            }
            itbis = subtotal * decimal.Parse("0.18");
            Total = subtotal + itbis;
            TempData["Cantidad"] = String.Format("{0:N0}", cantidad);
            TempData["Subtotal"] = String.Format("{0:C2}", subtotal);
            TempData["Itbis"] = String.Format("{0:C2}", itbis);
            TempData["Total"] = String.Format("{0:C2}", Total);
        }

        private void MantieneDatosClientes()
        {
            Cliente cliente = db.Clientes.Find(ID_Cliente);
            if (cliente != null)
            {
                TempData["ClienteID"] = cliente.Id.ToString();
                TempData["ClienteNombre"] = cliente.Nombre + " " + cliente.Apellidos;
                TempData["ClienteNombreComercial"] = cliente.NombreComercial;
                TempData["ClienteDireccion"] = cliente.Direccion;
                TempData["ClienteTelefono"] = cliente.Telefono;
                TempData["ClienteRNC"] = cliente.Num_Documento;
                TempData["ClienteEmail"] = cliente.Email;
                TempData["TipoNCF"] = cliente.TipoNCF;
                TempData["NCF"] = NCF;
                // Preparamos todo para la factura y dejamos que el view la haga.
                String Fecha = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
                TempData["FECHAFACTURA"] = Fecha;
                if (cliente.TipoNCF == "01")
                {
                    TempData["ClienteTipoFactura"] = "FACTURA DE CREDITO FISCAL";
                }
                else if (cliente.TipoNCF == "15")
                {
                    TempData["ClienteTipoFactura"] = "FACTURA GUBERNAMENTAL";
                }
                else if (cliente.TipoNCF == "14")
                {
                    TempData["ClienteTipoFactura"] = "FACTURA REG. ESPECIALES";
                }
                else
                {
                    TempData["ClienteTipoFactura"] = "FACTURA DE CONSUMO";
                }
            }

        }


        // GET: Ventas
        //public ActionResult Index()
        //{
        //    return View(ventasCarrito);
        //}

        //[HttpPost]
        public ActionResult Index(int? idArt, string TextoBuscar)
        {
            // if int==nul redireccionar al carrito  
            if (idArt != null)
                return RedirectToAction("Carro");
            if (TextoBuscar == null)
            {
                TextoBuscar = "";
            }

            string buscar = TextoBuscar.ToUpper();
            var ArtQuery =
                from art in db.InventarioArticulos
                where (art.Descripcion).Contains(buscar)
                select art;
            return View(ArtQuery.ToList());
        }


        // Carro es el shopping cart.(Carrito de Compras/Ventas)
        public ActionResult Carro()
        {
            FacturaRegistrada = false;
            CalculaTotales();
            return View(ventasCarrito);
        }


        [HttpPost]
        public ActionResult Carro(int? idArt)//, int? txtCantidad, string BotonParam, string Actualizar, string Borrar, string add_tag)
        {
            if (idArt != null)
            {
                ventasCart = ventasCarrito.SingleOrDefault(s => s.Id_Articulo == idArt);
                if (ventasCart == null)
                {
                    // Es nuevo en el carrito
                    var ArtQuery = (from art in db.InventarioArticulos
                                    where art.Id_Articulo == idArt
                                    select art).FirstOrDefault();

                    if (ArtQuery != null && ArtQuery.CantidadDisponible > 0) // Confirmamos Exista y haya disponibilidad
                    {
                        VentasCarrito reg = new VentasCarrito();
                        reg.Id = 0;
                        reg.Id_Articulo = ArtQuery.Id_Articulo;
                        reg.Descripcion = ArtQuery.Descripcion;
                        reg.CantidadDisponible = int.Parse(ArtQuery.CantidadDisponible.ToString());
                        reg.Cantidad = 1;
                        reg.PrecioUnitarioVenta = ArtQuery.PrecioUnitarioVenta;
                        reg.Total = reg.PrecioUnitarioVenta * reg.Cantidad;
                        reg.ArtImagen = ArtQuery.ArtImagen;
                        ventasCarrito.Add(reg);
                    }
                }
                else
                {
                    // Si ya esta en el carrito y se vuelve a seleccionar el art. 1 a la cantidad
                    ventasCart.Cantidad++;
                    if (ventasCart.Cantidad > ventasCart.CantidadDisponible) // Si sobrepasa disponibilidad dejamos el maximo disp.
                    {
                        ventasCart.Cantidad = ventasCart.CantidadDisponible;
                    }
                }
            }

            CalculaTotales(); // Siempre hacemos refresh de los totales
            return View(ventasCarrito);
        }





        // Para borrar articulo del carrito
        public ActionResult BorrarItem(int? id)
        {
            if (id == null)
            {
                id = 1;
            }
            VentasCarrito reg = ventasCarrito.SingleOrDefault(s => s.Id_Articulo == id);
            return View(reg);
        }

        [HttpPost]
        public ActionResult BorrarItem(int? id, VentasCarrito reg)
        {
            ventasCart = ventasCarrito.SingleOrDefault(s => s.Id_Articulo == id);

            if (ventasCart != null)
            {
                ventasCarrito.Remove(ventasCart);
                CalculaTotales(); // Siempre hacemos refresh de los totales
                return RedirectToAction("Carro");
            }
            return View(reg);
        }


        // Para actualizar articulo del carrito
        public ActionResult ActualizarItem(int? id)
        {
            if (id == null)
            {
                id = 1;
            }
            VentasCarrito reg = ventasCarrito.SingleOrDefault(s => s.Id_Articulo == id);
            return View(reg);
        }

        [HttpPost]
        public ActionResult ActualizarItem(int? id, VentasCarrito reg)
        {
            ventasCart = ventasCarrito.SingleOrDefault(s => s.Id_Articulo == id);
            // Si lo que se requiere supera lo disponible, entonces se indica el error.
            if (reg.Cantidad > ventasCart.CantidadDisponible)
            {
                reg = ventasCarrito.SingleOrDefault(s => s.Id_Articulo == id);
                TempData["Error"] = "No Hay Suficientes(" + ventasCart.CantidadDisponible.ToString() + ")";
                return View(reg);
            }
            else if (reg.Cantidad < 1) // Si se coloca una cantidad negativa o cero, se indica
            {
                reg = ventasCarrito.SingleOrDefault(s => s.Id_Articulo == id);
                TempData["Error"] = "Cantidad Inválida";
                return View(reg);
            }
            else
            {
                if (ventasCart.Id_Articulo != 0)
                {
                    ventasCart.Cantidad = reg.Cantidad;
                    ventasCart.Total = ventasCart.Cantidad * ventasCart.PrecioUnitarioVenta;
                    CalculaTotales(); // Siempre hacemos refresh de los totales
                    return RedirectToAction("Carro");
                }
                else
                {
                    TempData["Error"] = "Error";
                }
                return View(reg);
            }
        }


        // GET: Ventas/Seleccion Cliente a Facturar
        public ActionResult SelCli(int? id)
        {
            return View(db.Clientes.ToList());
        }


        // GET: Ventas/Seleccion Cliente a Facturar
        [HttpPost]
        public ActionResult SelCli(int? id, FormCollection reg)
        {

            if (id != null)
            {
                ID_Cliente = int.Parse(id.ToString());
                MantieneDatosClientes();
                CalculaTotales(); // Siempre hacemos refresh de los totales
                return RedirectToAction("Facturar", ventasCart); // Se envia a la pagina de confirmacion final
            }
            return View();
        }

        public ActionResult Facturar(int id)
        {
            // Leer Cliente y Preparar Variables Generales (Refresh en caso de ser necesario)
            ID_Cliente = id;
            MantieneDatosClientes();
            CalculaTotales(); // Siempre hacemos refresh de los totales
            return View(ventasCarrito);
        }

        [HttpPost]
        public ActionResult Facturar(int? id)
        {
            if (id != null)
            {
                //RedirectToAction(FacturaFinal);
            }
            CalculaTotales();
            return View();
        }


        // ****************************************************************
        //string buscar = TextoBuscar.ToUpper();
        //var ArtQuery =
        //    from art in db.InventarioArticulos
        //    where (art.Descripcion).Contains(buscar)
        //    select art;
        //    return View(ArtQuery.ToList());



        // Aqui se registra y genera la factura final
        public ActionResult RegistrarFactura(int? id)
        {
            String NombreEmpleado;
            NombreEmpleado = (String)Session["PersonalName"];
            if (FacturaRegistrada == false)
            {
                CalculaTotales(); // Siempre hacemos refresh de los totales
                MantieneDatosClientes();



                String tipo = (String)TempData.Peek("TipoNCF");


                // Incrementamos el NCFs Correspondiente
                EmpresaConfig empresa = new EmpresaConfig();
                empresa = db.EmpresaConfigs.SingleOrDefault(s => s.EmpresaID > 0);
                if (tipo == "01")
                {
                    NCF = String.Format("{0:00000000}", empresa.NCF01_Proximo);
                    NCF = "B01" + NCF;
                    empresa.NCF01_Proximo++;
                }
                else
                {
                    NCF = String.Format("{0:00000000}", empresa.NCF02_Proximo);
                    NCF = "B02" + NCF;
                    empresa.NCF02_Proximo++;
                }

                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();


                // Registramos Factura
                Venta venta = new Venta();
                venta.Id_Cliente = ID_Cliente;
                //venta.Id_Trabajador =  // confirmar donde esta este dato.
                venta.MontoDescuento = decimal.Parse("0.00");
                venta.MontoExento = decimal.Parse("0.00");
                String Monto = String.Format("{0:F2}", TempData.Peek("Subtotal"));
                Monto = Monto.Replace('$', ' ');
                venta.MontoGravado = decimal.Parse(Monto);
                Monto = (String)TempData.Peek("Itbis");
                Monto = Monto.Replace('$', ' ');
                venta.MontoItbis = decimal.Parse(Monto);
                venta.ComprobanteFiscal = NCF;
                venta.Fecha = DateTime.Now;
                venta.Estatus = true;
                venta.Correlativo = "";
                venta.Igv = decimal.Parse("0.00");
                venta.ComprobanteFiscal = NCF;
                db.Ventas.Add(venta);
                db.SaveChanges();
                int ID_Venta = venta.Id;  // VERIFICAR QUE AQUI YA TIENE NUMERO Ok verificado

                // Registramos Inventario
                InventarioArticulo inventario = new InventarioArticulo();
                DetalleMovInventario movInventario = new DetalleMovInventario();
                DetalleVenta detalleVenta = new DetalleVenta();


                // Por cada item en el carrito, rebajamos del inventario
                var query = (from a in ventasCarrito
                             select a).ToList();

                foreach (var item in query)
                {
                    // Buscamos Inventario
                    // Es nuevo en el carrito
                    var inv = (from art in db.InventarioArticulos

                               where art.Id_Articulo == item.Id_Articulo
                               select art).FirstOrDefault();

                    // Escribimos Movimiento en inventario
                    movInventario.Id_Articulo = inv.Id_Articulo;
                    movInventario.Id_Ventas = ID_Venta; //***************************** Depende de arriba
                    movInventario.Precio_Compra = decimal.Parse("0.00");
                    movInventario.Precio_Venta = (decimal)item.PrecioUnitarioVenta;
                    movInventario.Requisicion = "";
                    movInventario.Stock_Inicial = (int)inv.CantidadDisponible;
                    movInventario.Stock_Actual = (int)(inv.CantidadDisponible - item.Cantidad);
                    movInventario.Estatus = true;
                    movInventario.Fecha_produccion = DateTime.Now;
                    movInventario.Fecha_vencimiento = DateTime.Now;
                    db.DetalleMovInventarios.Add(movInventario);
                    db.SaveChanges();

                    // Escribimos Movimiento en ventas
                    detalleVenta.Cantidad = item.Cantidad;
                    detalleVenta.DescuentoVenta = decimal.Parse("0.00");
                    detalleVenta.Estatus = true;
                    detalleVenta.Id_Articulo = item.Id_Articulo;
                    detalleVenta.Id_MovInventario = movInventario.Id; // ***** VERIFICAR SI DESPUES DEL ADD ARRIBA YA TIENE ID(verificado) 
                    detalleVenta.Id_Venta = ID_Venta;
                    detalleVenta.PrecioUnitarioVenta = (decimal)item.PrecioUnitarioVenta;
                    detalleVenta.PrecioVenta = (decimal)(item.PrecioUnitarioVenta * item.Cantidad);
                    db.DetalleVentas.Add(detalleVenta);
                    db.SaveChanges();

                    // Actualizamos Inventario
                    inv.CantidadDisponible = inv.CantidadDisponible - item.Cantidad;
                    db.Entry(inv).State = EntityState.Modified;
                    db.SaveChanges();
                    FacturaRegistrada = true;
                }

                // Preparamos todo para la factura y dejamos que el view la haga.
                String Fecha = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
                TempData["FECHAFACTURA"] = Fecha;
                TempData["VENDEDOR"] = NombreEmpleado;
                TempData["METODOPAGO"] = "-";

            }



            CalculaTotales(); // Siempre hacemos refresh de los totales
            MantieneDatosClientes();
            return View(ventasCarrito);
        }

        public ActionResult Continuar()
        {
            // Resetear todas las variables y listas temporales.
            ventasCarrito.Clear();

            TempData.Remove("Cantidad");
            TempData.Remove("Subtotal");
            TempData.Remove("Itbis");
            TempData.Remove("Total");
            TempData.Remove("ClienteID");
            TempData.Remove("ClienteNombre");
            TempData.Remove("ClienteNombreComercial");
            TempData.Remove("ClienteDireccion");
            TempData.Remove("ClienteTelefono");
            TempData.Remove("ClienteRNC");
            TempData.Remove("ClienteEmail");
            TempData.Remove("TipoNCF");
            TempData.Remove("NCF");
            TempData.Remove("FECHAFACTURA");
            TempData.Remove("ClienteTipoFactura");
            FacturaRegistrada = false;

            return RedirectToAction("Index", "Home");
        }


        public ActionResult CrearLista()
        {
            return View();
        }


        // GET: Ventas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // POST: Ventas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }

}
