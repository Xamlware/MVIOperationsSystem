using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVIOperationsSystem.Controls
{
	/// <summary>
	/// Interaction logic for LoginView.xaml
	/// </summary>
	public partial class LoginView : UserControl
	{
		public LoginView()
		{
			InitializeComponent();
			Messenger.Default.Register<CancelLoginMessage>(this, HandleCancelLoginMessage);
		}

		private void HandleCancelLoginMessage(CancelLoginMessage obj)
		{

		}

		private void LoginPassword_PasswordChanged(object sender, RoutedEventArgs e)
		{
			//var dvm = SimpleIoc.Default.GetInstance<LoginViewModel>()
			var pm = new PasswordMessage { Password = ((PasswordBox)sender).Password };
			Messenger.Default.Send<PasswordMessage>(pm);
		}


		//private void OnClick(object sender, EventArgs e)
		//{
		//	var password = this.LoginPassword.Password;
		//}

	}
}
