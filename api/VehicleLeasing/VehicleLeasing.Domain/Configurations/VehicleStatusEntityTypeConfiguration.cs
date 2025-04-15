using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class VehicleStatusEntityTypeConfiguration : IEntityTypeConfiguration<VehicleStatus>
{
    public void Configure(EntityTypeBuilder<VehicleStatus> builder)
    {
        builder.HasKey(e => e.Id).HasName("vehicle_statuses_pkey");

        builder.ToTable("vehicle_statuses");

        builder.HasIndex(e => e.Status, "vehicle_statuses_status_name_key").IsUnique();

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .HasColumnName("status");
    }
}