using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace MVIOperationsSystem.ViewModels
{
	public class MVIViewModelBase : ViewModelBase
	{
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
			this.IsConnected = false;
		}


		public void CheckForConnectivity()
		{
			// Create a timer with a two second interval.
			var timer = new System.Timers.Timer(30000);
			// Hook up the Elapsed event for the timer. 
			timer.Elapsed += OnTimedEvent;
			timer.AutoReset = true;
			timer.Enabled = true;

		}


		private void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			CheckForNetworkConnection();
		}

		public void CheckForNetworkConnection()
		{
			var retVal = false;
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.milvetindapi.com/");
				request.Timeout = 5000;
				request.Credentials = CredentialCache.DefaultNetworkCredentials;
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();

				if (response.StatusCode == HttpStatusCode.OK)
					retVal=true;
			}
			catch(Exception e)
			{
			}

			this.IsConnected = retVal;
			Messenger.Default.Send(new NetworkConnectionMessage { IsConnected = this.IsConnected });
		}

		private void HandleCleanUpMessage(CleanUpMessage cm)
		{
			Cleanup();
		}


	}
}
