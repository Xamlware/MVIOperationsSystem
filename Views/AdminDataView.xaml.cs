using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.ViewModels;
using MVIOperationsSystem.Views.DataEditViews;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Xamlware.Framework.Extensions;

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
				//var objName = "MVIOperationsSystem.Views.DataEditViews." + item + "EditView";
				////var t = typeof(DistrictEditView).AssemblyQualifiedName;
				//Console.WriteLine(t);
				////const string objectToInstantiate = t; //"SampleProject.Domain.MyNewTestClass, MyTestProject";
				//var editObject = Activator.CreateInstance(Type.GetType(objName));

				//TabControlExt tc = new TabControlExt();
				var found = false;
				var index = -1;
				switch (item)
				{
					case "District":
						//vm.AddTabItem(e, item);
						//if (!this.AdminTabControl.Items.Contains("District"))
						//{
						foreach (var t in this.AdminTabControl.Items)
						{
							index++;
							if (((TabItemExt)t).Header.Equals(item))
							{

								found = true;
								break;
							}
						}

						if (!found)
						{
							var districtTab = new TabItemExt();
							districtTab.Header = item;
							districtTab.Content = new DistrictEditView();
							this.AdminTabControl.Items.Add(districtTab);
						}
					
						break;

					case "Employee":
						var employeeTab = new TabItemExt();
						employeeTab.Header = item;
						employeeTab.Content = new EmployeeEditView();
						this.AdminTabControl.Items.Add(employeeTab);
						break;
					//case "Gender":
					//	this.ContentPresenter.Content = new GenderEditView();
					//	break;
					//case "Inventory":
					//	this.ContentPresenter.Content = new InventoryEditView();
					//	break;
					case "Region":
						var regionTab = new TabItemExt();
						regionTab.Header = item;
						regionTab.Content = new RegionEditView();
						this.AdminTabControl.Items.Add(regionTab);
						break;

						// Creating an instances of TabControl and adding the tabitems into the TabControl
				}



				//if (((AdminManagementTreeModel)e.OldValue).Header.IsNotNullOrEmpty())
				//{
				//	Messenger.Default.Send(new CleanUpMessage());
				//}

				
		}


		}

		private void ActionTree_Loaded(object sender, RoutedEventArgs e)
		{
			//var vm = SimpleIoc.Default.GetInstance<AdminDataViewModel>();
			//vm.InitializeActionList();
		}
	}
}







