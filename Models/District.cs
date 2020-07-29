using System;
using System.ComponentModel.DataAnnotations;

namespace MVIOperations.Models
{
    public class District
    {
        [Key]
        public int PK_District { get; set; }

        [Required(ErrorMessage = "Region id is required")]
        public int FK_Region { get; set; }

        [Required(ErrorMessage = "District name is required")]
        public string DistrictName { get; set; }
    }
}

