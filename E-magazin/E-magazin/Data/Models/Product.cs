using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_magazin.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(100,MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        public Manufacturer Manufacturer { get; set; }

        [Required]
        public int UnitOfMeasureId { get; set; }

        [Required]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public bool IsSoldOut { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal BuyingPrice { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal SellingPrice { get; set; }
    }
}
