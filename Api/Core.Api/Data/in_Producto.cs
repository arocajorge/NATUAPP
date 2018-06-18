//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Api.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class in_Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public in_Producto()
        {
            this.tbl_producto = new HashSet<tbl_producto>();
        }
    
        public int IdEmpresa { get; set; }
        public decimal IdProducto { get; set; }
        public string pr_codigo { get; set; }
        public string pr_descripcion { get; set; }
        public string pr_descripcion_2 { get; set; }
        public int IdProductoTipo { get; set; }
        public int IdMarca { get; set; }
        public string IdPresentacion { get; set; }
        public string IdCategoria { get; set; }
        public int IdLinea { get; set; }
        public int IdGrupo { get; set; }
        public int IdSubGrupo { get; set; }
        public string IdUnidadMedida { get; set; }
        public string IdUnidadMedida_Consumo { get; set; }
        public string IdNaturaleza { get; set; }
        public Nullable<int> IdMotivo_Vta { get; set; }
        public string pr_codigo_barra { get; set; }
        public string pr_observacion { get; set; }
        public double pr_precio_publico { get; set; }
        public double pr_precio_mayor { get; set; }
        public double pr_precio_minimo { get; set; }
        public double pr_precio_puerta { get; set; }
        public string pr_ManejaIva { get; set; }
        public string pr_ManejaSeries { get; set; }
        public double pr_costo_fob { get; set; }
        public double pr_costo_CIF { get; set; }
        public double pr_costo_promedio { get; set; }
        public int pr_DiasMaritimo { get; set; }
        public int pr_DiasAereo { get; set; }
        public int pr_DiasTerrestre { get; set; }
        public double pr_largo { get; set; }
        public double pr_alto { get; set; }
        public double pr_profundidad { get; set; }
        public double pr_peso { get; set; }
        public byte[] pr_imagenPeque { get; set; }
        public byte[] pr_imagen_Grande { get; set; }
        public string pr_partidaArancel { get; set; }
        public decimal pr_porcentajeArancel { get; set; }
        public double pr_Por_descuento { get; set; }
        public double pr_stock_maximo { get; set; }
        public double pr_stock_minimo { get; set; }
        public string IdUsuario { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public System.DateTime Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string pr_motivoAnulacion { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string Estado { get; set; }
        public Nullable<double> AnchoEspecifico { get; set; }
        public Nullable<double> PesoEspecifico { get; set; }
        public string ManejaKardex { get; set; }
        public string IdCod_Impuesto_Iva { get; set; }
        public string IdCod_Impuesto_Ice { get; set; }
        public bool Aparece_modu_Ventas { get; set; }
        public bool Aparece_modu_Compras { get; set; }
        public bool Aparece_modu_Inventario { get; set; }
        public bool Aparece_modu_Activo_F { get; set; }
        public string mobile_cod_produccion { get; set; }
    
        public virtual in_UnidadMedida in_UnidadMedida { get; set; }
        public virtual in_UnidadMedida in_UnidadMedida1 { get; set; }
        public virtual tb_empresa tb_empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_producto> tbl_producto { get; set; }
    }
}
