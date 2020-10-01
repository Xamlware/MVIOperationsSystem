using MVIOperations.Models;
using MVIOperationsSystem.Enums;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public interface IDataService<T>
	{
		T UpdateTable<T>(bool isConnected, T t, HttpRequestMethods method, string route, int? id) where T:class;
		
		ObservableCollection<T> GetTableList(bool isConnected, T t, HttpRequestMethods method, string route);
	}
}
