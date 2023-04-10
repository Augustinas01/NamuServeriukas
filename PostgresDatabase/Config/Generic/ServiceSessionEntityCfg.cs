using Domain.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgresDatabase.Config.Generic
{
    internal class ServiceSessionEntityCfg : IEntityTypeConfiguration<ServiceSession>
    {

        public void Configure(EntityTypeBuilder<ServiceSession> builder)
        {
            builder.ToTable("session", "main");
            builder.HasKey(e => e.Id).HasName("session_pkey");

            builder.Property(e => e.ServiceId).IsRequired();
            builder.Property(e => e.StartTimestamp).IsRequired();

            builder
                .HasMany(e => e.Players)
                .WithOne(p => p.Session)
                .HasForeignKey(p => p.SessionId);

            builder
                .HasOne(e => e.Service)
                .WithMany(s => s.Sessions)
                .HasForeignKey(e => e.ServiceId);
        }
    }
}
