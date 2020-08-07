using System.Collections.ObjectModel;

namespace MVIOperationsSystem.Models
{
	public class MainMenuModel
	{

        public string Header { get; set; }

        public ObservableCollection<MainMenuModel> SubItems { get; set; }

        public MainMenuModel()
        {
            SubItems = new ObservableCollection<MainMenuModel>();
        }
    }
}
