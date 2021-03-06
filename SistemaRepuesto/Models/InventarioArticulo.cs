//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaRepuesto.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InventarioArticulo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InventarioArticulo()
        {
            this.DetalleVentas = new HashSet<DetalleVenta>();
            this.DetalleMovInventarios = new HashSet<DetalleMovInventario>();
        }
    
        public int Id_Articulo { get; set; }
        public int Id_Trabajador { get; set; }
        public int Id_Proveedor { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Id_Categoria { get; set; }
        public Nullable<int> Id_Presentacion { get; set; }
        public Nullable<System.DateTime> Fecha_Entrada { get; set; }
        public Nullable<decimal> CostoUnitario { get; set; }
        public Nullable<decimal> PrecioUnitarioVenta { get; set; }
        public Nullable<int> CantidadDisponible { get; set; }
        public Nullable<int> CantidadReposicion { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public string Almacen { get; set; }
        public string Tramo { get; set; }
        public string ArtImagen { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleVenta> DetalleVentas { get; set; }
        public virtual Presentacion Presentacion { get; set; }
        public virtual Proveedore Proveedore { get; set; }
        public virtual Categoria Categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleMovInventario> DetalleMovInventarios { get; set; }
    }
}
