﻿using Core.App.Helpers;
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
        private ObservableCollection<StockModel> _lst_stock;
        private string _filter;
        private DataAccess data;
        #endregion

        #region Propiedades
        public ObservableCollection<StockModel> lst_stock
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
        private void cargar_stock()
        {
            lst_stock = new ObservableCollection<StockModel>(data.GetListStock(Settings.IdEmpresa, Settings.IdSucursal, Settings.IdBodega));
            MainViewModel.GetInstance().lst_stock = new List<StockModel>(lst_stock);
        }

        private IEnumerable<StockModel> ToStockModel()
        {
            return MainViewModel.GetInstance().lst_stock.Select(l => new StockModel
            {
                IdEmpresa = l.IdEmpresa,
                IdSucursal = l.IdSucursal,
                IdBodega = l.IdBodega,
                IdProducto = l.IdProducto,
                CodProducto = l.CodProducto,
                NomProducto = l.NomProducto,
                IdUnidadMedidaConsumo = l.IdUnidadMedidaConsumo,
                Stock = l.Stock,
                NomUnidadMedida = l.NomUnidadMedida            
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
                this.lst_stock = new ObservableCollection<StockModel>(ToStockModel());
            else
                this.lst_stock = new ObservableCollection<StockModel>(
                    ToStockModel().Where(q => q.NomProducto.ToLower().Contains(filter.ToLower())).OrderBy(q=>q.NomProducto));
        }
        #endregion
    }
}
