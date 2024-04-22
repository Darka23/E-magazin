using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_magazin.Data.Models
{
    public class CashDesk
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalSum { get; set; }
    }
}
