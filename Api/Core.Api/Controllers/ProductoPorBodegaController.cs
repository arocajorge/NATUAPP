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
    public class ProductoPorBodegaController : ApiController
    {
        Entities_general db = new Entities_general();
        public IEnumerable<tbl_producto_model> Get(string IdUsuario = "")
        {
            db.Database.CommandTimeout = 3000;
            IEnumerable<tbl_producto_model> lst = from p in db.in_Producto
                                                  join tp in db.tbl_producto
                                                  on new { p.IdEmpresa, p.IdProducto } equals new { tp.IdEmpresa, tp.IdProducto }
                                                  join li in db.in_linea
                                                  on new { p.IdEmpresa, p.IdCategoria, p.IdLinea } equals new { li.IdEmpresa, li.IdCategoria, li.IdLinea }
                                                  join pxb in db.tbl_producto_x_tbl_bodega
                                                  on new { p.IdEmpresa, p.IdProducto } equals new { pxb.IdEmpresa, pxb.IdProducto }
                                                  join bxu in db.tbl_usuario_x_bodega
                                                  on new { pxb.IdEmpresa, pxb.IdSucursal, pxb.IdBodega } equals new { bxu.IdEmpresa, bxu.IdSucursal, bxu.IdBodega }
                                                  where p.Aparece_modu_Inventario == true && bxu.IdUsuarioSCI == IdUsuario
                                                  select new tbl_producto_model
                                                  {
                                                      IdEmpresa = p.IdEmpresa,
                                                      IdProducto = p.IdProducto,
                                                      IdSucursal = pxb.IdSucursal,
                                                      IdBodega = pxb.IdBodega,
                                                      nom_producto = p.pr_descripcion,
                                                      IdUnidad_consumo = p.IdUnidadMedida_Consumo,
                                                      cod_producto = p.pr_codigo,
                                                      MuestraObservacionAPP = li.MuestraObservacionAPP,
                                                      MuestraPesoAPP = li.MuestraPesoAPP
                                                  };
            /*
            lst = lst.GroupBy(q => new { q.IdEmpresa, q.IdProducto, q.nom_producto, q.IdUnidad_consumo, q.cod_producto, q.MuestraObservacionAPP, q.MuestraPesoAPP }).Select(q => new tbl_producto_model
            {
                IdEmpresa = q.Key.IdEmpresa,
                IdProducto = q.Key.IdProducto,
                nom_producto = q.Key.nom_producto,
                IdUnidad_consumo = q.Key.IdUnidad_consumo,
                cod_producto = q.Key.cod_producto,
                MuestraObservacionAPP = q.Key.MuestraObservacionAPP,
                MuestraPesoAPP = q.Key.MuestraPesoAPP
            }).ToList();
            */
            return lst;
        }
    }
}

