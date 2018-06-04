namespace Core.App.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class BodegaModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty(PropertyName = "IdEmpresa")]
        public int IdEmpresa { get; set; }
        [JsonProperty(PropertyName = "IdSucursal")]
        public int IdSucursal { get; set; }
        [JsonProperty(PropertyName = "IdBodega")]
        public int IdBodega { get; set; }
        [JsonProperty(PropertyName = "nom_bodega")]
        public string Nom_bodega { get; set; }
        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
