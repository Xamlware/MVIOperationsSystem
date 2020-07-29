using System;
using System.ComponentModel.DataAnnotations;

namespace MVIOperations.Models
{
	public class SalesItem
	{
		public SalesItem()
		{
		}

		[Key]
		public int PK_SalesItem { get; set; }

		public int FK_Sales { get; set; }
		[Required(ErrorMessage = "Inventory id is required")]
		public int FK_Inventory { get; set; }

		[Required(ErrorMessage = "Quantity is required")]
		public float Quantity { get; set; }

		[Required(ErrorMessage = "Price is required")]
		public float Price { get; set; }

		[Required(ErrorMessage = "Discount is required")]
		public float Discount { get; set; }

	}
}
