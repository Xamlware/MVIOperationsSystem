using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{

	public class DataService<T> : IDataService<T>
	{
		ExecuteDataRequest edr = new ExecuteDataRequest();
		public T UpdateTable(T t, HttpRequestMethods method, string route = "", int? id = null)
		{
			T retVal = default(T);
			try
			{
				var jsonString = JsonConvert.SerializeObject(t);
				var result = edr.ExecuteRequest(route, method, jsonString, id);

				if (method.ToString() != "Put")
				{
					retVal = JsonConvert.DeserializeObject<T>(result.ToString());
				}
			}
			catch (Exception e)
			{


			}
			return retVal;
		}

		public ObservableCollection<T> GetTableList(HttpRequestMethods method, string route)
		{
			try
			{
				var task = edr.ExecuteRequest(route, method);
				if (task.Result.Contains("Exception"))
				{
					return null;
				}
				else
				{
					return JsonConvert.DeserializeObject<ObservableCollection<T>>(task.Result);
				}
			}
			catch (Exception e)
			{
				return null;
			}
			
		}
	}
}
