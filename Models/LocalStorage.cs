using System.ComponentModel.DataAnnotations;

namespace MVIOperationsSystem.Models
{
    public class LocalStorage
    {
        [Key]
        public string KeyString { get; set; }
        public string ValueString { get; set; }
    }
}
