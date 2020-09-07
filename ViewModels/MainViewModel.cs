using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Services;
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
        #endregion

        public MainViewModel(ILocalStorageService ls)
        {
            _ls = ls;
            this.DirtyViews = new Dictionary<string, string>();
            this.ActiveViewModels = new Dictionary<Type, string>();
            this.LoginButtonVisibility = Visibility.Visible;
            this.LabelText = "Sign In";
            this.LoginCommand = new RelayCommand(this.ExecuteLoginCommand, CanExecuteLoginCommand);
            Messenger.Default.Register<ClosingMessage>(this, HandleClosingMessage);
            Messenger.Default.Register<ClosedMessage>(this, HandleClosedMessage);
            Messenger.Default.Register<ContentEmptyMessage>(this, HandleContentEmptyMessage);
            Messenger.Default.Register<ContentFilledMessage>(this, HandleContentFilledMessage);

        }

		private void HandleClosedMessage(ClosedMessage obj)
		{
			
		}

		#region MessageHandlers
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