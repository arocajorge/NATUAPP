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
    public class CentroCostoController : ApiController
    {
        Entities_general db = new Entities_general();
        // GET: api/CentroCosto
        public IEnumerable<tbl_centro_costo_model> Get(string IdUsuario)
        {
            db.SetCommandTimeOut(3000);
            IEnumerable<tbl_centro_costo_model> lst = from u in db.tbl_usuario
                                                join us in db.tbl_usuario_x_subcentro
                                                on u.IdUsuarioSCI equals us.IdUsuarioSCI
                                                join e in db.tbl_subcentro
                                                on new { us.IdEmpresa, us.IdCentroCosto, us.IdCentroCosto_sub_centro_costo } equals new { e.IdEmpresa, e.IdCentroCosto, e.IdCentroCosto_sub_centro_costo }                                                      
                                                join c in db.ct_centro_costo
                                                on new { e.IdEmpresa, e.IdCentroCosto} equals new { c.IdEmpresa, c.IdCentroCosto}
                                                      where u.IdUsuarioSCI == IdUsuario
                                                      group c by new { c.IdEmpresa, c.IdCentroCosto, c.Centro_costo }
                                                  into grouping
                                                select new tbl_centro_costo_model
                                                {
                                                    IdEmpresa = grouping.Key.IdEmpresa,
                                                    IdCentroCosto = grouping.Key.IdCentroCosto,
                                                    nom_centro_costo = grouping.Key.Centro_costo
                                                };

            return lst;
        }

        // GET: api/CentroCosto/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CentroCosto
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CentroCosto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CentroCosto/5
        public void Delete(int id)
        {
        }
    }
}
