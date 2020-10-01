using MVIOperationsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public interface ISyncService
	{
		List<Sync> SyncList { get; set; }

		void SyncOfflineDb();

		void AddTransactionToSyncTable(Sync s);
	}
}
