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
    public class UnidadMedidaController : ApiController
    {
        Entities_general db = new Entities_general();
        // GET: api/UnidadMedida
        public IEnumerable<tbl_unidad_medida_model> Get()
        {
            var lst = from q in db.in_UnidadMedida
                      select new tbl_unidad_medida_model
                      {
                          IdUnidadMedida = q.IdUnidadMedida,
                          nom_unidad_medida = q.Descripcion
                      };
            return lst;
        }

        // GET: api/UnidadMedida/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UnidadMedida
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UnidadMedida/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UnidadMedida/5
        public void Delete(int id)
        {
        }
    }
}
