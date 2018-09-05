using Newtonsoft.Json;
using SQLite;

namespace Core.App.Models
{
    public class ConsumoSemanalModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        [JsonProperty("IdEmpresa")]
        public int IdEmpresa { get; set; }

        [JsonProperty("IdSucursal")]
        public int IdSucursal { get; set; }

        [JsonProperty("IdBodega")]
        public int IdBodega { get; set; }

        [JsonProperty("IdProducto")]
        public decimal IdProducto { get; set; }

        [JsonProperty("IdCentroCosto")]
        public string IdCentroCosto { get; set; }

        [JsonProperty("IdCentroCosto_sub_centro_costo")]
        public string IdCentroCostoSubCentroCosto { get; set; }

        [JsonProperty("NomProducto")]
        public string NomProducto { get; set; }

        [JsonProperty("NomSubCentro")]
        public string NomSubCentro { get; set; }

        [JsonProperty("LUNES")]
        public double Lunes { get; set; }

        [JsonProperty("MARTES")]
        public double Martes { get; set; }

        [JsonProperty("MIERCOLES")]
        public double Miercoles { get; set; }

        [JsonProperty("JUEVES")]
        public double Jueves { get; set; }

        [JsonProperty("VIERNES")]
        public double Viernes { get; set; }

        [JsonProperty("SABADO")]
        public double Sabado { get; set; }

        [JsonProperty("DOMINGO")]
        public double Domingo { get; set; }
    }
}
