using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Controls;
using MVIOperationsSystem.Data;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Services;
using MVIOperationsSystem.Views;
using System;
using System.ComponentModel;
using System.Windows;
using Xamlware.Framework.Extensions;

namespace MVIOperationsSystem
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {

		public MainWindow()
		{
			InitializeComponent();
			

			Messenger.Default.Register<NavigationMessage>(this, HandleNavigationMessage);
			Messenger.Default.Register<CancelLoginMessage>(this, HandleCancelLoginMessage);
			Messenger.Default.Register<AdminDataCloseMessage>(this, HandleAdminDataCloseMessage);
			//Messenger.Default.Register<OpenAdminDataManagementMessage>(this, HandleOpenAdminDataManagementMessage);
			
			Closing += this.OnWindowClosing;
		}

		private void HandleAdminDataCloseMessage(AdminDataCloseMessage obj)
		{
			
		}

		private void HandleCancelLoginMessage(CancelLoginMessage obj)
		{
			this.SignInButton.Label = "Sign In";
			this.ContentPresenter.Content = "";
		}

		private void OnWindowClosing(object sender, CancelEventArgs e)
		{

			Messenger.Default.Send<ClosingMessage>(new ClosingMessage { IsClosing = true });
			//_ls.ClearLocalStorage();


		}

		private void HandleNavigationMessage(NavigationMessage obj)
		{
			switch (obj.Action)
			{

				case "Login":
					this.ContentPresenter.Content = new LoginView();
					break;
				case "AdminLogin":
					this.ContentPresenter.Content = new AdminDataView();
					break;
				case "Close":
					this.ContentPresenter.Content = "";
					break;

			}

		}

		private void ContentPresenter_ContentChanged(object sender, RoutedEventArgs e)
		{
			if (this.ContentPresenter.Content.ToString().IsNullOrEmpty())
			{
				Messenger.Default.Send<ContentEmptyMessage>(new ContentEmptyMessage());
			}
			else
			{
				Messenger.Default.Send<ContentFilledMessage>(new ContentFilledMessage());

			}
		}
	}
}
