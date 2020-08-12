using MVIOperationsSystem.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Models.DataRequestObjects
{
	public class DataRequest<T>
	{
			private static ExecuteDataRequest edr = new ExecuteDataRequest();

			public async Task<T> UpdateTable(T t, HttpRequestMethods method)
			{
			// Initialization.  
			// RegInfoResponseObj responseObj = new RegInfoResponseObj();
			T resp = default(T);
				var result = "";
				//HttpRequestMethods mehod = HttpRequestMethods.Put;
				try
				{
					var jsonString = JsonConvert.SerializeObject(t);

					result = await edr.ExecuteRequest("api/district/", method, jsonString);

					var sresp = JsonConvert.DeserializeObject<ObservableCollection<T>>(result);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					//resp = ex.Message.ToString();
				}

				return resp;
			}
		
		//public static async Task<ObservableCollection<District>> GetDistrictList(HttpRequestMethods method)
		//	{
		//		ObservableCollection<District> resp = null;
		//		var result = "";

		//		try
		//		{
		//			result = await edr.ExecuteRequest("api/district/", method, null);
		//			resp = JsonConvert.DeserializeObject<ObservableCollection<District>>(result);
		//		}
		//		catch (Exception ex)
		//		{
		//			Console.WriteLine(ex.Message);
		//			//resp = ex.Message.ToString();
		//		}

		//		return resp;
		//	}
		//}
	}
}

