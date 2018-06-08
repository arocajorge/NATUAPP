using Core.App.Models;
using Core.App.Views;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Core.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }
        public ConfiguracionViewModel Configuracion { get; set; }
        public ParametrizacionViewModel Parametrizacion { get; set; }        
        public StockViewModel Stock { get; set; }
        public EgresosViewModel Egresos { get; set; }
        public EgresoViewModel Egreso { get; set; }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        public List<StockModel> lst_stock { get; set; }        
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.loadMenu();
        }        
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            else
                return instance;
        }
        #endregion

        #region Metodos
        private void loadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_settings",
                PageName = "ParametrizacionPage",
                Title = "Parametrización"
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_assignment_turned_in",
                PageName = "AprobacionIngresosPage",
                Title = "Ingresos"
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_assignment_returned",
                PageName = "EgresosPage",
                Title = "Egresos"
            });                        
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_cloud_upload",
                PageName = "SincronizacionPage",
                Title = "Sincronizar"
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = "Cerrar sesión"
            });
        }

        #endregion

        #region Comandos
        public ICommand NuevoEgresoCommand
        {
            get { return new RelayCommand(NuevoEgreso); }
        }

        private void NuevoEgreso()
        {
            App.Master.IsPresented = false;
            MainViewModel.GetInstance().Egreso = new EgresoViewModel();
            App.Navigator.PushAsync(new EgresoPage());
        }

        public void cargar_combos()
        {
            
        }
        #endregion
    }
}
