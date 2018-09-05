using Core.Api.Data;
using Core.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Core.Api.Controllers
{
    public class ConsumoSemanalController : ApiController
    {
        Entities_general db = new Entities_general();
        public IEnumerable<tbl_consumo_semanal_model> Get(string IdUsuario = "")
        {
            var lst = db.sp_get_consumo_semanal(IdUsuario).Select(q => new tbl_consumo_semanal_model
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
                DOMINGO = q.DOMINGO
            }).ToList();

            return lst;
        }
    }
}
