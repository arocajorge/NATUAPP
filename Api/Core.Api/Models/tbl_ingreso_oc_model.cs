using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Api.Models
{
    public class tbl_ingreso_oc_model
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdOrdenCompra { get; set; }
        public int Secuencia { get; set; }
        public decimal IdProducto { get; set; }
        public string IdUnidadMedida { get; set; }
        public double cantidad_in { get; set; }
        public double cantidad_oc { get; set; }
        public double saldo { get; set; }
        public decimal IdProveedor { get; set; }
        public string nom_unidad_medida { get; set; }
        public string nom_proveedor { get; set; }
        public string nom_producto { get; set; }
        public string cod_producto { get; set; }
        public DateTime oc_fecha { get; set; }
        public string IdUnidadMedida_Consumo { get; set; }
        public string oc_observacion { get; set; }
    }
}