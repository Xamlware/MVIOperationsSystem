﻿using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.ViewModels;
using System.Windows;
using System.Windows.Controls;
namespace MVIOperationsSystem.Views.DataEditViews
{
	/// <summary>
	/// Interaction logic for InventoryEditView.xaml
	/// </summary>
	public partial class InventoryEditView : UserControl
	{
		public InventoryEditView()
		{
			InitializeComponent();
		}

		private void ListItemSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Messenger.Default.Send<ListItemChangedMessage>(new ListItemChangedMessage { Action = "TextChanged" });
		}

		private void InventoryNameChanged(object sender, TextChangedEventArgs e)
		{
			Messenger.Default.Send<InventoryNameChangedMessage>(new InventoryNameChangedMessage { Action = this.InventoryName.ToString() });
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



