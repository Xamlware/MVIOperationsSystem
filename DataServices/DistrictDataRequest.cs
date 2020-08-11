using MilVetIndApi.Authentication.Models;
using MVIOperations.Models;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.DataServices
{
	public class DistrictDataRequest
	{
		private static ExecuteDataRequest edr = new ExecuteDataRequest();

		public static async Task<DistrictResponse> UpdateDistrict(District dr, HttpRequestMethods method)
		{
			// Initialization.  
			// RegInfoResponseObj responseObj = new RegInfoResponseObj();
			DistrictResponse resp = null;
			var result = "";
			//HttpRequestMethods method = HttpRequestMethods.Put;
			try
			{
				var jsonString = JsonConvert.SerializeObject(dr);

				result = await edr.ExecuteRequest("api/district/", method, jsonString);

				var sresp = JsonConvert.DeserializeObject<ObservableCollection<District>>(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				//resp = ex.Message.ToString();
			}

			return resp;
		}
	}
}