using SQLite;
using System;

namespace Core.App.Models
{
    public class EgresoModel
    {
        [PrimaryKey]
        public int PKSQLite { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public DateTime Fecha { get; set; }
        public decimal IdProducto { get; set; }
        public string IdUnidadMedida { get; set; }
        public string NomProducto { get; set; }
        public string NomUnidadMedida { get; set; }
        public double Cantidad { get; set; }
        public string IdCentroCosto { get; set; }
        public string IdSubCentroCosto { get; set; }
        public string NomSubCentro { get; set; }
        public float Peso { get; set; }
        public string Observacion { get; set; }
    }
}
