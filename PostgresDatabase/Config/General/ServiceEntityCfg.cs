
using Domain.Entities.General;
using Domain.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgresDatabase.Config.General
{
    public class ServiceEntityCfg : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("service", "config");

            builder.HasKey(e => e.Id).HasName("service_pkey");

            builder.Property(e => e.Type).IsRequired();
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.PathToExe).IsRequired();

            builder
                .HasMany(e => e.Sessions)
                .WithOne(s => s.Service)
                .HasForeignKey(s => s.ServiceId);
        }
    }
}
