using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CashRegisterAPI2.Model
{
    public class Receipt
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("receiptTimestamp")]
        public long ReceiptTimestamp { get; set; }

        [Required]
        [JsonPropertyName("receiptLines")]
        public List<ReceiptLine> ReceiptLines { get; set; }

        [Required]
        [JsonPropertyName("totalPrice")]
        public double TotalPrice { get; set; }
    }
}
