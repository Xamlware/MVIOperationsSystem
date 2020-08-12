using MVIOperations.Models;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Models.DataRequestObjects;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public interface IRegionDataService
	{
		Task<RegionResponse> UpdateRegion(Region dr, HttpRequestMethods method);
		Task<ObservableCollection<Region>> GetRegionList(HttpRequestMethods method);
	}

}
