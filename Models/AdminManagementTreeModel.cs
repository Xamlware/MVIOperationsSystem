using System.Collections.ObjectModel;

namespace MVIOperationsSystem.Models
{
	public class AdminManagementTreeModel
	{

        public string Header { get; set; }

        public ObservableCollection<AdminManagementTreeModel> SubItems { get; set; }

        public AdminManagementTreeModel()

        {

            SubItems = new ObservableCollection<AdminManagementTreeModel>();

        }
    }
}
