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
    public class ProductoController : ApiController
    {
        Entities_general db = new Entities_general();
        // GET: api/Producto
        public IEnumerable<tbl_producto_model> Get(string IdUsuario)
        {
            IEnumerable<tbl_producto_model> lst = from p in db.in_Producto
                                                  join tp in db.tbl_producto
                                                  on new { p.IdEmpresa, p.IdProducto} equals new { tp.IdEmpresa, tp.IdProducto}
                                                  where p.Aparece_modu_Inventario == true
                                                  select new tbl_producto_model
                                                  {
                                                      IdEmpresa = p.IdEmpresa,
                                                      IdProducto = p.IdProducto,
                                                      nom_producto = p.pr_descripcion,
                                                      IdUnidad_consumo = p.IdUnidadMedida_Consumo,
                                                      cod_producto = p.pr_codigo
                                                  };
            return lst;
        }

        // GET: api/Producto/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Producto
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Producto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
        }
    }
}
