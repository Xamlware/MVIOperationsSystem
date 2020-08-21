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
	public class RegionEditViewModel : MVIViewModelBase
	{
		private readonly IDataService<Region> _rs;
		private readonly ILocalStorageService _ls;
		private bool isInitializing = false;
		private bool isNew = false;

		#region Commands
		public RelayCommand AddRegionCommand { get; private set; }
		public RelayCommand EditRegionCommand { get; private set; }
		public RelayCommand DeleteRegionCommand { get; private set; }
		public RelayCommand SaveRegionCommand { get; private set; }
		public RelayCommand CancelRegionCommand { get; private set; }
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


		public const string IsRegionNameEnabledProperty = "IsRegionNameEnabled";
		private bool _isRegionNameEnabled;

		public bool IsRegionNameEnabled
		{
			get
			{
				return _isRegionNameEnabled;
			}
			set
			{
				_isRegionNameEnabled = value;
				this.RaisePropertyChanged(IsRegionNameEnabledProperty);
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



		//public const string RegionListProperty = "RegionList";
		//private ObservableCollection<Region> _RegionList;

		//public ObservableCollection<Region> RegionList
		//{
		//	get
		//	{
		//		return _RegionList;
		//	}
		//	set
		//	{
		//		_RegionList = value;
		//		this.RaisePropertyChanged(RegionListProperty);
		//	}
		//}

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
		private Region _selectedListItem;

		public Region SelectedListItem
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
				this.IsDirty = true;
				this.SaveRegionCommand.RaiseCanExecuteChanged();
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
				this.SaveRegionCommand.RaiseCanExecuteChanged();
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
				SaveRegionCommand.RaiseCanExecuteChanged();
				this.RaisePropertyChanged(IsDirtyTextProperty);
			}
		}
		#endregion


		//
		public RegionEditViewModel(IDataService<Region> rs, ILocalStorageService ls)
		{
			_rs = rs;
			_ls = ls;
			this.isInitializing = true;

			Messenger.Default.Register<RegionNameChangedMessage>(this, this.HandleRegionNameChangedMessage);
			Messenger.Default.Register<ContentPresenterChangedMessage>(this, this.HandleContentPresenterChangedMessage);
			Messenger.Default.Register<ListItemChangedMessage>(this, this.HandleListItemChangedMessage);
			//Messenger.Default.Register<RegionComboChangedMessage>(this, this.HandleRegionComboChangedMessage);
			Messenger.Default.Register<AdminDataCloseMessage>(this, this.HandleAdminDataCloseMessage);


			this.AddRegionCommand = new RelayCommand(this.ExecuteAddRegionCommand, this.CanExecuteAddRegionCommand);
			this.EditRegionCommand = new RelayCommand(this.ExecuteEditRegionCommand, this.CanExecuteEditRegionCommand);
			this.DeleteRegionCommand = new RelayCommand(this.ExecuteDeleteRegionCommand, this.CanExecuteDeleteRegionCommand);
			this.SaveRegionCommand = new RelayCommand(this.ExecuteSaveRegionCommand, this.CanExecuteSaveRegionCommand);
			this.CancelRegionCommand = new RelayCommand(this.ExecuteCancelRegionCommand, this.CanExecuteCancelRegionCommand);
			this.NotificationMessageViewedCommand = new RelayCommand(this.ExecuteNotificationMessageViewedCommand, thisCanExecuteNotificationMessageViewedCommand);

			this.isInitializing = false;
			this.ShowEditButtons(true);

			#region CreateManualLists
			//var dist = new Region() { PK_Region=1, RegionName = "Gap Region", FK_Region = 2 };
			//this.RegionList = new ObservableCollection<Region>
			//{
			//	dist
			//};

			//if (this.RegionList.Count > 0)
			//{ 
			//	this.SelectedListItem = this.RegionList[0];
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
					"Region",
					NotifyButtonEnum.ThreeButton,
					new List<NotifyButtonLabelEnum> { { NotifyButtonLabelEnum.Yes }, { NotifyButtonLabelEnum.No }, { NotifyButtonLabelEnum.Cancel } },
					"You have unsaved changes.  Save now?",
					false);

			}
		}

		private void EnableEditControls(bool isEnabled = false)
		{
			this.IsRegionNameEnabled = isEnabled;
			this.IsRegionComboEnabled = isEnabled;
		}

		private void ShowEditButtons(bool show)
		{
			this.IsEditButtonPanelVisible = show ? Visibility.Visible : Visibility.Hidden;
			this.IsSaveButtonPanelVisible = show ? Visibility.Hidden : Visibility.Visible;
		}

		private ObservableCollection<Region> GetRegionListAsync()
		{
			var task = _rs.GetTableList(HttpRequestMethods.Get, "api/region/");
			return task;
		}

		//private ObservableCollection<Region> GetRegionListAsync()
		//{
		//	var task = _RegionDataService.GetTableList(HttpRequestMethods.Get, "api/Region/");
		//	return task;
		//}


		#region Message handlers
		private void HandleContentPresenterChangedMessage(ContentPresenterChangedMessage obj)
		{
			if (obj.Action.Contains("Region"))
			{
				this.IsBusy = true;

				ObservableCollection<Region> disTask = this.GetRegionListAsync();
				this.RegionList = disTask;

				if (this.RegionList.IsNotNull() && this.RegionList.Count > 0)
				{
					this.SelectedListItem = this.RegionList[0];
				}

				this.IsBusy = false;
			}
		}

		private void HandleListItemChangedMessage(ListItemChangedMessage obj)
		{
			if (this.SelectedListItem != null)
			{
				this.SelectedRegionItem = this.RegionList.Where(w => w.PK_Region == this.SelectedListItem.PK_Region).FirstOrDefault();
			}
		}

		//private void HandleRegionComboChangedMessage(RegionComboChangedMessage obj)
		//{
		//	if (!this.isInitializing && this.SelectedRegionItem != null)
		//	{
		//		this.SelectedListItem.FK_Region = this.SelectedRegionItem.PK_Region;
		//	}

		//	this.IsDirty = true;
		//	this.SaveRegionCommand.RaiseCanExecuteChanged();
		//}

		private void HandleRegionNameChangedMessage(RegionNameChangedMessage dm)
		{
			if ((dm.Action.Trim()).IsNullOrEmpty())
			{
				this.IsDirty = true;
				this.SaveRegionCommand.RaiseCanExecuteChanged();
			}
		}

		#endregion

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
			var resp = _rs.UpdateTable(SelectedListItem, HttpRequestMethods.Delete, "api/Region/", null);
			this.ShowEditButtons(false);

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
			return true;

			//var retVal = this.IsDirty &&   
			//	this.SelectedListItem.RegionName.IsNotNull() && 
			//	this.SelectedListItem.RegionName != "New Region" && 
			//	this.SelectedRegionItem.RegionName.IsNotNull();

			//return retVal;
		}

		private void ExecuteSaveRegionCommand()
		{
			this.SaveRegion();
		}

		private bool CanExecuteAddRegionCommand()
		{
			return !this.isNew;

		}

		private void ExecuteAddRegionCommand()
		{
			this.isNew = true;
			this.IsDirty = true;

			var item = new Region { RegionName = "New Region" };
			this.RegionList.Add(item);
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


		private bool CanExecuteEditRegionCommand()
		{
			return true;
		}

		private void ExecuteEditRegionCommand()
		{
			this.EnableEditControls(true);
			this.ShowEditButtons(false);

		}

		#endregion

		public void SaveRegion(bool skipNotify = false)
		{
			var isError = false;
			var message = "Changes successfully sent to database.";

			try
			{
				var resp = _rs.UpdateTable(this.SelectedListItem, this.isNew ? HttpRequestMethods.Post : HttpRequestMethods.Put, "api/Region/", this.SelectedListItem.PK_Region);
			}
			catch (Exception e)
			{
				isError = true;
				message = e.Message;
			}
			this.isNew = false;

			if (!skipNotify)
			{
				Helpers.Helpers.Notify("Region", NotifyButtonEnum.OneButton, new List<NotifyButtonLabelEnum> { NotifyButtonLabelEnum.Ok }, message, isError);
			}

			this.IsDirty = false;
			this.ShowEditButtons(true);
			this.EnableEditControls(false);
		}


	}
}
