using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MVIOperationsSystem.ViewModels
{
	class AuthMenuViewModel : ViewModelBase
    {

        #region Commands
        public RelayCommand LoginCommand { get; private set; }

        #endregion

        private string _visibility = "Hidden";

        private string _header = "Sign In";

        public string Header
        {
            get { 
                return _header; 
            }
            set {
                _header = value;
                this.RaisePropertyChanged(Header);
            }
        }

        #region Properties
        public string Visibility
        {
            get { 
                return _visibility; 
            }
            set
            {
                _visibility = value;
                this.RaisePropertyChanged(Visibility);
            }
        }
        #endregion

        public AuthMenuViewModel()
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