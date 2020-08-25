using System;
using System.ComponentModel.DataAnnotations;

namespace MVIOperations.Models
{
	public class SaleItem
	{
		public SaleItem()
		{
		}

		[Key]
		public int PK_SaleItem { get; set; }

		[Required(ErrorMessage = "Sale id is required")]
		public int FK_Sale { get; set; }
	
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
