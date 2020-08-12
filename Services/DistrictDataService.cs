using MVIOperations.Models;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public class DistrictDataService : IDistrictDataService
	{
		public async Task<DistrictResponse> UpdateDistrict(District dr, HttpRequestMethods method)
		{
			var districtResponse = await DistrictDataRequest.UpdateDistrict(dr, method);

			return districtResponse;
		}
		public async Task<ObservableCollection<District>> GetDistrictList(HttpRequestMethods method)
		{
			return  await DistrictDataRequest.GetDistrictList(method);
		}
	}
}
