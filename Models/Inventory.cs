using System;
using System.ComponentModel.DataAnnotations;

namespace MVIOperations.Models
{
	public class Inventory
	{
		public Inventory()
		{
		}

		[Key]
		public int PK_Inventory { get; set; }

		[StringLength(50, ErrorMessage = "The Inventory Name value cannot exceed 50 characters. ")]
		[Required]
		public string ItemName { get; set; }

	
	}
}
