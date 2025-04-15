using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class VehicleTransmissionEntityTypeConfiguration : IEntityTypeConfiguration<VehicleTransmission>
{
    public void Configure(EntityTypeBuilder<VehicleTransmission> builder)
    {
        builder.HasKey(e => e.Id).HasName("transmissions_pkey");

        builder.ToTable("vehicle_transmissions");

        builder.HasIndex(e => e.Transmission, "transmissions_name_key").IsUnique();

        builder.Property(e => e.Id)
            .HasDefaultValueSql("nextval('transmissions_id_seq'::regclass)")
            .HasColumnName("id");
        builder.Property(e => e.Transmission)
            .HasMaxLength(50)
            .HasColumnName("transmission");
    }
}