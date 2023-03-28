using DataAccessLayer.Entities.Factorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DataAccessLayer.Context
{
    public class PostgresContext : DbContext 
    {
        private readonly IConfiguration _configuration;

        //public PostgresContext() //For POWERSHELL
        //{

        //}
        public PostgresContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Postgres")); // _configuration.GetConnectionString("Postgres")
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("player_pkey");

                entity.ToTable("player", "factorio");

                entity.Property(e => e.Id).HasColumnName("id");

                entity
                    .Property(e => e.JoinTimestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("join_timestamp");

                entity
                    .Property(e => e.LeaveTimestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("leave_timestamp");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity
                    .HasOne(d => d.Session).WithMany(p => p.Players)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("player_session_id_fkey");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("session_pkey");

                entity.ToTable("session", "factorio");

                entity.Property(e => e.Id).HasColumnName("id");

                entity
                    .Property(e => e.StartTimestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("start_timestamp");

                entity
                    .Property(e => e.StopTimestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("stop_timestamp");
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //private void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
