using MVIOperationsSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Messages
{
	public class NotifyViewActionMessage
	{
		public string Message { get; set; }
		public NotifyButtonEnum ButtonMode { get; set; }
		public  List<NotifyButtonLabelEnum> ButtonLabels { get; set; }
		public bool IsError { get; set; }
		public string Action  { get; set; }
		public string Origin { get; set; }
	}
}
