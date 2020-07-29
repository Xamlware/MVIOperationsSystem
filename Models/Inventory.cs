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

	}
}
