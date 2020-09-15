using MVIOperationsSystem.Enums;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public interface IDataService<T>
	{
		T UpdateTable(T t, HttpRequestMethods method, string route, int? id);
		
		ObservableCollection<T> GetTableList(bool isConnected, T t, HttpRequestMethods method, string route);
		
		//ObservableCollection<T> GetOfflineTableList(HttpRequestMethods method, string route);
	}
}
