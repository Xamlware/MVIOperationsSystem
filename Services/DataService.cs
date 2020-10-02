using GalaSoft.MvvmLight.Ioc;
using MVIOperations.Models;
using MVIOperationsSystem.Data;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Models;
using MVIOperationsSystem.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MVIOperationsSystem.Services
{

	public class DataService<T> : IDataService<T>
	{

		ExecuteDataRequest edr = new ExecuteDataRequest();
		OfflineContext _dbOffline = new OfflineContext();
		IUnitOfWork unitOfWork = new UnitOfWork(new OfflineContext());
		ISyncService _ss = SimpleIoc.Default.GetInstance<ISyncService>();



		public T UpdateTable<T>(bool isConnected, T t, HttpRequestMethods method, string route = "", int? id = null) where T:class
		{
			T retVal = default(T);
			if (isConnected)
			{
				try
				{
					var x = UpdateOfflineDb(t, method, route, id);

					(t as District).PK_District = 0;
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
			else
			{
				// we're offline - add transaction to sync 
				_ss.AddTransactionToSyncTable(new Sync { Active = true, Entity = JsonConvert.SerializeObject(t), EntityType = route.Replace("api/","").Replace("/", ""), Method = method, Id = id }); ;
				return UpdateOfflineDb(t, method, route, id);

			}
		}

		private T UpdateOfflineDb<T>(T t, HttpRequestMethods method, string route, int? id) where T : class

		{
			try
			{
				switch (method)
				{
					case HttpRequestMethods.Post:
						{
							if (t is District)
							{
								var d =  (_dbOffline.District.Add(t as District));
								_dbOffline.SaveChanges();
								return d as T;
							}
							else if (t is Region)
							{
								var r = _dbOffline.Region.Add(t as Region);
								_dbOffline.SaveChanges();
								return r as T;
							}
							else if (t is Employee)
							{
								var e = _dbOffline.Employee.Add(t as Employee);
								_dbOffline.SaveChanges();
								return e as T;
							}

						}

						break;

					case HttpRequestMethods.Delete:
						if (t is District)
						{
							var dist = _dbOffline.District.FirstOrDefault(f => f.PK_District == id);
							_dbOffline.District.Remove(dist);
						}
						else if (t is Region)
						{
							var dist = _dbOffline.Region.FirstOrDefault(f => f.PK_Region == id); 
							_dbOffline.Region.Remove(t as Region);
						}
						else if (t is Employee)
						{
							var dist = _dbOffline.Employee.FirstOrDefault(f => f.PK_Employee == id);
							_dbOffline.Employee.Remove(t as Employee);
						}
						break;

					case HttpRequestMethods.Put:

						if (t is District)
						{
							var d = _dbOffline.District.First(f => f.PK_District == id);
							d.FK_Region = (t as District).FK_Region;
							d.DistrictName = (t as District).DistrictName;

						}
						else if (t is Region)
						{
							var r = _dbOffline.Region.First(d => d.PK_Region == id);
							r.RegionName = (r as Region).RegionName;

						}
						else if (t is Employee)
						{
							var em = t as Employee;
							var e = _dbOffline.Employee.First(d => d.PK_Employee == id);
							e.FK_Store = em.FK_Store;
							e.FK_State = em.FK_State;
							e.FK_Country = em.FK_Country;
							e.FK_Race = em.FK_Race;
							e.FK_Gender = em.FK_Gender;
							e.EmployeeId = em.EmployeeId;
							e.AspUserId = em.AspUserId;
							e.FirstName = em.FirstName; 
							e.MiddleName = em.MiddleName;
							e.LastName = em.LastName;
							e.NameSuffix = em.NameSuffix;
							e.Email = em.Email;
							e.Address = em.Address;
							e.Address1 = em.Address1;
							e.City = em.City;
							e.ZipCode = em.ZipCode;
						}
						break;
				}
				_dbOffline.SaveChanges();

			}
			catch (Exception e)
			{
			
			}

			return default(T);
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
				// offline
				var oc = new ObservableCollection<T>();
				if (t is District)
				{

					var resp = unitOfWork.District.GetAll();
					jsonString = JsonConvert.SerializeObject(resp);

				}
				else
				{
					if (t is Region)
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

