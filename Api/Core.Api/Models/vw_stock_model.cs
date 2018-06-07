using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Api.Models
{
    public class vw_stock_model
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdProducto { get; set; }
        public string CodProducto { get; set; }
        public string NomProducto { get; set; }
        public string IdUnidadMedida_Consumo { get; set; }
        public double Stock { get; set; }
        public string NomUnidadMedida { get; set; }
    }
}