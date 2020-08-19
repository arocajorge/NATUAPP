using Core.Api.Models;
using Core.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Core.Api.Controllers
{
    public class EmpresaController : ApiController
    {
        Entities_general db = new Entities_general();
        // GET: api/Empresa
        public IEnumerable<tbl_empresa_model> Get(string IdUsuario)
        {
            db.Database.CommandTimeout = 3000;
            IEnumerable<tbl_empresa_model> lst = from u in db.tbl_usuario
                                                join b in db.tbl_usuario_x_bodega
                                                on u.IdUsuarioSCI equals b.IdUsuarioSCI
                                                join e in db.tbl_bodega
                                                on new { b.IdEmpresa, b.IdSucursal, b.IdBodega } equals new { e.IdEmpresa, e.IdSucursal, e.IdBodega }
                                                join bo in db.tb_empresa
                                                on b.IdEmpresa equals bo.IdEmpresa
                                                where u.IdUsuarioSCI == IdUsuario
                                                group new { u, b, e, bo } by new { bo.IdEmpresa, bo.em_nombre }
                                                  into grouping

                                                select new tbl_empresa_model
                                                {
                                                    IdEmpresa = grouping.Key.IdEmpresa,
                                                    nom_empresa = grouping.Key.em_nombre
                                                };

            return lst;
        }

        // GET: api/Empresa/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Empresa
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Empresa/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Empresa/5
        public void Delete(int id)
        {
        }
    }
}
