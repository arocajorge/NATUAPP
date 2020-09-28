using Core.Api.Data;
using Core.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Core.Api.Controllers
{
    public class ConsumoSemanalController : ApiController
    {
        Entities_general db = new Entities_general();
        public IEnumerable<tbl_consumo_semanal_model> Get(string IdUsuario = "")
        {
            db.SetCommandTimeOut(3000);
            List<tbl_consumo_semanal_model> Lista = new List<tbl_consumo_semanal_model>();
            var lst = db.sp_get_consumo_semanal(IdUsuario).ToList();
            foreach (var q in lst)
            {
                Lista.Add(new tbl_consumo_semanal_model
                {
                    IdEmpresa = q.IdEmpresa,
                    IdSucursal = q.IdSucursal,
                    IdBodega = q.IdBodega,
                    IdProducto = q.IdProducto,
                    IdCentroCosto = q.IdCentroCosto,
                    IdCentroCosto_sub_centro_costo = q.IdCentroCosto_sub_centro_costo,
                    NomProducto = q.NomProducto,
                    NomSubCentro = q.NomSubCentro,
                    LUNES = q.LUNES,
                    MARTES = q.MARTES,
                    MIERCOLES = q.MIERCOLES,
                    JUEVES = q.JUEVES,
                    VIERNES = q.VIERNES,
                    SABADO = q.SABADO,
                    DOMINGO = q.DOMINGO,
                    TOTAL = q.TOTAL
                });
            }
            return Lista;
        }
    }
}
