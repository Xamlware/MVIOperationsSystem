using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.ViewModels;
using System.Windows;
using System.Windows.Controls;
namespace MVIOperationsSystem.Views.DataEditViews
{
	/// <summary>
	/// Interaction logic for DistrictEditView.xaml
	/// </summary>
	public partial class DistrictEditView : UserControl
	{
		public DistrictEditView()
		{
			InitializeComponent();
		}

		private void ListItemSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Messenger.Default.Send<ListItemChangedMessage>(new ListItemChangedMessage { Action = "TextChanged" });
		}

		private void DistrictNameChanged(object sender, TextChangedEventArgs e)
		{
			Messenger.Default.Send<DistrictNameChangedMessage>(new DistrictNameChangedMessage { Action = this.DistrictName.ToString() });
		}

		private void RegionComboSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Messenger.Default.Send<RegionComboChangedMessage>(new RegionComboChangedMessage { Action = "RegionChanged" });
		}
	}
}



