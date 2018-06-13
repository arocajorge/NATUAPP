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

        [JsonProperty("IdUnidadMedida_equiva")]
        public string IdUnidadMedidaEquiva { get; set; }

        [JsonProperty("valor_equiv")]
        public double ValorEquiv { get; set; }

        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
