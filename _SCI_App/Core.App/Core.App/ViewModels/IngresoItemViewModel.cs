namespace Core.App.ViewModels
{
    using Core.App.Models;
    using Core.App.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;

    public class IngresoItemViewModel : IngresoOrdenCompraModel
    {
        #region Comandos
        public ICommand SelectIngresoCommand
        {
            get { return new RelayCommand(SelectIngreso); }
        }

        private void SelectIngreso()
        {
            App.Master.IsPresented = false;
            MainViewModel.GetInstance().Ingreso = new IngresoViewModel(this);
            App.Navigator.Navigation.PushAsync(new IngresoPage());
        }
        #endregion
    }
}
