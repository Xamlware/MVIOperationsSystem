using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.ViewModels;
using System.Windows;
using System.Windows.Controls;
namespace MVIOperationsSystem.Views.DataEditViews
{
	/// <summary>
	/// Interaction logic for RegionEditView.xaml
	/// </summary>
	public partial class RegionEditView : UserControl
	{
		public RegionEditView()
		{
			InitializeComponent();
		}

		private void ListItemSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Messenger.Default.Send<ListItemChangedMessage>(new ListItemChangedMessage { Action = "TextChanged" });
		}

		private void RegionNameChanged(object sender, TextChangedEventArgs e)
		{
			Messenger.Default.Send<RegionNameChangedMessage>(new RegionNameChangedMessage { Action = this.RegionName.ToString() });
		}

		private void RegionComboSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Messenger.Default.Send<RegionComboChangedMessage>(new RegionComboChangedMessage { Action = "RegionChanged" });
		}

	}	
}



