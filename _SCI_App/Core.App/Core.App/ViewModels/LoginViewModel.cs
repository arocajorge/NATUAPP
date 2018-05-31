namespace Core.App.ViewModels
{
    using Core.App.Helpers;
    using Core.App.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Variables
        private string _usuario;
        private string _contrasenia;
        private bool _IsEnabled;
        private bool _IsRunning;
        #endregion

        #region Propiedades
        public string usuario {
            get { return this._usuario; }
            set { SetValue(ref this._usuario, value); }
        }
        public string contrasenia {
            get { return this._contrasenia; }
            set { SetValue(ref this._contrasenia, value); }
        }
        public bool IsRunning {
            get { return this._IsRunning; }
            set { SetValue(ref this._IsRunning, value); }
        }
        public bool IsEnabled {
            get { return this._IsEnabled; }
            set { SetValue(ref this._IsEnabled, value); }
        }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            IsEnabled = true;
            usuario = "admin";
            contrasenia = "123";
        }
        #endregion

        #region Comandos
        public ICommand LoginCommand {
            get
            {
                return new RelayCommand(Login);
            }
        }
        private async void Login()
        {
            this.IsEnabled = false;
            this.IsRunning = true;
            if (string.IsNullOrEmpty(this.usuario))
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Debe ingresar un usuario",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.contrasenia))
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Debe ingresar una contraseña",
                    "Aceptar");
                return;
            }

            this.IsEnabled = true;
            this.IsRunning = false;
            Settings.IdUsuario = this.usuario;
            MainViewModel.GetInstance().Configuracion = new ConfiguracionViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        #endregion
    }
}
