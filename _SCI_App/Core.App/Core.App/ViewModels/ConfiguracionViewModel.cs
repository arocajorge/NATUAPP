using Core.App.Helpers;
using Core.App.Models;
using Core.App.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Core.App.ViewModels
{
    public class ConfiguracionViewModel : BaseViewModel
    {
        #region Variables
        private string _usuario;
        private string _urlServidor;
        private string _RutaCarpeta;
        private bool _IsRunning;
        private bool _IsEnabled;
        private ApiService apiService;
        private DataAccess data = new DataAccess();
        #endregion

        #region Propiedades
        public string usuario
        {
            get { return this._usuario; }
            set { SetValue(ref this._usuario, value); }
        }
        public string UrlServidor
        {
            get { return this._urlServidor; }
            set { SetValue(ref this._urlServidor, value); }
        }
        public string RutaCarpeta
        {
            get { return this._RutaCarpeta; }
            set { SetValue(ref this._RutaCarpeta, value); }
        }
        public bool IsRunning
        {
            get { return this._IsRunning; }
            set { SetValue(ref this._IsRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this._IsEnabled; }
            set { SetValue(ref this._IsEnabled, value); }
        }
        #endregion

        #region Constructor
        public ConfiguracionViewModel()
        {
            this.IsEnabled = true;
            apiService = new ApiService();
            data = new DataAccess();
            this.usuario = "admin";
            this.UrlServidor = string.IsNullOrEmpty(Settings.UrlConexion) ? "http://nat-app-01.naturisa.com.ec" : Settings.UrlConexion;
            this.RutaCarpeta = string.IsNullOrEmpty(Settings.RutaCarpeta) ? "/vzenmob/Api" : Settings.RutaCarpeta;
        }
        #endregion

        #region Metodos
        public ICommand SincronizarCommand
        {
            get
            {
                return new RelayCommand(Sincronizar);
            }
        }

        private async void Sincronizar()
        {
            this.IsEnabled = false;
            this.IsRunning = true;

            #region Validaciones
            if (string.IsNullOrEmpty(usuario))
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Debe ingresar el usuario",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(UrlServidor))
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Debe ingresar la url del servidor",
                    "Aceptar");
                return;
            }
            Response con = await apiService.CheckConnection(this.UrlServidor);
            if (!con.IsSuccess)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    con.Message,
                    "Aceptar");
                return;
            }
            #endregion

            Settings.UrlConexion = this.UrlServidor;
            Settings.RutaCarpeta = this.RutaCarpeta;

            #region Usuario
            var response_usuario = await apiService.GetList<UsuarioModel>(UrlServidor, RutaCarpeta, "Usuario", usuario);
            if (!response_usuario.IsSuccess)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    response_usuario.Message,
                    "Aceptar");
                return;
            }

            var list_usuario = (List<UsuarioModel>)response_usuario.Result;
            if (list_usuario.Count == 0)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "El usuario no se encuentra registrado para el uso de la aplicación",
                    "Aceptar");
                return;
            }
            data.DeleteAll<EgresoModel>();
            data.DeleteAll<UsuarioModel>();
            int PKI = 1;
            list_usuario.ForEach(q => q.PKSQLite = PKI++);
            data.InsertAll<UsuarioModel>(list_usuario);
            #endregion
            
            #region Empresas
            var response_empresa = await apiService.GetList<EmpresaModel>(UrlServidor, RutaCarpeta, "Empresa", usuario);
            if (!response_empresa.IsSuccess)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    response_empresa.Message,
                    "Aceptar");
                return;
            }
            var list_empresa = (List<EmpresaModel>)response_empresa.Result;
            data.DeleteAll<EmpresaModel>();
            PKI = 1;
            list_empresa.ForEach(q => { q.PKSQLite = PKI++; q.NomEmpresa = q.NomEmpresa.Trim(); });
            data.InsertAll<EmpresaModel>(list_empresa);
            #endregion

            #region Sucursales
            var response_sucursal = await apiService.GetList<SucursalModel>(UrlServidor, RutaCarpeta, "Sucursal", usuario);
            if (!response_sucursal.IsSuccess)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    response_sucursal.Message,
                    "Aceptar");
                return;
            }
            var list_sucursal = (List<SucursalModel>)response_sucursal.Result;
            data.DeleteAll<SucursalModel>();
            PKI = 1;
            list_sucursal.ForEach(q => { q.PKSQLite = PKI++; q.Nom_sucursal = q.Nom_sucursal.Trim(); });
            data.InsertAll<SucursalModel>(list_sucursal);
            #endregion

            #region Bodegas
            var response_bodega = await apiService.GetList<BodegaModel>(UrlServidor, RutaCarpeta, "Bodega", usuario);
            if (!response_bodega.IsSuccess)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    response_bodega.Message,
                    "Aceptar");
                return;
            }
            var list_bodega = (List<BodegaModel>)response_bodega.Result;
            data.DeleteAll<BodegaModel>();
            PKI = 1;
            list_bodega.ForEach(q => { q.PKSQLite = PKI++; q.Nom_bodega = q.Nom_bodega.Trim(); });
            data.InsertAll<BodegaModel>(list_bodega);
            #endregion

            #region Centros de costo
            var response_centro_costo = await apiService.GetList<CentroCostoModel>(UrlServidor, RutaCarpeta, "CentroCosto", usuario);
            if (!response_centro_costo.IsSuccess)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    response_centro_costo.Message,
                    "Aceptar");
                return;
            }
            var list_CentroCosto = (List<CentroCostoModel>)response_centro_costo.Result;
            data.DeleteAll<CentroCostoModel>();
            PKI = 1;
            list_CentroCosto.ForEach(q => { q.PKSQLite = PKI++; q.Nom_centro_costo = q.Nom_centro_costo.Trim(); });
            data.InsertAll<CentroCostoModel>(list_CentroCosto);
            #endregion

            #region Sub centros de costo
            var response_sub_centro_costo = await apiService.GetList<SubCentroCostoModel>(UrlServidor, RutaCarpeta, "SubCentroCosto", usuario);
            if (!response_sub_centro_costo.IsSuccess)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    response_sub_centro_costo.Message,
                    "Aceptar");
                return;
            }
            var list_subcentro = (List<SubCentroCostoModel>)response_sub_centro_costo.Result;
            data.DeleteAll<SubCentroCostoModel>();
            PKI = 1;
            list_subcentro.ForEach(q => { q.PKSQLite = PKI++; q.Nom_subcentro = q.Nom_subcentro.Trim(); });
            data.InsertAll<SubCentroCostoModel>(list_subcentro);
            #endregion

            #region Productos
            var response_producto = await apiService.GetList<ProductoModel>(UrlServidor, RutaCarpeta, "Producto", usuario);
            if (!response_producto.IsSuccess)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    response_producto.Message,
                    "Aceptar");
                return;
            }
            var list_producto = (List<ProductoModel>)response_producto.Result;
            data.DeleteAll<ProductoModel>();
            PKI = 1;
            list_producto.ForEach(q => { q.PKSQLite = PKI++; q.NomProducto = q.NomProducto.Trim(); });
            data.InsertAll<ProductoModel>(list_producto);
            #endregion

            #region Stock
            var response_stock = await apiService.GetList<StockModel>(UrlServidor, RutaCarpeta, "Stock", usuario);
            if (!response_stock.IsSuccess)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    response_stock.Message,
                    "Aceptar");
                return;
            }
            var list_stock = (List<StockModel>)response_stock.Result;
            data.DeleteAll<StockModel>();
            PKI = 1;
            list_stock.ForEach(q => { q.PKSQLite = PKI++; q.NomProducto = q.NomProducto.Trim(); q.NomUnidadMedida = q.NomUnidadMedida.Trim(); });
            data.InsertAll<StockModel>(list_stock);
            #endregion

            #region Ingresos por OC
            var response_oc = await apiService.GetList<IngresoOrdenCompraModel>(UrlServidor, RutaCarpeta, "IngresoOrdenCompra", usuario);
            if (!response_oc.IsSuccess)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    response_oc.Message,
                    "Aceptar");
                return;
            }
            var list_oc = (List<IngresoOrdenCompraModel>)response_oc.Result;
            data.DeleteAll<IngresoOrdenCompraModel>();
            PKI = 1;
            list_oc.ForEach(q => { q.PKSQLite = PKI++; q.NomProducto = q.NomProducto.Trim(); q.NomProveedor = q.NomProveedor.Trim(); q.NomUnidadMedida = q.NomUnidadMedida.Trim(); });
            data.InsertAll<IngresoOrdenCompraModel>(list_oc);
            #endregion

            #region Limpio los settings
            Settings.IdEmpresa = 0;
            Settings.IdSucursal = 0;
            Settings.IdBodega = 0;
            Settings.IdCentroCosto = "0";
            Settings.NomEmpresa = string.Empty;
            Settings.NomSucursal = string.Empty;
            Settings.NomBodega = string.Empty;
            Settings.NomCentroCosto = string.Empty;
            Settings.IdUsuario = string.Empty;
            #endregion

            this.IsEnabled = true;
            this.IsRunning = false;
            await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Configuración OK",
                    "Aceptar");
        }
        #endregion
    }
}
