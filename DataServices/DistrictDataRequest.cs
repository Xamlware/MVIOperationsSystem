using MilVetIndApi.Authentication.Models;
using MVIOperations.Models;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.DataServices
{
	public class DistrictDataRequest
	{
		private static ExecuteDataRequest edr = new ExecuteDataRequest();

		public static async Task<DistrictResponse> UpdateDistrict(District dr)
		{
			// Initialization.  
			// RegInfoResponseObj responseObj = new RegInfoResponseObj();
			DistrictResponse resp = null;
			var result = "";
			try
			{
				var jsonString = JsonConvert.SerializeObject(dr);
				result = await edr.ExecuteRequest("api/district/", HttpRequestMethods.Post, jsonString);

				resp = JsonConvert.DeserializeObject<DistrictResponse>(result);
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