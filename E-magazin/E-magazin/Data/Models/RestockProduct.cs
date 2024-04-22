using System.ComponentModel.DataAnnotations;

namespace E_magazin.Data.Models
{
    public class RestockProduct
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}
