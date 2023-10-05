using Microsoft.EntityFrameworkCore;
using System.Configuration;
using ViruBackend.Models;

namespace ViruBackend
{
    public class DbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options) { }

        public DbSet<Wallet> Wallets { get; set; }

    }
}
