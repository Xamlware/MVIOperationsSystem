using MVIOperations.Models;
using MVIOperationsSystem.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public interface IDistrictDataService
	{
		Task<DistrictResponse> UpdateDistrict(District dr, HttpRequestMethods method);
		Task<ObservableCollection<District>> GetDistrictList(HttpRequestMethods method);
	}

}
