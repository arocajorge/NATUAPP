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
        private DataAccess data;
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
            usuario = string.IsNullOrEmpty(Settings.IdUsuario) ? "AdminAPP" : Settings.IdUsuario;
            contrasenia = string.IsNullOrEmpty(Settings.IdUsuario) ? "%Natu201805*" : string.Empty;
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
            bool validar_usuario_local = true;
            if (this.usuario == "AdminAPP" && contrasenia == "%Natu201805*")
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                this.usuario = string.Empty;
                contrasenia = string.Empty;
                Settings.IdUsuario = "";
                validar_usuario_local = false;
                MainViewModel.GetInstance().Configuracion = new ConfiguracionViewModel();                
                await Application.Current.MainPage.Navigation.PushAsync(new ConfiguracionPage());                
            }
            if (validar_usuario_local)
            {
                data = new DataAccess();
                var usuarioModel = data.GetUsuario(this.usuario, this.contrasenia);
                if (usuarioModel == null)
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        "Alerta",
                        "Credenciales incorrectas",
                        "Aceptar");
                    return;
                }
                else
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;
                    Settings.IdUsuario = this.usuario;

                    if (Settings.IdEmpresa == 0)
                    {
                        MainViewModel.GetInstance().Parametrizacion = new ParametrizacionViewModel();
                        Application.Current.MainPage = new NavigationPage(new ParametrizacionPage());
                    }
                    else
                    {
                        MainViewModel.GetInstance().Stock = new StockViewModel();
                        Application.Current.MainPage = new MasterPage();
                    }
                }
            }
            
        }
        #endregion
    }
}
