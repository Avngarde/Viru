﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viru.Dto
{
    public class PaymentTypeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public int WalletId { get; set; }
    }
}
