﻿using MilVetIndApi.Authentication.Models;
using MVIOperations.Models;
using MVIOperationsSystem.Enums;
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
	public class LoginDataRequest
	{
		private static readonly ExecuteDataRequest dr = new ExecuteDataRequest();

		public static async Task<LoginResponse> Login(LoginRequest lr)
		{
			// Initialization.  
			// RegInfoResponseObj responseObj = new RegInfoResponseObj();
			LoginResponse resp;
			var result="";
			try
			{
				var jsonString = JsonConvert.SerializeObject(lr);
				result = await dr.ExecuteRequest("api/authenticate/login", HttpRequestMethods.Post, jsonString);
			
				resp =  JsonConvert.DeserializeObject<LoginResponse>(result);
			}
			catch (Exception ex)
			{
				
				resp = null;
			}

			return resp;
		}
	}
}