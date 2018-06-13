using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Api.Models
{
    public class tbl_movimientos_det_model
    {
        public decimal IdSincronizacion { get; set; }
        public int IdSecuencia { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdProducto { get; set; }
        public string IdUnidadMedida { get; set; }
        public string IdCentroCosto { get; set; }
        public string IdCentroCosto_sub_centro_costo { get; set; }
        public System.DateTime Fecha { get; set; }
        public double cantidad { get; set; }
        public Nullable<int> IdEmpresa_oc { get; set; }
        public Nullable<int> IdSucursal_oc { get; set; }
        public Nullable<decimal> IdOrdenCompra { get; set; }
        public Nullable<int> secuencia_oc { get; set; }
    }
}