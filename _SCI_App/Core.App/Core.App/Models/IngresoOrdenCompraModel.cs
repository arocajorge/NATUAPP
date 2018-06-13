using System;

namespace Core.App.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class IngresoOrdenCompraModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty("IdEmpresa")]
        public int IdEmpresa { get; set; }

        [JsonProperty("IdSucursal")]
        public int IdSucursal { get; set; }

        [JsonProperty("IdOrdenCompra")]
        public decimal IdOrdenCompra { get; set; }

        [JsonProperty("Secuencia")]
        public int Secuencia { get; set; }

        [JsonProperty("IdProducto")]
        public decimal IdProducto { get; set; }

        [JsonProperty("IdUnidadMedida")]
        public string IdUnidadMedida { get; set; }

        [JsonProperty("cantidad_in")]
        public double CantidadIn { get; set; }

        [JsonProperty("cantidad_oc")]
        public double CantidadOc { get; set; }

        [JsonProperty("saldo")]
        public double Saldo { get; set; }

        [JsonProperty("IdProveedor")]
        public decimal IdProveedor { get; set; }

        [JsonProperty("nom_unidad_medida")]
        public string NomUnidadMedida { get; set; }

        [JsonProperty("nom_proveedor")]
        public string NomProveedor { get; set; }

        [JsonProperty("nom_producto")]
        public string NomProducto { get; set; }

        [JsonProperty("cod_producto")]
        public string CodProducto { get; set; }

        [JsonProperty("oc_fecha")]
        public DateTime OcFecha { get; set; }

        [JsonProperty("oc_observacion")]
        public string OcObservacion { get; set; }

        [JsonProperty("IdUnidadMedida_Consumo")]
        public string IdUnidadMedidaConsumo { get; set; }

        public double CantidadApro { get; set; }

        public DateTime FechaApro { get; set; }
        
        public double CantidadApro_convertida { get; set; }

        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
