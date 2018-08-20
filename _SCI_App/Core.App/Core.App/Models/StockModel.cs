using Newtonsoft.Json;
using SQLite;

namespace Core.App.Models
{
    public class StockModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty("IdEmpresa")]
        public long IdEmpresa { get; set; }

        [JsonProperty("IdSucursal")]
        public long IdSucursal { get; set; }

        [JsonProperty("IdBodega")]
        public long IdBodega { get; set; }

        [JsonProperty("IdProducto")]
        public long IdProducto { get; set; }

        [JsonProperty("CodProducto")]
        public string CodProducto { get; set; }

        [JsonProperty("NomProducto")]
        public string NomProducto { get; set; }

        [JsonProperty("IdUnidadMedida_Consumo")]
        public string IdUnidadMedidaConsumo { get; set; }

        [JsonProperty("Stock")]
        public double Stock { get; set; }

        [JsonProperty("NomUnidadMedida")]
        public string NomUnidadMedida { get; set; }
        [JsonProperty("CodProdProducto")]
        public string CodProdProducto { get; set; }

        public double Ingresos { get; set; }
        public double Egresos { get; set; }
        public double Saldo { get; set; }
        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
