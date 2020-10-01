using MVIOperationsSystem.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVIOperationsSystem.Models
{
	public class Sync
	{
		[Key]
		public int PK_Sync { get; set; }

		public HttpRequestMethods Method { get; set; }

		public string EntityType { get; set; }

		public string Entity { get; set; }

		public int? Id { get; set; }
		public bool Active { get; set; }
	}
}
