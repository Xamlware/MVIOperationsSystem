using MVIOperations.Models;
using MVIOperationsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public interface IDistrictDataService
	{
		Task<DistrictResponse> UpdateDistrict(District dr);
	}

}
