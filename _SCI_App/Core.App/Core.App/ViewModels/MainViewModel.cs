using System;
using System.Collections.ObjectModel;

namespace Core.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }
        public ConfiguracionViewModel Configuracion { get; set; }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
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
                Icon = "ic_assignment_returned",
                PageName = "LoginPage",
                Title = "Egresos"
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_assignment_turned_in",
                PageName = "LoginPage",
                Title = "Ingresos"
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_settings",
                PageName = "LoginPage",
                Title = "Configuración"
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_cloud_upload",
                PageName = "LoginPage",
                Title = "Sincronizar datos"
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
