using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Models.DataRequestObjects
{
	public class RegionResponse
	{
		public ObservableCollection<RegionResponse> RegionList { get; set; }
	}
}
