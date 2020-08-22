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

		[Required(ErrorMessage = "State Id Required")]
		public int FK_State { get; set; }

		[Required(ErrorMessage = "Country Id Required")]
		public int FK_Country { get; set; }

		[Required(ErrorMessage = "RaceRequired")]
		public int FK_Race { get; set; }

		[Required(ErrorMessage = "Gender Required")]
		public int FK_Gender { get; set; }

		[Required(ErrorMessage = "Employee Id Required")]
		public string EmployeeId { get; set; }

		[Required(ErrorMessage = "Asp User id Required")]
		public string AspUserId { get; set; }

		[Required(ErrorMessage = "First Name Required")]
		public string FirstName { get; set; }

		public string MiddleName { get; set; }


		[Required(ErrorMessage = "Last Name Required")]
		public string LastName { get; set; }

		public string NameSuffix { get; set; }

		[Required(ErrorMessage = "Email Required")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Address is Required")]
		public string Address { get; set; }


		public string Address1 { get; set; }

		[Required(ErrorMessage = "City required")]
		public string City { get; set; }

		[Required(ErrorMessage = "State is Required")] 
		public string State { get; set; }

		
	}
}
