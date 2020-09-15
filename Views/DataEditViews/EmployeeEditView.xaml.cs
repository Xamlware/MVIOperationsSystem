using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.ViewModels;
using Syncfusion.Windows.Controls.Input;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MVIOperationsSystem.Views.DataEditViews
{
	/// <summary>
	/// Interaction logic for EmployeeEditView.xaml
	/// </summary>
	public partial class EmployeeEditView : UserControl
	{
		public EmployeeEditView()
		{
			Messenger.Default.Register<UpdateSourceEmployeeMessage>(this, HandleUpdateSourceEmployeeMessage);
			InitializeComponent();
		}

	
		private void ListItemSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Messenger.Default.Send<ListItemChangedMessage>(new ListItemChangedMessage { Action = "TextChanged" });
		}

		private void EmployeeChanged(object sender, TextChangedEventArgs e)
		{
			Messenger.Default.Send(new EmployeeChangedMessage());
		}

		private void EmployeeChanged(object sender, SelectionChangedEventArgs e)
		{
			Messenger.Default.Send(new EmployeeChangedMessage());
		}

		private void HandleUpdateSourceEmployeeMessage(UpdateSourceEmployeeMessage obj)
		{
			try
			{
				BindingExpression be = EmployeeId.GetBindingExpression(SfTextBoxExt.TextProperty);
				be.UpdateSource();

				BindingExpression bf = FirstName.GetBindingExpression(SfTextBoxExt.TextProperty);
				bf.UpdateSource();

				BindingExpression bm = MiddleName.GetBindingExpression(SfTextBoxExt.TextProperty);
				bm.UpdateSource();

				BindingExpression bl = LastName.GetBindingExpression(SfTextBoxExt.TextProperty);
				bl.UpdateSource();

				BindingExpression bs = NameSuffix.GetBindingExpression(SfTextBoxExt.TextProperty);
				bs.UpdateSource();

				BindingExpression ba = AddressView.txtAddress.GetBindingExpression(SfTextBoxExt.TextProperty);
				ba.UpdateSource();

				BindingExpression ba2 = AddressView.txtAddress2.GetBindingExpression(SfTextBoxExt.TextProperty);
				ba2.UpdateSource();

				BindingExpression bt = AddressView.txtZipCode.GetBindingExpression(SfTextBoxExt.TextProperty);
				bt.UpdateSource();
			}
			catch (Exception e)
			{ 
			}
		}
	}
}



