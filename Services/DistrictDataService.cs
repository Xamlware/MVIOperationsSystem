using MVIOperations.Models;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Models;
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
	}
}