using MVIOperationsSystem.Enums;

namespace MVIOperationsSystem.Messages
{
	public class NotifyResultMessage
	{
		public NotifyButtonLabelEnum ButtonSelected { get; set; }
		public string Action { get; set; }
		public string Origin { get; set; }
	}
}
