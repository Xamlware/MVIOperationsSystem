using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.ViewModels
{
    class MainMenuViewModel : ViewModelBase
    {

        #region Commands
        public RelayCommand LoginCommand { get; private set; }

        #endregion

        private string _visibility = "Visible";

        #region Properties
        public string Visibility
        {
            get { return _visibility; }

            set
            {
                _visibility = value;
                this.RaisePropertyChanged(Visibility);
            }
        }
        #endregion

        public MainMenuViewModel()
        {
            this.LoginCommand = new RelayCommand(this.ExecuteLoginCommand, CanExecuteLoginCommand);

        }

        #region Execute/CanExecute

        private bool CanExecuteLoginCommand()
        {
            return true; 
        }

        private void ExecuteLoginCommand()
        {
            //this.SelectedTabItem = this.
        }

        #endregion

    }
}