using MVIOperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public interface ILoginDataService
	{
		LoginRequest Login(LoginRequest lr);
	}
}
