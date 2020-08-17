using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVIOperations.Models;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Services;
using MVIOperationsSystem.Views.DataEditViews;
using MVIOperationsSystem.Helpers;
using Syncfusion.UI.Xaml.TreeGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Xamlware.Framework.Extensions;
using Region = MVIOperationsSystem.Models.Region;

namespace MVIOperationsSystem.ViewModels.DataEditViewModels
{
	public class DistrictEditViewModel : ViewModelBase
	{
		private readonly IDataService<District> _districtDataService;
		private readonly IDataService<Region> _regionDataService;
		private readonly LocalStorageService _ls = new LocalStorageService();
		private bool isInitializing = false;
		private bool isNew = false;

		#region Commands
		public RelayCommand AddDistrictCommand { get; private set; }
		public RelayCommand EditDistrictCommand { get; private set; }
		public RelayCommand DeleteDistrictCommand { get; private set; }
		public RelayCommand SaveDistrictCommand { get; private set; }
		public RelayCommand CancelDistrictCommand { get; private set; }
		public RelayCommand NotificationMessageViewedCommand { get; private set; }
		#endregion

		#region Properties

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


		public const string IsDistrictNameEnabledProperty = "IsDistrictNameEnabled";
		private bool _isDistrictNameEnabled;

		public bool IsDistrictNameEnabled
		{
			get
			{
				return _isDistrictNameEnabled;
			}
			set
			{
				_isDistrictNameEnabled = value;
				this.RaisePropertyChanged(IsDistrictNameEnabledProperty);
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
			set
			{
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
				this.IsDirty = true;
				this.SaveDistrictCommand.RaiseCanExecuteChanged();
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
				this.SaveDistrictCommand.RaiseCanExecuteChanged();
				this.IsDirty = true;
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
				SaveDistrictCommand.RaiseCanExecuteChanged();
				this.RaisePropertyChanged(IsDirtyTextProperty);
			}
		}
		#endregion


		//
		public DistrictEditViewModel(IDataService<District> districtDataService, IDataService<Region> regionDataService)
		{
			_districtDataService = districtDataService;
			_regionDataService = regionDataService;
			this.isInitializing = true;

			Messenger.Default.Register<DistrictNameChangedMessage>(this, this.HandleDistrictNameChangedMessage);
			Messenger.Default.Register<ContentPresenterChangedMessage>(this, this.HandleContentPresenterChangedMessage);
			Messenger.Default.Register<ListItemChangedMessage>(this, this.HandleListItemChangedMessage);
			Messenger.Default.Register<RegionComboChangedMessage>(this, this.HandleRegionComboChangedMessage);
			Messenger.Default.Register<AdminDataCloseMessage>(this, this.HandleAdminDataCloseMessage);


			this.AddDistrictCommand = new RelayCommand(this.ExecuteAddDistrictCommand, this.CanExecuteAddDistrictCommand);
			this.EditDistrictCommand = new RelayCommand(this.ExecuteEditDistrictCommand, this.CanExecuteEditDistrictCommand);
			this.DeleteDistrictCommand = new RelayCommand(this.ExecuteDeleteDistrictCommand, this.CanExecuteDeleteDistrictCommand);
			this.SaveDistrictCommand = new RelayCommand(this.ExecuteSaveDistrictCommand, this.CanExecuteSaveDistrictCommand);
			this.CancelDistrictCommand = new RelayCommand(this.ExecuteCancelDistrictCommand, this.CanExecuteCancelDistrictCommand);
			this.NotificationMessageViewedCommand = new RelayCommand(this.ExecuteNotificationMessageViewedCommand, thisCanExecuteNotificationMessageViewedCommand);

			this.isInitializing = false;
			this.ShowEditButtons(true);

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
		}

		private void HandleAdminDataCloseMessage(AdminDataCloseMessage m)
		{
			if (this.IsDirty)
			{
				Helpers.Helpers.Notify(
					"District", 
					NotifyButtonEnum.ThreeButton,
					new List<NotifyButtonLabelEnum> { { NotifyButtonLabelEnum.Yes }, { NotifyButtonLabelEnum.No }, { NotifyButtonLabelEnum.Cancel } },
					"You have unsaved changes.  Save now?",
					false );

			}
		}

		private void EnableEditControls(bool isEnabled = false)
		{
			this.IsDistrictNameEnabled = isEnabled;
			this.IsRegionComboEnabled = isEnabled;
		}

		private void ShowEditButtons(bool show)
		{
			this.IsEditButtonPanelVisible = show ? Visibility.Visible : Visibility.Hidden;
			this.IsSaveButtonPanelVisible = show ? Visibility.Hidden : Visibility.Visible;
		}

		private ObservableCollection<Region> GetRegionListAsync()
		{
			var task = _regionDataService.GetTableList(HttpRequestMethods.Get, "api/region/");
			return task;
		}

		private ObservableCollection<District> GetDistrictListAsync()
		{
			var task = _districtDataService.GetTableList(HttpRequestMethods.Get, "api/district/");
			return task;
		}


		#region Message handlers
		private void HandleContentPresenterChangedMessage(ContentPresenterChangedMessage obj)
		{
			if (obj.Action.Contains("District"))
			{
				this.IsBusy = true;
				ObservableCollection<Region> regTask = this.GetRegionListAsync();
				this.RegionList = regTask;

				ObservableCollection<District> disTask = this.GetDistrictListAsync();
				this.DistrictList = disTask;

				if (this.DistrictList.IsNotNull() && this.DistrictList.Count > 0)
				{
					this.SelectedListItem = this.DistrictList[0];
				}

				this.IsBusy = false;
			}
		}

		private void HandleListItemChangedMessage(ListItemChangedMessage obj)
		{
			this.SelectedRegionItem = this.RegionList.Where(w => w.PK_Region == this.SelectedListItem.FK_Region).FirstOrDefault();
		}

		private void HandleRegionComboChangedMessage(RegionComboChangedMessage obj)
		{
			if (!this.isInitializing && this.SelectedRegionItem != null)
			{
				this.SelectedListItem.FK_Region = this.SelectedRegionItem.PK_Region;
			}

			this.IsDirty = true;
			this.SaveDistrictCommand.RaiseCanExecuteChanged();
		}

		private void HandleDistrictNameChangedMessage(DistrictNameChangedMessage obj)
		{
			this.IsDirty = true;
			this.SaveDistrictCommand.RaiseCanExecuteChanged();
		}

		#endregion

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
			var resp = _districtDataService.UpdateTable(SelectedListItem, HttpRequestMethods.Delete, "api/district/", null);
			this.ShowEditButtons(false);

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
			return true;

			//var retVal = this.IsDirty &&   
			//	this.SelectedListItem.DistrictName.IsNotNull() && 
			//	this.SelectedListItem.DistrictName != "New District" && 
			//	this.SelectedRegionItem.RegionName.IsNotNull();

			//return retVal;
		}

		private void ExecuteSaveDistrictCommand()
		{
			this.SaveDistrict();
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


		private bool CanExecuteEditDistrictCommand()
		{
			return true;
		}

		private void ExecuteEditDistrictCommand()
		{
			this.EnableEditControls(true);
			this.ShowEditButtons(false);

		}

		#endregion

		public void SaveDistrict(bool skipNotify = false)
		{
			var isError = false;
			var message = "Changes successfully sent to database.";

			try
			{
				var resp = _districtDataService.UpdateTable(this.SelectedListItem, this.isNew ? HttpRequestMethods.Post : HttpRequestMethods.Put, "api/district/", this.SelectedListItem.PK_District);
			}
			catch (Exception e)
			{
				isError = true;
				message = e.Message;
			}
			this.isNew = false;

			if (!skipNotify)
			{
				Helpers.Helpers.Notify("District", NotifyButtonEnum.OneButton, new List<NotifyButtonLabelEnum> { NotifyButtonLabelEnum.Ok }, message, isError);
			}

			this.IsDirty = false;
			this.ShowEditButtons(true);
			this.EnableEditControls(false);
		}


	}
}
