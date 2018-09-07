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
    public class StockViewModel : BaseViewModel
    {
        #region Variables
        private ObservableCollection<StockItemViewModel> _lst_stock;
        private string _filter;
        private DataAccess data;
        private bool _IsRefreshing;
        #endregion

        #region Propiedades
        public bool IsRefreshing
        {
            get { return this._IsRefreshing; }
            set
            {
                SetValue(ref this._IsRefreshing, value);
            }
        }
        public ObservableCollection<StockItemViewModel> lst_stock
        {
            get { return this._lst_stock; }
            set
            {
                SetValue(ref this._lst_stock, value);
            }
        }
        public string filter
        {
            get { return this._filter; }
            set
            {
                SetValue(ref this._filter, value);
                Buscar();
            }
        }
        #endregion

        #region Constructor
        public StockViewModel()
        {
            data = new DataAccess();
            cargar_stock();
        }
        #endregion

        #region Métodos
        public void cargar_stock()
        {
            IsRefreshing = true;
            MainViewModel.GetInstance().lst_stock = data.GetListStock(Settings.IdEmpresa, Settings.IdSucursal, Settings.IdBodega);
            lst_stock = new ObservableCollection<StockItemViewModel>(ToStockItemModel());
            IsRefreshing = false;
        }

        private IEnumerable<StockItemViewModel> ToStockItemModel()
        {
            return MainViewModel.GetInstance().lst_stock.Select(l => new StockItemViewModel
            {
                IdEmpresa = l.IdEmpresa,
                IdSucursal = l.IdSucursal,
                IdBodega = l.IdBodega,
                IdProducto = l.IdProducto,
                CodProducto = l.CodProducto,
                NomProducto = l.NomProducto,
                IdUnidadMedidaConsumo = l.IdUnidadMedidaConsumo,
                Stock = l.Stock,
                NomUnidadMedida = l.NomUnidadMedida,
                Ingresos = l.Ingresos,
                Saldo = l.Saldo,
                Egresos = l.Egresos
            });
        }

        #endregion

        #region Comandos
        public ICommand BuscarCommand
        {
            get { return new RelayCommand(Buscar); }
        }

        private void Buscar()
        {
            if (string.IsNullOrEmpty(filter))
                this.lst_stock = new ObservableCollection<StockItemViewModel>(ToStockItemModel());
            else
                this.lst_stock = new ObservableCollection<StockItemViewModel>(
                    ToStockItemModel().Where(q => q.NomProducto.ToLower().Contains(filter.ToLower())).OrderBy(q=>q.NomProducto));
        }
        #endregion
    }
}
