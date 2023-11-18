using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ViruBackend.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Created { get; set; }
        public string? Color { get; set; }
        public float TotalBalance { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
