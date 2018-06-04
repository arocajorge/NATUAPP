namespace Core.App.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class ProductoModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty("IdEmpresa")]
        public long IdEmpresa { get; set; }

        [JsonProperty("IdProducto")]
        public long IdProducto { get; set; }

        [JsonProperty("nom_producto")]
        public string NomProducto { get; set; }

        [JsonProperty("cod_producto")]
        public string CodProducto { get; set; }

        [JsonProperty("IdUnidad_consumo")]
        public string IdUnidadConsumo { get; set; }
        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
