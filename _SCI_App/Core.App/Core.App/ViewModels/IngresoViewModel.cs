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
        #endregion

        #region Constructores
        public IngresoViewModel(IngresoOrdenCompraModel ingresoItemViewModel)
        {
            data = new DataAccess();
            Ingreso = ingresoItemViewModel;
            if (Ingreso.FechaApro == null)
            {
                Ingreso.FechaApro = DateTime.Now.Date;
                Ingreso.CantidadApro = 0;
            }
            IsEnabled = true;
        }
        #endregion

        #region Comandos
        public ICommand GuardarCommand
        {
            get { return new RelayCommand(Guardar); }
        }

        private async void Guardar()
        {
            IsEnabled = false;

            data.Guardar(Ingreso);
            this.IsEnabled = true;
            await Application.Current.MainPage.DisplayAlert(
                "Exito",
                "Registro guardado exitósamente",
                "Aceptar");
            MainViewModel.GetInstance().AprobacionIngresos.CargarLista();
            await App.Navigator.Navigation.PopAsync();
        }
        #endregion
    }
}
