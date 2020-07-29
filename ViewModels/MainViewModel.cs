using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace MVIOperationsSystem.ViewModels
{

    public class MainViewModel : ViewModelBase
    {

        #region Commands
        public RelayCommand TestCommand { get; private set; }
       
        #endregion

        private string _visibility="Hidden";

        #region Properties
        public string Visibility
        {
            get { return  _visibility; }

            set
            {  
                _visibility = value;
                this.RaisePropertyChanged(Visibility);
            }
        }
        #endregion

        public MainViewModel()
        {
            this.TestCommand = new RelayCommand(this.ExecuteTestCommand, CanExecuteTestCommand);

        }

        #region Execute/CanExecute

        private bool CanExecuteTestCommand()
        {
            return true; ;
        }

        private void ExecuteTestCommand()
        {
            //this.SelectedTabItem = this.
        }

        #endregion

    }
}