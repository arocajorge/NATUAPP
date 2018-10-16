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
            db.SetCommandTimeOut(3000);
            var lst = (from q in db.sp_oc_x_aprobar()
                       select new tbl_ingreso_oc_model
                       {
                           IdEmpresa = q.IdEmpresa,
                           IdSucursal = q.IdSucursal,
                           IdOrdenCompra = q.IdOrdenCompra,
                           Secuencia = q.Secuencia,
                           IdProducto = q.IdProducto,
                           IdUnidadMedida = q.IdUnidadMedida,
                           cantidad_in = q.cant_in,
                           cantidad_oc = q.cant_oc,
                           saldo = q.saldo,
                           IdProveedor = q.IdProveedor,
                           nom_unidad_medida = q.Descripcion,
                           nom_proveedor = q.pe_nombreCompleto,
                           nom_producto = q.pr_descripcion,
                           cod_producto = q.pr_codigo,
                           oc_fecha = q.oc_fecha,
                           IdUnidadMedida_Consumo = q.IdUnidadMedida_Consumo,
                           oc_observacion = q.oc_observacion,
                           NomSucursal = q.NomSucursal
                       }).ToList();
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
