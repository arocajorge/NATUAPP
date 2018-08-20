using Core.Api.Data;
using Core.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

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
    }
}
