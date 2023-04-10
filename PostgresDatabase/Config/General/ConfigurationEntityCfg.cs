
using Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgresDatabase.Config.General
{
    internal class ConfigurationEntityCfg : IEntityTypeConfiguration<Configuration>
    {
        public void Configure(EntityTypeBuilder<Configuration> builder)
        {
            builder.ToTable("configuration", "config");
            builder.HasKey(e => e.Key);

            builder.Property(e => e.Key).IsRequired();
            builder.Property(e => e.Value).IsRequired();
        }
    }
}
