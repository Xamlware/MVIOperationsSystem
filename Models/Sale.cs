using System;
using System.ComponentModel.DataAnnotations;

namespace MVIOperations.Models
{
	public class Sale
	{
		public Sale()
		{
		}

		[Key]
		public int PK_Sale { get; set; }

		[Required(ErrorMessage = "Store id is required")]
		public int FK_Store { get; set; }

		[Required(ErrorMessage = "Sales date is required")]
		public DateTime SalesDate { get; set; }

		[Required(ErrorMessage = "Total sales is required")]
		public float TotalSales { get; set; }

		[Required(ErrorMessage = "Tax is required")]
		public float Tax { get; set; }

		[Required(ErrorMessage = "Payment type is required")]
		public PaymentType Payment { get; set; }

		[Required(ErrorMessage = "Employee id is required")]
		public string EmployeeId { get; set; }

		[Required(ErrorMessage = "Employee id is required")]
		public string TransactionId { get; set; }
	}
}