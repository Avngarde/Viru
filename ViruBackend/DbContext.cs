using Microsoft.EntityFrameworkCore;
using System.Configuration;
using ViruBackend.Models;

namespace ViruBackend
{
    public class DbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString);
        }
    }
}
