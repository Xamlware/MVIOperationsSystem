using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVIOperationsSystem.Views.DataEditViews
{
	/// <summary>
	/// Interaction logic for DistrictEditView.xaml
	/// </summary>
	public partial class DistrictEditView : UserControl
	{
		public DistrictEditView()
		{
			InitializeComponent();
		}

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void SfTextBoxExt_TextChanged(object sender, TextChangedEventArgs e)
		{
			Messenger.Default.Send<DistrictNameChangedMessage>(new DistrictNameChangedMessage { Action = "TextChanged" });
		}
	}
}



