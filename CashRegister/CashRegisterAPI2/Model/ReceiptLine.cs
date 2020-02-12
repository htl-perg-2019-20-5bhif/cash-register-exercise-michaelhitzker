using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CashRegisterAPI2.Model
{
    public class ReceiptLine
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("product")]
        public Product Product { get; set; }

        [Required]
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [Required]
        [JsonPropertyName("totalPrice")]
        public double TotalPrice { get; set; }
    }
}
