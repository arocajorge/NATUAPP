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
            if (this.PageName == "LoginPage")
            {
                Application.Current.MainPage = new LoginPage();
            }
        }
        #endregion
    }
}
