using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.ViewModels;
using Syncfusion.Windows.Controls.Input;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MVIOperationsSystem.Views.DataEditViews
{
	/// <summary>
	/// Interaction logic for DistrictEditView.xaml
	/// </summary>
	public partial class DistrictEditView : UserControl
	{
		public DistrictEditView()
		{
			Messenger.Default.Register<UpdateSourceDistrictMessage>(this, HandleUpdateSourceDistrictMessage);
			InitializeComponent();
		}

		private void HandleUpdateSourceDistrictMessage(UpdateSourceDistrictMessage obj)
		{
			BindingExpression bd = DistrictName.GetBindingExpression(SfTextBoxExt.TextProperty);
			bd.UpdateSource();
			
			BindingExpression br = RegionCombo.GetBindingExpression(ComboBoxAdv.DisplayMemberPathProperty);
			br.UpdateSource();

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



