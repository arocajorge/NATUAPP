using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Core.App.Helpers;
using System;
using Core.App.Views;

namespace Core.App.ViewModels
{
    public class PopUpUbicacionViewModel : BaseViewModel
    {
        #region Variables
        private string _NomEmpresa;
        private string _NomSucursal;
        private string _NomBodega;
        private string _NomCentroCosto;
        #endregion

        #region Propiedades
        public string NomEmpresa{
            get { return this._NomEmpresa; }
            set { SetValue(ref this._NomEmpresa, value); }
        }
        public string NomSucursal
        {
            get { return this._NomSucursal; }
            set { SetValue(ref this._NomSucursal, value); }
        }
        public string NomBodega
        {
            get { return this._NomBodega; }
            set { SetValue(ref this._NomBodega, value); }
        }
        public string NomCentroCosto
        {
            get { return this._NomCentroCosto; }
            set { SetValue(ref this._NomCentroCosto, value); }
        }
        #endregion

        #region Constructor
        public PopUpUbicacionViewModel()
        {
            SetSettings();
        }
        #endregion
        private void SetSettings()
        {
            NomEmpresa = Settings.NomEmpresa;
            NomSucursal = Settings.NomSucursal;
            NomBodega = Settings.NomBodega;
            NomCentroCosto = Settings.NomCentroCosto;
        }
        #region Metodos

        #endregion

        #region Comandos
        public ICommand CerrarCommand {
            get { return new RelayCommand(Cerrar); } }
        public ICommand CambiarUbicacionCommand
        {
            get { return new RelayCommand(CambiarUbicacion); }
        }

        private async void CambiarUbicacion()
        {
            await Application.Current.MainPage.Navigation.PopAllPopupAsync();
            MainViewModel.GetInstance().Parametrizacion = new ParametrizacionViewModel();
            Application.Current.MainPage = new NavigationPage(new ParametrizacionPage());
        }

        private async void Cerrar()
        {
            await Application.Current.MainPage.Navigation.PopAllPopupAsync();
        }
        #endregion
    }
}
