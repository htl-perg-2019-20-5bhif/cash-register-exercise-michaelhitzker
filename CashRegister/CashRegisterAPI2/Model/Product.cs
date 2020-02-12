using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CashRegisterAPI2.Model
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [Required]
        [JsonPropertyName("unitPrice")]
        public double UnitPrice { get; set; }
    }
}
