namespace Core.App.Models
{
    using Newtonsoft.Json;
    using SQLite;

    public class UnidadMedidaModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty("IdUnidadMedida")]
        public string IdUnidadMedida { get; set; }

        [JsonProperty("nom_unidad_medida")]
        public string NomUnidadMedida { get; set; }
        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
