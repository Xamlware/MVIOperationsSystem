using System.ComponentModel.DataAnnotations;

namespace MVIOperationsSystem.Models
{
	public class Country
	{
	[Key]
		public int PK_Country { get; set; }

		public string CountryName { get; set; }
	}
}
