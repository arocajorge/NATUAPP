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
    
    public partial class sp_stock_Result
    {
        public decimal IdRow { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdProducto { get; set; }
        public string CodProducto { get; set; }
        public string NomProducto { get; set; }
        public string IdUnidadMedida_Consumo { get; set; }
        public double Stock { get; set; }
        public string NomUnidadMedida { get; set; }
        public string CodProdProducto { get; set; }
        public string CodProdSubcentro { get; set; }
    }
}
