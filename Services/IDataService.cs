using System.Collections.ObjectModel;

namespace MVIOperationsSystem.Services
{
	public interface IDataService<T>
	{
		ObservableCollection<T> UpdateTable(T t, HttpRequestMethods method, string route);
		ObservableCollection<T> GetTableList(HttpRequestMethods method, string route);
	}
}
