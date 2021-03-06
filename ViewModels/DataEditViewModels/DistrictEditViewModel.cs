﻿using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MVIOperations.Models;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Services;
using MVIOperationsSystem.ViewModels.CustomViewModels;
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
	public class DistrictEditViewModel : MVIViewModelBase
	{
		private readonly IDataService<District> _ds;
		private readonly IDataService<Region> _rs;
		private readonly ILocalStorageService _ls;
		private StatusBarViewModel _svm;
		private MainViewModel _vm;
		private bool isInitializing = false;
//		private bool isNew = false;

		#region Commands
		public RelayCommand AddDistrictCommand { get; private set; }
		public RelayCommand EditDistrictCommand { get; private set; }
		public RelayCommand DeleteDistrictCommand { get; private set; }
		public RelayCommand SaveDistrictCommand { get; private set; }
		public RelayCommand CancelDistrictCommand { get; private set; }
		public RelayCommand NotificationMessageViewedCommand { get; private set; }
		#endregion

		#region Properties

		private District PreEditValues = new District();
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

		public const string IsNewProperty = "IsNew";
		private bool _isNew;


		public bool IsNew
		{
			get
			{
				return _isNew;
			}
			set
			{
				_isNew = value;
				this.RaisePropertyChanged(IsNewProperty);
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
				var oldName = _districtName;
				_districtName = value;
				//if (!this.isInitializing && oldName != _districtName)
				//{
				//	//this.IsDirty = true;
				//}
				this.RaisePropertyChanged(DistrictNameProperty);
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
				var oldName = _region;
				_region = value;
				//if (!this.isInitializing && oldName != _region)
				//{
				//	//this.IsDirty = true;
				//}

				this.RaisePropertyChanged(RegionProperty);
				this.SaveDistrictCommand.RaiseCanExecuteChanged();
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
					var d = _vm.DirtyViews.FirstOrDefault(f => f.Key == "District").Key == "District";
					if (d.IsFalse())
					{
						_vm.DirtyViews.Add("District", "");
					}
				}
				else
				{
					_vm.DirtyViews.Remove("District");
				}
				SaveDistrictCommand.RaiseCanExecuteChanged();
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
				SaveDistrictCommand.RaiseCanExecuteChanged();
				this.RaisePropertyChanged(IsEditingTextProperty);
			}
		}
		#endregion

		public DistrictEditViewModel(IDataService<District> ds, IDataService<Region> rs, ILocalStorageService ls, MainViewModel vm, StatusBarViewModel svm)
		{
			_svm = svm;
			_vm = vm;
			_ds = ds;
			_rs = rs;
			_ls = ls;


			this.isInitializing = true;
			_vm.ActiveViewModels.Add(this.GetType(), "District");

			Messenger.Default.Register<DistrictNameChangedMessage>(this, this.HandleDistrictNameChangedMessage);
			Messenger.Default.Register<ContentPresenterChangedMessage>(this, this.HandleContentPresenterChangedMessage);
			Messenger.Default.Register<ListItemChangedMessage>(this, this.HandleListItemChangedMessage);
			Messenger.Default.Register<RegionComboChangedMessage>(this, this.HandleRegionComboChangedMessage);
			Messenger.Default.Register<AdminDataCloseMessage>(this, this.HandleAdminDataCloseMessage);
			Messenger.Default.Register<NotifyResultMessage>(this, this.HandleNotifyResultMessage);


			this.AddDistrictCommand = new RelayCommand(this.ExecuteAddDistrictCommand, this.CanExecuteAddDistrictCommand);
			this.EditDistrictCommand = new RelayCommand(this.ExecuteEditDistrictCommand, this.CanExecuteEditDistrictCommand);
			this.DeleteDistrictCommand = new RelayCommand(this.ExecuteDeleteDistrictCommand, this.CanExecuteDeleteDistrictCommand);
			this.SaveDistrictCommand = new RelayCommand(this.ExecuteSaveDistrictCommand, this.CanExecuteSaveDistrictCommand);
			this.CancelDistrictCommand = new RelayCommand(this.ExecuteCancelDistrictCommand, this.CanExecuteCancelDistrictCommand);
			this.NotificationMessageViewedCommand = new RelayCommand(this.ExecuteNotificationMessageViewedCommand, thisCanExecuteNotificationMessageViewedCommand);

			this.ShowEditButtons(true);
			this.GetDataListsAsync();

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

		public override void Cleanup()
		{
			SimpleIoc.Default.Unregister<DistrictEditViewModel>();
			SimpleIoc.Default.Register<DistrictEditViewModel>();
			base.Cleanup();
		}

		public void SaveDistrict(bool skipNotify = false)
		{
			_svm.ShowProgressBar(true, "updating District information");
			var isError = false;
			var message = "Changes successfully sent to database.";
			try
			{
				var resp = _ds.UpdateTable(_vm.IsConnected, this.SelectedListItem, this.IsNew ? HttpRequestMethods.Post : HttpRequestMethods.Put, "api/district/", this.SelectedListItem.PK_District);
				
			}
			catch (Exception e)
			{
				isError = true;
				message = e.Message;
				
				Helpers.Helpers.LogErrorMessage(_vm.IsConnected, e.Message, e.Message);
			}
			this.IsNew = false;

			if (!skipNotify)
			{
				Helpers.Helpers.Notify(
					"District",
					NotifyButtonEnum.OneButton,
					new List<NotifyButtonLabelEnum> { NotifyButtonLabelEnum.Ok },
					message,
					isError,
					"DistrictSave");
			}

			this.IsDirty = false;
			this.ShowEditButtons(true);
			this.EnableEditControls(false);
			_svm.ShowProgressBar(false, "");
		}

		public void NotifyUserIsDirty(string origin = null)
		{
			Helpers.Helpers.Notify(
					"District",
					NotifyButtonEnum.ThreeButton,
					new List<NotifyButtonLabelEnum> { { NotifyButtonLabelEnum.Yes }, { NotifyButtonLabelEnum.No }, { NotifyButtonLabelEnum.Cancel } },
					"You have unsaved changes.  Save now?",
					false,
					origin);


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

			this.IsEditing = (this.IsEditButtonPanelVisible == Visibility.Hidden);
			Messenger.Default.Send(new EnableAdminTreePanelMessage { Action = (this.IsEditButtonPanelVisible == Visibility.Visible) });
		}

		private ObservableCollection<Region> GetRegionListAsync()
		{
			var region = new Region();
			var task = _rs.GetTableList(_vm.IsConnected, region, HttpRequestMethods.Get, "api/region/");
			return task;
		}

		private ObservableCollection<District> GetDistrictListAsync()
		{
			var district = new District();
			var task = _ds.GetTableList(_vm.IsConnected, district, HttpRequestMethods.Get, "api/district/");
			return task;
		}

		private void GetDataListsAsync()
		{
			this.IsBusy = true;
			_svm.ShowProgressBar(true, "Fetching Data Records");
			ObservableCollection<Region> regTask = this.GetRegionListAsync();
			this.RegionList = regTask;

			ObservableCollection<District> disTask = this.GetDistrictListAsync();
			this.DistrictList = disTask;

			if (this.DistrictList.IsNotNull() && this.DistrictList.Count > 0)
			{
				this.SelectedListItem = this.DistrictList[0];
				_svm.ShowProgressBar(false, "");
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
			if (this.SelectedListItem != null && this.RegionList != null)
			{
				this.SelectedRegionItem = this.RegionList.Where(w => w.PK_Region == this.SelectedListItem.FK_Region).FirstOrDefault();
			}
		}

		private void HandleRegionComboChangedMessage(RegionComboChangedMessage obj)
		{
			if (!this.isInitializing && this.IsEditing && this.SelectedRegionItem != null)
			{
				this.SelectedListItem.FK_Region = this.SelectedRegionItem.PK_Region;
				this.IsDirty = true;
			}

			if (this.isInitializing)
			{
				this.isInitializing = false;
			}

			this.SaveDistrictCommand.RaiseCanExecuteChanged();
		}

		private void HandleDistrictNameChangedMessage(DistrictNameChangedMessage dm)
		{
			if (!this.isInitializing && this.IsEditing && this.SelectedListItem != null)
			{
				this.IsDirty = true;
				Console.WriteLine("is dirty = true;");
			}

			this.SaveDistrictCommand.RaiseCanExecuteChanged();
		}

		#endregion

		#region Can/Execute

		private void ExecuteCancelDistrictCommand()
		{
			if (this.IsDirty)
			{
				this.NotifyUserIsDirty("District");
			}

			//this.ShowEditButtons(true);
			//this.EnableEditControls(false);

		}


		private bool CanExecuteCancelDistrictCommand()
		{
			return true;
		}

		private void ExecuteDeleteDistrictCommand()
		{

			var resp = _ds.UpdateTable(_vm.IsConnected, this.SelectedListItem, HttpRequestMethods.Delete, "api/district/", this.SelectedListItem.PK_District);
			this.DistrictList.Remove(this.SelectedListItem);
			if (this.DistrictList.Count > 0)
			{
				this.SelectedListItem = this.DistrictList[0];
			}
			this.ShowEditButtons(false);
			//this.EnableEditControls(true);

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
			Messenger.Default.Send(new UpdateSourceDistrictMessage());
			this.SaveDistrict();
		}

		private bool CanExecuteAddDistrictCommand()
		{
			return !this.IsNew;

		}

		private void ExecuteAddDistrictCommand()
		{
			this.IsNew = true;
			this.IsDirty = true;

			var item = new District { DistrictName = "New District" };
			this.DistrictList.Add(item);
			this.SelectedListItem = item;
			this.SelectedRegionItem = null;

			//this.IsNew = false;
			this.ShowEditButtons(false);
			this.EnableEditControls(true);

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

	}
}
