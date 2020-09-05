//using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MVIOperations.Models;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Services;
using MVIOperationsSystem.ViewModels.CustomViewModels;
using MVIOperationsSystem.ViewModels.DataEditViewModels;
using Microsoft.Practices.ServiceLocation;
using System.Runtime.CompilerServices;

namespace MVIOperationsSystem.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => (IServiceLocator)SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MainMenuViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<AdminManagementViewModel>();
            SimpleIoc.Default.Register<AdminDataViewModel>();
            SimpleIoc.Default.Register<DistrictEditViewModel>(); 
            SimpleIoc.Default.Register<RegionEditViewModel>();
            SimpleIoc.Default.Register<EmployeeEditViewModel>();
            SimpleIoc.Default.Register<InventoryEditViewModel>();
            SimpleIoc.Default.Register<NameViewModel>();
            SimpleIoc.Default.Register<AddressViewModel>();

            SimpleIoc.Default.Register<NotifyViewModel>();

            SimpleIoc.Default.Register<ILoginDataService, LoginDataService>();
            SimpleIoc.Default.Register<IDataService<District>, DataService<District>>();
            SimpleIoc.Default.Register<IDataService<Region>, DataService<Region>>();
            SimpleIoc.Default.Register<IDataService<Employee>, DataService<Employee>>();
            SimpleIoc.Default.Register<IDataService<Inventory>, DataService<Inventory>>();
            SimpleIoc.Default.Register<IDataService<State>, DataService<State>>();
            SimpleIoc.Default.Register<IDataService<Country>, DataService<Country>>();
            SimpleIoc.Default.Register<IDataService<Gender>, DataService<Gender>>();
            SimpleIoc.Default.Register<IDataService<Race>, DataService<Race>>();

            SimpleIoc.Default.Register<ILocalStorageService, LocalStorageService>();

        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public MainMenuViewModel MainMenuViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainMenuViewModel>();
            }
        }

        public LoginViewModel LoginViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }
       
        public AdminManagementViewModel AdminManagementViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AdminManagementViewModel>();
            }
        }

        public AdminDataViewModel AdminDataViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AdminDataViewModel>();
            }
        }

        public DistrictEditViewModel DistrictEditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DistrictEditViewModel>();
            }
        }

        public RegionEditViewModel RegionEditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RegionEditViewModel>();
            }
        }

        public EmployeeEditViewModel EmployeeEditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EmployeeEditViewModel>();
            }
        }

        public InventoryEditViewModel InventoryEditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InventoryEditViewModel>();
            }
        }


        //public NameViewModel NameViewModel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<NameViewModel>();
        //    }
        //}


        //public AddressViewModel AddressViewModel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<AddressViewModel>();
        //    }
        //}

        public NotifyViewModel NotifyViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NotifyViewModel>();
            }
        }


        public static void Cleanup()
        {
            Messenger.Default.Send<CleanUpMessage>(new CleanUpMessage());
        }
    }
} 