using Core.App.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Core.App.ViewModels
{
    public class ParametrizacionViewModel : BaseViewModel
    {
        #region Variables
        private int _IdEmpresa;
        private int _IdSucursal;
        private int _IdBodega;
        private string _IdCentroCosto;
        private DataAccess data;
        #endregion

        #region Propiedades
        public int IdEmpresa
        {
            get { return this._IdEmpresa; }
            set { SetValue(ref this._IdEmpresa, value); }
        }

        public int IdSucursal
        {
            get { return this._IdSucursal; }
            set { SetValue(ref this._IdSucursal, value); }
        }

        public int IdBodega
        {
            get { return this._IdBodega; }
            set { SetValue(ref this._IdBodega, value); }
        }

        public string IdCentroCosto
        {
            get { return this._IdCentroCosto; }
            set { SetValue(ref this._IdCentroCosto, value); }
        }
        #endregion

        #region Constructor
        public ParametrizacionViewModel()
        {
            if (Settings.IdEmpresa != 0)
            {
                IdEmpresa = Settings.IdEmpresa;
                IdSucursal = Settings.IdSucursal;
                IdBodega = Settings.IdBodega;
                IdCentroCosto = Settings.IdCentroCosto;
            }
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            var lst_empresa = data.GetListEmpresa();
            if (lst_empresa.Count == 1)
            {
                this.IdEmpresa = lst_empresa[0].IdEmpresa;
                Settings.IdEmpresa = this.IdEmpresa;
            }

            var lst_sucursal = data.GetListSucursal();
            
        }
        #endregion
    }
}
