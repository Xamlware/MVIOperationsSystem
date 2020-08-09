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
	public class DistrictEditViewModel : ViewModelBase
	{
		private readonly ILoginDataService _dataService;
		private readonly LocalStorageService _ls = new LocalStorageService();

		#region Commands
		public RelayCommand AddCommand { get; private set; }
		public RelayCommand DeleteCommand { get; private set; }
		public RelayCommand SaveCommand { get; private set; }
		public RelayCommand	CancelCommand { get; private set; }

		#endregion

		#region Properties



		public const string DistrictListProperty = "DistrictList";
		private List<District> _districtList;

		public List<District> DistrictList
		{
			get
			{
				return _districtList;
			}
			set
			{
				_districtList = value; 
				this.RaisePropertyChanged(DistrictListProperty);
			}
		}


		public const string DistrictNameProperty = "DistrictName";
		private string _districtName;

		public string DistrictName
		{
			get
			{
				return _districtName;
			}
			set
			{
				_districtName = value;
				this.RaisePropertyChanged(DistrictNameProperty);
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



		public DistrictEditViewModel(ILoginDataService dataService)
		{

			_dataService = dataService;
			//Messenger.Default.Register<PasswordMessage>(this, HandlePasswordMessage);
			this.AddCommand = new RelayCommand(this.ExecuteAddDistrictCommand, this.CanExecuteAddDistrictCommand);
			this.DeleteCommand = new RelayCommand(this.ExecuteDeleteDistrictCommand, this.CanExecuteDeleteDistrictCommand);
			this.SaveCommand = new RelayCommand(this.ExecuteSaveDistrictCommand, this.CanExecuteSaveDistrictCommand);
			this.CancelCommand = new RelayCommand(this.ExecuteCancelDistrictCommand, this.CanExecuteCancelDistrictCommand);

			var dist = new District() { DistrictName = "GapDistrict", FK_Region = 1 };
			this.DistrictList = new List<District>();
			this.DistrictList.Add(dist);
			//this.IsAddButtonEnabled = true;
			//this.IsDeleteButtonEnabled = false;
			//this.IsSaveButtonEnabled = false;
		}

	
		#region Can/Execute
	
		private void ExecuteCancelDistrictCommand()
		{
		
		}


		private bool CanExecuteCancelDistrictCommand()
		{
			return true;
		}

		private void ExecuteDeleteDistrictCommand()
		{
			
		}

		private bool CanExecuteDeleteDistrictCommand()
		{
			var retVal = true;
			if (this.DistrictList != null && this.DistrictList.Count() > 0)
			{
				retVal = true;
			}

			return retVal;
		}

		private bool CanExecuteSaveDistrictCommand()
		{
			
			return this.IsDirty;

		}

		private void ExecuteSaveDistrictCommand()
		{
			
		}

		private bool CanExecuteAddDistrictCommand()
		{
			return true;
		
		}

		private void ExecuteAddDistrictCommand()
		{
			
		}
		#endregion

	}
}
