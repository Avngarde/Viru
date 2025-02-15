﻿namespace ViruBackend.Dto
{
    public class AddPaymentDto
    {
        public string? Description { get; set; }
        public string? Currency { get; set; }
        public float Value { get; set; }
        public int WalletId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime Created {  get; set; }
    }
}
