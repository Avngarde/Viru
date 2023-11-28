using System.ComponentModel.DataAnnotations;

namespace ViruBackend.Models
{
    public class PaymentType
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public int WalletId { get; set; }
    }
}