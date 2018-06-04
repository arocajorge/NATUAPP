using Core.App.Helpers;
using Core.App.Services;
using Core.App.Views;
using Core.App.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Core.App.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Core.App
{
    public partial class App : Application
	{
        #region Propiedades
        public static NavigationPage Navigator { get; internal set; }
        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(Settings.IdUsuario))
            {                
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainViewModel.GetInstance().Configuracion = new ConfiguracionViewModel();
                MainPage = new MasterPage();
            }
        }
        #endregion

        #region Metodos
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion

    }
}
