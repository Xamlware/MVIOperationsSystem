using GalaSoft.MvvmLight.Ioc;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MVIOperationsSystem.Views
{
	/// <summary>
	/// Interaction logic for AdminDataView.xaml
	/// </summary>
	public partial class AdminDataView : UserControl
	{

		AdminDataViewModel vm = SimpleIoc.Default.GetInstance<AdminDataViewModel>();
		public AdminDataView()
		{
			InitializeComponent();
		}

		private void OnSelectedActionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (e.NewValue != null)
			{
				var item = ((AdminManagementTreeModel)e.NewValue).Header.Trim();
				vm.AddTabItem(item);
			}
		}

		private void ActionTree_Loaded(object sender, RoutedEventArgs e)
		{
		}
	}
}







