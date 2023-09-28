using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViruBackend.Models;

namespace ViruBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        DbContext db = new();

        [HttpGet]
        public async Task<Wallet> GetWallet(int id)
        {
            Wallet wallet = await db.Wallets.FindAsync(id);
            return wallet;
        }
    }
}
