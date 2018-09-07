using Core.App.Models;
using Core.App.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace Core.App.ViewModels
{
    public class StockItemViewModel : StockModel
    {
        #region Comandos
        public ICommand SelectStockCommand
        {
            get { return new RelayCommand(SelectStock); }
        }

        private void SelectStock()
        {
            App.Master.IsPresented = false;
            MainViewModel.GetInstance().ConsumoSemanal = new ConsumoSemanalViewModel(this);
            App.Navigator.Navigation.PushAsync(new ConsumoSemanalPage());
        }
        #endregion
    }
}
