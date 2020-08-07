using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVIOperations.Models;
using MVIOperationsSystem.Data;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Services;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.WindowsRuntime;
using Xamlware.Framework.Extensions;

namespace MVIOperationsSystem.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		private readonly ILoginDataService _dataService;
		private readonly LocalStorageService _ls = new LocalStorageService();

		#region Commands
		public RelayCommand LoginCommand { get; private set; }
		public RelayCommand CancelLoginCommand { get; private set; }

		#endregion

		#region Properties

		public const string ColorProperty = "Color";
		private string _color;

		public string Color
		{
			get
			{
				return _color;
			}
			set
			{
				_color = value;
				this.RaisePropertyChanged(ColorProperty);
			}
		}


		public const string MessageProperty = "Message";
		private string _message;

		public string Message
		{
			get { return _message; }
			set
			{
				_message = value;
				this.RaisePropertyChanged(MessageProperty);
			}
		}


		public const string PasswordProperty = "Password";
		private string _password;
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
				this.RaisePropertyChanged(PasswordProperty);
				this.Message = "";
				LoginCommand.RaiseCanExecuteChanged();
			}
		}


		public const string UsernameTextProperty = "Username";
		private string _username;

		public string Username
		{
			get { return _username; }

			set
			{
				_username = value;
				this.Message = "";
				this.RaisePropertyChanged(UsernameTextProperty);
			}
		}

		public const string IsBusyTextProperty = "IsBusy";
		private bool _isBusy;

		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				_isBusy = value;
				this.RaisePropertyChanged(IsBusyTextProperty);
			}
		}

		#endregion



		public LoginViewModel(ILoginDataService dataService)
		{
			_dataService = dataService;
			Messenger.Default.Register<PasswordMessage>(this, HandlePasswordMessage);
			this.LoginCommand = new RelayCommand(this.ExecuteLoginCommand, this.CanExecuteLoginCommand);
			this.CancelLoginCommand = new RelayCommand(this.ExecuteCancelLoginCommand, this.CanExecuteLoginCancelCommand);
			this.Username = "jbaird";
			this.Password = "((B((jb((1(";
		}

		private void HandlePasswordMessage(PasswordMessage msg)
		{
			this.Password = msg.Password;
		}


		#region Can/Execute
		#region LoginCommand

		private bool CanExecuteLoginCommand()
		{
			var retVal = true;
			if (this.Username.IsNullOrEmpty() || this.Password.IsNullOrEmpty())
			{
				retVal = false;
			}

			return retVal;
		}

		private async void ExecuteLoginCommand()
		{
			this.IsBusy = true;
			var loginRequest = new LoginRequest { Username = this.Username, Password = this.Password };
			var lr = await _dataService.Login(loginRequest);

			if (lr != null)
			{
				if (Message.IsNotNullOrEmpty())
				{
					this.Message = "";
				}

				if (lr.Status.IsNotNullOrEmpty())
				{
					this.IsBusy = false;
					this.Color = "Red";
					this.Message = "Username or Password error!";
				}
				else
				{
					//write data to local storage
					var result = _ls.WriteValue("token", lr.Token);
					if (result == "OK")
					{
						this.IsBusy = false;
						this.Color = "SaddleBrown";
						this.Message = "Login succeeded";
						this.IsBusy = false;

						if (lr.Roles.Contains("Admin"))
						{
							Messenger.Default.Send<NavigationMessage>(new NavigationMessage { Action = "AdminLogin" });
						}
					}
				}
			}
		}
		#endregion

		#region CancelLoginCommand
		private bool CanExecuteLoginCancelCommand()
		{
			return true;
		}

		private void ExecuteCancelLoginCommand()
		{
			Messenger.Default.Send<NavigationMessage>(new NavigationMessage { Action = "CancelLogin" });
		}
		#endregion

		#endregion
	}
}
