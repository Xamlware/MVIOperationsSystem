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
	public class InventoryEditViewModel : ViewModelBase
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



		public const string InventoryListProperty = "InventoryList";
		private List<Inventory> _InventoryList;

		public List<Inventory> InventoryList
		{
			get
			{
				return _InventoryList;
			}
			set
			{
				_InventoryList = value;
				this.RaisePropertyChanged(InventoryListProperty);
			}
		}


		public const string InventoryNameProperty = "InventoryName";
		private string _InventoryName;

		public string InventoryName
		{
			get
			{
				return _InventoryName;
			}
			set
			{
				_InventoryName = value;
				this.RaisePropertyChanged(InventoryNameProperty);
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



		public InventoryEditViewModel(ILoginDataService dataService)
		{

			_dataService = dataService;
			//Messenger.Default.Register<PasswordMessage>(this, HandlePasswordMessage);
			this.AddCommand = new RelayCommand(this.ExecuteAddInventoryCommand, this.CanExecuteAddInventoryCommand);
			this.DeleteCommand = new RelayCommand(this.ExecuteDeleteInventoryCommand, this.CanExecuteDeleteInventoryCommand);
			this.SaveCommand = new RelayCommand(this.ExecuteSaveInventoryCommand, this.CanExecuteSaveInventoryCommand);
			this.CancelCommand = new RelayCommand(this.ExecuteCancelInventoryCommand, this.CanExecuteCancelInventoryCommand);

			var dist = new Inventory() { ItemName = "Inventory 1" };
			this.InventoryList = new List<Inventory>{ dist };
			//this.IsAddButtonEnabled = true;
			//this.IsDeleteButtonEnabled = false;
			//this.IsSaveButtonEnabled = false;
		}


		#region Can/Execute

		private void ExecuteCancelInventoryCommand()
		{

		}


		private bool CanExecuteCancelInventoryCommand()
		{
			return true;
		}

		private void ExecuteDeleteInventoryCommand()
		{

		}

		private bool CanExecuteDeleteInventoryCommand()
		{
			var retVal = true;
			if (this.InventoryList != null && this.InventoryList.Count() > 0)
			{
				retVal = true;
			}

			return retVal;
		}

		private bool CanExecuteSaveInventoryCommand()
		{

			return this.IsDirty;

		}

		private void ExecuteSaveInventoryCommand()
		{

		}

		private bool CanExecuteAddInventoryCommand()
		{
			return true;

		}

		private void ExecuteAddInventoryCommand()
		{

		}
		#endregion

	}
}
