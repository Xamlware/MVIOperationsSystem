using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilVetIndApi.Authentication.Models
{
	public class LoginResponse
	{
		public string Token { get; set; }

		public DateTime Expiration { get; set; }

		public string Id { get; set; }

		public IList<string> Roles { get; set; }

		public string Status { get; set; }

		public string Message { get; set; }
	}
}
