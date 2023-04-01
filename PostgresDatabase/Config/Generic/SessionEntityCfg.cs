using Domain.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgresDatabase.Config.Generic
{
    internal class SessionEntityCfg : IEntityTypeConfiguration<GameSession>
    {

        public void Configure(EntityTypeBuilder<GameSession> builder)
        {
            builder.ToTable("session", "main");
            builder.HasKey(e => e.Id).HasName("session_pkey");

            builder.HasDiscriminator(e => e.Type)
                .HasValue<GameSession>("session_base");

        }
    }
}
