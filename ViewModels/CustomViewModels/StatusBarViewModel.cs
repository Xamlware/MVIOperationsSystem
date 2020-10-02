using GalaSoft.MvvmLight.Messaging;
using MVIOperations.Models;
using MVIOperationsSystem.Messages;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;
using Xamlware.Framework.Extensions;

namespace MVIOperationsSystem.ViewModels.CustomViewModels
{
	public class StatusBarViewModel : MVIViewModelBase
	{ 

		DispatcherTimer _timer;
		MainViewModel _mvm;
		#region Properties

		public const string StatusBarLoggedInEmployeeProperty = "StatusBarLoggedInEmployee";
		private string _statusBarLoggedInEmployee;

		public string StatusBarLoggedInEmployee
		{
			get { return _statusBarLoggedInEmployee; }
			set {
				_statusBarLoggedInEmployee = value;
				this.RaisePropertyChanged(StatusBarLoggedInEmployeeProperty);
			}
		}


		public const string IsSaveButtonPanelVisibleProperty = "IsSaveButtonPanelVisible";
		private Visibility _isSaveButtonPanelVisible;

		public Visibility IsSaveButtonPanelVisible
		{
			get
			{
				return _isSaveButtonPanelVisible;
			}
			set
			{
				_isSaveButtonPanelVisible = value;
				this.RaisePropertyChanged(IsSaveButtonPanelVisibleProperty);
			}
		}

		public const string StatusBarDateTimeProperty = "StatusBarDateTime";
		private string _statusBarDateTime = DateTime.Now.AddMonths(1).ToString("g", CultureInfo.CreateSpecificCulture("en-us")).Trim();
		public string StatusBarDateTime
		{

			get { return _statusBarDateTime; }
			set
			{
				_statusBarDateTime = value;
				this.RaisePropertyChanged(StatusBarDateTimeProperty);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		private const string IsProgressBarVisiblePropertyName = "IsProgressBarVisible";
		private bool _isProgressBarVisible;
		public bool IsProgressBarVisible
		{
			get { return _isProgressBarVisible; }
			set
			{
				_isProgressBarVisible = value;
				this.RaisePropertyChanged(IsProgressBarVisiblePropertyName);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private const string IsLoggedInEmployeeVisiblePropertyName = "IsLoggedInEmployeeVisible";
		private bool _isLoggedInEmployeeVisible;
		public bool IsLoggedInEmployeeVisible
		{
			get { return _isLoggedInEmployeeVisible; }
			set
			{
				_isLoggedInEmployeeVisible = value;
				this.RaisePropertyChanged(IsLoggedInEmployeeVisiblePropertyName);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private const string ProgressBarTextPropertyName = "ProgressBarText";
		private string _progressBarText;
		public string ProgressBarText
		{
			get { return _progressBarText; }
			set
			{
				_progressBarText = value;
				this.RaisePropertyChanged(ProgressBarTextPropertyName);
			}
		}

		public const string IsGreenLightVisibleProperty = "IsGreenLightVisible";
		private Visibility _isGreenLightVisible;

		public Visibility IsGreenLightVisible
		{
			get
			{
				return _isGreenLightVisible;
			}
			set
			{
				_isGreenLightVisible = value;
				this.RaisePropertyChanged(IsGreenLightVisibleProperty);
			}
		}

		public const string IsRedLightVisibleProperty = "IsRedLightVisible";
		private Visibility _isRedLightVisible;

		public Visibility IsRedLightVisible
		{
			get
			{
				return _isRedLightVisible;
			}
			set
			{
				_isRedLightVisible = value;
				this.RaisePropertyChanged(IsRedLightVisibleProperty);
			}
		}
		#endregion


		public StatusBarViewModel(MainViewModel mvm)
		{
			_mvm = mvm;	
			this.InitializeTimer();
			this.IsLoggedInEmployeeVisible = false;
			this.IsProgressBarVisible = false;
			this.IsGreenLightVisible = Visibility.Collapsed;
			this.IsRedLightVisible = Visibility.Collapsed;
			Messenger.Default.Register<NetworkConnectionMessage>(this, this.HandleNetworkConnectionMessage);
			Messenger.Default.Register<LoggedInMessage>(this, this.HandleLoggedInMessage);
			//this.ProgressBarText = "Retrieving data!";
		}

		private void HandleLoggedInMessage(LoggedInMessage lim)
		{
			this.IsLoggedInEmployeeVisible = lim.User.IsNotNullOrEmpty();
			this.StatusBarLoggedInEmployee = lim.User;
		}

		private void HandleNetworkConnectionMessage(NetworkConnectionMessage ncm)
		{
			this.SetNetworkLight();
		}


		private void SetNetworkLight()
		{
			this.IsGreenLightVisible = _mvm.IsConnected ? Visibility.Visible : Visibility.Collapsed;
			this.IsRedLightVisible = this.IsConnected ? Visibility.Collapsed : Visibility.Visible;
		}

		/// <summary>
		/// 
		/// </summary>
		private void InitializeTimer()
		{
			_timer = new DispatcherTimer();
			_timer.Tick += TimerTick;
			_timer.Interval = new TimeSpan(0, 0, 10);
			this.StartStatusTimer();
		}


		/// <summary>
		/// 
		/// </summary>
		public void StartStatusTimer()
		{
			_timer.Start();
		}


		/// <summary>
		/// 
		/// </summary>
		public void StopStatusTimer()
		{
			_timer.Stop();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TimerTick(object sender, EventArgs e)
		{
			this.StatusBarDateTime = DateTime.Now.ToString("g", CultureInfo.CreateSpecificCulture("en-us")).Trim();
		}

		public void ShowProgressBar(bool state, string text)
		{
			this.ProgressBarText = text;
			this.IsProgressBarVisible = state;
		}
	}
}
