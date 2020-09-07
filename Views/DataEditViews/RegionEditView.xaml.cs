using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.ViewModels;
using Syncfusion.Windows.Controls.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MVIOperationsSystem.Views.DataEditViews
{
	/// <summary>
	/// Interaction logic for RegionEditView.xaml
	/// </summary>
	public partial class RegionEditView : UserControl
	{
		public RegionEditView()
		{
			Messenger.Default.Register<UpdateSourceRegionMessage>(this, HandleUpdateSourceRegionMessage);

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

		private void HandleUpdateSourceRegionMessage(UpdateSourceRegionMessage rm)
		{
			BindingExpression br = RegionName.GetBindingExpression(SfTextBoxExt.TextProperty);
			br.UpdateSource();
			
		}


		//private void RegionComboSelectionChanged(object sender, SelectionChangedEventArgs e)
		//{
		//	Messenger.Default.Send<RegionComboChangedMessage>(new RegionComboChangedMessage { Action = "RegionChanged" });
		//}

	}
}



