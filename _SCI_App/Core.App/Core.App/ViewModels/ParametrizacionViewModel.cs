using Core.App.Helpers;
using Core.App.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Core.App.ViewModels
{
    public class ParametrizacionViewModel : BaseViewModel
    {
        #region Variables
        private DataAccess data;
        private List<EmpresaModel> _ListaEmpresa;
        private EmpresaModel _SelectedEmpresa;
        private List<SucursalModel> _ListaSucursal;
        private SucursalModel _SelectedSucursal;
        private bool _IsEnabled;
        #endregion

        #region Propiedades
        public List<EmpresaModel> ListaEmpresa
        {
            get { return this._ListaEmpresa; }
            set { SetValue(ref this._ListaEmpresa, value); }
        }
        public List<SucursalModel> ListaSucursal
        {
            get { return this._ListaSucursal; }
            set { SetValue(ref this._ListaSucursal, value); }
        }
        public EmpresaModel SelectedEmpresa
        {
            get { return this._SelectedEmpresa; }
            set { SetValue(ref this._SelectedEmpresa, value); }
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
        
        public ICommand EmpresaChangedCommand
        {
            get
            {
                return new RelayCommand(EmpresaChanged);
            }
        }

        private void EmpresaChanged()
        {
            ListaSucursal = data.GetListSucursal(this.SelectedEmpresa.IdEmpresa);
            
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
            this.IsEnabled = true;
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            ListaEmpresa = data.GetListEmpresa();
            if (ListaEmpresa.Count == 1)
            {
                this._SelectedEmpresa = ListaEmpresa[0];
                Settings.IdEmpresa = this.SelectedEmpresa.IdEmpresa;
                EmpresaChanged();
            }
        }
        #endregion
    }
}
