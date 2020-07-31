using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVIOperations.Models;
using MVIOperationsSystem.Messages;

namespace MVIOperationsSystem.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{

		#region Commands

                # region LoginCommand
                public RelayCommand LoginCommand { get; private set; }

		        private bool CanExecuteLoginCommand()
                {
                    return true;
                }

                private void ExecuteLoginCommand()
                {
                    var loginRequest = new LoginRequest { Username = this.Username, Password = this.Password, Token = "" };
                }
            #endregion

            #region LoginCancelCommand

                public RelayCommand LoginCancelCommand { get; private set; }

                private bool CanExecuteLoginCancelCommand()
                {
                    return true;
                }

                private void ExecuteLoginCancelCommand()
                {
                }
		#endregion
		#endregion

		#region Properties

		private string _password;
		public string Password		
        {
			get 
            { 
                return _password; 
            }
			set {
                _password = value; 
            }
		}

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        #endregion



        public LoginViewModel()
        {
            Messenger.Default.Register<PasswordMessage>(this, HandlePasswordMessage);
            this.LoginCommand = new RelayCommand(this.ExecuteLoginCommand, this.CanExecuteLoginCommand);
        }

        private void HandlePasswordMessage(PasswordMessage msg)
        {
            this.Password = msg.Password;
        }
        //this.LoginCommand = new RelayCommand<LoginRequest>(this.CanExecuteLoginCommand);
        //this.LoginCancelCommand = new RelayCommand(this.ExecuteLoginCancelCommand, CanExecuteLoginCancelCommand);

    }
}
