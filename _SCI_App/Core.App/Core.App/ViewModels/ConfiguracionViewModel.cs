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
        private bool _IsRunning;
        private bool _IsEnabled;
        private ApiService apiService;
        private DataService dataService;
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
            dataService = new DataService();
            this.usuario = "admin";
            this.UrlServidor = string.IsNullOrEmpty(Settings.UrlConexion) ? "http://192.168.1.115" : Settings.UrlConexion;
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
            Settings.UrlConexion = this.UrlServidor;
            var var_response_usuario = await apiService.GetList<UsuarioModel>(UrlServidor, "/api", "Usuario", "");
            if (!var_response_usuario.IsSuccess)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    var_response_usuario.Message,
                    "Aceptar");
                return;
            }
            List<UsuarioModel> list_usuario = (List<UsuarioModel>)var_response_usuario.Result;
            this.dataService.DeleteAll<UsuarioModel>();
            this.dataService.Save<UsuarioModel>(list_usuario);
            
            var response_empresa = await apiService.GetList<EmpresaModel>(UrlServidor, "/api", "Empresa", usuario);
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
            var response_sucursal = await apiService.GetList<SucursalModel>(UrlServidor, "/api", "Sucursal",usuario);
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
            var response_bodega = await apiService.GetList<BodegaModel>(UrlServidor, "/api", "Bodega", usuario);
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
            var response_centro_costo = await apiService.GetList<CentroCostoModel>(UrlServidor, "/api", "CentroCosto", usuario);
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
            var response_sub_centro_costo = await apiService.GetList<SubCentroCostoModel>(UrlServidor, "/api", "SubCentroCosto", usuario);
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
            var response_producto = await apiService.GetList<ProductoModel>(UrlServidor, "/api", "Producto", usuario);
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

            this.IsEnabled = true;
            this.IsRunning = false;
            await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "OK",
                    "Aceptar");
        }


        #endregion
    }
}
