using MVIOperations.Models;
using MVIOperationsSystem.Data;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Repositories;
using Newtonsoft.Json;
using Syncfusion.Data.Extensions;
using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace MVIOperationsSystem.Services
{

	public class DataService<T> : IDataService<T>
	{

		ExecuteDataRequest edr = new ExecuteDataRequest();
		OfflineContext _dbOffline = new OfflineContext();
		IUnitOfWork unitOfWork = new UnitOfWork(new OfflineContext());


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



		public ObservableCollection<T> GetTableList(bool isConnected, T t, HttpRequestMethods method, string route)
		{ 

			string jsonString = null;
			if (isConnected)
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
			else
			{

				var oc = new ObservableCollection<T>();
				if (t is District)
				{
					
					var resp = unitOfWork.District.GetAll();
					jsonString = JsonConvert.SerializeObject(resp);

				}
				else
				{
					if(t is Region)
					{
						var resp = unitOfWork.Region.GetAll();
						jsonString = JsonConvert.SerializeObject(resp);

					}
				}

				oc = JsonConvert.DeserializeObject<ObservableCollection<T>>(jsonString);
				return oc;

			}
		}

		//public ObservableCollection<T> GetOfflineTableList(T t)
		//{
		//	var oc = new ObservableCollection<T>();
		//	try
		//	{
		//		if (t is District)
		//		{
		//			oc = (ObservableCollection<T>)unitOfWork.District.GetAll();
		//			return oc;
		//		}

		//	}
		//	catch (Exception e)
		//	{
		//		return null;
		//	}

		//	return null;
		//}
	}
}

