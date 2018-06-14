namespace Core.App.ViewModels
{
    using Core.App.Helpers;
    using Core.App.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class MenuItemViewModel
    {
        #region Propiedades
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Comandos
        public ICommand NavigateCommand {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        private void Navigate()
        {
            App.Master.IsPresented = false;
            switch (this.PageName)
            {                
                case "LoginPage":
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
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
                case "ParametrizacionPage":
                    MainViewModel.GetInstance().Parametrizacion = new ParametrizacionViewModel();
                    Application.Current.MainPage = new NavigationPage(new ParametrizacionPage());
                    break;
                case "AprobacionIngresosPage":
                    MainViewModel.GetInstance().AprobacionIngresos = new AprobacionIngresosViewModel();
                    App.Navigator.PushAsync(new AprobacionIngresosPage());
                    break;
                case "EgresosPage":
                    MainViewModel.GetInstance().Egresos = new EgresosViewModel();
                    App.Navigator.PushAsync(new EgresosPage());
                    break;
                case "SincronizacionPage":
                    MainViewModel.GetInstance().Sincronizacion = new SincronizacionViewModel();
                    App.Navigator.PushAsync(new SincronizacionPage());
                    break;
            }
        }
        #endregion
    }
}
