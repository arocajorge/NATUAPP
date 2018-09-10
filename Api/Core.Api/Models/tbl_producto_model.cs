using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Api.Models
{
    public class tbl_producto_model
    {
        public int IdEmpresa { get; set; }
        public decimal IdProducto { get; set; }
        public string nom_producto { get; set; }
        public string cod_producto { get; set; }
        public string IdUnidad_consumo { get; set; }
        public bool MuestraObservacionAPP { get; set; }
        public bool MuestraPesoAPP { get; set; }
    }
}