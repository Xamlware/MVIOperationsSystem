using MVIOperations.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Models
{
	public class DistrictResponse
	{
		public ObservableCollection<District> DistrictList { get; set; }
	}
}
