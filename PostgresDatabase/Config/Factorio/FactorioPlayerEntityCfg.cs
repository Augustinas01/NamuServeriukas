using Domain.Entities.Factorio;
using Domain.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgresDatabase.Config.Factorio
{
    internal class FactorioPlayerEntityCfg : IEntityTypeConfiguration<FactorioPlayer>
    {
        public void Configure(EntityTypeBuilder<FactorioPlayer> builder)
        {



            //builder.Property(e => e.Id).HasColumnName("id");

            //builder
            //    .Property(e => e.JoinTimestamp)
            //    .HasColumnType("timestamp without time zone")
            //    .HasColumnName("join_timestamp");

            //builder
            //    .Property(e => e.LeaveTimestamp)
            //    .HasColumnType("timestamp without time zone")
            //    .HasColumnName("leave_timestamp");

            //builder.Property(e => e.SessionId).HasColumnName("session_id");

            //builder
            //    .HasOne(d => d.Session).WithMany(p => p.Players.Cast<FactorioPlayer>())
            //    .HasForeignKey(d => d.SessionId)
            //    .HasConstraintName("player_session_id_fkey");
        }
    }
}
