using MVIOperations.Models;
using MVIOperationsSystem.DataServices;
using MVIOperationsSystem.Models;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public class DistrictDataService
	{
		public async Task<DistrictResponse> UpdateDistrict(District dr)
		{
			var districtResponse = await DistrictDataRequest.UpdateDistrict(dr);

			return districtResponse;
		}
	}
}