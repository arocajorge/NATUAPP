namespace Core.App.Models
{
    using Newtonsoft.Json;
    using SQLite.Net.Attributes;

    public class EmpresaModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty("IdEmpresa")]
        public long IdEmpresa { get; set; }

        [JsonProperty("nom_empresa")]
        public string NomEmpresa { get; set; }
        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
