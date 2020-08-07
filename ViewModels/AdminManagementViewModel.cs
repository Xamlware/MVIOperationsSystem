using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Services;
using System.Collections.ObjectModel;

namespace MVIOperationsSystem.ViewModels
{
	public class AdminManagementViewModel : ViewModelBase
	{
		private readonly ILoginDataService _dataService;
		private readonly LocalStorageService _ls = new LocalStorageService();

		#region Commands
		public RelayCommand LoginCommand { get; private set; }
		public RelayCommand CancelLoginCommand { get; private set; }

		#endregion

		#region Properties

		private string SelectedItemProperty { get; set; }
		private object selectedtreeitem;
		public object SelectedTreeItem
		{
			get
			{
				return selectedtreeitem;
			}
			set
			{
				selectedtreeitem = value;
				this.RaisePropertyChanged("SelectedTreeItem");
			}
		}


		private string ActionListProperty { get; set; }
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


		public const string MessageProperty = "Message";
		private string _message;

		public string Message
		{
			get { return _message; }
			set
			{
				_message = value;
				this.RaisePropertyChanged(MessageProperty);
			}
		}

		#endregion



		public AdminManagementViewModel(ILoginDataService dataService)
		{
			_dataService = dataService;
			//Messenger.Default.Register<PasswordMessage>(this, HandlePasswordMessage);
			this.LoginCommand = new RelayCommand(this.ExecuteLoginCommand, this.CanExecuteLoginCommand);
			this.CancelLoginCommand = new RelayCommand(this.ExecuteCancelLoginCommand, this.CanExecuteLoginCancelCommand);

			this.InitialzieActionList();

		}

		public void InitialzieActionList()
		{
			this.ActionList = new ObservableCollection<AdminManagementTreeModel>();
			AdminManagementTreeModel DataRoot = new AdminManagementTreeModel() { Header = "Data Maintenance" };
			this.AddDataSubItems(DataRoot);
			this.ActionList.Add(DataRoot);
			AdminManagementTreeModel ReportRoot = new AdminManagementTreeModel() { Header = "Reports" };
			this.AddReportSubItems(ReportRoot);
			this.ActionList.Add(ReportRoot);

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



		public void SelectedTreeItemChanged()
		{
			var item = this.SelectedTreeItem;	

		}
		
		#region Can/Execute
		#region LoginCommand

		private bool CanExecuteLoginCommand()
		{
			var retVal = true;
		
			return retVal;
		}

		private async void ExecuteLoginCommand()
		{
		}
		#endregion

		#region CancelLoginCommand
		private bool CanExecuteLoginCancelCommand()
		{
			return true;
		}

		private void ExecuteCancelLoginCommand()
		{
			Messenger.Default.Send<NavigationMessage>(new NavigationMessage { Action = "CancelLogin" });
		}
		#endregion

		#endregion
	}
}
