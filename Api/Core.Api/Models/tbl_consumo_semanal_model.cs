namespace Core.Api.Models
{
    public class tbl_consumo_semanal_model
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdProducto { get; set; }
        public string IdCentroCosto { get; set; }
        public string IdCentroCosto_sub_centro_costo { get; set; }
        public string NomProducto { get; set; }
        public string NomSubCentro { get; set; }
        public double LUNES { get; set; }
        public double MARTES { get; set; }
        public double MIERCOLES { get; set; }
        public double JUEVES { get; set; }
        public double VIERNES { get; set; }
        public double SABADO { get; set; }
        public double DOMINGO { get; set; }
        public double TOTAL { get; set; }
    }
}