using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Controls;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.ViewModels.DataEditViewModels;
using MVIOperationsSystem.Views.DataEditViews;
using System.Windows;
using System.Windows.Controls;
using Xamlware.Framework.Extensions;

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
			Messenger.Default.Register<CloseAdminManagementContentMessage>(this, HandleCloseAdminManagementContentMessage);
		}

		private void HandleCloseAdminManagementContentMessage(CloseAdminManagementContentMessage obj)
		{
			this.ContentPresenter.Content = "";
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
				//if (((AdminManagementTreeModel)e.OldValue).Header.IsNotNullOrEmpty())
				//{
				//	Messenger.Default.Send(new CleanUpMessage());
				//}
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
			Messenger.Default.Send<ContentPresenterControlChangedMessage>(new ContentPresenterControlChangedMessage { Action = t.Name });
		}
	}
}
