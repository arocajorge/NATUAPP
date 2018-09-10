using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Core.Api.Models;
using Core.Api.Data;

namespace Core.Api.Controllers
{
    public class MovimientoController : ApiController
    {
        // GET: api/Movimiento
        public IEnumerable<tbl_sincronizacion_turno> Get()
        {
            Entities_general db = new Entities_general();
            if(db.tbl_sincronizacion_turno.FirstOrDefault() != null)
            {
                TimeSpan span = DateTime.Now - db.tbl_sincronizacion_turno.FirstOrDefault().FechaInicio;
                if (span.TotalMinutes > 15)
                {
                    db.Database.ExecuteSqlCommand("DELETE mobileSCI.tbl_sincronizacion_turno");
                }
            }
            return db.tbl_sincronizacion_turno.ToList();
        }

        // POST: api/Movimiento
        public void Post([FromBody]tbl_movimientos_model model)
        {
            try
            {
                using (Entities_general db = new Entities_general())
                {
                    db.SetCommandTimeOut(3000);

                    #region Valido turno de sincronizacion
                    if (db.tbl_sincronizacion_turno.Count() > 0)
                    {
                        return;
                    }
                    else
                    {
                        tbl_sincronizacion_turno Entity_turno = new tbl_sincronizacion_turno
                        {
                            EnUso = true,
                            FechaInicio = DateTime.Now
                        };
                        db.tbl_sincronizacion_turno.Add(Entity_turno);
                        db.SaveChanges();
                    }
                    #endregion

                    #region Guardo cabecera
                    tbl_movimientos Entity = new tbl_movimientos
                    {
                        IdFecha = model.IdFecha,
                        Fecha = model.Fecha,
                        IdUsuarioSCI = model.IdUsuarioSCI,
                        IdSincronizacion = db.tbl_movimientos.Count() == 0 ? 1 : (db.tbl_movimientos.Max(q => q.IdSincronizacion) + 1)
                    };
                    db.tbl_movimientos.Add(Entity);
                    db.SaveChanges();
                    #endregion

                    #region Guardo detalle
                    int secuencia = 1;
                    foreach (var item in model.lst_det)
                    {
                        tbl_movimientos_det Entity_det = new tbl_movimientos_det
                        {
                            IdSincronizacion = Entity.IdSincronizacion,
                            IdSecuencia = secuencia++,
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdProducto = item.IdProducto,
                            IdUnidadMedida = item.IdUnidadMedida,
                            IdCentroCosto = item.IdCentroCosto,
                            IdCentroCosto_sub_centro_costo = item.IdCentroCosto_sub_centro_costo,
                            Fecha = item.Fecha,
                            cantidad = item.cantidad,
                            IdEmpresa_oc = item.IdEmpresa_oc,
                            IdSucursal_oc = item.IdSucursal_oc,
                            IdOrdenCompra = item.IdOrdenCompra,
                            secuencia_oc = item.secuencia_oc,
                            Aprobado = false,
                            Estado = "A",
                            Peso = item.Peso,
                            Observacion = item.Observacion                            
                        };
                        db.tbl_movimientos_det.Add(Entity_det);
                    }
                    db.SaveChanges();
                    #endregion

                    #region Actualizo estado de ingresos
                    db.sp_revisar_estados_oc();
                    #endregion

                    #region Limpio tabla de turnos
                    db.Database.ExecuteSqlCommand("DELETE mobileSCI.tbl_sincronizacion_turno");
                    #endregion
                }
            }
            catch (Exception)
            {
                #region Limpio tabla de turnos
                using (Entities_general db = new Entities_general())
                {
                    db.Database.ExecuteSqlCommand("DELETE mobileSCI.tbl_sincronizacion_turno");
                }
                #endregion
            }            
        }
    }
}
