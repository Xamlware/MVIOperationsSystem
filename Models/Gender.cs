using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVIOperations.Models
{
    public class Gender
    {
        public Gender()
        {
        }


        [Key]
        [Required(ErrorMessage = "Gender id is required")]
        public int PK_Gender { get; set; }

        [Required(ErrorMessage = "Gender name is required")]
        public string GenderName { get; set; }
    }

}
