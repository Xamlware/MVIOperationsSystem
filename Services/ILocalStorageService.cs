using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public interface ILocalStorageService
	{
		string WriteValue(string key, string value);

		bool ValueExists(string key);

		string GetValue(string key);
	
		bool ClearValue(string key);

		void ClearLocalStorage();
		
	}
}
