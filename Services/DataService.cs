using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{

	public class DataService<T> : IDataService<T>
	{
		ExecuteDataRequest edr = new ExecuteDataRequest();
		public async Task<ObservableCollection<T>> UpdateTable(T t, HttpRequestMethods method, string route)
		{
			var jsonString = JsonConvert.SerializeObject(t);
			var result = await edr.ExecuteRequest(route, method, jsonString).Result;
			ObservableCollection<T> sresp = JsonConvert.DeserializeObject<ObservableCollection<T>>(result);

			return sresp;
		}

		public async Task<ObservableCollection<T>> GetTableList(HttpRequestMethods method, string route)
		{
			var result = await edr.ExecuteRequest(route, method).Result;
			var sresp = JsonConvert.DeserializeObject<ObservableCollection<T>>(result);

			return sresp;
		}
	}
}
