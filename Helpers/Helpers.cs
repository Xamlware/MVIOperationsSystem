using GalaSoft.MvvmLight.Messaging;
using MVIOperationsSystem.Data;
using MVIOperationsSystem.Enums;
using MVIOperationsSystem.Messages;
using MVIOperationsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;

namespace MVIOperationsSystem.Helpers
{
	public static class Helpers
	{
		public static void Notify(string action, NotifyButtonEnum b, List<NotifyButtonLabelEnum> bl, string message, bool isError=false, string origin=null)
		{
			var am = new NotifyViewActionMessage
			{
				Origin=origin,
				Action = action,
				ButtonMode = b,
				ButtonLabels = bl,
				IsError = isError,
				Message = message
			};

			Messenger.Default.Send<NotifyViewActionMessage>(am);

		}

		public static void LogErrorMessage(bool isConnected, string error, string innerException)
		{
			MVIOperationsContext _db = new MVIOperationsContext();
			OfflineContext _oc = new OfflineContext();

			var e = new Error();
			e.Time = DateTime.Now;
			e.ErrorMessage = error;
			e.InnerException = innerException;

			if(isConnected)
			{
				_db.Error.Add(e);
			}
			else
			{
				_oc.Error.Add(e);
			}
		}
	}
}
