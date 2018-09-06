namespace Core.App.Helpers
{
    using Core.App.Models;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            try
            {
                this.connection = new SQLiteConnection(
                    Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DBSCI.db3"));
                connection.CreateTable<UsuarioModel>();
                connection.CreateTable<EmpresaModel>();
                connection.CreateTable<SucursalModel>();
                connection.CreateTable<BodegaModel>();
                connection.CreateTable<CentroCostoModel>();
                connection.CreateTable<SubCentroCostoModel>();
                connection.CreateTable<ProductoModel>();
                connection.CreateTable<IngresoOrdenCompraModel>();
                connection.CreateTable<UnidadMedidaModel>();
                connection.CreateTable<StockModel>();
                connection.CreateTable<EgresoModel>();
                connection.CreateTable<ConsumoSemanalModel>();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void Insert<T>(T model)
        {
            this.connection.Insert(model);
        }

        public void Guardar(EgresoModel model)
        {
            EgresoModel OldModel = this.connection.Table<EgresoModel>().Where(q => q.PKSQLite == model.PKSQLite).FirstOrDefault();
            if(OldModel == null)
            {
                model.PKSQLite = GetID();
                model.Cantidad = Math.Abs(model.Cantidad) * -1;
                this.connection.Insert(model);
            }else
            {
                OldModel.IdProducto = model.IdProducto;
                OldModel.IdSubCentroCosto = model.IdSubCentroCosto;
                OldModel.Cantidad = Math.Abs(model.Cantidad) *-1;
                OldModel.Fecha = model.Fecha;
                OldModel.NomProducto = model.NomProducto;
                OldModel.NomSubCentro = model.NomSubCentro;
                OldModel.NomUnidadMedida = model.NomUnidadMedida;
                OldModel.Peso = model.Peso;
                this.connection.Update(OldModel);
            }
        }

        public void Guardar(IngresoOrdenCompraModel model)
        {
            IngresoOrdenCompraModel OldModel = this.connection.Table<IngresoOrdenCompraModel>().Where(q => q.PKSQLite == model.PKSQLite).FirstOrDefault();
            if (OldModel == null)
                return;
            else
            {
                //Si existe conversion de Uni. OC a Uni. Consumo
                UnidadMedidaModel model_unidad = this.connection.Table<UnidadMedidaModel>().Where(q => q.IdUnidadMedida == model.IdUnidadMedida && q.IdUnidadMedidaEquiva == model.IdUnidadMedidaConsumo).FirstOrDefault();
                if (model_unidad != null)
                    OldModel.CantidadApro_convertida = model.CantidadApro * model_unidad.ValorEquiv;
                else
                {
                    //Busco si es valida la conversión al reves de Uni. Consumo a Uni. OC
                    model_unidad = this.connection.Table<UnidadMedidaModel>().Where(q => q.IdUnidadMedida == model.IdUnidadMedidaConsumo && q.IdUnidadMedidaEquiva == model.IdUnidadMedida).FirstOrDefault();
                    if (model_unidad != null)
                        OldModel.CantidadApro_convertida = model.CantidadApro * model_unidad.ValorEquiv;
                    else
                        OldModel.CantidadApro_convertida = model.CantidadApro;
                }
                OldModel.IdSucursal_apro = Settings.IdSucursal;
                OldModel.IdBodega = Settings.IdBodega;
                OldModel.CantidadApro = model.CantidadApro;
                OldModel.FechaApro = model.FechaApro;                
                this.connection.Update(OldModel);
            }
        }

        private int GetID()
        {
            int ID = 1;
            var lst=  this.connection.Table<EgresoModel>().ToList();
            if (lst.Count > 0)
                ID = lst.Max(q => q.PKSQLite) + 1;
            return ID;
        }

        public void Update<T>(T model)
        {
            this.connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            this.connection.Delete(model);
        }

        public void DeleteEgreso(int PKSQLite)
        {
            this.connection.Table<EgresoModel>().Delete(q => q.PKSQLite == PKSQLite);
        }
        public void DeleteIngreso(int PKSQLite)
        {
            var ingreso = this.connection.Table<IngresoOrdenCompraModel>().Where(q => q.PKSQLite == PKSQLite).FirstOrDefault();
            if (ingreso == null)
                return;
            ingreso.CantidadApro = 0;
            ingreso.CantidadApro_convertida = 0;
            ingreso.FechaApro = DateTime.MinValue;
            this.connection.Update(ingreso);
        }

        public void DeleteAll<T>()
        {
            this.connection.DeleteAll<T>();
        }

        public void InsertAll<T>(List<T> model)
        {
            this.connection.InsertAll(model);
        }

        public UsuarioModel GetUsuario(string IdUsuario, string Contrasenia)
        {
            return this.connection.Table<UsuarioModel>().Where(q => q.IdUsuario == IdUsuario && q.Clave == Contrasenia).FirstOrDefault();
        }

        #region GetList
        public MovimientosModel GetMovimientos()
        {
            MovimientosModel Model = new MovimientosModel
            {
                IdUsuarioSCI = Settings.IdUsuario,
                IdFecha = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")),
                Fecha = DateTime.Now,
                lst_det = new List<MovimientosDetModel>()
            };
            var lst_ingresos = this.connection.Table<IngresoOrdenCompraModel>().Where(q => q.CantidadApro > 0).ToList();
            foreach (var ingreso in lst_ingresos)
            {
                MovimientosDetModel det = new MovimientosDetModel
                {
                    IdEmpresa = ingreso.IdEmpresa,
                    IdSucursal = ingreso.IdSucursal_apro,
                    IdBodega = ingreso.IdBodega,
                    IdProducto = ingreso.IdProducto,
                    cantidad = ingreso.CantidadApro,
                    IdUnidadMedida = ingreso.IdUnidadMedida,
                    Fecha = ingreso.FechaApro,

                    IdCentroCosto = null,
                    IdCentroCosto_sub_centro_costo = null,

                    IdEmpresa_oc = ingreso.IdEmpresa,
                    IdSucursal_oc = ingreso.IdSucursal,
                    IdOrdenCompra = ingreso.IdOrdenCompra,
                    secuencia_oc = ingreso.Secuencia,

                    Peso = 0
                };
                Model.lst_det.Add(det);
            }
            var lst_egreso = this.connection.Table<EgresoModel>().ToList();
            foreach (var egreso in lst_egreso)
            {
                MovimientosDetModel det = new MovimientosDetModel
                {
                    IdEmpresa = egreso.IdEmpresa,
                    IdSucursal = egreso.IdSucursal,
                    IdBodega = egreso.IdBodega,
                    IdProducto = egreso.IdProducto,
                    cantidad = egreso.Cantidad,
                    IdUnidadMedida = egreso.IdUnidadMedida,
                    Fecha = egreso.Fecha,

                    IdCentroCosto = egreso.IdCentroCosto,
                    IdCentroCosto_sub_centro_costo = egreso.IdSubCentroCosto,

                    IdEmpresa_oc = null,
                    IdSucursal_oc = null,
                    IdOrdenCompra = null,
                    secuencia_oc = null,

                    Peso = egreso.Peso
                };
                Model.lst_det.Add(det);
            }

            return Model;
        }
        public List<UsuarioModel> GetListUsuario()
        {
            return this.connection.Table<UsuarioModel>().ToList();
        }

        public List<EmpresaModel> GetListEmpresa()
        {
            return this.connection.Table<EmpresaModel>().ToList();
        }

        public List<SucursalModel> GetListSucursal()
        {
            return this.connection.Table<SucursalModel>().ToList();
        }
        public List<SucursalModel> GetListSucursal(int IdEmpresa)
        {
            return this.connection.Table<SucursalModel>().Where(q=>q.IdEmpresa == IdEmpresa).ToList();
        }

        public List<BodegaModel> GetListBodega()
        {
            return this.connection.Table<BodegaModel>().ToList();
        }

        public List<BodegaModel> GetListBodega(int IdEmpresa, int IdSucursal)
        {
            return this.connection.Table<BodegaModel>().Where(q=>q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal).ToList();
        }

        public List<CentroCostoModel> GetListCentroCosto()
        {
            return this.connection.Table<CentroCostoModel>().ToList();
        }
        public List<CentroCostoModel> GetListCentroCosto(int IdEmpresa)
        {
            return this.connection.Table<CentroCostoModel>().Where(q=>q.IdEmpresa == IdEmpresa).ToList();
        }

        public List<SubCentroCostoModel> GetListSubCentroCosto(int IdEmpresa, string IdCentroCosto)
        {
            return this.connection.Table<SubCentroCostoModel>().Where(q => q.IdEmpresa == IdEmpresa && q.IdCentroCosto == IdCentroCosto).OrderBy(q=>q.Nom_subcentro).ToList();
        }

        public List<ProductoModel> GetListProducto(int IdEmpresa)
        {
            return this.connection.Table<ProductoModel>().Where(q=>q.IdEmpresa == IdEmpresa).OrderBy(q=>q.NomProducto).ToList();
        }

        public List<IngresoOrdenCompraModel> GetListIngresoOrdenCompra(int IdEmpresa, int IdSucursal, int IdBodega, bool MostrarAprobados = false)
        {
            List<IngresoOrdenCompraModel> Lista;
            if (!MostrarAprobados)
            {
                Lista = this.connection.Table<IngresoOrdenCompraModel>().Where(q => q.IdEmpresa == IdEmpresa && q.CantidadApro == 0).OrderBy(q => q.NomProducto).ToList();
            }
            else
            {
                if (IdEmpresa != 0)
                    Lista = this.connection.Table<IngresoOrdenCompraModel>().Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal_apro == IdSucursal && q.IdBodega == IdBodega && q.CantidadApro > 0).OrderBy(q => q.NomProducto).ToList();
                else
                    Lista = this.connection.Table<IngresoOrdenCompraModel>().Where(q => q.CantidadApro > 0).OrderBy(q => q.NomProducto).ToList();
            }
            Lista.ForEach(q => q.CantidadOcConsulta = q.CantidadApro > 0 ? q.CantidadApro : q.Saldo);
            return Lista;
        }

        public List<UnidadMedidaModel> GetListUnidadMedida()
        {
            return this.connection.Table<UnidadMedidaModel>().ToList();
        }
        public List<ConsumoSemanalModel> GetListConsumoSemanal(int IdEmpresa, int IdSucursal, int IdBodega, string IdCentroCosto)
        {
            return this.connection.Table<ConsumoSemanalModel>().Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCentroCosto == IdCentroCosto
            && (q.Lunes > 0 || q.Martes > 0 || q.Miercoles > 0 || q.Jueves > 0 || q.Viernes > 0 || q.Domingo > 0 || q.Sabado > 0)
            ).OrderBy(q => q.NomProducto).ThenBy(q=>q.NomSubCentro).ToList();
        }
        public List<StockModel> GetListStock(int IdEmpresa, int IdSucursal, int IdBodega)
        {
            var lst_stock = this.connection.Table<StockModel>().Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega).ToList();
            var lst_ingresos = this.connection.Table<IngresoOrdenCompraModel>().Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal_apro == IdSucursal && q.IdBodega == IdBodega).ToList();
            var lst_egresos = this.connection.Table<EgresoModel>().Where(q=> q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega).ToList();

            lst_ingresos = (from q in lst_ingresos
                            group q by q.IdProducto into g
                            select new IngresoOrdenCompraModel
                            {
                                IdProducto = g.Key,
                                CantidadApro_convertida = g.Sum(q=>q.CantidadApro_convertida)
                            }).ToList();

            lst_egresos = (from q in lst_egresos
                           group q by q.IdProducto into g
                           select new EgresoModel
                           {
                               IdProducto = g.Key,
                               Cantidad = g.Sum(q => q.Cantidad)
                           }).ToList();

            var lst = (from s in lst_stock
                       join i in lst_ingresos
                       on s.IdProducto equals i.IdProducto into ig
                       from ing in ig.DefaultIfEmpty()

                       join e in lst_egresos                       
                       on s.IdProducto equals e.IdProducto into eg
                       from egr in eg.DefaultIfEmpty()
                       
                       select new StockModel
                       {
                           PKSQLite = s.PKSQLite,
                           IdEmpresa = s.IdEmpresa,
                           IdSucursal = s.IdSucursal,
                           IdProducto = s.IdProducto,
                           IdBodega = s.IdBodega,
                           NomProducto = s.NomProducto,
                           NomUnidadMedida = s.NomUnidadMedida,
                           Stock = s.Stock,
                           Ingresos = ing == null ? 0 : ing.CantidadApro_convertida,
                           Egresos = egr == null ? 0 : egr.Cantidad
                       }).ToList();

            lst.ForEach(q => q.Saldo = q.Stock + q.Ingresos + q.Egresos);

            return lst.OrderBy(q=>q.NomProducto).ToList();
            //return this.connection.Table<StockModel>().Where(q=>q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega).OrderBy(q=>q.NomProducto).ToList();

        }

        public List<EgresoModel> GetListEgresos(int IdEmpresa, int IdSucursal, int IdBodega, string IdCentroCosto)
        {
            if(IdEmpresa != 0)
                return this.connection.Table<EgresoModel>().Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCentroCosto == IdCentroCosto).OrderBy(q => q.Fecha).ToList();
            else
                return this.connection.Table<EgresoModel>().ToList();
        }
        #endregion

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
