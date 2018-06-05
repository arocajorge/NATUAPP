namespace Core.App.ViewModels
{
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
            switch (this.PageName)
            {
                case "LoginPage":
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
                case "ParametrizacionPage":
                    Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ParametrizacionPage()));
                    break;
                case "AprobacionIngresosPage":
                    Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new AprobacionIngresosPage()));
                    break;
                case "EgresosPage":
                    Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new EgresosPage()));
                    break;
                case "SincronizacionPage":
                    Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new SincronizacionPage()));
                    break;
            }
        }
        #endregion
    }
}
