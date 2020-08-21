using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVIOperations.Models;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using Xamlware.Framework.Extensions;

namespace MVIOperationsSystem.ViewModels.DataEditViewModels
{
	public class EmployeeEditViewModel : MVIViewModelBase
	{
		private readonly IDataService<Employee> _es;
		private readonly ILocalStorageService _ls;
		private bool isInitializing = false;
		private bool isNew = false;

		#region Commands
		public RelayCommand AddEmployeeCommand { get; private set; }
		public RelayCommand EditEmployeeCommand { get; private set; }
		public RelayCommand DeleteEmployeeCommand { get; private set; }
		public RelayCommand SaveEmployeeCommand { get; private set; }
		public RelayCommand CancelEmployeeCommand { get; private set; }
		public RelayCommand NotificationMessageViewedCommand { get; private set; }
		#endregion

		#region Properties

		public const string IsFormEnabledProperty = "IsFormEnabled";
		private bool _isFormEnabled;

		public bool IsFormEnabled
		{
			get { return _isFormEnabled; }
			set {
				_isFormEnabled = value;
				this.RaisePropertyChanged(IsFormEnabledProperty);
			}
		}

		public const string FirstNameProperty = "FirstName";

		private string _firstName;

		public string FirstName
		{
			get { return _firstName; }
			set { 
				_firstName = value;
				this.RaisePropertyChanged(FirstNameProperty);
			}
		}

		public const string MiddleNameProperty = "MiddleName";
		private string _middleName;

		public string MiddleName
		{
			get { return _middleName; }
			set
			{
				_middleName = value;
				this.RaisePropertyChanged(MiddleNameProperty);
			}
		}

		public const string LastNameProperty = "LastName";
		private string _lastName;

		public string LastName
		{
			get { return _lastName; }
			set
			{
				_lastName = value;
				this.RaisePropertyChanged(LastNameProperty);
			}
		}

		public const string NameSuffixProperty = "NameSuffix";
		private string _nameSuffix;

		public string NameSuffix
		{
			get { return _nameSuffix; }
			set
			{
				_nameSuffix = value;
				this.RaisePropertyChanged(NameSuffixProperty);
			}
		}



		public const string IsSaveButtonPanelVisibleProperty = "IsSaveButtonPanelVisible";
		private Visibility _isSaveButtonPanelVisible;

		public Visibility IsSaveButtonPanelVisible
		{
			get
			{
				return _isSaveButtonPanelVisible;
			}
			set
			{
				_isSaveButtonPanelVisible = value;
				this.RaisePropertyChanged(IsSaveButtonPanelVisibleProperty);
			}
		}

		public const string IsEditButtonPanelVisibleProperty = "IsEditButtonPanelVisible";
		private Visibility _isEditButtonPanelVisible;

		public Visibility IsEditButtonPanelVisible
		{
			get
			{
				return _isEditButtonPanelVisible;
			}
			set
			{
				_isEditButtonPanelVisible = value;
				this.RaisePropertyChanged(IsEditButtonPanelVisibleProperty);
			}
		}


		public const string NotifyLabelTextColorProperty = "NotifyLabelTextColor";
		private Color _notifyLabelTextColor;

		public Color NotifyLabelTextColor
		{
			get { return _notifyLabelTextColor; }
			set
			{
				_notifyLabelTextColor = value;
				this.RaisePropertyChanged(NotifyLabelTextColorProperty);
			}
		}

		public const string IsNotifyStackPanelVisibleProperty = "IsNotifyStackPanelVisible";
		private Visibility _isNotifyStackPanelVisible;

		public Visibility IsNotifyStackPanelVisible
		{
			get
			{
				return _isNotifyStackPanelVisible;
			}
			set
			{
				_isNotifyStackPanelVisible = value;
				this.RaisePropertyChanged(IsNotifyStackPanelVisibleProperty);
			}
		}

		public const string IsEmployeeNameEnabledProperty = "IsEmployeeNameEnabled";
		private bool _isEmployeeNameEnabled;

		public bool IsEmployeeNameEnabled
		{
			get
			{
				return _isEmployeeNameEnabled;
			}
			set
			{
				_isEmployeeNameEnabled = value;
				this.RaisePropertyChanged(IsEmployeeNameEnabledProperty);
			}
		}


		public const string IsBusyProperty = "IsBusy";
		private bool _isBusy;

		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				_isBusy = value;
				this.RaisePropertyChanged(IsBusyProperty);
			}
		}



		public const string EmployeeListProperty = "EmployeeList";
		private ObservableCollection<Employee> _EmployeeList;

		public ObservableCollection<Employee> EmployeeList
		{
			get
			{
				return _EmployeeList;
			}
			set
			{
				_EmployeeList = value;
				this.RaisePropertyChanged(EmployeeListProperty);
			}
		}


		public const string SelectedListItemProperty = "SelectedListItem";
		private Employee _selectedListItem;

		public Employee SelectedListItem
		{
			get { return _selectedListItem; }
			set
			{
				_selectedListItem = value;
				//				this.SelectedRegionItem.PK_Region = this.SelectedListItem.FK_Region;
				this.RaisePropertyChanged(SelectedListItemProperty);
			}
		}




		public const string EmployeeNameProperty = "EmployeeName";
		private string _EmployeeName;

		public string EmployeeName
		{
			get
			{
				return _EmployeeName;
			}
			set
			{
				_EmployeeName = value;
				this.RaisePropertyChanged(EmployeeNameProperty);
				this.IsDirty = true;
				this.SaveEmployeeCommand.RaiseCanExecuteChanged();
			}
		}

		public const string IsDirtyTextProperty = "IsDirty";
		private bool _isDirty;

		public bool IsDirty
		{
			get
			{
				return _isDirty;
			}
			set
			{
				_isDirty = value;
				SaveEmployeeCommand.RaiseCanExecuteChanged();
				this.RaisePropertyChanged(IsDirtyTextProperty);
			}
		}
		#endregion


		//
		public EmployeeEditViewModel(IDataService<Employee> es, ILocalStorageService ls)
		{
			_es = es;
			_ls = ls;

			this.isInitializing = true;

			Messenger.Default.Register<EmployeeNameChangedMessage>(this, this.HandleEmployeeNameChangedMessage);
			Messenger.Default.Register<ContentPresenterChangedMessage>(this, this.HandleContentPresenterChangedMessage);
			Messenger.Default.Register<ListItemChangedMessage>(this, this.HandleListItemChangedMessage);
			Messenger.Default.Register<AdminDataCloseMessage>(this, this.HandleAdminDataCloseMessage);

			this.AddEmployeeCommand = new RelayCommand(this.ExecuteAddEmployeeCommand, this.CanExecuteAddEmployeeCommand);
			this.EditEmployeeCommand = new RelayCommand(this.ExecuteEditEmployeeCommand, this.CanExecuteEditEmployeeCommand);
			this.DeleteEmployeeCommand = new RelayCommand(this.ExecuteDeleteEmployeeCommand, this.CanExecuteDeleteEmployeeCommand);
			this.SaveEmployeeCommand = new RelayCommand(this.ExecuteSaveEmployeeCommand, this.CanExecuteSaveEmployeeCommand);
			this.CancelEmployeeCommand = new RelayCommand(this.ExecuteCancelEmployeeCommand, this.CanExecuteCancelEmployeeCommand);
			this.NotificationMessageViewedCommand = new RelayCommand(this.ExecuteNotificationMessageViewedCommand, thisCanExecuteNotificationMessageViewedCommand);

			this.isInitializing = false;
			this.ShowEditButtons(true);
			this.IsFormEnabled = true;
		}

		private void HandleAdminDataCloseMessage(AdminDataCloseMessage m)
		{
			if (this.IsDirty)
			{
				Helpers.Helpers.Notify(
					"Employee",
					NotifyButtonEnum.ThreeButton,
					new List<NotifyButtonLabelEnum> { { NotifyButtonLabelEnum.Yes }, { NotifyButtonLabelEnum.No }, { NotifyButtonLabelEnum.Cancel } },
					"You have unsaved changes.  Save now?",
					false);

			}
		}

		private void EnableEditControls(bool isEnabled = false)
		{
			this.IsEmployeeNameEnabled = isEnabled;
		}

		private void ShowEditButtons(bool show)
		{
			this.IsEditButtonPanelVisible = show ? Visibility.Visible : Visibility.Hidden;
			this.IsSaveButtonPanelVisible = show ? Visibility.Hidden : Visibility.Visible;
		}


		private ObservableCollection<Employee> GetEmployeeListAsync()
		{
			var task = _es.GetTableList(HttpRequestMethods.Get, "api/Employee/");
			return task;
		}


		#region Message handlers
		private void HandleContentPresenterChangedMessage(ContentPresenterChangedMessage obj)
		{
			if (obj.Action.Contains("Employee"))
			{
				this.IsBusy = true;

				ObservableCollection<Employee> disTask = this.GetEmployeeListAsync();
				this.EmployeeList = disTask;

				if (this.EmployeeList.IsNotNull() && this.EmployeeList.Count > 0)
				{
					this.SelectedListItem = this.EmployeeList[0];
				}

				this.IsBusy = false;
			}
		}

		private void HandleListItemChangedMessage(ListItemChangedMessage obj)
		{
			if (this.SelectedListItem != null)
			{
			}
		}

		private void HandleEmployeeNameChangedMessage(EmployeeNameChangedMessage nc)
		{
			if ((nc.Action.Trim()).IsNullOrEmpty())
			{
				this.IsDirty = true;
				this.SaveEmployeeCommand.RaiseCanExecuteChanged();
				this.ValidateName();
			}
		}

		private void ValidateName()
		{
			//show error on ui
		}


		#endregion

		#region Can/Execute

		private void ExecuteCancelEmployeeCommand()
		{
		}


		private bool CanExecuteCancelEmployeeCommand()
		{
			return true;
		}

		private void ExecuteDeleteEmployeeCommand()
		{
			var resp = _es.UpdateTable(SelectedListItem, HttpRequestMethods.Delete, "api/Employee/", null);
			this.ShowEditButtons(false);

		}

		private bool CanExecuteDeleteEmployeeCommand()
		{
			var retVal = true;
			if (this.EmployeeList != null && this.EmployeeList.Count() > 0)
			{
				retVal = true;
			}

			return retVal;
		}

		private bool CanExecuteSaveEmployeeCommand()
		{
			return true;

			//var retVal = this.IsDirty &&   
			//	this.SelectedListItem.EmployeeName.IsNotNull() && 
			//	this.SelectedListItem.EmployeeName != "New Employee" && 
			//	this.SelectedRegionItem.RegionName.IsNotNull();

			//return retVal;
		}

		private void ExecuteSaveEmployeeCommand()
		{
			this.SaveEmployee();
		}

		private bool CanExecuteAddEmployeeCommand()
		{
			return !this.isNew;

		}

		private void ExecuteAddEmployeeCommand()
		{
			this.isNew = true;
			this.IsDirty = true;

			var item = new Employee();
			this.EmployeeList.Add(item);
			this.SelectedListItem = item;

			this.isNew = false;
			this.ShowEditButtons(false);

		}

		private bool thisCanExecuteNotificationMessageViewedCommand()
		{
			return true;
		}

		private void ExecuteNotificationMessageViewedCommand()
		{
			this.ShowEditButtons(false);
			this.EnableEditControls(false);
		}


		private bool CanExecuteEditEmployeeCommand()
		{
			return true;
		}

		private void ExecuteEditEmployeeCommand()
		{
			this.EnableEditControls(true);
			this.ShowEditButtons(false);

		}

		#endregion

		public void SaveEmployee(bool skipNotify = false)
		{
			var isError = false;
			var message = "Changes successfully sent to database.";

			try
			{
				var resp = _es.UpdateTable(this.SelectedListItem, this.isNew ? HttpRequestMethods.Post : HttpRequestMethods.Put, "api/Employee/", this.SelectedListItem.PK_Employee);
			}
			catch (Exception e)
			{
				isError = true;
				message = e.Message;
			}
			this.isNew = false;

			if (!skipNotify)
			{
				Helpers.Helpers.Notify("Employee", NotifyButtonEnum.OneButton, new List<NotifyButtonLabelEnum> { NotifyButtonLabelEnum.Ok }, message, isError);
			}

			this.IsDirty = false;
			this.ShowEditButtons(true);
			this.EnableEditControls(false);
		}


	}
}
