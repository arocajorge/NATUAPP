namespace Core.App.Helpers
{
    using Interfaces;
    using Core.App.Models;
    using SQLite.Net;
    using SQLiteNetExtensions.Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xamarin.Forms;

    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            try
            {
                var config = DependencyService.Get<IConfig>();
                this.connection = new SQLiteConnection(
                    config.Platform,
                    Path.Combine(config.DirectoryDB, "DBSCI.db3"));
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

        public T First<T>(bool WithChildren) where T: class
        {
            if (WithChildren)
                return connection.GetAllWithChildren<T>().FirstOrDefault();
            else
                return connection.Table<T>().FirstOrDefault();
        }

        public List<T> GetList<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
                return connection.GetAllWithChildren<T>().ToList();
            else
                return connection.Table<T>().ToList();
        }

        public T Find<T>(int pk, bool WithChildren) where T: class
        {
            if (WithChildren)
                return connection.GetAllWithChildren<T>().FirstOrDefault(q => q.GetHashCode() == pk);
            else
                return connection.Table<T>().FirstOrDefault(q => q.GetHashCode() == pk);
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
