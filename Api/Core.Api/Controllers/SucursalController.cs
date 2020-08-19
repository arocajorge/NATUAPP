using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Api.Data;
using Core.Api.Models;

namespace Core.Api.Controllers
{
    public class SucursalController : ApiController
    {
        Entities_general db = new Entities_general();
        // GET: api/SCI
        public IEnumerable<tbl_sucursal_model> Get(string IdUsuario)
        {
            db.Database.CommandTimeout = 3000;
            IEnumerable<tbl_sucursal_model> lst = from u in db.tbl_usuario
                                                  join b in db.tbl_usuario_x_bodega
                                                  on u.IdUsuarioSCI equals b.IdUsuarioSCI
                                                  join e in db.tbl_bodega
                                                  on new { b.IdEmpresa, b.IdSucursal, b.IdBodega } equals new { e.IdEmpresa, e.IdSucursal, e.IdBodega }
                                                  join s in db.tb_sucursal 
                                                  on new { b.IdEmpresa, b.IdSucursal} equals new { s.IdEmpresa, s.IdSucursal}
                                                  where u.IdUsuarioSCI == IdUsuario
                                                  group new { u,b,e,s} by new { s.IdEmpresa, s.IdSucursal, s.Su_Descripcion}
                                                  into grouping
                                                  
                                                  select new tbl_sucursal_model
                                                  {
                                                      IdEmpresa = grouping.Key.IdEmpresa,
                                                      IdSucursal = grouping.Key.IdSucursal,
                                                      nom_sucursal = grouping.Key.Su_Descripcion
                                                  };

            return lst;
        }

        

        // GET: api/SCI/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SCI
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SCI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SCI/5
        public void Delete(int id)
        {
        }
    }
}
