using MVIOperations.Models;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Models.DataRequestObjects;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public class RegionDataService : IRegionDataService
	{
		public async Task<RegionResponse> UpdateRegion(Region dr, HttpRequestMethods method)
		{
			var RegionResponse = await RegionDataRequest.UpdateRegion(dr, method);

			return RegionResponse;
		}


		public async Task<ObservableCollection<Region>> GetRegionList(HttpRequestMethods method)
		{
			return await RegionDataRequest.GetRegionList(method);
		}
	}
}
