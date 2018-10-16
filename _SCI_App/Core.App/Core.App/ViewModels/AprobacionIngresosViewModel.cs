namespace Core.App.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Core.App.Helpers;
    using Core.App.Models;
    using GalaSoft.MvvmLight.Command;

    public class AprobacionIngresosViewModel : BaseViewModel
    {
        #region Variables
        private ObservableCollection<IngresoItemViewModel> _lst_ingresos;
        private DataAccess data;
        private bool _MostrarAprobados;
        private bool _IsRefreshing;
        private string _filter;
        #endregion

        #region Propiedades
        public string filter
        {
            get { return this._filter; }
            set
            {
                SetValue(ref this._filter, value);
                Buscar();
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

        public ObservableCollection<IngresoItemViewModel> lst_ingresos {
            get { return this._lst_ingresos; }
            set
            {
                SetValue(ref this._lst_ingresos, value);
            }
        }
        public bool MostrarAprobados
        {
            get { return this._MostrarAprobados; }
            set
            {
                SetValue(ref this._MostrarAprobados, value);
                CargarLista();
            }
        }
        #endregion

        #region Constructor
        public AprobacionIngresosViewModel()
        {
            data = new DataAccess();
            MostrarAprobados = false;
            CargarLista();
        }
        #endregion

        #region Singleton
        private static AprobacionIngresosViewModel instance;
        public static AprobacionIngresosViewModel GetInstance()
        {
            if (instance == null)
            {
                return new AprobacionIngresosViewModel();
            }
            else
                return instance;
        }
        #endregion

        #region Metodos
        private IEnumerable<IngresoItemViewModel> ToIngresoItemViewModel()
        {
            return MainViewModel.GetInstance().lst_ingresos.Select(l => new IngresoItemViewModel
            {
                PKSQLite = l.PKSQLite,
                IdEmpresa = l.IdEmpresa,
                IdSucursal = l.IdSucursal,
                CantidadApro = l.CantidadApro,
                CantidadIn = l.CantidadIn,
                CantidadOc = l.CantidadOc,
                CodProducto = l.CodProducto,
                IdOrdenCompra = l.IdOrdenCompra,
                IdProducto = l.IdProducto,
                IdProveedor = l.IdProveedor,
                IdUnidadMedida = l.IdUnidadMedida,
                NomProducto = l.NomProducto,
                NomProveedor = l.NomProveedor,
                NomUnidadMedida = l.NomUnidadMedida,
                OcFecha = l.OcFecha,
                OcObservacion = l.OcObservacion,
                Saldo = l.Saldo,
                Secuencia = l.Secuencia,
                CantidadApro_convertida = l.CantidadApro_convertida,
                FechaApro = l.FechaApro,
                IdUnidadMedidaConsumo = l.IdUnidadMedidaConsumo,  
                CantidadOcConsulta = l.CantidadOcConsulta,
                PKSQLitePadre = l.PKSQLitePadre,
                NomSucursal = l.NomSucursal
            });
        }
        public void CargarLista()
        {
            IsRefreshing = true;
            MainViewModel.GetInstance().lst_ingresos = data.GetListIngresoOrdenCompra(Settings.IdEmpresa, Settings.IdSucursal, Settings.IdBodega, MostrarAprobados);
            lst_ingresos = new ObservableCollection<IngresoItemViewModel>(ToIngresoItemViewModel());
            IsRefreshing = false;
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
                this.lst_ingresos = new ObservableCollection<IngresoItemViewModel>(ToIngresoItemViewModel());
            else
                this.lst_ingresos = new ObservableCollection<IngresoItemViewModel>(
                    ToIngresoItemViewModel().Where(q => q.NomProducto.ToLower().Contains(filter.ToLower()) || q.NomSucursal.ToLower().Contains(filter.ToLower()) || q.IdOrdenCompra.ToString().Contains(filter.ToLower()) || q.NomProveedor.ToLower().Contains(filter.ToLower())).OrderBy(q => q.NomProducto));
        }
        #endregion
    }
}
