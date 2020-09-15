using System;
using System.ComponentModel.DataAnnotations;

namespace MVIOperations.Models
{ 
	public class EmployeeDto
	{
		public EmployeeDto()
		{
		}
		[Key]
		public string EmployeeId { get; set; }
		public int PK_District { get; set; }
		public int PK_Region { get; set; }
		public int PK_Store { get; set; }
	}
}
