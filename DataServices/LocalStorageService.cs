using MVIOperationsSystem.Data;
using MVIOperationsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.DataServices
{
	public class LocalStorageService
	{
		LocalStorageContext db = new LocalStorageContext();
		public string WriteValue(string key, string value)
		{
			var status = "Error";
			try
			{
				var ls = new LocalStorage();
				ls.KeyString = key;
				ls.ValueString = value;

				db.LocalStorage.Add(ls);
				db.SaveChanges();
				status = "OK";
			}
			catch(Exception e)
			{
				status = e.Message;
			}

			return status;
		}

		public string GetValue(string key)
		{
			var d = db.LocalStorage.Where(w => w.KeyString == key).FirstOrDefault();

			return d.ValueString;
		}

		public string ClearValue(string key)
		{
			return "";
		}

		public string ClearLocalStorage()
		{
			return "";
		}

	}
}
