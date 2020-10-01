using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Services;
using MVIOperationsSystem.ViewModels.CustomViewModels;
using MVIOperationsSystem.ViewModels.DataEditViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MVIOperationsSystem.ViewModels
{
    public class MainViewModel : MVIViewModelBase
    {
        ILocalStorageService _ls;
        public Dictionary<string, string> DirtyViews { get; set; }
        private readonly ISyncService _ss;
        private readonly StatusBarViewModel _sb;

        #region Commands
        public RelayCommand LoginCommand { get; private set; }

        #endregion

        #region Properties
        public IDictionary<Type, string> ActiveViewModels { get; private set; }


        public const string LabelTextProperty = "LabelText";
        private string _labelText;

          public string LabelText
        {
            get { return _labelText; }

            set
            {
                _labelText = value;
                this.RaisePropertyChanged(LabelTextProperty);
            }
        }


        public const string LoginButtonVisibilityProperty = "LoginButtonVisibility";
        private Visibility _loginButtonVisibility;

        public Visibility LoginButtonVisibility
        {
            get { return _loginButtonVisibility; }

            set
            {
                _loginButtonVisibility = value;
                this.RaisePropertyChanged(LoginButtonVisibilityProperty);
            }
        }

        public const string IsGreenLightVisibleProperty = "IsGreenLightVisible";
        private Visibility _isGreenLightVisible;

        public Visibility IsGreenLightVisible
        {
            get
            {
                return _isGreenLightVisible;
            }
            set
            {
                _isGreenLightVisible = value;
                this.RaisePropertyChanged(IsGreenLightVisibleProperty);
            }
        }

        public const string IsRedLightVisibleProperty = "IsRedLightVisible";
        private Visibility _isRedLightVisible;

        public Visibility IsRedLightVisible
        {
            get
            {
                return _isRedLightVisible;
            }
            set
            {
                _isRedLightVisible = value;
                this.RaisePropertyChanged(IsRedLightVisibleProperty);
            }
        }
		#endregion

		public MainViewModel(ILocalStorageService ls, ISyncService ss, StatusBarViewModel sb)
        {
            _ls = ls;
            _ss = ss;
            _sb = sb;

            this.DirtyViews = new Dictionary<string, string>();
            this.ActiveViewModels = new Dictionary<Type, string>();
            this.LoginButtonVisibility = Visibility.Visible;
            this.LabelText = "Sign In";
            this.IsGreenLightVisible = Visibility.Collapsed;
            this.IsRedLightVisible = Visibility.Visible;

            this.LoginCommand = new RelayCommand(this.ExecuteLoginCommand, CanExecuteLoginCommand);
            Messenger.Default.Register<ClosingMessage>(this, HandleClosingMessage);
            Messenger.Default.Register<ClosedMessage>(this, HandleClosedMessage);
            Messenger.Default.Register<ContentEmptyMessage>(this, HandleContentEmptyMessage);
            Messenger.Default.Register<ContentFilledMessage>(this, HandleContentFilledMessage);
            Messenger.Default.Register<NetworkConnectionMessage>(this, this.HandleNetworkConnectionMessage);

            //_ss.SyncList = new List<SyncModel>();
            base.CheckForConnectivity();
        }

        private void SetNetworkLight()
        {
            this.IsGreenLightVisible = this.IsConnected ? Visibility.Visible : Visibility.Collapsed;
            this.IsRedLightVisible = this.IsConnected ? Visibility.Collapsed : Visibility.Visible;
        }

        private void HandleClosedMessage(ClosedMessage obj)
		{
			
		}

        #region MessageHandlers

        private void HandleNetworkConnectionMessage(NetworkConnectionMessage ncm)
        {
            if (ncm.IsConnected)
            {
                this.SetNetworkLight();
                this.StopTimer();
                _ss.SyncOfflineDb();
            }
        }


        private void HandleContentFilledMessage(ContentFilledMessage obj)
		{
            this.LoginButtonVisibility = Visibility.Hidden;
		}

		private void HandleContentEmptyMessage(ContentEmptyMessage obj)
		{
            this.LoginButtonVisibility = Visibility.Visible;
		}

		public void HandleClosingMessage(ClosingMessage cm)
        {
            var avm = SimpleIoc.Default.GetInstance<AdminDataViewModel>();
            foreach(var v in this.ActiveViewModels)
			{
                if (v.Value == "District")
                {
                    var vm = SimpleIoc.Default.GetInstance(v.Key) as DistrictEditViewModel;
                    if(vm.IsDirty)
                    {
                        //_dirtyViews.Add(v.Value);
                        avm.SetSelectedTab("District");
                        vm.NotifyUserIsDirty("Main");
                    }
                }
                else if (v.Value == "Region")
                {
                    var vm = SimpleIoc.Default.GetInstance(v.Key) as RegionEditViewModel;
                    if (vm.IsDirty)
                    {
                        //_dirtyViews.Add(v.Value);
                        avm.SetSelectedTab("Region");
                        vm.NotifyUserIsDirty("Main");
                    }
                }
                else if (v.Value == "Employee")
                {
                    var vm = SimpleIoc.Default.GetInstance(v.Key) as EmployeeEditViewModel;
                    if (vm.IsDirty)
                    {
                        //_dirtyViews.Add(v.Value);
                        avm.SetSelectedTab("Employee");
                        vm.NotifyUserIsDirty("Main");
                    }
                }

            }
        }
		#endregion

		#region Execute/CanExecute

		private bool CanExecuteLoginCommand()
        {

            return true;
        }

        private void ExecuteLoginCommand()
        {
            if (this.LabelText == "Sign In")
            {
                this.LabelText = "Sign Out";
                Messenger.Default.Send(new NavigationMessage { Action = "Login" });
            }
            else
            {
                this.LabelText = "Sign In";
                Messenger.Default.Send(new NavigationMessage { Action = "Logout" });
            };

            if (this.LabelText == "Sign Out")
            {
                Messenger.Default.Send<SignOutMessage>(new SignOutMessage());
                _ls.ClearLocalStorage(); 
            }
            #endregion

        }
    }
}