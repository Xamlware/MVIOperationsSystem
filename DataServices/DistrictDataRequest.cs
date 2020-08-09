using MilVetIndApi.Authentication.Models;
using MVIOperations.Models;
using MVIOperationsSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.DataServices
{
	public class DistrictDataRequest
	{
		//private static ExecuteDataRequest dr = new ExecuteDataRequest();

		//public static async Task<LoginResponse> Login(LoginRequest lr)
		//{
		//	// Initialization.  
		//	// RegInfoResponseObj responseObj = new RegInfoResponseObj();
		//	LoginResponse resp = null;
		//	var result = "";
		//	try
		//	{
		//		var jsonString = JsonConvert.SerializeObject(lr);
		//		result = await dr.ExecuteRequest("api/authenticate/login", HttpRequestMethods.Post, jsonString);

		//		resp = JsonConvert.DeserializeObject<LoginResponse>(result);
		//	}
		//	catch (Exception ex)
		//	{

		//		resp = null;
		//	}

		//	return resp;
		//}
	}
}