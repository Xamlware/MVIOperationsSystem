using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MVIOperationsSystem.ViewModels
{
	public class AdminManagementViewModel : MVIViewModelBase
	{
		private readonly ILoginDataService _dataService;
		private readonly LocalStorageService _ls = new LocalStorageService();

		#region Commands
		public RelayCommand LoginCommand { get; private set; }
		public RelayCommand CancelLoginCommand { get; private set; }
		public RelayCommand CloseAdminDataWindowCommand { get; private set; }
		#endregion


		#region Propertie
		private bool _isContentPresent = false;

		public bool IsContentPresent
		{
			get { return _isContentPresent; }
			set { _isContentPresent = value; }
		}

		public const string IsDirtyProperty = "IsDirty";
		private bool _isDirty;

		public bool IsDirty
		{
			get { return _isDirty; }
			set {
				_isDirty = value;
				this.RaisePropertyChanged(IsDirtyProperty);
			}
		}


		public const string IsBusyProperty = "IsBusy";
		private bool _isBusy;

		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				this.RaisePropertyChanged(IsBusyProperty);
			}
		}




		public const string SelectedTreeItemProperty = "SelectedTreeItem";
		private object _selectedTreeItem;
		public object SelectedTreeItem
		{
			get
			{
				return _selectedTreeItem;
			}
			set
			{
				_selectedTreeItem = value;
				this.RaisePropertyChanged(SelectedTreeItemProperty);

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


		public const string DataLabelProperty = "DataLabel";
		private string _dataLabel;

		public string DataLabel
		{
			get { return _dataLabel; }
			set 
			{
				_dataLabel = value;
				this.RaisePropertyChanged(DataLabelProperty);
			}
		}

		#endregion



		public AdminManagementViewModel(ILoginDataService dataService)
		{
			_dataService = dataService;
			this.IsDirty = false;

			//Messenger.Default.Register<PasswordMessage>(this, HandlePasswordMessage);
			Messenger.Default.Register<RemoveAdminLabelMessage>(this, this.HandleRemoveAdminLabelMessage);
			Messenger.Default.Register<SignOutMessage>(this, this.HandleSignOutMessage);
			Messenger.Default.Register<ContentPresenterChangedMessage>(this, this.HandleContentPresenterChangedMessage);


			this.LoginCommand = new RelayCommand(this.ExecuteLoginCommand, this.CanExecuteLoginCommand);
			this.CancelLoginCommand = new RelayCommand(this.ExecuteCancelLoginCommand, this.CanExecuteLoginCancelCommand);
			this.CloseAdminDataWindowCommand = new RelayCommand(this.ExecuteCloseAdminDataWindowCommand, this.CanExecuteCloseAdminDataWindowCommand);
			this.InitialzieActionList();
			this.DataLabel = "Admin Data Management";
		}

		private void HandleContentPresenterChangedMessage(ContentPresenterChangedMessage cm)
		{
			this.IsContentPresent = false;
		}

		//private void Cleanup()
		//{
		
		//}

		private void ExecuteCloseAdminDataWindowCommand()
		{
			if (!this.IsContentPresent)
			{
				this.Cleanup();
				Messenger.Default.Send<NavigationMessage>(new NavigationMessage { Action = "Close" });
			}
		}

		private bool CanExecuteCloseAdminDataWindowCommand()
		{
			return true;
		}

		private void HandleSignOutMessage(SignOutMessage obj)
		{
			
		}

		private void HandleRemoveAdminLabelMessage(RemoveAdminLabelMessage obj)
		{
			this.DataLabel = "";
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


		#region CreateTree
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

		public void OnSelectedItemChanged()
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

		private void ExecuteLoginCommand()
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
			Messenger.Default.Send<CancelLoginMessage>(new CancelLoginMessage { Action = "CancelLogin" });
		}
		#endregion

		#endregion
	}
}
