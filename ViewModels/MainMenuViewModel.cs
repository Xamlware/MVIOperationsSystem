using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVIOperationsSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {

        #region Commands
        public RelayCommand LoginCommand { get; private set; }

        #endregion

        private string _visibility = "Visible";

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

        public string Visibility
        {
            get { return _visibility; }

            set
            {
                _visibility = value;
                this.RaisePropertyChanged(Visibility);
            }
        }
        #endregion

        public MainMenuViewModel()
        {
            //this.LoginCommand = new RelayCommand(this.ExecuteLoginCommand, CanExecuteLoginCommand);

            this.InitialzieMenuItems();

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

        private bool CanExecuteLoginCommand()
        {
            return true; 
        }

        private void ExecuteLoginCommand()
        {
            //this.SelectedTabItem = this.
        }

        #endregion

    }
}