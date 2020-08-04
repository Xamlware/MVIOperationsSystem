using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Controls;
using MVIOperationsSystem.Data;
using MVIOperationsSystem.Messages;
using System;
using System.ComponentModel;
using System.Windows;

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
			Messenger.Default.Register<SignInMessage>(this, HandleSignInMessage);
            Closing += this.OnWindowClosing;
    
			


		}

		private void OnWindowClosing(object sender, CancelEventArgs e)
		{

			var cm = new ClosingMessage { IsClosing = true };
			Messenger.Default.Send<ClosingMessage>(cm);	
		}

		private void HandleSignInMessage(SignInMessage obj)
		{
            if (obj.Action == "Sign In")
            {
                this.ContentPresenter.Content = new LoginView();
            }
		}
	}
}
