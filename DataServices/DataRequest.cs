using MVIOperations.Models;
using MVIOperationsSystem.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.DataServices
{
	public class DataRequest
	{
		private static ExecuteDataRequest dr = new ExecuteDataRequest();

		public static async Task<LoginRequest> Login(LoginRequest lr)
		{
			// Initialization.  
			// RegInfoResponseObj responseObj = new RegInfoResponseObj();
			LoginRequest resultlr = null;
			var result = "";
			try
			{
				var jsonString = JsonConvert.SerializeObject(lr);
				result = await dr.ExecuteRequest("api/authenticate/login", HttpRequestMethods.Post, jsonString);
			
				resultlr =  JsonConvert.DeserializeObject<LoginRequest>(result);
			}
			catch (Exception ex)
			{
				
				resultlr = null;
			}

			return resultlr;
		}
	}
}