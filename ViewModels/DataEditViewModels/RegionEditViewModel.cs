using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Services;
using System.Collections.Generic;
using System.Linq;

namespace MVIOperationsSystem.ViewModels.DataEditViewModels
{
	public class RegionEditViewModel : ViewModelBase
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



		public const string RegionListProperty = "RegionList";
		private List<Region> _RegionList;

		public List<Region> RegionList
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


		public const string RegionNameProperty = "RegionName";
		private string _RegionName;

		public string RegionName
		{
			get
			{
				return _RegionName;
			}
			set
			{
				_RegionName = value;
				this.RaisePropertyChanged(RegionNameProperty);
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



		public RegionEditViewModel(ILoginDataService dataService)
		{

			_dataService = dataService;
			//Messenger.Default.Register<PasswordMessage>(this, HandlePasswordMessage);
			this.AddCommand = new RelayCommand(this.ExecuteAddRegionCommand, this.CanExecuteAddRegionCommand);
			this.DeleteCommand = new RelayCommand(this.ExecuteDeleteRegionCommand, this.CanExecuteDeleteRegionCommand);
			this.SaveCommand = new RelayCommand(this.ExecuteSaveRegionCommand, this.CanExecuteSaveRegionCommand);
			this.CancelCommand = new RelayCommand(this.ExecuteCancelRegionCommand, this.CanExecuteCancelRegionCommand);

			var dist = new Region() { RegionName = "PA Region"};
			this.RegionList = new List<Region>();
			this.RegionList.Add(dist);
			//this.IsAddButtonEnabled = true;
			//this.IsDeleteButtonEnabled = false;
			//this.IsSaveButtonEnabled = false;
		}


		#region Can/Execute

		private void ExecuteCancelRegionCommand()
		{

		}


		private bool CanExecuteCancelRegionCommand()
		{
			return true;
		}

		private void ExecuteDeleteRegionCommand()
		{

		}

		private bool CanExecuteDeleteRegionCommand()
		{
			var retVal = true;
			if (this.RegionList != null && this.RegionList.Count() > 0)
			{
				retVal = true;
			}

			return retVal;
		}

		private bool CanExecuteSaveRegionCommand()
		{

			return this.IsDirty;

		}

		private void ExecuteSaveRegionCommand()
		{

		}

		private bool CanExecuteAddRegionCommand()
		{
			return true;

		}

		private void ExecuteAddRegionCommand()
		{

		}
		#endregion

	}
}
