using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DutchTreatEmpty.Data
{
    public class DutchContext : IdentityDbContext<StoreUser>
    {
        private readonly IConfiguration config;

        public DutchContext(IConfiguration config)
        {
            this.config = config;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(this.config["ConnectionString:DutchContext"]);
        }
    }
}
