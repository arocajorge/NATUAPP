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
                //var config = DependencyService.Get<IConfig>();
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
                this.connection.Insert(model);
            }else
            {
                OldModel.IdProducto = model.IdProducto;
                OldModel.IdSubCentroCosto = model.IdSubCentroCosto;
                OldModel.Cantidad = model.Cantidad;
                OldModel.Fecha = model.Fecha;
                OldModel.NomProducto = model.NomProducto;
                OldModel.NomSubCentro = model.NomSubCentro;
                OldModel.NomUnidadMedida = model.NomUnidadMedida;
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
            return this.connection.Table<SubCentroCostoModel>().Where(q => q.IdEmpresa == IdEmpresa && q.IdCentroCosto == IdCentroCosto).ToList();
        }

        public List<ProductoModel> GetListProducto(int IdEmpresa)
        {
            return this.connection.Table<ProductoModel>().Where(q=>q.IdEmpresa == IdEmpresa).ToList();
        }

        public List<IngresoOrdenCompraModel> GetListIngresoOrdenCompra()
        {
            return this.connection.Table<IngresoOrdenCompraModel>().ToList();
        }

        public List<UnidadMedidaModel> GetListUnidadMedida()
        {
            return this.connection.Table<UnidadMedidaModel>().ToList();
        }

        public List<StockModel> GetListStock(int IdEmpresa, int IdSucursal, int IdBodega)
        {
            return this.connection.Table<StockModel>().Where(q=>q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega).OrderBy(q=>q.NomProducto).ToList();
        }

        public List<EgresoModel> GetListEgresos(int IdEmpresa, int IdSucursal, int IdBodega, string IdCentroCosto)
        {
            return this.connection.Table<EgresoModel>().Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCentroCosto == IdCentroCosto).OrderBy(q => q.Fecha).ToList();
        }
        #endregion

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
