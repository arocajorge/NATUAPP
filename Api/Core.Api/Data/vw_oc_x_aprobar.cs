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
    
    public partial class vw_oc_x_aprobar
    {
        public long IdRow { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdOrdenCompra { get; set; }
        public int Secuencia { get; set; }
        public decimal IdProducto { get; set; }
        public string IdUnidadMedida { get; set; }
        public decimal IdProveedor { get; set; }
        public double cant_oc { get; set; }
        public double cant_in { get; set; }
        public double saldo { get; set; }
        public string pr_descripcion { get; set; }
        public string pr_codigo { get; set; }
        public string Descripcion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public System.DateTime oc_fecha { get; set; }
    }
}
