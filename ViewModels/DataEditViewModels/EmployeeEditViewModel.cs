using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVIOperations.Models;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Services;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Documents;
using Xamlware.Framework.Extensions;

namespace MVIOperationsSystem.ViewModels.DataEditViewModels
{
	public class EmployeeEditViewModel : ViewModelBase
	{
		private readonly ILoginDataService _dataService;
		private readonly LocalStorageService _ls = new LocalStorageService();

		#region Commands
		public RelayCommand AddCommand { get; private set; }
		public RelayCommand DeleteCommand { get; private set; }
		public RelayCommand SaveCommand { get; private set; }
		public RelayCommand CancelCommand { get; private set; }

		#endregion

		#region Properties



		public const string EmployeeListProperty = "EmployeeList";
		private List<Employee> _EmployeeList;

		public List<Employee> EmployeeList
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
			}
		}


		public const string RegionProperty = "Region";
		private string _region;

		public string Region
		{
			get { return _region; }
			set
			{
				_region = value;
				this.RaisePropertyChanged(RegionProperty);
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
				this.RaisePropertyChanged(IsDirtyTextProperty);
			}
		}

		//public const string IsAddButtonEnabledTextProperty = "IsAddButtonEnabled";
		//private bool _isAddButtonEnabled;

		//public bool IsAddButtonEnabled
		//{
		//	get
		//	{
		//		return _isAddButtonEnabled;
		//	}
		//	set
		//	{
		//		_isAddButtonEnabled = value;
		//		this.RaisePropertyChanged(IsAddButtonEnabledTextProperty);
		//	}
		//}


		//public const string IsDeleteButtonEnabledTextProperty = "IsDeleteButtonEnabled";
		//private bool _isDeleteButtonEnabled;

		//public bool IsDeleteButtonEnabled
		//{
		//	get
		//	{
		//		return _isDeleteButtonEnabled;
		//	}
		//	set
		//	{
		//		_isDeleteButtonEnabled = value;
		//		this.RaisePropertyChanged(IsDeleteButtonEnabledTextProperty);
		//	}
		//}



		//public const string IsSaveButtonEnabledTextProperty = "IsSaveButtonEnabled";
		//private bool _isSaveButtonEnabled;

		//public bool IsSaveButtonEnabled
		//{
		//	get
		//	{
		//		return _isSaveButtonEnabled;
		//	}
		//	set
		//	{
		//		_isSaveButtonEnabled = value;
		//		this.RaisePropertyChanged(IsSaveButtonEnabledTextProperty);
		//	}
		//}

		#endregion



		public EmployeeEditViewModel(ILoginDataService dataService)
		{

			_dataService = dataService;
			//Messenger.Default.Register<PasswordMessage>(this, HandlePasswordMessage);
			this.AddCommand = new RelayCommand(this.ExecuteAddEmployeeCommand, this.CanExecuteAddEmployeeCommand);
			this.DeleteCommand = new RelayCommand(this.ExecuteDeleteEmployeeCommand, this.CanExecuteDeleteEmployeeCommand);
			this.SaveCommand = new RelayCommand(this.ExecuteSaveEmployeeCommand, this.CanExecuteSaveEmployeeCommand);
			this.CancelCommand = new RelayCommand(this.ExecuteCancelEmployeeCommand, this.CanExecuteCancelEmployeeCommand);

			var dist = new Employee() { EmployeeName = "Employee 1" };
			this.EmployeeList = new List<Employee>();
			this.EmployeeList.Add(dist);
			//this.IsAddButtonEnabled = true;
			//this.IsDeleteButtonEnabled = false;
			//this.IsSaveButtonEnabled = false;
		}


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

			return this.IsDirty;

		}

		private void ExecuteSaveEmployeeCommand()
		{

		}

		private bool CanExecuteAddEmployeeCommand()
		{
			return true;

		}

		private void ExecuteAddEmployeeCommand()
		{

		}
		#endregion

	}
}
