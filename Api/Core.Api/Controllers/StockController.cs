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
    public class StockController : ApiController
    {
        Entities_general db = new Entities_general();

        // GET: api/Stock
        public IEnumerable<vw_stock_model> Get(string IdUsuario = "")
        {
            db.SetCommandTimeOut(3000);
            var lst = from s in db.vw_stock
                      join b in db.tbl_usuario_x_bodega
                      on new { s.IdEmpresa, s.IdSucursal, s.IdBodega } equals new { b.IdEmpresa, b.IdSucursal, b.IdBodega }
                      where b.IdUsuarioSCI == IdUsuario
                      select new vw_stock_model
                      {
                          IdEmpresa = s.IdEmpresa,
                          IdSucursal = s.IdSucursal,
                          IdBodega = s.IdBodega,
                          IdProducto = s.IdProducto,
                          CodProducto = s.CodProducto,
                          NomProducto = s.NomProducto,
                          IdUnidadMedida_Consumo = s.IdUnidadMedida_Consumo,
                          Stock = s.Stock,
                          NomUnidadMedida = s.NomUnidadMedida
                      };
            return lst;
        }

        // GET: api/Stock/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Stock
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Stock/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Stock/5
        public void Delete(int id)
        {
        }
    }
}
