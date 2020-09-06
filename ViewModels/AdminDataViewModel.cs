using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Services;
using MVIOperationsSystem.ViewModels.DataEditViewModels;
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
		MainViewModel _vm;
		ILocalStorageService _ls;

		#region Commands
		public RelayCommand CloseAdminDataWindowCommand { get; private set; }
		#endregion

		#region Properties
		private Dictionary<int, string> TabsUsed = new Dictionary<int, string>();


		public const string IsTreePanelEnabledProperty = "IsTreePanelEnabled";
		private bool _isTreePanelEnabled;

		public bool IsTreePanelEnabled
		{
			get
			{
				return _isTreePanelEnabled;
			}
			set
			{
				_isTreePanelEnabled = value;
				this.RaisePropertyChanged(IsTreePanelEnabledProperty);
			}
		}


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

		public const string SelectedItemProperty = "SelectedItem";
		private TabItem _selectedItem;
		public TabItem SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				_selectedItem = value;
				this.RaisePropertyChanged(SelectedItemProperty);
			}
		}

		public const string SelectedIndexProperty = "SelectedIndex";
		private int _selectedIndex;
		public int SelectedIndex
		{
			get { return _selectedIndex; }
			set
			{
				_selectedIndex = value;
				this.RaisePropertyChanged(SelectedIndexProperty);
			}
		}
		#endregion

		public AdminDataViewModel(ILocalStorageService ls, MainViewModel vm)
		{
			_ls = ls;
			_vm = vm;
			//_vm.ActiveViewModels.Add(this.GetType(), "Admin");

			this.CloseAdminDataWindowCommand = new RelayCommand(this.ExecuteCloseAdminDataWindowCommand, this.CanCloseAdminDataWindowCommand);
			Messenger.Default.Register<EnableAdminTreePanelMessage>(this, HandleEnableAdminTreePanelMessage);
			Messenger.Default.Register<ClosedMessage>(this, HandleClosedMessage);
			this.InitializeActionList();
			this.TabItems = new ObservableCollection<TabItem>();
			this.IsTreePanelEnabled = true;
		}

		private void HandleClosedMessage(ClosedMessage obj)
		{
			_ls.ClearLocalStorage();
		}

		public void AddTabItem(string tabName)
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
						TabsUsed.Add(this.TabItems.Count() - 1, tabName);
					}
					else
					{
						SetSelectedTab();

					}
					break;

				case "Employee":
					var employeeTab = new TabItem();
					employeeTab.Header = tabName;
					employeeTab.Content = new EmployeeEditView();
					this.TabItems.Add(employeeTab);
					TabsUsed.Add(this.TabItems.Count() - 1, tabName);

					break;

				case "Region":
					var regionTab = new TabItem();
					regionTab.Header = tabName;
					regionTab.Content = new RegionEditView();
					this.TabItems.Add(regionTab);
					TabsUsed.Add(this.TabItems.Count() - 1, tabName);

					break;

					// Creating an instances of TabControl and adding the tabitems into the TabControl
			}



			//if (((AdminManagementTreeModel)e.OldValue).Header.IsNotNullOrEmpty())
			//{
			//	Messenger.Default.Send(new CleanUpMessage());
			//}


		}

		public void SetSelectedTab()
		{
			var i = this.TabsUsed.First(f => f.Value == "District");
			this.SelectedItem = this.TabItems[i.Key];
		}

		public bool CheckForDirtyViews()
		{
			var dirtyView = false;
			foreach (var t in this.TabItems)
			{
				if (t.Header.Equals("District"))
				{
					var vm = SimpleIoc.Default.GetInstance<DistrictEditViewModel>();

					if (vm.IsDirty)
					{
						dirtyView = true;
						break;
					}
				}
				else if (t.Header.Equals("Employee"))
				{
					var vm = SimpleIoc.Default.GetInstance<EmployeeEditViewModel>();

					if (vm.IsDirty)
					{
						dirtyView = true;
						break;
					}
				}
				else if (t.Header.Equals("Region"))
				{
					var vm = SimpleIoc.Default.GetInstance<RegionEditViewModel>();

					if (vm.IsDirty)
					{
						dirtyView = true;
						break;
					}
				};
			}
			return dirtyView;
		}

		private bool CanCloseAdminDataWindowCommand()
		{
			//return CheckForDirtyViews();
			return true;
		}

		private void ExecuteCloseAdminDataWindowCommand()
		{
			Messenger.Default.Send(new AdminDataCloseMessage { Action = "Close" });
		}

		public override void Cleanup()
		{
			SimpleIoc.Default.Unregister<AdminDataViewModel>();
            SimpleIoc.Default.Register<AdminDataViewModel>();
			base.Cleanup();
		}

		#region Message Handlers
		private void HandleEnableAdminTreePanelMessage(EnableAdminTreePanelMessage m)
		{
			this.IsTreePanelEnabled = m.Action;
		}

		#endregion

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
