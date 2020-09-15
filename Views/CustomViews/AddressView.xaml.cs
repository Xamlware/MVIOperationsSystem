using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using Syncfusion.Windows.Controls.Input;
using System.Windows.Controls;
using System.Windows.Data;

namespace MVIOperationsSystem.Views.CustomViews
{
	/// <summary>
	/// Interaction logic for AddressView.xaml
	/// </summary>
	public partial class AddressView : UserControl
	{
		public AddressView()
		{
			InitializeComponent();
		}
	
		private void AddressChanged(object sender, TextChangedEventArgs e)
		{
			Messenger.Default.Send(new AddressChangedMessage());
		}

		private void AddressChanged(object sender, SelectionChangedEventArgs e)
		{
			Messenger.Default.Send(new AddressChangedMessage());
		}
	}
	
}
