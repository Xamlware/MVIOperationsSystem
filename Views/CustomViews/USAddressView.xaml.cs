using GalaSoft.MvvmLight.Ioc;
using MVIOperationsSystem.ViewModels.DataEditViewModels;
using System.Windows.Controls;

namespace MVIOperationsSystem.Views.CustomViews
{
	/// <summary>
	/// Interaction logic for USAddressControl.xaml
	/// </summary>
	public partial class USAddressView : UserControl
	{
		public EmployeeEditViewModel vm;

		public USAddressView()
		{
			this.vm = SimpleIoc.Default.GetInstance<EmployeeEditViewModel>();
			
			InitializeComponent();

			this.DataContext = this.vm;
		}

		private void ComboBoxAdv_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
