using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
