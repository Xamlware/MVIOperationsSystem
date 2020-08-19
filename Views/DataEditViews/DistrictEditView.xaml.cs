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

		private void NotifyView_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}



