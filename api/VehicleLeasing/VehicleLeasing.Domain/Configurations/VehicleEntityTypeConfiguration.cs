using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class VehicleEntityTypeConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(e => e.Id).HasName("vehicles_pkey");

        builder.ToTable("vehicles");

        builder.HasIndex(e => e.Brand, "vehicle_brand_index");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Brand)
            .HasMaxLength(100)
            .HasColumnName("brand");
        builder.Property(e => e.CategoryId).HasColumnName("category_id");
        builder.Property(e => e.EstimatedPrice)
            .HasPrecision(15, 2)
            .HasColumnName("estimated_price");
        builder.Property(e => e.FuelTypeId).HasColumnName("fuel_type_id");
        builder.Property(e => e.LastModified)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("last_modified");
        builder.Property(e => e.Model)
            .HasMaxLength(100)
            .HasColumnName("model");
        builder.Property(e => e.StatusId).HasColumnName("status_id");
        builder.Property(e => e.TransmissionId).HasColumnName("transmission_id");
        builder.Property(e => e.Year).HasColumnName("year");

        builder.HasOne(d => d.Category).WithMany(p => p.Vehicles)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("vehicles_category_id_fkey");

        builder.HasOne(d => d.FuelType).WithMany(p => p.Vehicles)
            .HasForeignKey(d => d.FuelTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("vehicles_fuel_type_id_fkey");

        builder.HasOne(d => d.Status).WithMany(p => p.Vehicles)
            .HasForeignKey(d => d.StatusId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("vehicles_status_id_fkey");

        builder.HasOne(d => d.Transmission).WithMany(p => p.Vehicles)
            .HasForeignKey(d => d.TransmissionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("vehicles_transmission_id_fkey");
    }
}