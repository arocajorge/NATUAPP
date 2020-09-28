namespace Core.App.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class ProductoModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty("IdEmpresa")]
        public int IdEmpresa { get; set; }

        [JsonProperty("IdProducto")]
        public decimal IdProducto { get; set; }

        [JsonProperty("nom_producto")]
        public string NomProducto { get; set; }

        [JsonProperty("cod_producto")]
        public string CodProducto { get; set; }

        [JsonProperty("IdUnidad_consumo")]
        public string IdUnidadConsumo { get; set; }
        [JsonProperty("MuestraObservacionAPP")]
        public bool MuestraObservacionAPP { get; set; }
        [JsonProperty("MuestraPesoAPP")]
        public bool MuestraPesoAPP { get; set; }


        [JsonProperty("IdSucursal")]
        public long IdSucursal { get; set; }
        [JsonProperty("IdBodega")]
        public long IdBodega { get; set; }

        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
