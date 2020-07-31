using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Controls;
using MVIOperationsSystem.Messages;
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
