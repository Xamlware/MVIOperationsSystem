using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Models.DataRequestObjects
{
    public class RegisterRequestResponse
    {
        public bool status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
}
