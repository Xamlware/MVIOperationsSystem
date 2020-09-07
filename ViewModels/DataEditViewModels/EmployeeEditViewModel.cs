using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
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
using Region = MVIOperationsSystem.Models.Region;

namespace MVIOperationsSystem.ViewModels.DataEditViewModels
{
	public class EmployeeEditViewModel : MVIViewModelBase
	{
		private readonly IDataService<Employee> _ds;
		private readonly IDataService<Region> _rs;
		private readonly ILocalStorageService _ls;
		private MainViewModel _vm;
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

		private Employee PreEditValues = new Employee();
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

		public const string IsRegionComboEnabledProperty = "IsRegionComboEnabled";
		private bool _isRegionComboEnabled;

		public bool IsRegionComboEnabled
		{
			get
			{
				return _isRegionComboEnabled;
			}
			set
			{
				_isRegionComboEnabled = value;
				this.RaisePropertyChanged(IsRegionComboEnabledProperty);
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

		public const string RegionListProperty = "RegionList";
		private ObservableCollection<Region> _RegionList;

		public ObservableCollection<Region> RegionList
		{
			get
			{
				return _RegionList;
			}
			set
			{
				_RegionList = value;
				this.RaisePropertyChanged(RegionListProperty);
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
				this.RaisePropertyChanged(SelectedListItemProperty);
			}
		}



		public const string SelectedRegionItemProperty = "SelectedRegionItem";
		private Region _selectedRegionItem;

		public Region SelectedRegionItem
		{
			get { return _selectedRegionItem; }
			set
			{
				_selectedRegionItem = value;
				this.RaisePropertyChanged(SelectedRegionItemProperty);
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
				var oldName = _EmployeeName;
				_EmployeeName = value;
				//if (!this.isInitializing && oldName != _EmployeeName)
				//{
				//	//this.IsDirty = true;
				//}
				this.RaisePropertyChanged(EmployeeNameProperty);
				this.SaveEmployeeCommand.RaiseCanExecuteChanged();
			}
		}


		public const string RegionProperty = "Region";
		private string _region;

		public string Region
		{
			get { return _region; }
			set
			{
				var oldName = _region;
				_region = value;
				//if (!this.isInitializing && oldName != _region)
				//{
				//	//this.IsDirty = true;
				//}

				this.RaisePropertyChanged(RegionProperty);
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
				if (this.IsDirty)
				{
					var d = _vm.DirtyViews.FirstOrDefault(f => f.Key == "Employee").Key == "Employee";
					if (d.IsFalse())
					{
						_vm.DirtyViews.Add("Employee", "");
					}
				}
				else
				{
					_vm.DirtyViews.Remove("Employee");
				}
				SaveEmployeeCommand.RaiseCanExecuteChanged();
				this.RaisePropertyChanged(IsDirtyTextProperty);
			}
		}

		public const string IsEditingTextProperty = "IsEditing";
		private bool _isEditing;

		public bool IsEditing
		{
			get
			{
				return _isEditing;
			}
			set
			{
				_isEditing = value;
				SaveEmployeeCommand.RaiseCanExecuteChanged();
				this.RaisePropertyChanged(IsEditingTextProperty);
			}
		}
		#endregion


		//
		public EmployeeEditViewModel(IDataService<Employee> ds, IDataService<Region> rs, ILocalStorageService ls, MainViewModel vm)
		{
			_vm = vm;
			_ds = ds;
			_rs = rs;
			_ls = ls;
			this.isInitializing = true;
			_vm.ActiveViewModels.Add(this.GetType(), "Employee");

			Messenger.Default.Register<EmployeeNameChangedMessage>(this, this.HandleEmployeeNameChangedMessage);
			Messenger.Default.Register<ContentPresenterChangedMessage>(this, this.HandleContentPresenterChangedMessage);
			Messenger.Default.Register<AdminDataCloseMessage>(this, this.HandleAdminDataCloseMessage);
			Messenger.Default.Register<NotifyResultMessage>(this, this.HandleNotifyResultMessage);


			this.AddEmployeeCommand = new RelayCommand(this.ExecuteAddEmployeeCommand, this.CanExecuteAddEmployeeCommand);
			this.EditEmployeeCommand = new RelayCommand(this.ExecuteEditEmployeeCommand, this.CanExecuteEditEmployeeCommand);
			this.DeleteEmployeeCommand = new RelayCommand(this.ExecuteDeleteEmployeeCommand, this.CanExecuteDeleteEmployeeCommand);
			this.SaveEmployeeCommand = new RelayCommand(this.ExecuteSaveEmployeeCommand, this.CanExecuteSaveEmployeeCommand);
			this.CancelEmployeeCommand = new RelayCommand(this.ExecuteCancelEmployeeCommand, this.CanExecuteCancelEmployeeCommand);
			this.NotificationMessageViewedCommand = new RelayCommand(this.ExecuteNotificationMessageViewedCommand, thisCanExecuteNotificationMessageViewedCommand);

			this.ShowEditButtons(true);
			this.GetDataListsAsync();

			#region CreateManualLists
			//var dist = new Employee() { PK_Employee=1, EmployeeName = "Gap Employee", FK_Region = 2 };
			//this.EmployeeList = new ObservableCollection<Employee>
			//{
			//	dist
			//};

			//if (this.EmployeeList.Count > 0)
			//{ 
			//	this.SelectedListItem = this.EmployeeList[0];
			//}

			//var reg = new Region() { PK_Region=2, RegionName = "Pennsylvania Region" };
			//this.RegionList = new ObservableCollection<Region> { reg };
			//reg = new Region() { PK_Region=1, RegionName = "Alabama Region" };
			//this.RegionList.Add(reg);
			//if (this.RegionList.Count > 0)
			//{
			//	var item = this.RegionList.Where(w => w.PK_Region == dist.FK_Region).FirstOrDefault();
			//	if(item != null)
			//	{
			//		this.SelectedRegionItem = item;
			//	}
			//}
			#endregion
		}

		public override void Cleanup()
		{
			SimpleIoc.Default.Unregister<EmployeeEditViewModel>();
			SimpleIoc.Default.Register<EmployeeEditViewModel>();
			base.Cleanup();
		}

		public void SaveEmployee(bool skipNotify = false)
		{
			var isError = false;
			var message = "Changes successfully sent to database.";
			try
			{
				var resp = _ds.UpdateTable(this.SelectedListItem, this.isNew ? HttpRequestMethods.Post : HttpRequestMethods.Put, "api/Employee/", this.SelectedListItem.PK_Employee);
			}
			catch (Exception e)
			{
				isError = true;
				message = e.Message;
			}
			this.isNew = false;

			if (!skipNotify)
			{
				Helpers.Helpers.Notify(
					"Employee",
					NotifyButtonEnum.OneButton,
					new List<NotifyButtonLabelEnum> { NotifyButtonLabelEnum.Ok },
					message,
					isError,
					"EmployeeSave");
			}

			this.IsDirty = false;
			this.ShowEditButtons(true);
			this.EnableEditControls(false);
		}

		public void NotifyUserIsDirty(string origin = null)
		{
			Helpers.Helpers.Notify(
					"Employee",
					NotifyButtonEnum.ThreeButton,
					new List<NotifyButtonLabelEnum> { { NotifyButtonLabelEnum.Yes }, { NotifyButtonLabelEnum.No }, { NotifyButtonLabelEnum.Cancel } },
					"You have unsaved changes.  Save now?",
					false,
					origin);


		}

		private void EnableEditControls(bool isEnabled = false)
		{
			this.IsEmployeeNameEnabled = isEnabled;
			this.IsRegionComboEnabled = isEnabled;
		}

		private void ShowEditButtons(bool show)
		{
			this.IsEditButtonPanelVisible = show ? Visibility.Visible : Visibility.Hidden;
			this.IsSaveButtonPanelVisible = show ? Visibility.Hidden : Visibility.Visible;

			this.IsEditing = (this.IsEditButtonPanelVisible == Visibility.Hidden);
			Messenger.Default.Send(new EnableAdminTreePanelMessage { Action = (this.IsEditButtonPanelVisible == Visibility.Visible) });
		}

		private ObservableCollection<Region> GetRegionListAsync()
		{
			var task = _rs.GetTableList(HttpRequestMethods.Get, "api/region/");
			return task;
		}

		private ObservableCollection<Employee> GetEmployeeListAsync()
		{
			var task = _ds.GetTableList(HttpRequestMethods.Get, "api/Employee/");
			return task;
		}

		private void GetDataListsAsync()
		{
			this.IsBusy = true;
			ObservableCollection<Region> regTask = this.GetRegionListAsync();
			this.RegionList = regTask;

			ObservableCollection<Employee> disTask = this.GetEmployeeListAsync();
			this.EmployeeList = disTask;

			if (this.EmployeeList.IsNotNull() && this.EmployeeList.Count > 0)
			{
				this.SelectedListItem = this.EmployeeList[0];
			}

			this.IsBusy = false;
		}

		#region Message handlers
		private void HandleNotifyResultMessage(NotifyResultMessage nrm)
		{
			switch (nrm.ButtonSelected)
			{
				case NotifyButtonLabelEnum.No:

					this.IsDirty = false;

					if (nrm.Origin == "Main")
					{
					}
					else if (nrm.Origin == "Admin")
					{
						Messenger.Default.Send(new CleanUpMessage());
						Messenger.Default.Send(new NavigationMessage { Action = "Close" });
					}
					else
					{
						this.RaisePropertyChanged(SelectedListItemProperty);

						this.ShowEditButtons(true);
						this.EnableEditControls(false);
					}

					break;
				case NotifyButtonLabelEnum.Cancel:
					//this.ShowEditButtons(true);
					//this.EnableEditControls(false);
					break;
			}
		}

		private void HandleAdminDataCloseMessage(AdminDataCloseMessage m)
		{
			if (this.IsDirty)
			{
				this.NotifyUserIsDirty("Admin");
			}
			else
			{
				Messenger.Default.Send(new CleanUpMessage());
				Messenger.Default.Send(new NavigationMessage { Action = "Close" });
			}

		}

		private void HandleContentPresenterChangedMessage(ContentPresenterChangedMessage obj)
		{
			if (obj.Action.Contains("Employee"))
			{
				this.IsBusy = true;
				ObservableCollection<Region> regTask = this.GetRegionListAsync();
				this.RegionList = regTask;

				ObservableCollection<Employee> disTask = this.GetEmployeeListAsync();
				this.EmployeeList = disTask;

				if (this.EmployeeList.IsNotNull() && this.EmployeeList.Count > 0)
				{
					this.SelectedListItem = this.EmployeeList[0];
				}

				this.IsBusy = false;
			}
		}

		
		private void HandleEmployeeNameChangedMessage(EmployeeNameChangedMessage dm)
		{
			if (!this.isInitializing && this.IsEditing && this.SelectedListItem != null)
			{
				this.IsDirty = true;
				Console.WriteLine("is dirty = true;");
			}

			this.SaveEmployeeCommand.RaiseCanExecuteChanged();
		}

		#endregion

		#region Can/Execute

		private void ExecuteCancelEmployeeCommand()
		{
			if (this.IsDirty)
			{
				this.NotifyUserIsDirty("Employee");
			}

			this.ShowEditButtons(true);
			this.EnableEditControls(false);

		}


		private bool CanExecuteCancelEmployeeCommand()
		{
			return true;
		}

		private void ExecuteDeleteEmployeeCommand()
		{
			//var index = this.SelectedListItem.PK_Employee;
			var resp = _ds.UpdateTable(SelectedListItem, HttpRequestMethods.Delete, "api/Employee/", this.SelectedListItem.PK_Employee);
			this.EmployeeList.Remove(this.SelectedListItem);
			if (this.EmployeeList.Count > 0)
			{
				this.SelectedListItem = this.EmployeeList[0];
			}
			this.ShowEditButtons(false);
			//this.EnableEditControls(true);

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
			Messenger.Default.Send(new UpdateSourceEmployeeMessage());
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

			var item = new Employee { EmployeeName = "New Employee" };
			this.EmployeeList.Add(item);
			this.SelectedListItem = item;
			this.SelectedRegionItem = null;

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

	}
}
