using Core.App.Helpers;
using Core.App.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Core.App.ViewModels
{
    public class EgresosViewModel : BaseViewModel
    {
        #region variables
        private DataAccess data;
        ObservableCollection<EgresoItemViewModel> _lst_egresos;
        private bool _IsRefreshing;
        #endregion

        #region Propiedades
        public ObservableCollection<EgresoItemViewModel> lst_egresos
        {
            get { return this._lst_egresos; }
            set
            {
                SetValue(ref this._lst_egresos, value);
            }
        }
        public bool IsRefreshing
        {
            get { return this._IsRefreshing; }
            set
            {
                SetValue(ref this._IsRefreshing, value);
            }
        }
        #endregion

        #region Singleton
        private static EgresosViewModel instance;
        public static EgresosViewModel GetInstance()
        {
            if (instance == null)
            {
                return new EgresosViewModel();
            }
            else
                return instance;
        }
        #endregion
        
        #region Constructor
        public EgresosViewModel()
        {
            data = new DataAccess();
            cargar_egresos();
        }
        #endregion

        #region Metodos
        private IEnumerable<EgresoItemViewModel> ToEgresoItemViewModel(List<EgresoModel> lst)
        {
            lst.ForEach(q => q.Cantidad = Math.Abs(q.Cantidad));
            return lst.Select(l => new EgresoItemViewModel
            {
                PKSQLite = l.PKSQLite,
                IdEmpresa = l.IdEmpresa,
                IdSucursal = l.IdSucursal,
                IdBodega = l.IdBodega,
                IdProducto = l.IdProducto,
                NomProducto = l.NomProducto,
                IdUnidadMedida = l.IdUnidadMedida,
                Cantidad = l.Cantidad,
                NomUnidadMedida = l.NomUnidadMedida,
                IdCentroCosto = l.IdCentroCosto,
                IdSubCentroCosto = l.IdSubCentroCosto,
                Fecha = l.Fecha
            });
        }
    
        public void cargar_egresos()
        {
            IsRefreshing = true;
            lst_egresos = new ObservableCollection<EgresoItemViewModel>(ToEgresoItemViewModel(data.GetListEgresos(Settings.IdEmpresa, Settings.IdSucursal, Settings.IdBodega, Settings.IdCentroCosto)));           
            IsRefreshing = false;
        }
        #endregion
    }
}
