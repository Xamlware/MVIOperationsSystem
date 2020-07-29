using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVIOperations.Models
{
    public class Race
    {
        public Race()
        {
        }

        [Key]
        [Required(ErrorMessage = "Race id is required")]
        public int PK_Race { get; set; }

        [Required(ErrorMessage = "Race name is required")] 
        public string RaceName { get; set; }
    }

}
