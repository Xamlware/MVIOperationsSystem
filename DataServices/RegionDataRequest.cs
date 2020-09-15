using MilVetIndApi.Authentication.Models;
using MVIOperations.Models;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Models.DataRequestObjects;
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
	public class RegionDataRequest
	{
		private static ExecuteDataRequest edr = new ExecuteDataRequest();

		public static async Task<RegionResponse> UpdateRegion(Region dr, HttpRequestMethods method)
		{
			// Initialization.  
			// RegInfoResponseObj responseObj = new RegInfoResponseObj();
			RegionResponse resp = null;
			var result = "";
			//HttpRequestMethods method = HttpRequestMethods.Put;
			try
			{
				var jsonString = JsonConvert.SerializeObject(dr);

				result = await edr.ExecuteRequest("api/Region/", method, jsonString);

				var sresp = JsonConvert.DeserializeObject<ObservableCollection<Region>>(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				//resp = ex.Message.ToString();
			}

			return resp;
		}
		public static async Task<ObservableCollection<Region>> GetRegionList(HttpRequestMethods method)
		{
			ObservableCollection<Region> resp = null;
			var result = "";

			try
			{
				result = await edr.ExecuteRequest("api/Region/", method, null);
				resp = JsonConvert.DeserializeObject<ObservableCollection<Region>>(result);
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