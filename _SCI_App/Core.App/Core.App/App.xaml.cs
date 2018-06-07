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
                MainViewModel.GetInstance().Login = new LoginViewModel();
                MainPage = new NavigationPage(new LoginPage());
            }else
            {
                if (Settings.IdEmpresa == 0)
                {
                    MainViewModel.GetInstance().Parametrizacion = new ParametrizacionViewModel();
                    MainPage = new NavigationPage(new ParametrizacionPage());
                }
                else
                {
                    MainViewModel.GetInstance().Stock = new StockViewModel();
                    MainPage = new MasterPage();
                }
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
