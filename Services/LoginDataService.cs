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
		public async Task<LoginRequest> Login(LoginRequest lr)
		{
			var loginRequest = await DataRequest.Login(lr);

			return loginRequest;			
		}
	}
}
