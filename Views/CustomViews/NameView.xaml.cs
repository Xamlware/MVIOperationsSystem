using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.ViewModels.DataEditViewModels;
using System.Windows.Controls;

namespace MVIOperationsSystem.Views.CustomViews
{
	/// <summary>
	/// Interaction logic for NameControl.xaml
	/// </summary>
	public partial class NameView : UserControl
	{
		public EmployeeEditViewModel vm;
		public NameView()
		{

			this.vm = SimpleIoc.Default.GetInstance<EmployeeEditViewModel>();
			InitializeComponent();
			this.DataContext = this.vm;
		}

		private void EmployeeNameChanged(object sender, TextChangedEventArgs e)
		{
			var name = ((Syncfusion.Windows.Controls.Input.SfTextBoxExt)sender).Name;
			Messenger.Default.Send(new EmployeeNameChangedMessage { Action = name });
		}
	}
}
