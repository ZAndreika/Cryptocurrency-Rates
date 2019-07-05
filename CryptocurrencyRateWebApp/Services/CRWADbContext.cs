using CryptocurrencyRateWebApp.Models;
using System.Data.Entity;

namespace CryptocurrencyRateWebApp.DBWorkers {
    public class CRWADbContext : DbContext {
        public CRWADbContext() : base("DbConnectionString") {}

        public DbSet<User> Users { get; set; }
    }
}