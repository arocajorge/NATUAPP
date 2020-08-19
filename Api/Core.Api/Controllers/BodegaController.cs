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
    public class BodegaController : ApiController
    {
        // GET: api/Bodega
        Entities_general db = new Entities_general();
        public IEnumerable<tbl_bodega_model> Get(string IdUsuario)
        {
            db.Database.CommandTimeout = 3000;
            IEnumerable<tbl_bodega_model> lst = from u in db.tbl_usuario
                                                join b in db.tbl_usuario_x_bodega
                                                on u.IdUsuarioSCI equals b.IdUsuarioSCI
                                                join e in db.tbl_bodega
                                                on new { b.IdEmpresa, b.IdSucursal, b.IdBodega } equals new { e.IdEmpresa, e.IdSucursal, e.IdBodega }
                                                join bo in db.tb_bodega
                                                on new { b.IdEmpresa, b.IdSucursal, b.IdBodega } equals new { bo.IdEmpresa, bo.IdSucursal, bo.IdBodega }
                                                where u.IdUsuarioSCI == IdUsuario
                                                group new { u, b, e, bo } by new { bo.IdEmpresa, bo.IdSucursal, bo.IdBodega, bo.bo_Descripcion }
                                                  into grouping

                                                select new tbl_bodega_model
                                                {
                                                    IdEmpresa = grouping.Key.IdEmpresa,
                                                    IdSucursal = grouping.Key.IdSucursal,
                                                    IdBodega = grouping.Key.IdBodega,
                                                    nom_bodega = grouping.Key.bo_Descripcion
                                                };

            return lst;
        }

        // GET: api/Bodega/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Bodega
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Bodega/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bodega/5
        public void Delete(int id)
        {
        }
    }
}
