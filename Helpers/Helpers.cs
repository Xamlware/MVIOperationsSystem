using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Messages;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;

namespace MVIOperationsSystem.Helpers
{
	public static class Helpers
	{
		public static void Notify(string action, NotifyButtonEnum b, List<NotifyButtonLabelEnum> bl, string message, bool isError=false)
		{
			var am = new NotifyViewActionMessage
			{
				Action = action,
				ButtonMode = b,
				ButtonLabels = bl,
				IsError = isError,
				Message = message
			};

			Messenger.Default.Send<NotifyViewActionMessage>(am);

		}


	}
}
