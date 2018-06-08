namespace Core.App.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class SubCentroCostoModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty(PropertyName = "IdEmpresa")]
        public int IdEmpresa { get; set; }
        [JsonProperty(PropertyName = "IdCentroCosto")]
        public string IdCentroCosto { get; set; }
        [JsonProperty(PropertyName = "IdSubCentroCosto")]
        public string IdSubCentroCosto { get; set; }
        [JsonProperty(PropertyName = "nom_subcentro")]
        public string Nom_subcentro { get; set; }

        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
