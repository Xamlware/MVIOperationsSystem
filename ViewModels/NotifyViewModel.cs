using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Services;
using System.Windows;
using System.Windows.Media;

namespace MVIOperationsSystem.ViewModels
{
	public class NotifyViewModel : ViewModelBase
	{
		private readonly ILoginDataService _dataService;
		private readonly LocalStorageService _ls = new LocalStorageService();

		#region Commands
		public RelayCommand YesCommand { get; private set; }
		public RelayCommand NoCommand { get; private set; }
		public RelayCommand OneCommand { get; private set; }
		#endregion

		#region Properties
		public const string TextColorProperty = "TextColor";
		private Color _textColor;

		public Color TextColor
		{
			get { return _textColor; }
			set
			{
				_textColor = value;
				this.RaisePropertyChanged(TextColorProperty);
			}
		}


		public const string YesLabelProperty = "YesLabel";
		private string _yesLabel;

		public string YesLabel
		{
			get { return _yesLabel; }
			set 
			{
				_yesLabel = value;
				this.RaisePropertyChanged(YesLabelProperty);
			}
		}

		public const string NoLabelProperty = "NoLabel";
		private string _noLabel;

		public string NoLabel
		{
			get { return _noLabel; }
			set
			{
				_noLabel = value;
				this.RaisePropertyChanged(NoLabelProperty);
			}
		}


		public const string OneLabelProperty = "OneLabel";
		private string _oneLabel;

		public string OneLabel
		{
			get { return _oneLabel; }
			set
			{
				_oneLabel = value;
				this.RaisePropertyChanged(OneLabelProperty);
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

		public const string NotifyVisibilityProperty = "NotifyVisibility";
		private Visibility _notifyVisibility;

		public Visibility NotifyVisibility
		{
			get { return _notifyVisibility; }
			set
			{
				_notifyVisibility = value;
				this.RaisePropertyChanged(NotifyVisibilityProperty);
			}
		}

		public const string TwoButtonVisibilityProperty = "TwoButtonVisibility";
		private Visibility _twoButtonVisibility;

		public Visibility TwoButtonVisibility
		{
			get
			{
				return _twoButtonVisibility;
			}
			set
			{
				_twoButtonVisibility = value;
				this.RaisePropertyChanged(TwoButtonVisibilityProperty);
			}
		}

		public const string OneButtonVisibilityProperty = "OneButtonVisibility";
		private Visibility _oneButtonVisibility;

		public Visibility OneButtonVisibility
		{
			get
			{
				return _oneButtonVisibility;
			}
			set
			{
				_oneButtonVisibility = value;
				this.RaisePropertyChanged(OneButtonVisibilityProperty);
			}
		}

		#endregion

		public NotifyViewModel(ILoginDataService dataService)
		{
			_dataService = dataService;
			this.NotifyVisibility = Visibility.Hidden;
			Messenger.Default.Register<NotifyViewActionMessage>(this, this.HandleNotifyViewActionMessage);
			this.YesCommand = new RelayCommand(this.ExecuteYesCommand, this.CanExecuteYesCommand);
			this.NoCommand = new RelayCommand(this.ExecuteNoCommand, this.CanExecuteNoCommand);
			this.OneCommand = new RelayCommand(this.ExecuteOneCommand, this.CanExecuteOneCommand);
			this.OneButtonVisibility = Visibility.Hidden;
		}

		private void HandleNotifyViewActionMessage(NotifyViewActionMessage nam)
		{
			this.NotifyVisibility = Visibility.Visible;
			this.SetButtonMode(nam);
			this.SetText(nam);
		}

		
		private void SetText(NotifyViewActionMessage nam)
		{
			this.Message = nam.Message;
			if(nam.IsError)
			{
				this.TextColor = Colors.Red;
			}
			else
			{
				this.TextColor = Colors.SaddleBrown;
			}
		}

		private void SetButtonMode(NotifyViewActionMessage nam)
		{

			switch (nam.ButtonMode)
			{
				case NotifyButtonEnum.OneButton:
					this.TwoButtonVisibility = Visibility.Hidden;
					this.OneButtonVisibility = Visibility.Visible;

					if (nam.ButtonLabels.Count.Equals(1))
					{
						this.OneLabel = nam.ButtonLabels[0].ToString();
					}
					break;

				case NotifyButtonEnum.TwoButton:
					this.TwoButtonVisibility = Visibility.Visible;
					this.OneButtonVisibility = Visibility.Hidden;

					if (nam.ButtonLabels.Count.Equals(2))
					{
						this.YesLabel = nam.ButtonLabels[0].ToString();
						this.NoLabel = nam.ButtonLabels[1].ToString();
					}
					break;


			}
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
			
			
		}
		#endregion

		#region NoCommand
		private bool CanExecuteNoCommand()
		{
			return true;
		}
		
		private void ExecuteNoCommand()
		{
			Messenger.Default.Send<CancelLoginMessage>(new CancelLoginMessage { Action = "CancelLogin" });
		}

		private bool CanExecuteOneCommand()
		{
			return true;
		}

		private void ExecuteOneCommand()
		{
			this.NotifyVisibility = Visibility.Hidden;
		}

		#endregion

		#endregion
	}
}
