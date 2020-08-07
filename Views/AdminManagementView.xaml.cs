using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
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

namespace MVIOperationsSystem.Views
{
	/// <summary>
	/// Interaction logic for AdminManagementView.xaml
	/// </summary>
	public partial class AdminManagementView : UserControl
	{
		public AdminManagementView()
		{
			InitializeComponent();
			Messenger.Default.Register<NavigationMessage>(this, HandleNavigationMessage);

		}

		private void HandleNavigationMessage(NavigationMessage obj)
		{
			this.ActionTree.ClearSelection();
			this.ActionTree.SelectedTreeItem = this.ActionTree.Items[0];
		}

		private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			var item = ((AdminManagementTreeModel) e.NewValue).Header;
		}
	}
}
