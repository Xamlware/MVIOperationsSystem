using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVIOperations.Models;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Services;
using MVIOperationsSystem.Views.DataEditViews;
using System.Collections.ObjectModel;
using System.Linq;
using Xamlware.Framework.Extensions;

namespace MVIOperationsSystem.ViewModels.DataEditViewModels
{
	public class DistrictEditViewModel : ViewModelBase
	{
		IDataService<District> _districtDataService ;
		private readonly IDataService<Region> _regionDataService;
		private readonly LocalStorageService _ls = new LocalStorageService();
		private bool isInitializing = false;
		private bool isNew = false;

		#region Commands
		public RelayCommand AddDistrictCommand { get; private set; }
		public RelayCommand DeleteDistrictCommand { get; private set; }
		public RelayCommand SaveDistrictCommand { get; private set; }
		public RelayCommand	CancelDistrictCommand { get; private set; }

		#endregion

		#region Properties



		public const string DistrictListProperty = "DistrictList";
		private ObservableCollection<District> _districtList;

		public ObservableCollection<District> DistrictList
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
		private District _selectedListItem;

		public District SelectedListItem
		{
			get { return _selectedListItem; }
			set { 
				_selectedListItem = value;
//				this.SelectedRegionItem.PK_Region = this.SelectedListItem.FK_Region;
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



		public DistrictEditViewModel(IDataService<District> districtDataService, IDataService<Region> regionDataService)
		{
			_districtDataService = districtDataService;
			_regionDataService = regionDataService;
			this.isInitializing = true;

			Messenger.Default.Register<DistrictNameChangedMessage>(this, this.HandleDistrictNameChangedMessage);
			
			Messenger.Default.Register<ListItemChangedMessage>(this, this.HandleListItemChangedMessage);
			Messenger.Default.Register<RegionComboChangedMessage>(this, this.HandleRegionComboChangedMessage);
			this.AddDistrictCommand = new RelayCommand(this.ExecuteAddDistrictCommand, this.CanExecuteAddDistrictCommand);
			this.DeleteDistrictCommand = new RelayCommand(this.ExecuteDeleteDistrictCommand, this.CanExecuteDeleteDistrictCommand);
			this.SaveDistrictCommand = new RelayCommand(this.ExecuteSaveDistrictCommand, this.CanExecuteSaveDistrictCommand);
			this.CancelDistrictCommand = new RelayCommand(this.ExecuteCancelDistrictCommand, this.CanExecuteCancelDistrictCommand);

			//this.DistrictList = new ObservableCollection<District>();
			this.GetRegionListAsync();
			this.GetDistrictListAsync();
		
				//ObservableCollection<MyType> obsCollection = new ObservableCollection<MyType>(myList);
	
				#region CreateManualLists
			//var dist = new District() { PK_District=1, DistrictName = "Gap District", FK_Region = 2 };
			//this.DistrictList = new ObservableCollection<District>
			//{
			//	dist
			//};

			//if (this.DistrictList.Count > 0)
			//{ 
			//	this.SelectedListItem = this.DistrictList[0];
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


			this.isInitializing = false;
		}

		private void HandleListItemChangedMessage(ListItemChangedMessage obj)
		{
			this.SelectedRegionItem = this.RegionList.Where(w=>w.PK_Region == this.SelectedListItem.FK_Region).FirstOrDefault();
		}

		private void GetRegionList()
		{
			this.RegionList =  _regionDataService.GetTableList(HttpRequestMethods.Get, "api/route");
		}

		private void GetDistrictList()
		{
			this.DistrictList = await _districtDataService.GetDistrictList(HttpRequestMethods.Get);
			if (this.DistrictList.Count > 0)
			{
				this.SelectedListItem = this.DistrictList[0];
			}
		}


		private void HandleRegionComboChangedMessage(RegionComboChangedMessage obj)
		{
			if (!this.isInitializing && this.SelectedRegionItem != null)
			{
				this.SelectedListItem.FK_Region = this.SelectedRegionItem.PK_Region;
			}

			this.IsDirty = true;
			this.CanExecuteSaveDistrictCommand();
		}

		private void HandleDistrictNameChangedMessage(DistrictNameChangedMessage obj)
		{
			this.IsDirty = true;
			this.CanExecuteSaveDistrictCommand();
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
			//	var resp =_districtDataService.UpdateDistrict(SelectedListItem, HttpRequestMethods.Delete);

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

			var retVal = this.IsDirty &&   
				this.SelectedListItem.DistrictName.IsNotNull() && 
				this.SelectedListItem.DistrictName != "New District" && 
				this.SelectedRegionItem.RegionName.IsNotNull();
	
			return retVal;
		}

		private void ExecuteSaveDistrictCommand()
		{
			//wire to database
			var resp = _districtDataService.UpdateDistrict(this.SelectedListItem, this.isNew ? HttpRequestMethods.Post : HttpRequestMethods.Put);
			this.isNew = false;
		}

		private bool CanExecuteAddDistrictCommand()
		{
			return !this.isNew;
		
		}

		private void ExecuteAddDistrictCommand()
		{
			this.isNew = true;
			this.IsDirty = true;

			var item = new District { DistrictName = "New District" };
			this.DistrictList.Add(item);
			this.SelectedListItem = item;
			this.SelectedRegionItem = null;
			
		}
		#endregion

	}
}
