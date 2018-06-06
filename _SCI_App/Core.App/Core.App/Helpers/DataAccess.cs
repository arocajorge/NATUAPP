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

        public List<ProductoModel> GetListProducto()
        {
            return this.connection.Table<ProductoModel>().ToList();
        }

        public List<IngresoOrdenCompraModel> GetListIngresoOrdenCompra()
        {
            return this.connection.Table<IngresoOrdenCompraModel>().ToList();
        }

        public List<UnidadMedidaModel> GetListUnidadMedida()
        {
            return this.connection.Table<UnidadMedidaModel>().ToList();
        }
        #endregion
        
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
