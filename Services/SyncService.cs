using MVIOperations.Models;
using MVIOperationsSystem.Data;
using MVIOperationsSystem.Models;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace MVIOperationsSystem.Services
{
	public class SyncService : ISyncService
	{
		private OfflineContext _db = new OfflineContext();
		private MVIOperationsContext _oc = new MVIOperationsContext();
		public SyncService()
		{
		}

		public List<Sync> SyncList { get; set; }

		public void SyncOfflineDb()
		{
			try
			{
				var list = _db.Sync.ToList<Sync>().Where(s => s.Active).ToList();
				if (list.Count > 0)
				{
					foreach (var sync in list)
					{
						switch (sync.EntityType)
						{
							case "district":
								var t = Newtonsoft.Json.JsonConvert.DeserializeObject<District>(sync.Entity);
								_oc.District.Add(t);
								_oc.SaveChanges();
								break;

						}
					}
				}
			}
			catch(Exception e)
			{
				var ex = e.Message;
			}
		}

		public void AddTransactionToSyncTable(Sync s)
		{
			try
			{
				_db.Sync.Add(s);
				_db.SaveChanges();
			}
			catch (Exception e)
			{
			}
		}
	}
}
