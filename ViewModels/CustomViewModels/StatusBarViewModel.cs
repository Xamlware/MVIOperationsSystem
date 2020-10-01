using MVIOperations.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;

namespace MVIOperationsSystem.ViewModels.CustomViewModels
{
	public class StatusBarViewModel : MVIViewModelBase
	{ 

		DispatcherTimer _timer;
		#region Properties

		public const string StatusBarLoggedInEmployeeProperty = "LoggedInEmployee";
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

		#endregion

		public StatusBarViewModel()
		{
			this.InitializeTimer();
			this.IsLoggedInEmployeeVisible = false;
			this.IsProgressBarVisible = false;
			//this.ProgressBarText = "Retrieving data!";
		}

		/// <summary>
		/// 
		/// </summary>
		private void InitializeTimer()
		{
			_timer = new DispatcherTimer();
			_timer.Tick += TimerTick;
			_timer.Interval = new TimeSpan(0, 0, 10);
			this.StartTimer();
		}


		/// <summary>
		/// 
		/// </summary>
		public void StartTimer()
		{
			_timer.Start();
		}


		/// <summary>
		/// 
		/// </summary>
		public void StopTimer()
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

	}
}
