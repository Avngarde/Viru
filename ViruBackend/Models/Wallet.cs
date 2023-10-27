using System.ComponentModel.DataAnnotations;

namespace ViruBackend.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Created { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
