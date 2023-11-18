using System.Drawing;

namespace ViruBackend.Dto
{
    public class AddWalletDto
    {
        public string? Name { get; set; }
        public DateTime Created { get; set; }
        public string? Color { get; set; }
        public float TotalBalance { get; set; }
    }
}
