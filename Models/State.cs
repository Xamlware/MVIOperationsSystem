using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVIOperations.Models
{
    public class State
    {
        public State()
        {
        }

        [Key]

        public int PK_State { get; set; }

        [Required(ErrorMessage = "State name is required.")]
        public string StateName { get; set; }

        [Required(ErrorMessage = "State abbreviation is required.")]
        public string Abbreviation { get; set; }

    }

}
