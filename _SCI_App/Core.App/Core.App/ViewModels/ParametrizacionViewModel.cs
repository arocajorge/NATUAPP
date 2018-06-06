using Core.App.Helpers;
using Core.App.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Core.App.ViewModels
{
    public class ParametrizacionViewModel : BaseViewModel
    {
        #region Variables
        private DataAccess data;
        private bool _IsEnabled;

        private ObservableCollection<EmpresaModel> _ListaEmpresa;
        private EmpresaModel _SelectedEmpresa;
        private int _EmpresaSelectedIndex;

        private ObservableCollection<SucursalModel> _ListaSucursal;
        private SucursalModel _SelectedSucursal;
        private int _SucursalSelectedIndex;

        private ObservableCollection<BodegaModel> _ListaBodega;
        private BodegaModel _SelectedBodega;
        private int _BodegaSelectedIndex;

        private ObservableCollection<CentroCostoModel> _ListaCentroCosto;
        private CentroCostoModel _SelectedCentroCosto;
        private int _CentroCostoSelectedIndex;
        #endregion

        #region Propiedades
        public ObservableCollection<EmpresaModel> ListaEmpresa
        {
            get { return this._ListaEmpresa; }
            set { SetValue(ref this._ListaEmpresa, value); }
        }
        public EmpresaModel SelectedEmpresa
        {
            get { return this._SelectedEmpresa; }
            set { SetValue(ref this._SelectedEmpresa, value); }
        }
        public int EmpresaSelectedIndex
        {
            get { return this._EmpresaSelectedIndex; }
            set
            {
                SetValue(ref this._EmpresaSelectedIndex, value);
                SelectedEmpresa = ListaEmpresa[EmpresaSelectedIndex];
                cargar_sucursal();
                cargar_cc();
            }
        }

        public ObservableCollection<BodegaModel> ListaBodega
        {
            get { return this._ListaBodega; }
            set { SetValue(ref this._ListaBodega, value); }
        }
        public BodegaModel SelectedBodega
        {
            get { return this._SelectedBodega; }
            set { SetValue(ref this._SelectedBodega, value); }
        }
        public int BodegaSelectedIndex
        {
            get { return this._BodegaSelectedIndex; }
            set
            {
                SetValue(ref this._BodegaSelectedIndex, value);
                SelectedBodega = ListaBodega[_BodegaSelectedIndex];
            }
        }

        public ObservableCollection<CentroCostoModel> ListaCentroCosto
        {
            get { return this._ListaCentroCosto; }
            set { SetValue(ref this._ListaCentroCosto, value); }
        }
        public CentroCostoModel SelectedCentroCosto
        {
            get { return this._SelectedCentroCosto; }
            set { SetValue(ref this._SelectedCentroCosto, value); }
        }
        public int CentroCostoSelectedIndex
        {
            get { return this._CentroCostoSelectedIndex; }
            set
            {
                SetValue(ref this._CentroCostoSelectedIndex, value);
                SelectedCentroCosto = ListaCentroCosto[CentroCostoSelectedIndex];
            }
        }

        public ObservableCollection<SucursalModel> ListaSucursal
        {
            get { return this._ListaSucursal; }
            set { SetValue(ref this._ListaSucursal, value); }
        }        
        public int SucursalSelectedIndex
        {
            get { return this._SucursalSelectedIndex; }
            set
            {
                SetValue(ref this._SucursalSelectedIndex, value);
                SelectedSucursal = ListaSucursal[_SucursalSelectedIndex];                
                cargar_bodega();
            }
        }        
        public SucursalModel SelectedSucursal
        {
            get { return this._SelectedSucursal; }
            set { SetValue(ref this._SelectedSucursal, value); }
        }
        
        public bool IsEnabled {
            get { return this._IsEnabled; }
            set { SetValue(ref this._IsEnabled, value); }
        }
        #endregion

        #region Constructor
        public ParametrizacionViewModel()
        {
            data = new DataAccess();
            IsEnabled = true;
            SelectedEmpresa = new EmpresaModel();
            SelectedSucursal = new SucursalModel();
            SelectedBodega = new BodegaModel();
            SelectedCentroCosto = new CentroCostoModel();
            cargar_combos();
        }
        #endregion

        #region Comandos
        public ICommand ParametrizarCommand
        {
            get
            {
                return new RelayCommand(Parametrizar);
            }
        }          

        private async void Parametrizar()
        {
            this.IsEnabled = false;
            
            if (this.SelectedEmpresa.IdEmpresa == 0)
            {
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Seleccione la empresa",
                    "Aceptar");
                return;
            }

            if (this.SelectedSucursal.IdSucursal == 0)
            {
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Seleccione la sucursal",
                    "Aceptar");
                return;
            }

            if (this.SelectedBodega.IdBodega == 0)
            {
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Seleccione la bodega",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.SelectedCentroCosto.IdCentroCosto))
            {
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Seleccione el centro de costo",
                    "Aceptar");
                return;
            }
            this.IsEnabled = true;
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            ListaEmpresa = new ObservableCollection<EmpresaModel>(data.GetListEmpresa());
        }
        private void cargar_sucursal()
        {
            ListaSucursal = new ObservableCollection<SucursalModel>(data.GetListSucursal(SelectedEmpresa.IdEmpresa));
        }        
        private void cargar_bodega()
        {
            ListaBodega = new ObservableCollection<BodegaModel>(data.GetListBodega(SelectedSucursal.IdEmpresa, SelectedSucursal.IdSucursal));
        }
        private void cargar_cc()
        {
            ListaCentroCosto = new ObservableCollection<CentroCostoModel>(data.GetListCentroCosto(SelectedEmpresa.IdEmpresa));
        }
        #endregion
    }
}
