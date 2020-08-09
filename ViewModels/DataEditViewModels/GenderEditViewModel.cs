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
	public class GenderEditViewModel : ViewModelBase
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



		public const string GenderListProperty = "GenderList";
		private List<Gender> _GenderList;

		public List<Gender> GenderList
		{
			get
			{
				return _GenderList;
			}
			set
			{
				_GenderList = value;
				this.RaisePropertyChanged(GenderListProperty);
			}
		}


		public const string GenderNameProperty = "GenderName";
		private string _GenderName;

		public string GenderName
		{
			get
			{
				return _GenderName;
			}
			set
			{
				_GenderName = value;
				this.RaisePropertyChanged(GenderNameProperty);
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



		public GenderEditViewModel(ILoginDataService dataService)
		{

			_dataService = dataService;
			//Messenger.Default.Register<PasswordMessage>(this, HandlePasswordMessage);
			this.AddCommand = new RelayCommand(this.ExecuteAddGenderCommand, this.CanExecuteAddGenderCommand);
			this.DeleteCommand = new RelayCommand(this.ExecuteDeleteGenderCommand, this.CanExecuteDeleteGenderCommand);
			this.SaveCommand = new RelayCommand(this.ExecuteSaveGenderCommand, this.CanExecuteSaveGenderCommand);
			this.CancelCommand = new RelayCommand(this.ExecuteCancelGenderCommand, this.CanExecuteCancelGenderCommand);

			var gender = new Gender() { GenderName = "Male" };
			this.GenderList = new List<Gender>();
			this.GenderList.Add(gender);
			//this.IsAddButtonEnabled = true;
			//this.IsDeleteButtonEnabled = false;
			//this.IsSaveButtonEnabled = false;
		}


		#region Can/Execute

		private void ExecuteCancelGenderCommand()
		{

		}


		private bool CanExecuteCancelGenderCommand()
		{
			return true;
		}

		private void ExecuteDeleteGenderCommand()
		{

		}

		private bool CanExecuteDeleteGenderCommand()
		{
			var retVal = true;
			if (this.GenderList != null && this.GenderList.Count() > 0)
			{
				retVal = true;
			}

			return retVal;
		}

		private bool CanExecuteSaveGenderCommand()
		{

			return this.IsDirty;

		}

		private void ExecuteSaveGenderCommand()
		{

		}

		private bool CanExecuteAddGenderCommand()
		{
			return true;

		}

		private void ExecuteAddGenderCommand()
		{

		}
		#endregion

	}
}
