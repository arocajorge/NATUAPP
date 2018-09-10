using Core.App.Helpers;
using Core.App.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Core.App.ViewModels
{
    public class EgresoViewModel : BaseViewModel
    {
        #region Variables
        private EgresoModel _Egreso;
        private EgresoModel Egreso_original;
        private bool _IsEnabled;
        private bool _IsVisible;
        private bool _IsVisiblePeso;
        private bool _IsVisibleObservacion;
        private DataAccess data;

        private ObservableCollection<ProductoModel> _ListaProducto;
        private ProductoModel _SelectedProducto;
        private int _ProductoSelectedIndex;

        private ObservableCollection<SubCentroCostoModel> _ListaSubCentro;
        private SubCentroCostoModel _SelectedSubcentro;
        private int _SubCentroSelectedIndex;
        
        #endregion

        #region Propiedades
        public EgresoModel Egreso
        {
            get { return this._Egreso; }
            set { SetValue(ref this._Egreso, value); }
        }
        public bool IsEnabled {
            get { return this._IsEnabled; }
            set { SetValue(ref this._IsEnabled, value); }
        }
        public bool IsVisible
        {
            get { return this._IsVisible; }
            set { SetValue(ref this._IsVisible, value); }
        }
        public bool IsVisiblePeso
        {
            get { return this._IsVisiblePeso; }
            set { SetValue(ref this._IsVisiblePeso, value); }
        }
        public bool IsVisibleObservacion
        {
            get { return this._IsVisibleObservacion; }
            set { SetValue(ref this._IsVisibleObservacion, value); }
        }
        public ObservableCollection<ProductoModel> ListaProducto
        {
            get { return this._ListaProducto; }
            set { SetValue(ref this._ListaProducto, value); }
        }
        public ProductoModel SelectedProducto
        {
            get { return this._SelectedProducto; }
            set {
                SetValue(ref this._SelectedProducto, value);

                if (_SelectedProducto.MuestraObservacionAPP)
                    this.IsVisibleObservacion = true;
                else
                    this.IsVisibleObservacion = false;

                if (_SelectedProducto.MuestraPesoAPP)
                    this.IsVisiblePeso = true;
                else
                    this.IsVisiblePeso = false;
            }
        }
        public int ProductoSelectedIndex
        {
            get { return this._ProductoSelectedIndex; }
            set
            {
                SetValue(ref this._ProductoSelectedIndex, value);
                SelectedProducto = _ProductoSelectedIndex < 0 ? new ProductoModel() : ListaProducto[_ProductoSelectedIndex];
            }
        }

        public ObservableCollection<SubCentroCostoModel> ListaSubCentro
        {
            get { return this._ListaSubCentro; }
            set { SetValue(ref this._ListaSubCentro, value); }
        }
        public SubCentroCostoModel SelectedSubcentro
        {
            get { return this._SelectedSubcentro; }
            set { SetValue(ref this._SelectedSubcentro, value); }
        }
        public int SubCentroSelectedIndex
        {
            get { return this._SubCentroSelectedIndex; }
            set
            {
                SetValue(ref this._SubCentroSelectedIndex, value);
                SelectedSubcentro = _SubCentroSelectedIndex < 0 ? new SubCentroCostoModel() : ListaSubCentro[_SubCentroSelectedIndex];
            }
        }
        #endregion

        #region Constructores
        public EgresoViewModel(EgresoModel egresoItemViewModel)
        {
            this.Egreso_original = egresoItemViewModel;
            this.Egreso = egresoItemViewModel;            
            this.IsVisible = true;
            this.IsEnabled = true;
            this.IsVisiblePeso = true;
            this.IsVisibleObservacion = true;
            data = new DataAccess();
            cargar_combos();
        }
        public EgresoViewModel()
        {
            
            this.Egreso = new EgresoModel
            {
                IdEmpresa = Settings.IdEmpresa,
                IdSucursal = Settings.IdSucursal,
                IdBodega = Settings.IdBodega,
                IdCentroCosto = Settings.IdCentroCosto,
                Fecha = DateTime.Now.Date
            };            
            this.IsVisible = false;
            this.IsEnabled = true;
            this.IsVisiblePeso = true;
            this.IsVisibleObservacion = true;
            data = new DataAccess();
            cargar_combos();
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            ListaProducto = new ObservableCollection<ProductoModel>(App.Productos);
            if (Egreso.IdProducto != 0)
            {
                SelectedProducto = ListaProducto.Where(q => q.IdProducto == Egreso.IdProducto).FirstOrDefault();
                ProductoSelectedIndex = ListaProducto.IndexOf(SelectedProducto);
            }
            ListaSubCentro = new ObservableCollection<SubCentroCostoModel>(App.SubCentros);
            if (!string.IsNullOrEmpty(Egreso.IdSubCentroCosto))
            {
                SelectedSubcentro = ListaSubCentro.Where(q => q.IdSubCentroCosto == Egreso.IdSubCentroCosto).FirstOrDefault();
                SubCentroSelectedIndex = ListaSubCentro.IndexOf(SelectedSubcentro);
            }
        }
        #endregion

        #region Comandos
        public ICommand GuardarCommand
        { get { return new RelayCommand(Guardar); }  }

        public ICommand EliminarCommand
        { get { return new RelayCommand(Eliminar); } }

        private async void Eliminar()
        {
            IsEnabled = false;

            var answer = await Application.Current.MainPage.DisplayAlert("Eliminar", "¿Está seguro que desea eliminar el registro?", "Si", "No");
            if (answer)
            {
                data.DeleteEgreso(Egreso_original.PKSQLite);
                IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                "Exito",
                "Registro eliminado exitósamente",
                "Aceptar");
                MainViewModel.GetInstance().Stock.cargar_stock();
                MainViewModel.GetInstance().Egresos.cargar_egresos();
                await App.Navigator.Navigation.PopAsync();
            }

            IsEnabled = true;
        }

        private async void Guardar()
        {
            IsEnabled = false;

            if (this.SelectedProducto.IdEmpresa == 0)
            {
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Seleccione el producto",
                    "Aceptar");
                return;
            }

            if (this.SelectedSubcentro.IdEmpresa == 0)
            {
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Seleccione el SubCentro",
                    "Aceptar");
                return;
            }

            if (this.Egreso.Cantidad == 0)
            {
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Ingrese la cantidad",
                    "Aceptar");
                return;
            }

            Egreso.IdProducto = SelectedProducto.IdProducto;
            Egreso.NomProducto = SelectedProducto.NomProducto;
            Egreso.IdSubCentroCosto = SelectedSubcentro.IdSubCentroCosto;
            Egreso.NomSubCentro = SelectedSubcentro.Nom_subcentro;
            Egreso.IdUnidadMedida = SelectedProducto.IdUnidadConsumo;            

            data.Guardar(Egreso);
            this.IsEnabled = true;
            await Application.Current.MainPage.DisplayAlert(
                "Exito",
                "Registro guardado exitósamente",
                "Aceptar");
            MainViewModel.GetInstance().Stock.cargar_stock();
            MainViewModel.GetInstance().Egresos.cargar_egresos();            
            await App.Navigator.Navigation.PopAsync();
        }
        #endregion
    }
}
