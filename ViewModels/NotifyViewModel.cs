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
	public class NotifyViewModel : ViewModelBase
	{
		private readonly ILoginDataService _dataService;
		private readonly LocalStorageService _ls = new LocalStorageService();

		#region Commands
		public RelayCommand YesCommand { get; private set; }
		public RelayCommand NoCommand { get; private set; }

		#endregion

		#region Properties

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

		public const string IsVisibleTextProperty = "IsVisible";
		private bool _isVisible;

		public bool IsVisible
		{
			get
			{
				return _isVisible;
			}
			set
			{
				_isVisible = value;
				this.RaisePropertyChanged(IsVisibleTextProperty);
			}
		}

		#endregion



		public NotifyViewModel(ILoginDataService dataService)
		{
			_dataService = dataService;
			Messenger.Default.Register<PasswordMessage>(this, HandlePasswordMessage);
			this.YesCommand = new RelayCommand(this.ExecuteYesCommand, this.CanExecuteYesCommand);
			this.NoCommand = new RelayCommand(this.ExecuteNoCommand, this.CanExecuteNoCommand);
		}

		private void HandlePasswordMessage(PasswordMessage msg)
		{
			
		}


		#region Can/Execute
		#region YesCommand

		private bool CanExecuteYesCommand()
		{
			var retVal = true;
			
			return retVal;
		}

		private async void ExecuteYesCommand()
		{
			this.IsVisible = true;
			
		}
		#endregion

		#region NoCommand
		private bool CanExecuteNoCommand()
		{
			return true;
		}

		private void ExecuteNoCommand()
		{
			Messenger.Default.Send<NavigationMessage>(new NavigationMessage { Action = "CancelLogin" });
		}
		#endregion

		#endregion
	}
}
