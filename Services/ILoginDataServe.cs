using MVIOperations.Models;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public interface ILoginDataService
	{
		Task<LoginRequest> Login(LoginRequest lr);
	}
}
