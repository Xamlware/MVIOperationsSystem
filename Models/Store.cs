using System;
using System.ComponentModel.DataAnnotations;

namespace MVIOperations.Models
{
	public class Store
	{
		public Store()
		{
		}

		[Key]
		public int PK_Store { get; set; }

		[Required(ErrorMessage ="District id is required")]
		public int FK_District { get; set; }

		[Required(ErrorMessage = "Store name is required")]
		public string StoreName { get; set; }

		[Required(ErrorMessage = "Store abbreviation is required")]
		public string StoreAbbreviation { get; set; }

		public int LastId { get; set; }
	}
}
