using System.ComponentModel.DataAnnotations;

namespace E_magazin.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100,MinimumLength = 3)]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
