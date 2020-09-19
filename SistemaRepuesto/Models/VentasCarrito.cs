using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaRepuesto.Models
{
    public class VentasCarrito
    {
        //[key]
        public int Id { get; set; }
        public int Id_Articulo { get; set; } // Id del art en inventario
        public string Descripcion { get; set; } // Descripcion del articulo
        public int CantidadDisponible { get; set; } // disponible inventario
        public int Cantidad { get; set; } // Cantidad que se quiere comprar
        public decimal? PrecioUnitarioVenta { get; set; }
        public decimal? Total { get; set; }
        public string ArtImagen { get; set; }
    }
}