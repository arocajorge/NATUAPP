using Newtonsoft.Json;

namespace Core.App.Models
{
    public class SincronizacionModel
    {
        [JsonProperty(PropertyName = "IdSincronizacion")]
        public int IdSincronizacion { get; set; }
        [JsonProperty(PropertyName = "EnUso")]
        public bool EnUso { get; set; }
        [JsonProperty(PropertyName = "FechaInicio")]
        public System.DateTime FechaInicio { get; set; }
    }
}
