namespace Core.App.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;
    public static class Settings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        const string urlConexion = "urlConexion";
        const string idUsuario = "idUsuario";
        const string idEmpresa = "idEmpresa";
        const string idSucursal = "idSucursal";
        const string idCentroCosto = "idCentroCosto";
        static readonly string stringDefault = string.Empty;
        static readonly int intDefault = 0;
        public static string UrlConexion
        {
            get
            {
                return AppSettings.GetValueOrDefault(urlConexion, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(urlConexion, value);
            }
        }

        public static string IdUsuario
        {
            get
            {
                return AppSettings.GetValueOrDefault(idUsuario, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(idUsuario, value);
            }
        }

        public static int IdEmpresa
        {
            get
            {
                return AppSettings.GetValueOrDefault(idEmpresa, intDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(idEmpresa, value);
            }
        }

        public static int IdSucursal
        {
            get
            {
                return AppSettings.GetValueOrDefault(idSucursal, intDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(idSucursal, value);
            }
        }

        public static string IdCentroCosto
        {
            get
            {
                return AppSettings.GetValueOrDefault(idCentroCosto, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(idCentroCosto, value);
            }
        }
    }
}
