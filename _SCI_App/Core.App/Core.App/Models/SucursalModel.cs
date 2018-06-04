namespace Core.App.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class SucursalModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty(PropertyName = "IdEmpresa")]
        public int IdEmpresa { get; set; }
        [JsonProperty(PropertyName = "IdSucursal")]
        public int IdSucursal { get; set; }
        [JsonProperty(PropertyName = "nom_sucursal")]
        public string Nom_sucursal { get; set; }
        public override int GetHashCode()
        {
            return PKSQLite;
        }

    }
}
