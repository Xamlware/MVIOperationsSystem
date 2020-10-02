using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using System;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Threading;

namespace MVIOperationsSystem.ViewModels
{
	public class MVIViewModelBase : ViewModelBase
	{
		DispatcherTimer _timer;
		public const string IsConnectedProperty = "IsConnected";
		private bool _isConnected;

		public bool IsConnected
		{
			get
			{
				return _isConnected;
			}
			set
			{
				_isConnected = value;
				this.RaisePropertyChanged(IsConnectedProperty);
			}
		}

		public MVIViewModelBase()
		{
			this.InitializeTimer();
			this.CheckForConnectivity();
		}

		
		/// <summary>
		/// 
		/// </summary>
		private void InitializeTimer()
		{
			_timer = new DispatcherTimer();
			_timer.Tick += _timer_Tick; 
			this.StartTimer(1);
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			CheckForNetworkConnection();
		}

		public void CheckForConnectivity()
		{
			this.StopTimer();
			this.StartTimer(10);
		}

		/// <summary>
		/// 
		/// </summary>
		public void StartTimer(int interval)
		{
			_timer.Interval = new TimeSpan(0, 0, interval);
			_timer.Start();
		}


		/// <summary>
		/// 
		/// </summary>
		public void StopTimer()
		{
			_timer.Stop();
		}


		public void CheckForNetworkConnection()
		{
			//this.IsConnected = false;
			try
			{
				var resp = IsInternetAvailable();
				this.IsConnected = resp;
			}
			catch (Exception e)
			{
			}

			//this.IsConnected = retVal;
			Messenger.Default.Send(new NetworkConnectionMessage { IsConnected = this.IsConnected });
		}

		[DllImport("wininet.dll")]
		private extern static bool InternetGetConnectedState(out int description, int reservedValue);
		public static bool IsInternetAvailable()
		{
			try
			{
				int description;
				return InternetGetConnectedState(out description, 0);
			}
			catch (Exception ex)
			{
				return false;
			}
		}


		private void HandleCleanUpMessage(CleanUpMessage cm)
		{
			Cleanup();
		}


	}
}
