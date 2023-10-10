using Domain.Entities.General;
using Domain.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PostgresDatabase.Config.Generic
{
    public class ServiceLocalPortEntityCfg : IEntityTypeConfiguration<ServiceLocalPort>
    {
        public void Configure(EntityTypeBuilder<ServiceLocalPort> builder)
        {
            builder.ToTable("service_local_port", "config");
            builder.HasKey(e => e.Id).HasName("service_local_port_pkey");

            builder.Property(e => e.ServiceId).IsRequired();
            builder.Property(e => e.Port).IsRequired();

            builder
                .HasOne(e => e.Service)
                .WithMany(s => s.LocalPorts)
                .HasForeignKey(e => e.ServiceId)
                .IsRequired();
        }
    }
}
