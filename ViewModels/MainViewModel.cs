using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Controls;
using MVIOperationsSystem.Messages;
using System;

namespace MVIOperationsSystem.ViewModels
{

    public class MainViewModel : ViewModelBase
    {

        #region Commands
        public RelayCommand LoginCommand { get; private set; }

        #endregion

        public const string LabelTextProperty = "LabelText";
        private string _labelText;

        #region Properties
        public string LabelText
        {
            get { return _labelText; }

            set
            {
                _labelText = value;
                this.RaisePropertyChanged(LabelTextProperty);
            }
        }

        #endregion

        public MainViewModel()
        {
            this.LabelText = "Sign In";
            this.LoginCommand = new RelayCommand(this.ExecuteLoginCommand, CanExecuteLoginCommand);
        }

		#region Execute/CanExecute

		private bool CanExecuteLoginCommand()
        {
            return true;
        }

        private void ExecuteLoginCommand()
        {
            if (this.LabelText == "Sign In")
            {
                this.LabelText = "Sign Out";
                Messenger.Default.Send(new SignInMessage { Action = "Sign In" });
            }
            else
            {
                this.LabelText = "Sign In";
                Messenger.Default.Send(new SignInMessage { Action = "Sign Out" });
            };


            #endregion

        }
    }
}