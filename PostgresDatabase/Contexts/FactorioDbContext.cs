using Domain.Entities.Factorio;
using Domain.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace PostgresDatabase.Contexts
{
    public class FactorioDbContext : DbContext
    {

        public FactorioDbContext(DbContextOptions<FactorioDbContext> options) : base(options) { }

        public DbSet<FactorioPlayer> Players { get; set; }
        public DbSet<FactorioSession> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FactorioDbContext).Assembly);
        }


    }
}
