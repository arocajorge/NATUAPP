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
        private string _MensajeSincronizacion;
        private ApiService apiService;
        private DataAccess data = new DataAccess();
        #endregion

        #region Propiedades
        public string usuario
        {
            get { return this._usuario; }
            set { SetValue(ref this._usuario, value); }
        }
        public string MensajeSincronizacion
        {
            get { return this._MensajeSincronizacion; }
            set { SetValue(ref this._MensajeSincronizacion, value); }
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
            MensajeSincronizacion = string.Empty;
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
            MensajeSincronizacion = "Validando que el usuario exista en el sistema";
            if (string.IsNullOrEmpty(usuario))
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                MensajeSincronizacion = string.Empty;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Debe ingresar el usuario",
                    "Aceptar");
                return;
            }
            MensajeSincronizacion = "Validando dirección del servidor";
            if (string.IsNullOrEmpty(UrlServidor))
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                MensajeSincronizacion = string.Empty;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Debe ingresar la url del servidor",
                    "Aceptar");
                return;
            }
            MensajeSincronizacion = "Validando conexión";
            Response con = await apiService.CheckConnection(this.UrlServidor);
            if (!con.IsSuccess)
            {
                MensajeSincronizacion = string.Empty;
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    con.Message,
                    "Aceptar");
                return;
            }
            #endregion
            MensajeSincronizacion = "Seteando rutas de servidor y carpeta";
            Settings.UrlConexion = this.UrlServidor;
            Settings.RutaCarpeta = this.RutaCarpeta;

            #region Unidad de medida
            MensajeSincronizacion = "Sincronizando unidades de medida";
            var response_unidad = await apiService.GetList<UnidadMedidaModel>(UrlServidor, RutaCarpeta, "UnidadMedida", "");
            if (!response_unidad.IsSuccess)
            {
                MensajeSincronizacion = string.Empty;
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    response_unidad.Message,
                    "Aceptar");
                return;
            }

            var list_unidades = (List<UnidadMedidaModel>)response_unidad.Result;
            data.DeleteAll<UnidadMedidaModel>();
            int PKI = 1;
            list_unidades.ForEach(q => q.PKSQLite = PKI++);
            MensajeSincronizacion = "Insertando unidades de medida";
            data.InsertAll<UnidadMedidaModel>(list_unidades);
            #endregion

            #region Usuario
            MensajeSincronizacion = "Sincronizando información de usuario";
            var response_usuario = await apiService.GetList<UsuarioModel>(UrlServidor, RutaCarpeta, "Usuario", usuario);
            if (!response_usuario.IsSuccess)
            {
                MensajeSincronizacion = string.Empty;
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
                MensajeSincronizacion = string.Empty;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "El usuario no se encuentra registrado para el uso de la aplicación",
                    "Aceptar");
                return;
            }
            data.DeleteAll<EgresoModel>();
            data.DeleteAll<UsuarioModel>();
            PKI = 1;
            list_usuario.ForEach(q => q.PKSQLite = PKI++);
            MensajeSincronizacion = "Insertando información de usuarios";
            data.InsertAll<UsuarioModel>(list_usuario);
            #endregion

            #region Empresas
            MensajeSincronizacion = "Sincronizando empresas";
            var response_empresa = await apiService.GetList<EmpresaModel>(UrlServidor, RutaCarpeta, "Empresa", usuario);
            if (!response_empresa.IsSuccess)
            {
                MensajeSincronizacion = string.Empty;
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
            MensajeSincronizacion = "Insertando empresas";
            data.InsertAll<EmpresaModel>(list_empresa);
            #endregion

            #region Sucursales
            MensajeSincronizacion = "Sincronizando sucursales";
            var response_sucursal = await apiService.GetList<SucursalModel>(UrlServidor, RutaCarpeta, "Sucursal", usuario);
            if (!response_sucursal.IsSuccess)
            {
                MensajeSincronizacion = string.Empty;
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
            MensajeSincronizacion = "Insertando sucursales";
            data.InsertAll<SucursalModel>(list_sucursal);
            #endregion

            #region Bodegas
            MensajeSincronizacion = "Sincronizando bodegas";
            var response_bodega = await apiService.GetList<BodegaModel>(UrlServidor, RutaCarpeta, "Bodega", usuario);
            if (!response_bodega.IsSuccess)
            {
                MensajeSincronizacion = string.Empty;
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
            MensajeSincronizacion = "Insertando bodegas";
            data.InsertAll<BodegaModel>(list_bodega);
            #endregion

            #region Centros de costo
            MensajeSincronizacion = "Sincronizando centros de costo";
            var response_centro_costo = await apiService.GetList<CentroCostoModel>(UrlServidor, RutaCarpeta, "CentroCosto", usuario);
            if (!response_centro_costo.IsSuccess)
            {
                MensajeSincronizacion = string.Empty;
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
            MensajeSincronizacion = "Insertando centros de costo";
            data.InsertAll<CentroCostoModel>(list_CentroCosto);
            #endregion

            #region Sub centros de costo
            MensajeSincronizacion = "Sincronizando subcentros de costo";
            var response_sub_centro_costo = await apiService.GetList<SubCentroCostoModel>(UrlServidor, RutaCarpeta, "SubCentroCosto", usuario);
            if (!response_sub_centro_costo.IsSuccess)
            {
                MensajeSincronizacion = string.Empty;
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
            MensajeSincronizacion = "Insertando subcentros de costo";
            data.InsertAll<SubCentroCostoModel>(list_subcentro);
            #endregion

            #region Productos
            MensajeSincronizacion = "Sincronizando productos";
            var response_producto = await apiService.GetList<ProductoModel>(UrlServidor, RutaCarpeta, "ProductoPorBodega", usuario);
            if (!response_producto.IsSuccess)
            {
                MensajeSincronizacion = string.Empty;
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
            MensajeSincronizacion = "Insertando productos";
            data.InsertAll<ProductoModel>(list_producto);
            #endregion

            #region Stock
            MensajeSincronizacion = "Sincronizando stock";
            var response_stock = await apiService.GetList<StockModel>(UrlServidor, RutaCarpeta, "Stock", usuario);
            if (!response_stock.IsSuccess)
            {
                MensajeSincronizacion = string.Empty;
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
            MensajeSincronizacion = "Insertando stock";
            data.InsertAll<StockModel>(list_stock);
            #endregion

            #region Ingresos por OC
            MensajeSincronizacion = "Sincronizando ordenes de compra";
            var response_oc = await apiService.GetList<IngresoOrdenCompraModel>(UrlServidor, RutaCarpeta, "IngresoOrdenCompra", usuario);
            if (!response_oc.IsSuccess)
            {
                MensajeSincronizacion = string.Empty;
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
            list_oc.ForEach(q => { q.PKSQLite = PKI++; q.NomProducto = q.NomProducto.Trim(); q.NomProveedor = q.NomProveedor.Trim(); q.NomUnidadMedida = q.NomUnidadMedida.Trim(); q.CantidadApro = 0; });
            MensajeSincronizacion = "Insertando órdenes de compra";
            data.InsertAll<IngresoOrdenCompraModel>(list_oc);
            #endregion

            #region ConsumoSemanal
            MensajeSincronizacion = "Sincronizando consumo semanal";
            var response_cs = await apiService.GetList<ConsumoSemanalModel>(UrlServidor, RutaCarpeta, "ConsumoSemanal", usuario);
            if (!response_cs.IsSuccess)
            {
                MensajeSincronizacion = string.Empty;
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    response_cs.Message,
                    "Aceptar");
                return;
            }
            var list_cs = (List<ConsumoSemanalModel>)response_cs.Result;
            data.DeleteAll<ConsumoSemanalModel>();
            PKI = 1;
            list_cs.ForEach(q => { q.PKSQLite = PKI++; q.Lunes = Math.Abs(q.Lunes); q.Martes = Math.Abs(q.Martes); q.Miercoles = Math.Abs(q.Miercoles); q.Jueves = Math.Abs(q.Jueves); q.Viernes = Math.Abs(q.Viernes); q.Sabado = Math.Abs(q.Sabado); q.Domingo = Math.Abs(q.Domingo); q.Total = Math.Abs(q.Total); });
            MensajeSincronizacion = "Insertando consumo semanal";
            data.InsertAll<ConsumoSemanalModel>(list_cs);
            #endregion

            #region Limpio los settings
            MensajeSincronizacion = "Reseteando configuraciones";
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

            MensajeSincronizacion = string.Empty;
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
