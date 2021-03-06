﻿namespace Core.App.Helpers
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
        const string rutaCarpeta = "rutaCarpeta";
        const string idUsuario = "idUsuario";
        const string idEmpresa = "idEmpresa";
        const string nomEmpresa = "nomEmpresa";
        const string idSucursal = "idSucursal";
        const string nomSucursal = "nomSucursal";
        const string idBodega = "idBodega";
        const string nomBodega = "nomBodega";
        const string idCentroCosto = "idCentroCosto";
        const string nomCentroCosto = "nomCentroCosto";
        const string selectedindexSCC = "selectedindexSCC";
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
        public static string RutaCarpeta
        {
            get
            {
                return AppSettings.GetValueOrDefault(rutaCarpeta, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(rutaCarpeta, value);
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
        public static int SelectedIndexSCC
        {
            get
            {
                return AppSettings.GetValueOrDefault(selectedindexSCC, intDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(selectedindexSCC, value);
            }
        }

        public static string NomEmpresa
        {
            get
            {
                return AppSettings.GetValueOrDefault(nomEmpresa, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nomEmpresa, value);
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

        public static string NomSucursal
        {
            get
            {
                return AppSettings.GetValueOrDefault(nomSucursal, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nomSucursal, value);
            }
        }

        public static int IdBodega
        {
            get
            {
                return AppSettings.GetValueOrDefault(idBodega, intDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(idBodega, value);
            }
        }

        public static string NomBodega
        {
            get
            {
                return AppSettings.GetValueOrDefault(nomBodega, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nomBodega, value);
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

        public static string NomCentroCosto
        {
            get
            {
                return AppSettings.GetValueOrDefault(nomCentroCosto, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nomCentroCosto, value);
            }
        }
    }
}
