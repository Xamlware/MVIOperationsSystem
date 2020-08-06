using MilVetIndApi.Authentication.Models;
using MVIOperations.Models;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public interface ILoginDataService
	{
		Task<LoginResponse> Login(LoginRequest lr);
	}
}
