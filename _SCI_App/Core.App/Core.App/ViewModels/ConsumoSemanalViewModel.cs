using Core.App.Helpers;
using Core.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Core.App.ViewModels
{
    public class ConsumoSemanalViewModel : BaseViewModel
    {
        #region Variables
        private ObservableCollection<ConsumoSemanalModel> _lst_consumos;
        private DataAccess data;
        #endregion

        #region Propiedades
        public ObservableCollection<ConsumoSemanalModel> lst_consumos
        {
            get { return this._lst_consumos; }
            set
            {
                SetValue(ref this._lst_consumos, value);
            }
        }
        #endregion
        
        #region Constructor
        public ConsumoSemanalViewModel(StockItemViewModel model)
        {
            data = new DataAccess();
            CargarList(model.IdProducto);
        }
        #endregion

        #region Metodos
        
        public void CargarList(decimal IdProducto)
        {
            MainViewModel.GetInstance().lst_consumos = new ObservableCollection<ConsumoSemanalModel>(data.GetListConsumoSemanal(Settings.IdEmpresa, Settings.IdSucursal, Settings.IdBodega, Settings.IdCentroCosto, IdProducto));
            lst_consumos = new ObservableCollection<ConsumoSemanalModel>(MainViewModel.GetInstance().lst_consumos);
        }
        #endregion

    }
}
