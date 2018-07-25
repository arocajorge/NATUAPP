using Core.App.Helpers;
using Core.App.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Core.App.ViewModels
{
    public class IngresoViewModel : BaseViewModel
    {
        #region Variables
        private IngresoOrdenCompraModel _Ingreso;
        private bool _IsEnabled;
        private DataAccess data;
        private bool _RecibirTodo;
        private double _CantidadApro;
        private bool _IsVisible;
        #endregion

        #region Propiedades
        public IngresoOrdenCompraModel Ingreso
        {
            get { return this._Ingreso; }
            set { SetValue(ref this._Ingreso, value); }
        }
        public bool IsEnabled
        {
            get { return this._IsEnabled; }
            set { SetValue(ref this._IsEnabled, value); }
        }
        public double CantidadApro
        {
            get { return this._CantidadApro; }
            set { SetValue(ref this._CantidadApro, value); }
        }
        public bool RecibirTodo
        {
            get { return this._RecibirTodo; }
            set {
                SetValue(ref this._RecibirTodo, value);
                if (_RecibirTodo)
                    CantidadApro = Ingreso.Saldo;
            }
        }
        public bool IsVisible
        {
            get { return this._IsVisible; }
            set { SetValue(ref this._IsVisible, value); }
        }
        #endregion

        #region Constructores
        public IngresoViewModel(IngresoOrdenCompraModel ingresoItemViewModel)
        {
            data = new DataAccess();
            Ingreso = ingresoItemViewModel;
            if (Ingreso.FechaApro == DateTime.MinValue)
            {
                Ingreso.FechaApro = DateTime.Now.Date;
                Ingreso.CantidadApro = 0;
                IsVisible = false;
            }
            else
            {
                CantidadApro = Ingreso.CantidadApro;
                IsVisible = true;
            }
            IsEnabled = true;
        }
        #endregion

        #region Comandos
        public ICommand GuardarCommand
        {
            get { return new RelayCommand(Guardar); }
        }

        public ICommand EliminarCommand
        {
            get { return new RelayCommand(Eliminar); }
        }

        private async void Guardar()
        {
            IsEnabled = false;

            if (this.CantidadApro > this.Ingreso.Saldo)
            {
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "La cantidad aprobada debe ser menor al saldo",
                    "Aceptar");
                return;
            }
            this.Ingreso.CantidadApro = CantidadApro;
            data.Guardar(Ingreso);
            this.IsEnabled = true;
            await Application.Current.MainPage.DisplayAlert(
                "Exito",
                "Registro guardado exitósamente",
                "Aceptar");
            MainViewModel.GetInstance().Stock.cargar_stock();
            MainViewModel.GetInstance().AprobacionIngresos.CargarLista();
            await App.Navigator.Navigation.PopAsync();
        }

        private async void Eliminar()
        {
            IsEnabled = false;

            var answer = await Application.Current.MainPage.DisplayAlert("Eliminar", "¿Está seguro que desea eliminar el registro?", "Si", "No");
            if (answer)
            {
                data.DeleteIngreso(Ingreso.PKSQLite);
                IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                "Exito",
                "Registro eliminado exitósamente",
                "Aceptar");
                MainViewModel.GetInstance().Stock.cargar_stock();
                MainViewModel.GetInstance().AprobacionIngresos.CargarLista();
                await App.Navigator.Navigation.PopAsync();
            }

            IsEnabled = true;
        }
        #endregion
    }
}
