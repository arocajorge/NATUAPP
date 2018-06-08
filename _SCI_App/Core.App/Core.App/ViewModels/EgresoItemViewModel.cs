using Core.App.Models;
using Core.App.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace Core.App.ViewModels
{
    public class EgresoItemViewModel : EgresoModel
    {
        #region Comandos
        public ICommand SelectEgresoCommand
        {
            get { return new RelayCommand(SelectEgreso); }
        }

        private void SelectEgreso()
        {
            App.Master.IsPresented = false;
            MainViewModel.GetInstance().Egreso = new EgresoViewModel(this);
            App.Navigator.Navigation.PushAsync(new EgresoPage());
        }
        #endregion
    }
}
