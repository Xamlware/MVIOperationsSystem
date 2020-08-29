using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVIOperations.Models;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
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
		private readonly IDataService<State> _ss;
		private readonly IDataService<Country> _cs;
		private readonly IDataService<Gender> _gs;
		private readonly IDataService<Race> _rs;
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

		public const string StateListProperty = "StateList";
		private ObservableCollection<State> _stateList;

		public ObservableCollection<State> StateList
		{
			get { return _stateList; }
			set { 
				_stateList = value;
				this.RaisePropertyChanged(StateListProperty);
			}
		}

		public const string CountryListProperty = "CountryList";
		private ObservableCollection<Country> _countryList;

		public ObservableCollection<Country> CountryList
		{
			get { return _countryList; }
			set
			{
				_countryList = value;
				this.RaisePropertyChanged(CountryListProperty);
			}
		}

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

		//public const string FirstNameProperty = "FirstName";
		//private string _firstName;

		//public string FirstName
		//{
		//	get { return _firstName; }
		//	set { 
		//		_firstName = value;
		//		this.RaisePropertyChanged(FirstNameProperty);
		//		this.BuildEmployeeName();
		//	}
		//}

		//public const string MiddleNameProperty = "MiddleName";
		//private string _middleName;

		//public string MiddleName
		//{
		//	get { return _middleName; }
		//	set
		//	{
		//		_middleName = value;
		//		this.RaisePropertyChanged(MiddleNameProperty);
		//		this.BuildEmployeeName();

		//	}
		//}

		//public const string LastNameProperty = "LastName";
		//private string _lastName;

		//public string LastName
		//{
		//	get { return _lastName; }
		//	set
		//	{
		//		_lastName = value;
		//		this.RaisePropertyChanged(LastNameProperty);
		//		this.BuildEmployeeName();
		//	}
		//}

		//public const string NameSuffixProperty = "NameSuffix";
		//private string _nameSuffix;

		//public string NameSuffix
		//{
		//	get { return _nameSuffix; }
		//	set
		//	{
		//		_nameSuffix = value;
		//		this.RaisePropertyChanged(NameSuffixProperty);
		//		this.BuildEmployeeName();
		//	}
		//}



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


		public const string EmployeeIdProperty = "EmployeeId";
		private ObservableCollection<Employee> _EmployeeId;

		public ObservableCollection<Employee> EmployeeId
		{
			get
			{
				return _EmployeeId;
			}
			set
			{
				_EmployeeId = value;
				this.RaisePropertyChanged(EmployeeIdProperty);
			}
		}

		public const string AspUserIdProperty = "AspUserId";
		private ObservableCollection<Employee> _aspUserId;

		public ObservableCollection<Employee> AspUserId
		{
			get
			{
				return _aspUserId;
			}
			set
			{
				_aspUserId = value;
				this.RaisePropertyChanged(AspUserIdProperty);
			}
		}

		public const string StoreProperty = "Store";
		private Store _store;

		public Store Store
		{
			get
			{
				return _store;
			}
			set
			{
				_store = value;
				this.RaisePropertyChanged(StoreProperty);
			}
		}

		public const string GenderProperty = "Gender";
		private Gender _gender;

		public Gender Gender
		{
			get
			{
				return _gender;
			}
			set
			{
				_gender = value;
				this.RaisePropertyChanged(GenderProperty);
			}
		}

		public const string RaceProperty = "Race";
		private Race _race;

		public Race Race
		{
			get
			{
				return _race;
			}
			set
			{
				_race = value;
				this.RaisePropertyChanged(RaceProperty);
			}
		}


		public const string SelectedEmployeeProperty = "SelectedEmployee";
		private Employee _selectedEmployee;

		public Employee SelectedEmployee
		{
			get { return _selectedEmployee; }
			set
			{
				_selectedEmployee = value;
				//				SelectedRegionItem.PK_Region = SelectedEmployee.FK_Region;
				RaisePropertyChanged(SelectedEmployeeProperty);
			}
		}


		public const string SelectedGenderProperty = "SelectedGender";
		private Gender _selectedGender;

		public Gender SelectedGender
		{
			get { return _selectedGender; }
			set
			{
				_selectedGender = value;
				//				SelectedRegionItem.PK_Region = SelectedGender.FK_Region;
				RaisePropertyChanged(SelectedGenderProperty);
			}
		}


		public const string SelectedRaceProperty = "SelectedRace";
		private Race _selectedRace;

		public Race SelectedRace
		{
			get { return _selectedRace; }
			set
			{
				_selectedRace = value;
				//				SelectedRegionItem.PK_Region = SelectedRace.FK_Region;
				RaisePropertyChanged(SelectedRaceProperty);
			}
		}



		public const string SelectedStateProperty = "SelectedState";
		private State _selectedState;

		public State SelectedState
		{
			get { return _selectedState; }
			set
			{
				_selectedState = value;
				//				SelectedRegionItem.PK_Region = SelectedState.FK_Region;
				RaisePropertyChanged(SelectedStateProperty);
			}
		}



		public const string SelectedCountryProperty = "SelectedCountry";
		private Country _selectedCountry;

		public Country SelectedCountry
		{
			get { return _selectedCountry; }
			set
			{
				_selectedCountry = value;
				//				SelectedRegionItem.PK_Region = SelectedCountry.FK_Region;
				RaisePropertyChanged(SelectedCountryProperty);
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

		public const string RaceListProperty = "RaceList";
		private ObservableCollection<Race> _raceList;

		public ObservableCollection<Race> RaceList
		{
			get
			{
				return _raceList;
			}
			set
			{
				_raceList = value;
				this.RaisePropertyChanged(RaceListProperty);
			}
		}

		public const string AddressProperty = "Address";
		private string _address;

		public string Address
		{
			get { return _address; }
			set { 
				_address = value;
				this.RaisePropertyChanged(AddressProperty);
			
			}
		}

		public const string Address1Property = "Address1";
		private string _address1;

		public string Address1
		{
			get { return _address1; }
			set
			{
				_address1 = value;
				this.RaisePropertyChanged(Address1Property);

			}
		}

		public const string CityProperty = "City";
		private string _city;

		public string City
		{
			get { return _city; }
			set
			{
				_city = value;
				this.RaisePropertyChanged(CityProperty);

			}
		}

		
		public const string StateProperty = "FK_State";
		private int _fk_state;

		public int FK_State
		{
			get { return _fk_state; }
			set
			{
				_fk_state = value;
				this.RaisePropertyChanged(StateProperty);

			}
		}


		public const string CountryProperty = "FK_Country";
		private int _fk_Country;

		public int FK_Country
		{
			get { return _fk_Country; }
			set
			{
				_fk_Country = value;
				this.RaisePropertyChanged(CountryProperty);

			}
		}

		public const string ZipCodeProperty = "Zipcode";
		private string _zipCode;

		public string ZipCode
		{
			get { return _zipCode; }
			set 
			{ 
				_zipCode = value;
				this.RaisePropertyChanged(ZipCodeProperty);
			}
		}

		public const string GenderListProperty = "GenderList";
		private ObservableCollection<Gender> _genderList;

		public ObservableCollection<Gender> GenderList
		{
			get
			{
				return _genderList;
			}
			set
			{
				_genderList = value;
				this.RaisePropertyChanged(GenderListProperty);
			}
		}

		#endregion


			//
		public EmployeeEditViewModel(IDataService<Employee> es, 
			IDataService<State> ss,
			IDataService<Country> cs,
			IDataService<Gender> gs,
			IDataService<Race> rs,
			ILocalStorageService ls)
		{
			_es = es;
			_ls = ls;
			_ss = ss;
			_cs = cs;
			_gs = gs;
			_rs = rs;

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


		private void BuildEmployeeName()
		{
			if (this.SelectedEmployee != null)
			{
				var first = this.SelectedEmployee.FirstName.IsNotNullOrEmpty() ? this.SelectedEmployee.FirstName.Trim() + " " : "";
				var middle = this.SelectedEmployee.MiddleName.IsNotNullOrEmpty() ? this.SelectedEmployee.MiddleName.Trim() + " " : "";
				var last = this.SelectedEmployee.LastName.IsNotNullOrEmpty() ? this.SelectedEmployee.LastName.Trim() + " " : " ";
				var suff = this.SelectedEmployee.NameSuffix.IsNotNullOrEmpty() ? this.SelectedEmployee.NameSuffix.Trim() : "";
				var name = first + middle + last + suff;
				this.EmployeeName = name;
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


		private void GetEmployeeListAsync()
		{
			var task = _es.GetTableList(HttpRequestMethods.Get, "api/Employee/");
			this.EmployeeList = task;

			if (this.EmployeeList.IsNotNull() && this.EmployeeList.Count > 0)
			{
				this.SelectedEmployee = this.EmployeeList[0];
			}

		}


		#region Message handlers
		private void HandleContentPresenterChangedMessage(ContentPresenterChangedMessage obj)
		{
			if (obj.Action.Contains("Employee"))
			{
				this.IsBusy = true;
				this.GetRaceListAsync();
				this.GetGenderListAsync();
				this.GetStateListAsync();
				this.CountryListAsync();
				this.GetEmployeeListAsync();
			
				this.IsBusy = false;
			}
		}

		private void GetGenderListAsync()
		{
			var task = _gs.GetTableList(HttpRequestMethods.Get, "api/Gender/");
			this.GenderList = task;

			if (this.GenderList.IsNotNull() && this.GenderList.Count > 0)
			{
				this.SelectedGender = this.GenderList[0];
			}


		}

		private void GetRaceListAsync()
		{
			var task = _rs.GetTableList(HttpRequestMethods.Get, "api/Race/");
			this.RaceList = task;

			if (this.RaceList.IsNotNull() && this.RaceList.Count > 0)
			{
				this.SelectedRace = this.RaceList[0];
			}

		}

		private void CountryListAsync()
		{
				var task = _cs.GetTableList(HttpRequestMethods.Get, "api/Country/");
				this.CountryList = task;

				if (this.CountryList.IsNotNull() && this.CountryList.Count > 0)
				{
					this.SelectedCountry = this.CountryList[0];
				}

			}

		private void GetStateListAsync()
		{
				var task = _ss.GetTableList(HttpRequestMethods.Get, "api/State/");
				this.StateList = task;
		}

		private void HandleListItemChangedMessage(ListItemChangedMessage obj)
		{
			if (this.SelectedEmployee != null)
			{
			}
		}

		private void HandleEmployeeNameChangedMessage(EmployeeNameChangedMessage nc)
		{
			if ((nc.Action.Trim()).IsNullOrEmpty())
			{
				this.IsDirty = true;
				this.SaveEmployeeCommand.RaiseCanExecuteChanged();
			}
			this.BuildEmployeeName();
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
			var resp = _es.UpdateTable(this.SelectedEmployee, HttpRequestMethods.Delete, "api/Employee/", null);
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
			//	this.SelectedEmployee.EmployeeName.IsNotNull() && 
			//	this.SelectedEmployee.EmployeeName != "New Employee" && 
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
			this.SelectedEmployee = item;

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
				var resp = _es.UpdateTable(this.SelectedEmployee, this.isNew ? HttpRequestMethods.Post : HttpRequestMethods.Put, "api/Employee/", this.SelectedEmployee.PK_Employee);
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
