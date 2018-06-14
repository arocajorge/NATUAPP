namespace Core.App.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Core.App.Helpers;
    using Core.App.Models;
    public class AprobacionIngresosViewModel : BaseViewModel
    {
        #region Variables
        private ObservableCollection<IngresoItemViewModel> _lst_ingresos;
        private DataAccess data;
        private bool _MostrarAprobados;
        #endregion

        #region Propiedades
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
        private IEnumerable<IngresoItemViewModel> ToIngresoItemViewModel(List<IngresoOrdenCompraModel> lst)
        {
            return lst.Select(l => new IngresoItemViewModel
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
                IdUnidadMedidaConsumo = l.IdUnidadMedidaConsumo
            });
        }
        public void CargarLista()
        {
            lst_ingresos = new ObservableCollection<IngresoItemViewModel>(ToIngresoItemViewModel(data.GetListIngresoOrdenCompra(Settings.IdEmpresa, Settings.IdSucursal, Settings.IdBodega, MostrarAprobados)));
        }
        #endregion
    }
}
