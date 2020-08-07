using MVIOperationsSystem.Data;
using MVIOperationsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamlware.Framework.Extensions;

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
				if (ValueExists(key))
				{
					this.ClearValue(key);
				}
				
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

		public bool ValueExists(string key)
		{
			return (GetValue(key).IsNotNullOrEmpty());
		}


		public string GetValue(string key)
		{
			var d = db.LocalStorage.Where(w => w.KeyString == key).FirstOrDefault();
			if(d == null || d.ValueString.IsNullOrEmpty())
			{
				return "";
			}

			return d.ValueString;
		}



		public bool ClearValue(string key)
		{
			var retVal = true;
			try
			{
				var ls = db.LocalStorage.Where(r => r.KeyString == key).FirstOrDefault();
				db.LocalStorage.Remove(ls);
				db.SaveChanges();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				retVal = false;
			}

			return retVal;
			
		}

		public void ClearLocalStorage()
		{
			var list = db.LocalStorage.ToList() ;
			foreach (var ls in list)
			{
				this.ClearValue(ls.KeyString);
			}

			db.SaveChanges();
		}

	}
}
