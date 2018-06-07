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
    public class IngresoOrdenCompraController : ApiController
    {
        Entities_general db = new Entities_general();
        // GET: api/IngresoOrdenCompra
        public IEnumerable<tbl_ingreso_oc_model> Get(string IdUsuario = "")
        {
            var lst = from oc in db.vw_oc_x_aprobar
                      join us in db.tbl_usuario_x_bodega
                      on oc.IdEmpresa equals us.IdEmpresa
                      where us.IdUsuarioSCI == IdUsuario
                      group oc by new
                      {
                          oc.IdEmpresa, oc.IdSucursal, oc.IdOrdenCompra, oc.Secuencia,
                          oc.IdProducto, oc.IdUnidadMedida, oc.IdProveedor, oc.cant_oc,
                          oc.cant_in, oc.saldo, oc.pr_descripcion, oc.pr_codigo,
                          oc.Descripcion, oc.pe_nombreCompleto, oc.oc_fecha
                      } into grouping
                      select new tbl_ingreso_oc_model
                      {
                          IdEmpresa = grouping.Key.IdEmpresa,
                          IdSucursal = grouping.Key.IdSucursal,
                          IdOrdenCompra = grouping.Key.IdOrdenCompra,
                          Secuencia = grouping.Key.Secuencia,
                          IdProducto = grouping.Key.IdProducto,
                          IdUnidadMedida = grouping.Key.IdUnidadMedida,
                          IdProveedor = grouping.Key.IdProveedor,
                          cantidad_oc = grouping.Key.cant_oc,
                          cantidad_in = grouping.Key.cant_in,
                          saldo = grouping.Key.saldo,
                          nom_producto = grouping.Key.pr_descripcion,
                          cod_producto = grouping.Key.pr_codigo,
                          nom_proveedor = grouping.Key.pe_nombreCompleto,
                          nom_unidad_medida = grouping.Key.Descripcion,
                          oc_fecha = grouping.Key.oc_fecha
                      };
            return lst;
        }

        // GET: api/IngresoOrdenCompra/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/IngresoOrdenCompra
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/IngresoOrdenCompra/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/IngresoOrdenCompra/5
        public void Delete(int id)
        {
        }
    }
}
