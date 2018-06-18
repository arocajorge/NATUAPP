﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Api.Data;
using Core.Api.Models;

namespace Core.Api.Controllers
{
    public class SubCentroCostoController : ApiController
    {
        Entities_general db = new Entities_general();
        // GET: api/SubCentroCosto
        public IEnumerable<tbl_subcentro_costo_model> Get(string IdUsuario)
        {
            db.SetCommandTimeOut(3000);
            IEnumerable<tbl_subcentro_costo_model> lst = from u in db.tbl_usuario
                                                      join us in db.tbl_usuario_x_subcentro
                                                      on u.IdUsuarioSCI equals us.IdUsuarioSCI
                                                      join e in db.tbl_subcentro
                                                      on new { us.IdEmpresa, us.IdCentroCosto, us.IdCentroCosto_sub_centro_costo } equals new { e.IdEmpresa, e.IdCentroCosto, e.IdCentroCosto_sub_centro_costo }
                                                      join c in db.ct_centro_costo_sub_centro_costo
                                                      on new { e.IdEmpresa, e.IdCentroCosto, e.IdCentroCosto_sub_centro_costo } equals new { c.IdEmpresa, c.IdCentroCosto, c.IdCentroCosto_sub_centro_costo }
                                                         where u.IdUsuarioSCI == IdUsuario
                                                         select new tbl_subcentro_costo_model
                                                      {
                                                          IdEmpresa = c.IdEmpresa,
                                                          IdCentroCosto = c.IdCentroCosto,
                                                          IdSubCentroCosto = c.IdCentroCosto_sub_centro_costo,
                                                          nom_subcentro = c.Centro_costo
                                                      };

            return lst;
        }

        // GET: api/SubCentroCosto/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SubCentroCosto
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SubCentroCosto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SubCentroCosto/5
        public void Delete(int id)
        {
        }
    }
}
