using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Models
{
    public class Region
    {
        [Key]
        public int PK_Region { get; set; }
 
        [StringLength(50, ErrorMessage = "The Region Name value cannot exceed 50 characters. ")]
        [Required]
        public string RegionName { get; set; }
    }
}
