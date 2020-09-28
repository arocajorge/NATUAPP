using Core.App.Helpers;
using Core.App.Services;
using Core.App.Views;
using Core.App.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Core.App.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Generic;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Core.App
{
    public partial class App : Application
	{
        #region Variables
        private DataAccess data;
        #endregion

        #region Propiedades
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        public static List<ProductoModel> Productos { get; set; }
        public static List<SubCentroCostoModel> SubCentros { get; set; }
        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();
            data = new DataAccess();
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
                    Productos = data.GetListProducto(Settings.IdEmpresa,Settings.IdSucursal, Settings.IdBodega);
                    SubCentros = data.GetListSubCentroCosto(Settings.IdEmpresa, Settings.IdCentroCosto);
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
