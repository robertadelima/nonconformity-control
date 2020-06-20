using Microsoft.EntityFrameworkCore;
using NonconformityControl.Infra.Mapping;
using NonconformityControl.Models;

namespace NonconformityControl.Infra.Repositories
{
    public class NonconformityContext : DbContext
    {
        public NonconformityContext(DbContextOptions<NonconformityContext> options) : base(options)
        {
        }

        public DbSet<Nonconformity> Nonconformities { get; set; }
        public DbSet<Action> Actions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NonconformityMap());
            builder.ApplyConfiguration(new ActionMap());
        }
    }
}