using System.ComponentModel.DataAnnotations;

namespace ViruBackend.Dto
{
    public class AddPaymentTypeDto
    {
        public string? Name { get; set; }
        public string? Color { get; set; }
        public int WalletId { get; set; }
    }
}