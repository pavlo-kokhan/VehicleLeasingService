using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class VehicleFuelTypeEntityTypeConfiguration : IEntityTypeConfiguration<VehicleFuelType>
{
    public void Configure(EntityTypeBuilder<VehicleFuelType> builder)
    {
        builder.HasKey(e => e.Id).HasName("fuel_types_pkey");

        builder.ToTable("vehicle_fuel_types");

        builder.HasIndex(e => e.Type, "fuel_types_name_key").IsUnique();

        builder.Property(e => e.Id)
            .HasDefaultValueSql("nextval('fuel_types_id_seq'::regclass)")
            .HasColumnName("id");
        builder.Property(e => e.Type)
            .HasMaxLength(50)
            .HasColumnName("type");
    }
}