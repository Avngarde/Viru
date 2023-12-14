﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viru.Dto
{
    public class AddPaymentDto
    {
        public string? Description { get; set; }
        public string? Currency { get; set; }
        public float Value { get; set; }
        public int WalletId { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
