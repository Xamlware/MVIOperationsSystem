/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MVIOperationsSystem"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using MVIOperations.Models;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Services;
using MVIOperationsSystem.ViewModels.DataEditViewModels;

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
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MainMenuViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<AdminManagementViewModel>();
            SimpleIoc.Default.Register<DistrictEditViewModel>(); 
            SimpleIoc.Default.Register<RegionEditViewModel>();
            SimpleIoc.Default.Register<NotifyViewModel>();

            SimpleIoc.Default.Register<ILoginDataService, LoginDataService>();
            SimpleIoc.Default.Register<IDataService<District>, DataService<District>>();
            SimpleIoc.Default.Register<IDataService<Region>, DataService<Region>>();

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


        public NotifyViewModel NotifyViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NotifyViewModel>();
            }
        }


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
} 