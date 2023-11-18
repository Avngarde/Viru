using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viru.Dto
{
    public class WalletDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string? Color { get; set; }
        public float TotalBalance { get; set; }
    }
}
