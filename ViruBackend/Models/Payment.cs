﻿using System.ComponentModel.DataAnnotations;

namespace ViruBackend.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Currency { get; set; }
        public int Value { get; set; }

        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
