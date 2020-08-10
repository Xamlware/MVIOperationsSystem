using GalaSoft.MvvmLight.Messaging;
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

		}

		private void HandleNavigationMessage(NavigationMessage obj)
		{
			this.ActionTree.ClearSelection();
			this.ActionTree.SelectedTreeItem = this.ActionTree.Items[0];
		}

		private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			var item = ((AdminManagementTreeModel) e.NewValue).Header.Trim();

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
}
//dataRoot.SubItems.Add(region);
//			dataRoot.SubItems.Add(district);
//			dataRoot.SubItems.Add(employee);
//			dataRoot.SubItems.Add(employeeTime);
//			dataRoot.SubItems.Add(gender);
//			dataRoot.SubItems.Add(inventory);
