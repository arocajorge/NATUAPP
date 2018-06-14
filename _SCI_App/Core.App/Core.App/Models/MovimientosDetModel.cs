using Newtonsoft.Json;
using System;

namespace Core.App.Models
{
    public class MovimientosDetModel
    {
        [JsonProperty("IdSincronizacion")]
        public decimal IdSincronizacion { get; set; }
        [JsonProperty("IdSecuencia")]
        public int IdSecuencia { get; set; }
        [JsonProperty("IdEmpresa")]
        public int IdEmpresa { get; set; }
        [JsonProperty("IdSucursal")]
        public int IdSucursal { get; set; }
        [JsonProperty("IdBodega")]
        public int IdBodega { get; set; }
        [JsonProperty("IdProducto")]
        public decimal IdProducto { get; set; }
        [JsonProperty("IdUnidadMedida")]
        public string IdUnidadMedida { get; set; }
        [JsonProperty("IdCentroCosto")]
        public string IdCentroCosto { get; set; }
        [JsonProperty("IdCentroCosto_sub_centro_costo")]
        public string IdCentroCosto_sub_centro_costo { get; set; }
        [JsonProperty("Fecha")]
        public System.DateTime Fecha { get; set; }
        [JsonProperty("cantidad")]
        public double cantidad { get; set; }
        [JsonProperty("IdEmpresa_oc")]
        public Nullable<int> IdEmpresa_oc { get; set; }
        [JsonProperty("IdSucursal_oc")]
        public Nullable<int> IdSucursal_oc { get; set; }
        [JsonProperty("IdOrdenCompra")]
        public Nullable<decimal> IdOrdenCompra { get; set; }
        [JsonProperty("secuencia_oc")]
        public Nullable<int> secuencia_oc { get; set; }
    }
}
