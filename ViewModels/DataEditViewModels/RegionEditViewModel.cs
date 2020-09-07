using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MVIOperations.Models;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Services;
using MVIOperationsSystem.Views.DataEditViews;
using Syncfusion.Windows.Controls.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Xamlware.Framework.Extensions;
using Region = MVIOperationsSystem.Models.Region;

namespace MVIOperationsSystem.ViewModels.DataEditViewModels
{
	public class RegionEditViewModel : MVIViewModelBase
	{
		private readonly IDataService<Region> _ds;
		private readonly IDataService<Region> _rs;
		private readonly ILocalStorageService _ls;
		private MainViewModel _vm;
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

		private Region PreEditValues = new Region();
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



		public const string RegionListProperty = "RegionList";
		private ObservableCollection<Region> _regionList;

		public ObservableCollection<Region> RegionList
		{
			get
			{
				return _regionList;
			}
			set
			{
				_regionList = value;
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
				var oldName = _RegionName;
				_RegionName = value;
				//if (!this.isInitializing && oldName != _RegionName)
				//{
				//	//this.IsDirty = true;
				//}
				this.RaisePropertyChanged(RegionNameProperty);
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
				var oldName = _region;
				_region = value;
				//if (!this.isInitializing && oldName != _region)
				//{
				//	//this.IsDirty = true;
				//}

				this.RaisePropertyChanged(RegionProperty);
				this.SaveRegionCommand.RaiseCanExecuteChanged();
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
					var d = _vm.DirtyViews.FirstOrDefault(f => f.Key == "Region").Key == "Region";
					if (d.IsFalse())
					{
						_vm.DirtyViews.Add("Region", "");
					}
				}
				else
				{
					_vm.DirtyViews.Remove("Region");
				}
				SaveRegionCommand.RaiseCanExecuteChanged();
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
				SaveRegionCommand.RaiseCanExecuteChanged();
				this.RaisePropertyChanged(IsEditingTextProperty);
			}
		}
		#endregion


		//
		public RegionEditViewModel(IDataService<Region> ds, IDataService<Region> rs, ILocalStorageService ls, MainViewModel vm)
		{
			_vm = vm;
			_ds = ds;
			_rs = rs;
			_ls = ls;

			this.isInitializing = true;
			_vm.ActiveViewModels.Add(this.GetType(), "Region");

			Messenger.Default.Register<RegionNameChangedMessage>(this, this.HandleRegionNameChangedMessage);
			Messenger.Default.Register<ContentPresenterChangedMessage>(this, this.HandleContentPresenterChangedMessage);
			Messenger.Default.Register<AdminDataCloseMessage>(this, this.HandleAdminDataCloseMessage);
			Messenger.Default.Register<NotifyResultMessage>(this, this.HandleNotifyResultMessage);


			this.AddRegionCommand = new RelayCommand(this.ExecuteAddRegionCommand, this.CanExecuteAddRegionCommand);
			this.EditRegionCommand = new RelayCommand(this.ExecuteEditRegionCommand, this.CanExecuteEditRegionCommand);
			this.DeleteRegionCommand = new RelayCommand(this.ExecuteDeleteRegionCommand, this.CanExecuteDeleteRegionCommand);
			this.SaveRegionCommand = new RelayCommand(this.ExecuteSaveRegionCommand, this.CanExecuteSaveRegionCommand);
			this.CancelRegionCommand = new RelayCommand(this.ExecuteCancelRegionCommand, this.CanExecuteCancelRegionCommand);
			this.NotificationMessageViewedCommand = new RelayCommand(this.ExecuteNotificationMessageViewedCommand, thisCanExecuteNotificationMessageViewedCommand);

			this.ShowEditButtons(true);
			this.GetDataListsAsync();

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

		public override void Cleanup()
		{
			SimpleIoc.Default.Unregister<RegionEditViewModel>();
			SimpleIoc.Default.Register<RegionEditViewModel>();
			base.Cleanup();
		}

		public void SaveRegion(bool skipNotify = false)
		{
			var isError = false;
			var message = "Changes successfully sent to database.";
			try
			{
				var resp = _ds.UpdateTable(this.SelectedListItem, this.isNew ? HttpRequestMethods.Post : HttpRequestMethods.Put, "api/Region/", this.SelectedListItem.PK_Region);
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
					"Region",
					NotifyButtonEnum.OneButton,
					new List<NotifyButtonLabelEnum> { NotifyButtonLabelEnum.Ok },
					message,
					isError,
					"RegionSave");
			}

			this.IsDirty = false;
			this.ShowEditButtons(true);
			this.EnableEditControls(false);
		}

		public void NotifyUserIsDirty(string origin = null)
		{
			Helpers.Helpers.Notify(
					"Region",
					NotifyButtonEnum.ThreeButton,
					new List<NotifyButtonLabelEnum> { { NotifyButtonLabelEnum.Yes }, { NotifyButtonLabelEnum.No }, { NotifyButtonLabelEnum.Cancel } },
					"You have unsaved changes.  Save now?",
					false,
					origin);


		}

		private void EnableEditControls(bool isEnabled = false)
		{
			this.IsRegionNameEnabled = isEnabled;
		}

		private void ShowEditButtons(bool show)
		{
			this.IsEditButtonPanelVisible = show ? Visibility.Visible : Visibility.Hidden;
			this.IsSaveButtonPanelVisible = show ? Visibility.Hidden : Visibility.Visible;

			this.IsEditing = (this.IsEditButtonPanelVisible == Visibility.Hidden);
			Messenger.Default.Send(new EnableAdminTreePanelMessage { Action = (this.IsEditButtonPanelVisible == Visibility.Visible) });
		}

		private void GetDataListsAsync()
		{
			this.IsBusy = true;
			ObservableCollection<Region> regTask = this.GetRegionListAsync();
			this.RegionList = regTask;

			if (this.RegionList.IsNotNull() && this.RegionList.Count > 0)
			{
				this.SelectedListItem = this.RegionList[0];
			}

			this.IsBusy = false;
		}


		private ObservableCollection<Region> GetRegionListAsync()
		{
			var task = _rs.GetTableList(HttpRequestMethods.Get, "api/region/");
			return task;
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
			if (obj.Action.Contains("Region"))
			{
				this.IsBusy = true;
				ObservableCollection<Region> regTask = this.GetRegionListAsync();
				this.RegionList = regTask;

				ObservableCollection<Region> disTask = this.GetRegionListAsync();
				this.RegionList = disTask;

				if (this.RegionList.IsNotNull() && this.RegionList.Count > 0)
				{
					this.SelectedListItem = this.RegionList[0];
				}

				this.IsBusy = false;
			}
		}

	
		private void HandleRegionNameChangedMessage(RegionNameChangedMessage dm)
		{
			if (!this.isInitializing && this.IsEditing && this.SelectedListItem != null)
			{
				this.IsDirty = true;
				Console.WriteLine("is dirty = true;");
			}

			this.SaveRegionCommand.RaiseCanExecuteChanged();
			if (this.isInitializing)
			{
				this.isInitializing = false;
			}
		}

		#endregion

		#region Can/Execute

		private void ExecuteCancelRegionCommand()
		{
			if (this.IsDirty)
			{
				this.NotifyUserIsDirty("Region");
			}

			//this.ShowEditButtons(true);
			//this.EnableEditControls(false);

		}


		private bool CanExecuteCancelRegionCommand()
		{
			return true;
		}

		private void ExecuteDeleteRegionCommand()
		{
			//var index = this.SelectedListItem.PK_Region;
			var resp = _ds.UpdateTable(SelectedListItem, HttpRequestMethods.Delete, "api/Region/", this.SelectedListItem.PK_Region);
			this.RegionList.Remove(this.SelectedListItem);
			if (this.RegionList.Count > 0)
			{
				this.SelectedListItem = this.RegionList[0];
			}
			this.ShowEditButtons(false);
			//this.EnableEditControls(true);

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
			Messenger.Default.Send(new UpdateSourceRegionMessage());
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

	}
}
