using System.ComponentModel.DataAnnotations;

namespace CashRegisterAPI2.Model
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public double UnitPrice { get; set; }
    }
}
