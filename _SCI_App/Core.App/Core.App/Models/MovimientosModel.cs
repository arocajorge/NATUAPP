using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.App.Models
{
    public class MovimientosModel
    {
        [JsonProperty("IdSincronizacion")]
        public decimal IdSincronizacion { get; set; }
        [JsonProperty("IdUsuarioSCI")]
        public string IdUsuarioSCI { get; set; }
        [JsonProperty("IdFecha")]
        public int IdFecha { get; set; }
        [JsonProperty("Fecha")]
        public System.DateTime Fecha { get; set; }
        [JsonProperty("lst_det")]
        public List<MovimientosDetModel> lst_det { get; set; }
    }
}
