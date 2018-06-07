using Core.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Core.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }
        public ConfiguracionViewModel Configuracion { get; set; }
        public ParametrizacionViewModel Parametrizacion { get; set; }
        public StockViewModel Stock { get; set; }
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
    }
}
