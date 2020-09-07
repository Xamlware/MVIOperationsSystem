using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Services;
using MVIOperationsSystem.ViewModels.DataEditViewModels;
using Syncfusion.UI.Xaml.Grid;
using System.ComponentModel;
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
		public RelayCommand CancelCommand { get; private set; }
		public RelayCommand OneCommand { get; private set; }
		#endregion

		#region Properties
		public static bool IsRunningInVisualStudioDesigner
		{
			get
			{
				// Are we looking at this dialog in the Visual Studio Designer or Blend?
				string appname = System.Reflection.Assembly.GetEntryAssembly().FullName;
				return appname.Contains("XDesProc");
			}
		}

		private string _action;
		public string Action
		{
			get { return _action; }
			set { _action = value; }
		}

		private string _origin;
		public string Origin
		{
			get { return _origin; }
			set { _origin = value; }
		}


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


		public const string YesButtonLabelProperty = "YesButtonLabel";
		private string _yesButtonLabel;

		public string YesButtonLabel
		{
			get { return _yesButtonLabel; }
			set 
			{
				_yesButtonLabel = value;
				this.RaisePropertyChanged(YesButtonLabelProperty);
			}
		}

		public const string NoButtonLabelProperty = "NoButtonLabel";
		private string _noButtonLabel;

		public string NoButtonLabel
		{
			get { return _noButtonLabel; }
			set
			{
				_noButtonLabel = value;
				this.RaisePropertyChanged(NoButtonLabelProperty);
			}
		}

		public const string Cancel3ButtonLabelProperty = "Cancel3ButtonLabel";
		private string _cancel3ButtonLabel;

		public string Cancel3ButtonLabel
		{
			get { return _cancel3ButtonLabel; }
			set
			{
				_cancel3ButtonLabel = value;
				this.RaisePropertyChanged(Cancel3ButtonLabelProperty);
			}
		}


		public const string OneButtonLabelProperty = "OneButtonLabel";
		private string _oneButtonLabel;

		public string OneButtonLabel
		{
			get { return _oneButtonLabel; }
			set
			{
				_oneButtonLabel = value;
				this.RaisePropertyChanged(OneButtonLabelProperty);
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

		public const string ThreeButtonVisibilityProperty = "ThreeButtonVisibility";
		private Visibility _ThreeButtonVisibility;

		public Visibility ThreeButtonVisibility
		{
			get
			{
				return _ThreeButtonVisibility;
			}
			set
			{
				_ThreeButtonVisibility = value;
				this.RaisePropertyChanged(ThreeButtonVisibilityProperty);
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
			Messenger.Default.Register<NotifyViewActionMessage>(this, this.HandleNotifyViewActionMessage);
			this.YesCommand = new RelayCommand(this.ExecuteYesCommand, this.CanExecuteYesCommand);
			this.NoCommand = new RelayCommand(this.ExecuteNoCommand, this.CanExecuteNoCommand);
			this.CancelCommand = new RelayCommand(this.ExecuteCancelCommand, this.CanExecuteCancelCommand);
			this.OneCommand = new RelayCommand(this.ExecuteOneCommand, this.CanExecuteOneCommand);
			this.NotifyVisibility = IsRunningInVisualStudioDesigner ? Visibility.Visible : Visibility.Hidden;
			this.OneButtonVisibility = Visibility.Hidden;
			this.TwoButtonVisibility = Visibility.Hidden;
			this.ThreeButtonVisibility = Visibility.Hidden;

		}

		private void HandleNotifyViewActionMessage(NotifyViewActionMessage nam)
		{
			this.NotifyVisibility = Visibility.Visible;
			this.Action = nam.Action;
			this.Origin = nam.Origin;
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
					this.ThreeButtonVisibility = Visibility.Hidden;
					this.TwoButtonVisibility = Visibility.Hidden;
					this.OneButtonVisibility = Visibility.Visible;

					if (nam.ButtonLabels.Count.Equals(1))
					{
						this.OneButtonLabel = nam.ButtonLabels[0].ToString();
					}
					break;

				case NotifyButtonEnum.TwoButton:
					this.TwoButtonVisibility = Visibility.Visible;
					this.OneButtonVisibility = Visibility.Hidden;
					this.ThreeButtonVisibility = Visibility.Hidden;

					if (nam.ButtonLabels.Count.Equals(2))
					{
						this.YesButtonLabel = nam.ButtonLabels[0].ToString();
						this.NoButtonLabel = nam.ButtonLabels[1].ToString();
					}
					break;

				case NotifyButtonEnum.ThreeButton:
					this.ThreeButtonVisibility = Visibility.Visible;
					this.TwoButtonVisibility = Visibility.Hidden;
					this.OneButtonVisibility = Visibility.Hidden;

					if (nam.ButtonLabels.Count.Equals(3))
					{
						this.YesButtonLabel = nam.ButtonLabels[0].ToString();
						this.NoButtonLabel = nam.ButtonLabels[1].ToString();
						this.Cancel3ButtonLabel = nam.ButtonLabels[2].ToString();
					}
					break;

			}
		}

		public void FinishAndReturn(NotifyButtonLabelEnum button, string action, string origin)
		{
			Messenger.Default.Send(new NotifyResultMessage { ButtonSelected = button, Action = action, Origin = origin });
		}
			
		#region Can/Execute
		#region YesCommand

		private bool CanExecuteYesCommand()
		{
			var retVal = true;
			
			return retVal;
		}

		private void ExecuteYesCommand()
		{
			switch(this.Action)
			{
				case "District":
					var d = SimpleIoc.Default.GetInstance<DistrictEditViewModel>();
					d.SaveDistrict(true);
					break;
				case "Region":
					var r = SimpleIoc.Default.GetInstance<RegionEditViewModel>();
					r.SaveRegion(true);
					break;
				case "Employee":
					var se = SimpleIoc.Default.GetInstance<EmployeeEditViewModel>();
					se.SaveEmployee(true);
					break;

					//case "Gender":
					//	var g = ServiceLocator.Current.GetInstance<GenderEditViewModel>();
					//	g.SaveGender(true);
					//	break;
					//case "Inventory":
					//	var inv = ServiceLocator.Current.GetInstance<InventoryEditViewModel>();
					//	inv.SaveInventory(true);
					//	break;

			}


			this.NotifyVisibility = Visibility.Hidden;
			FinishAndReturn(NotifyButtonLabelEnum.Yes, this.Action, this.Origin);
		}
		#endregion

		#region NoCommand
		private bool CanExecuteNoCommand()
		{
			return true;
		}
		
		private void ExecuteNoCommand()
		{
			this.NotifyVisibility = Visibility.Hidden;
			FinishAndReturn(NotifyButtonLabelEnum.No, this.Action, this.Origin);
		}
		#endregion

		#region CancelCommand
		private bool CanExecuteCancelCommand()
		{
			return true;
		}

		private void ExecuteCancelCommand()
		{
			this.NotifyVisibility = Visibility.Hidden;
			FinishAndReturn(NotifyButtonLabelEnum.Cancel, this.Action, this.Origin);
		}

		#endregion


		#region OneCommand
		private bool CanExecuteOneCommand()
		{
			var retVal = true;

			return retVal;
		}

		private async void ExecuteOneCommand()
		{
			this.NotifyVisibility = Visibility.Hidden;
		}
		#endregion

		#endregion
	}
}
