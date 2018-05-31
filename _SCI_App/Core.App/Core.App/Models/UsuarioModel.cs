namespace Core.App.Models
{
    using Newtonsoft.Json;
    using SQLite.Net.Attributes;

    public class UsuarioModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty(PropertyName = "IdUsuario")]
        public string IdUsuario { get; set; }
        [JsonProperty(PropertyName = "clave")]
        public string Clave { get; set; }
        public override int GetHashCode()
        {
            return PKSQLite;
        }
    }
}
