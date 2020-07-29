using System;
using System.ComponentModel.DataAnnotations;

namespace MVIOperations.Models
{
	public class Employee
	{
		public Employee()
		{
		}

		[Key]
		public int PK_Employee { get; set; }
		[Required(ErrorMessage = "Store Id Required")]
		public int FK_Store { get; set; }

		[Required(ErrorMessage = "Store Id Required")]
		public int FK_State { get; set; }

		[Required(ErrorMessage = "Store Id Required")]
		public int FK_Race { get; set; }

		[Required(ErrorMessage = "Store Id Required")]
		public int FK_Gender { get; set; }

		[Required(ErrorMessage = "Employee Id Required")]
		public string EmployeeId { get; set; }

		[Required(ErrorMessage = "Asp User id Required")]
		public string AspUserId { get; set; }

		[Required(ErrorMessage = "Store Id Required")]
		public string EmployeeName { get; set; }

		[Required(ErrorMessage = "Store Id Required")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Store Id Required")]
		public string Address { get; set; }

		[Required(ErrorMessage = "Store Id Required")]
		public string Address1 { get; set; }

		[Required(ErrorMessage = "Store Id Required")]
		public string City { get; set; }

	}
}
