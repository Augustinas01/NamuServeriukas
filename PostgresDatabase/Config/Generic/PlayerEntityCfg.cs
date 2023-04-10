
using Domain.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgresDatabase.Config.Generic
{
    public class PlayerEntityCfg : IEntityTypeConfiguration<Player>
    {

        public void Configure(EntityTypeBuilder<Player> builder)
        {
            
            builder.ToTable("player", "main");

            builder.HasKey(e => e.Id).HasName("player_pkey");

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.JoinTimestamp).IsRequired();
            builder.Property(e => e.SessionId).IsRequired();

            builder
                .HasOne(p => p.Session)
                .WithMany(s => s.Players)
                .HasForeignKey(d => d.SessionId);
        }
    }
}
