using System;
using System.ComponentModel.DataAnnotations;

namespace MVIOperationsSystem.Models
{
	public class Error
	{
		[Key]
		public int PK_Error { get; set; }
		public DateTime Time { get; set; }
		public string ErrorMessage { get; set; }
		public string InnerException { get; set; }
	}
}
