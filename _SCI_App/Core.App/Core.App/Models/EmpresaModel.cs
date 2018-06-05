namespace Core.App.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class EmpresaModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty("IdEmpresa")]
        public int IdEmpresa { get; set; }

        [JsonProperty("nom_empresa")]
        public string NomEmpresa { get; set; }
        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
