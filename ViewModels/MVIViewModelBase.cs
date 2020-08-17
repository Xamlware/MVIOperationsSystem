using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.ViewModels
{
	public class MVIViewModelBase : ViewModelBase
	{

		public MVIViewModelBase()
		{ 
			Messenger.Default.Register<CleanUpMessage>(this, this.HandleCleanUpMessage);
		}

		private void HandleCleanUpMessage(CleanUpMessage cm)
		{
			Cleanup();
		}

		
	}
}
