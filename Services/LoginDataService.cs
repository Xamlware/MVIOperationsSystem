using MilVetIndApi.Authentication.Models;
using MVIOperations.Models;
using MVIOperationsSystem.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public class LoginDataService : ILoginDataService
	{
		public async Task<LoginResponse> Login(LoginRequest lr)
		{
			var loginResponse = await DataRequest.Login(lr);

			return loginResponse;			
		}
	}
}
