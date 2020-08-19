using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Services;
using Syncfusion.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xamlware.Framework.Extensions;

namespace MVIOperationsSystem.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        ILocalStorageService _ls;

        #region Commands
        public RelayCommand OpenAdminDataManagementCommand { get; private set; }

        #endregion


        #region Properties

        private string SelectedMenuItemProperty { get; set; }
        private object selectedMenuItem;
        public object SelectedMenuItem
        {
            get
            {
                return selectedMenuItem;
            }
            set
            {
                selectedMenuItem = value;
                this.RaisePropertyChanged(SelectedMenuItemProperty);
            }
        }


        private string MenuItemsProperty { get; set; }
        private ObservableCollection<MainMenuModel> _menuItems;

        public ObservableCollection<MainMenuModel> MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                _menuItems = value;
                this.RaisePropertyChanged(MenuItemsProperty);
            }
        }

        private string MenuVisibilityProperty { get; set; }
        private Visibility _menuVisibility;
        public Visibility MenuVisibility
        {
            get { return _menuVisibility; }

            set
            {
                _menuVisibility = value;
                this.RaisePropertyChanged(MenuVisibilityProperty);
            }
        }

        private string AdminPadVisibilityProperty { get; set; }
        private Visibility _adminPadVisibility;
        public Visibility AdminPadVisibility
        {
            get { return _adminPadVisibility; }

            set
            {
                _adminPadVisibility = value;
                this.RaisePropertyChanged(AdminPadVisibilityProperty);
            }
        }

        #endregion

        public MainMenuViewModel(ILocalStorageService ls)
        {
            _ls = ls;
            this.MenuVisibility = Visibility.Hidden;
            this.OpenAdminDataManagementCommand = new RelayCommand(this.ExecuteOpenAdminDataManagementCommand, CanExecuteOpenAdminDataManagementCommand);
            Messenger.Default.Register<MenuMessage>(this, this.HandleShowMenuMessage);
            Messenger.Default.Register<AdminLoginMessage>(this, this.HandleAdminLoginMessage);
            //this.InitialzieMenuItems();

        }

        private void HandleShowMenuMessage(MenuMessage m)
        {
            if (m.Action.Equals("Show"))
            {
                this.MenuVisibility = Visibility.Visible;
            }
			else
			{
                this.MenuVisibility = Visibility.Hidden;
			}
        }

        private void HandleAdminLoginMessage(AdminLoginMessage m)
        {
            this.AdminPadVisibility = Visibility.Visible;
        }



        public void SetPadVisibility(string role)
        { 
        
        }
		
        public void InitialzieMenuItems()
        {
            this.MenuItems = new ObservableCollection<MainMenuModel>();
            MainMenuModel DataRoot = new MainMenuModel() { Header = "Data" };
            this.AddDataSubItems(DataRoot);
            this.MenuItems.Add(DataRoot);
            MainMenuModel ReportRoot = new MainMenuModel() { Header = "Reports" };
            this.AddReportSubItems(ReportRoot);
            this.MenuItems.Add(ReportRoot);

        }


        public void AddDataSubItems(MainMenuModel dataRoot)
        {
            MainMenuModel region = new MainMenuModel() { Header = " Region" };
            MainMenuModel district = new MainMenuModel() { Header = " District" };
            MainMenuModel employee = new MainMenuModel() { Header = " Employee" };
            MainMenuModel employeeTime = new MainMenuModel() { Header = " Employee Time" };
            MainMenuModel gender = new MainMenuModel() { Header = " Gender" };
            MainMenuModel inventory = new MainMenuModel() { Header = " Inventory" };

            dataRoot.SubItems.Add(district);
            dataRoot.SubItems.Add(employee);
            dataRoot.SubItems.Add(employeeTime);
            dataRoot.SubItems.Add(gender);
            dataRoot.SubItems.Add(inventory);
            dataRoot.SubItems.Add(region);
        }

        public void AddReportSubItems(MainMenuModel reportRoot)
        {
            MainMenuModel sales = new MainMenuModel() { Header = " Sales" };

            reportRoot.SubItems.Add(sales);
        }



        public void SelectedMenuItemChanged()
        {
            var item = this.SelectedMenuItem;

        }


        #region Execute/CanExecute

        private bool CanExecuteOpenAdminDataManagementCommand()
        {
            return true; 
        }

        private void ExecuteOpenAdminDataManagementCommand()
        {
            Messenger.Default.Send<NavigationMessage>(new NavigationMessage { Action = "AdminLogin" });
        }

        #endregion

    }
}