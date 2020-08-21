using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.ViewModels;
using System.Windows;
using System.Windows.Controls;
namespace MVIOperationsSystem.Views.DataEditViews
{
	/// <summary>
	/// Interaction logic for EmployeeEditView.xaml
	/// </summary>
	public partial class EmployeeEditView : UserControl
	{
		public EmployeeEditView()
		{
			InitializeComponent();
		}

		private void ListItemSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Messenger.Default.Send<ListItemChangedMessage>(new ListItemChangedMessage { Action = "TextChanged" });
		}

		private void EmployeeNameChanged(object sender, TextChangedEventArgs e)
		{
			//Messenger.Default.Send<EmployeeNameChangedMessage>(new EmployeeNameChangedMessage { Action = this.FirstName.ToString() });
		}


		private void NotifyView_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}



