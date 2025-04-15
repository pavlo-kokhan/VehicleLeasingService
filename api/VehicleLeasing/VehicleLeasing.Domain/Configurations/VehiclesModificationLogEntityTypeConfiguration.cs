using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class VehiclesModificationLogEntityTypeConfiguration : IEntityTypeConfiguration<VehiclesModificationLog>
{
    public void Configure(EntityTypeBuilder<VehiclesModificationLog> builder)
    {
        builder.HasKey(e => e.Id).HasName("vehicles_modification_log_pkey");

        builder.ToTable("vehicles_modification_log");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Operation)
            .HasMaxLength(50)
            .HasColumnName("operation");
        builder.Property(e => e.Time)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("time");
        builder.Property(e => e.VehicleId).HasColumnName("vehicle_id");

        builder.HasOne(d => d.Vehicle).WithMany(p => p.VehiclesModificationLogs)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("vehicles_modification_log_vehicle_id_fkey");
    }
}