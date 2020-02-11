using System.ComponentModel.DataAnnotations;

namespace CashRegisterAPI2.Model
{
    public class ReceiptLine
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public int AmountPieces { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public int ReceiptId { get; set; }

        public Receipt Receipt { get; set; }
    }
}
