using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Controls;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Views.DataEditViews;
using System.Windows;
using System.Windows.Controls;

namespace MVIOperationsSystem.Views
{
	/// <summary>
	/// Interaction logic for AdminManagementView.xaml
	/// </summary>
	public partial class AdminManagementView : UserControl
	{
		public AdminManagementView()
		{
			InitializeComponent();
			Messenger.Default.Register<NavigationMessage>(this, HandleNavigationMessage);
			Messenger.Default.Register<CloseMessage>(this, HandleCloseMessage);
		}

		private void HandleCloseMessage(CloseMessage obj)
		{
			Messenger.Default.Send<CleanUpMessage>(new CleanUpMessage());
		}

		private void HandleNavigationMessage(NavigationMessage obj)
		{
			this.ActionTree.ClearSelection();
			this.ActionTree.SelectedTreeItem = this.ActionTree.Items[0];
		}

		private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (e.NewValue != null)
			{
				var item = ((AdminManagementTreeModel)e.NewValue).Header.Trim();

				switch (item)
				{

					case "District":
						this.ContentPresenter.Content = new DistrictEditView();
						break;
					case "Employee":
						this.ContentPresenter.Content = new EmployeeEditView();
						break;
					case "Gender":
						this.ContentPresenter.Content = new GenderEditView();
						break;
					case "Inventory":
						this.ContentPresenter.Content = new InventoryEditView();
						break;
					case "Region":
						this.ContentPresenter.Content = new RegionEditView();
						break;
				}
			}

		}

		private void ContentPresenter_ContentChanged(object sender, RoutedEventArgs e)
		{
			var item = ((MVIContentPresenter)e.Source).Content;
			var t = item.GetType();
			Messenger.Default.Send<RemoveAdminLabelMessage>(new RemoveAdminLabelMessage { Action = "" });
			Messenger.Default.Send<ContentPresenterChangedMessage>(new ContentPresenterChangedMessage { Action = t.Name });
		}
	}
}
