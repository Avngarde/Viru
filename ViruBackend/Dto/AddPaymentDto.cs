namespace ViruBackend.Dto
{
    public class AddPaymentDto
    {
        public string? Description { get; set; }
        public string? Currency { get; set; }
        public int Value { get; set; }
        public int WalletId { get; set; }
    }
}
