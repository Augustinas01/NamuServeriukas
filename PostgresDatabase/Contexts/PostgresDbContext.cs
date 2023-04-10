using Domain.Entities.General;
using Domain.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;


namespace PostgresDatabase.Contexts
{
    public class PostgresDbContext : DbContext
    {

#if DEBUG
        public PostgresDbContext() { }
#endif
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }

        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<Player> Players { get; set; }
        public DbSet<ServiceSession> Sessions { get; set; }
#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("",
                x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "efcore"))
                .UseSnakeCaseNamingConvention();
            //base.OnConfiguring(optionsBuilder);
        }
#endif
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresDbContext).Assembly);
        }


    }
}
