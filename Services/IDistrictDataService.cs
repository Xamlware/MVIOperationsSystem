using MVIOperations.Models;
using MVIOperationsSystem.Models;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public interface IDistrictDataService
	{
		Task<DistrictResponse> UpdateDistrict(District dr, HttpRequestMethods method);
		//Task<System.Collections.ObjectModel.ObservableCollection<District>> GetDistrict
	}

}
