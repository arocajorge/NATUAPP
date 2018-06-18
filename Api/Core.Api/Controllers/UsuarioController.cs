using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Api.Models;
using Core.Api.Data;

namespace Core.Api.Controllers
{
    public class UsuarioController : ApiController
    {
        Entities_general db = new Entities_general();
        // GET: api/Usuario
        public IEnumerable<tbl_usuario_model> Get(string IdUsuario = "")
        {
            db.SetCommandTimeOut(3000);
            var lst = from q in db.tbl_usuario
                      where q.estado == true
                      && q.IdUsuarioSCI == IdUsuario
                      select new tbl_usuario_model
                      {
                          IdUsuario = q.IdUsuarioSCI,
                          clave = q.clave
                      };

            return lst;
        }

        // GET: api/Usuario/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Usuario
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Usuario/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usuario/5
        public void Delete(int id)
        {
        }
    }
}
