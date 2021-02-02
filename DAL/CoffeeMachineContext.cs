using Microsoft.EntityFrameworkCore;
using NetwrixCoffee.Models;

namespace NetwrixCoffee.DAL
{
    public class CoffeeMachineContext 
        : DbContext
    {
        public CoffeeMachineContext(
            DbContextOptions<CoffeeMachineContext> options)
            : base(options)
        {
        }


        public DbSet<CoffeeMachineRecord> CoffeeMachineRecords { get; set; }


        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoffeeMachineRecord>().ToTable("CoffeeMachineRecord");
        }
    }
}
