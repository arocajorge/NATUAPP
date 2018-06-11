using Core.App.Helpers;
using Core.App.Models;
using System;

namespace Core.App.ViewModels
{
    public class IngresoViewModel : BaseViewModel
    {
        #region Variables
        private IngresoOrdenCompraModel _Ingreso;
        private bool _IsEnabled;
        private bool _IsVisible;
        private DataAccess data;
        #endregion

        #region Propiedades
        public IngresoOrdenCompraModel Ingreso
        {
            get { return this._Ingreso; }
            set { SetValue(ref this._Ingreso, value); }
        }
        public bool IsEnabled
        {
            get { return this._IsEnabled; }
            set { SetValue(ref this._IsEnabled, value); }
        }
        public bool IsVisible
        {
            get { return this._IsVisible; }
            set { SetValue(ref this._IsVisible, value); }
        }
        #endregion

        #region Constructores
        public IngresoViewModel(IngresoOrdenCompraModel ingresoItemViewModel)
        {
            data = new DataAccess();
            Ingreso = ingresoItemViewModel;
            if (Ingreso.FechaApro == null)
            {
                Ingreso.FechaApro = DateTime.Now.Date;
                Ingreso.CantidadApro = 0;
            }
            IsEnabled = true;
        }
        #endregion
    }
}
