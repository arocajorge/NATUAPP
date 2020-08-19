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
            db.Database.CommandTimeout = 3000;
            var lst = (from q in db.sp_stock(IdUsuario)
                       select new vw_stock_model
                       {
                           IdEmpresa = q.IdEmpresa,
                           IdSucursal = q.IdSucursal,
                           IdBodega = q.IdBodega,
                           IdProducto = q.IdProducto,
                           CodProducto = q.CodProducto,
                           NomProducto = q.NomProducto,
                           IdUnidadMedida_Consumo = q.IdUnidadMedida_Consumo,
                           Stock = q.Stock,
                           NomUnidadMedida = q.NomUnidadMedida,
                           CodProdProducto = q.CodProdProducto
                       }).ToList();
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
