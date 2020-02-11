using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashRegisterAPI2.Model
{
    public class Receipt
    {
        public int Id { get; set; }
        [Required]
        public List<ReceiptLine> ReceiptLines { get; set; }

        [Required]
        public long ReceiptTimestamp { get; set; }

        [Required]
        public double TotalPrice { get; set; }
    }
}
