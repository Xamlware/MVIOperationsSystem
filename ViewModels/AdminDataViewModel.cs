using MVIOperationsSystem.Models;
using MVIOperationsSystem.Views.DataEditViews;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVIOperationsSystem.ViewModels
{
	public class AdminDataViewModel : MVIViewModelBase
	{


		#region Properties

		public const string ActionListProperty = "ActionList";
		private ObservableCollection<AdminManagementTreeModel> _actionList;

		public ObservableCollection<AdminManagementTreeModel> ActionList
		{
			get
			{
				return _actionList;
			}
			set
			{
				_actionList = value;
				this.RaisePropertyChanged(ActionListProperty);
			}
		}

		public const string SelectedActionItemProperty = "SelectedActionItem";
		private object _selectedActionItem;
		public object SelectedActionItem
		{
			get
			{
				return _selectedActionItem;
			}
			set
			{
				_selectedActionItem = value;
				this.RaisePropertyChanged(SelectedActionItemProperty);

			}
		}

		public const string TabItemsProperty = "TabItems";
		private ObservableCollection<TabItem> _tabItems;
		public ObservableCollection<TabItem> TabItems
		{
			get { return _tabItems; }
			set
			{
				_tabItems = value;
				this.RaisePropertyChanged("TabItems");
			}
		}

		public const string SelectedTabProperty = "SelectedTab";
		private string _selectedTab;

		public string SelectedTab
		{
			get { return _selectedTab; }
			set { 
				_selectedTab = value;
				this.RaisePropertyChanged(SelectedTabProperty);
			}
		}

		#endregion



		public AdminDataViewModel()
		{
			this.InitializeActionList();
			this.TabItems = new ObservableCollection<TabItem>();
		}

		public void AddTabItem(RoutedPropertyChangedEventArgs<object> e, string tabName)
		{
			var found = false;
			var index = -1;
			switch (tabName)
			{
				case "District":
					foreach (var t in this.TabItems)
					{
						index++;
						if (t.Header.Equals(tabName))
						{

							found = true;
							break;
						}
					}
					if (!found)
					{
						var districtTab = new TabItem();
						districtTab.Header = tabName;
						districtTab.Content = new DistrictEditView();
						this.TabItems.Add(districtTab);
					}
					else
					{
						this.SelectedTab = tabName;
					}
					break;

				case "Employee":
					var employeeTab = new TabItem();
					employeeTab.Header = tabName;
					employeeTab.Content = new EmployeeEditView();
					this.TabItems.Add(employeeTab);
					break;
				//case "Gender":
				//	this.ContentPresenter.Content = new GenderEditView();
				//	break;
				//case "Inventory":
				//	this.ContentPresenter.Content = new InventoryEditView();
				//	break;
				case "Region":
					var regionTab = new TabItem();
					regionTab.Header = tabName;
					regionTab.Content = new RegionEditView();
					this.TabItems.Add(regionTab);
					break;

					// Creating an instances of TabControl and adding the tabitems into the TabControl
			}



			//if (((AdminManagementTreeModel)e.OldValue).Header.IsNotNullOrEmpty())
			//{
			//	Messenger.Default.Send(new CleanUpMessage());
			//}


		}



	#region CreateTree

	public void InitializeActionList()
		{
			this.ActionList = new ObservableCollection<AdminManagementTreeModel>();
			AdminManagementTreeModel DataRoot = new AdminManagementTreeModel() { Header = "Data Maintenance" };
			this.AddDataSubItems(DataRoot);
			this.ActionList.Add(DataRoot);
			AdminManagementTreeModel ReportRoot = new AdminManagementTreeModel() { Header = "Reports" };
			this.AddReportSubItems(ReportRoot);
			this.ActionList.Add(ReportRoot);
			this.SelectedActionItem = this.ActionList[0];
		}

		public void AddDataSubItems(AdminManagementTreeModel dataRoot)
		{
			AdminManagementTreeModel region = new AdminManagementTreeModel() { Header = " Region" };
			AdminManagementTreeModel district = new AdminManagementTreeModel() { Header = " District" };
			AdminManagementTreeModel employee = new AdminManagementTreeModel() { Header = " Employee" };
			AdminManagementTreeModel employeeTime = new AdminManagementTreeModel() { Header = " Employee Time" };
			AdminManagementTreeModel gender = new AdminManagementTreeModel() { Header = " Gender" };
			AdminManagementTreeModel inventory = new AdminManagementTreeModel() { Header = " Inventory" };

			dataRoot.SubItems.Add(region);
			dataRoot.SubItems.Add(district);
			dataRoot.SubItems.Add(employee);
			dataRoot.SubItems.Add(employeeTime);
			dataRoot.SubItems.Add(gender);
			dataRoot.SubItems.Add(inventory);
		}

		public void AddReportSubItems(AdminManagementTreeModel reportRoot)
		{
			AdminManagementTreeModel sales = new AdminManagementTreeModel() { Header = " Sales" };

			reportRoot.SubItems.Add(sales);
		}


		#endregion
	}
}
