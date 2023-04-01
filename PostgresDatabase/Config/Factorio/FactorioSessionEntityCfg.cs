
using Domain.Entities.Factorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgresDatabase.Config.Factorio
{
    internal class FactorioSessionEntityCfg : IEntityTypeConfiguration<FactorioSession>
    {
        public void Configure(EntityTypeBuilder<FactorioSession> builder)
        {
            builder
                .HasDiscriminator(e => e.Type)
                .HasValue("factorio");

        }
    }
}
