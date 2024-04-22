using System.ComponentModel.DataAnnotations;

namespace E_magazin.Data.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
