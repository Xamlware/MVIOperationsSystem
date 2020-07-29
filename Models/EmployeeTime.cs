using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVIOperations.Models
{
	public class EmployeeTime
	{
		public EmployeeTime()
		{
		}

		[Key]
		public int PK_EmployeeTime { get; set; }

		[Required]
		public int FK_Store { get; set; }

		[Required(ErrorMessage ="Employee Id is required")]
		public string EmployeeId { get; set; }

		[Required(ErrorMessage = "Time stamp is required")]
		public DateTime TimeStamp { get; set; }


	}
}
