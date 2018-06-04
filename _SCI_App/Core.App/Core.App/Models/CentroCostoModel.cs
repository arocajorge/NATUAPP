namespace Core.App.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class CentroCostoModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty(PropertyName = "IdEmpresa")]
        public int IdEmpresa { get; set; }
        [JsonProperty(PropertyName = "IdCentroCosto")]
        public string IdCentroCosto { get; set; }
        [JsonProperty(PropertyName = "nom_centro_costo")]
        public string Nom_centro_costo { get; set; }
        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
