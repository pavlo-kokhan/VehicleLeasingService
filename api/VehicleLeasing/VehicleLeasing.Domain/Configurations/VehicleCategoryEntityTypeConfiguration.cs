using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class VehicleCategoryEntityTypeConfiguration : IEntityTypeConfiguration<VehicleCategory>
{
    public void Configure(EntityTypeBuilder<VehicleCategory> builder)
    {
        builder.HasKey(e => e.Id).HasName("vehicle_categories_pkey");

        builder.ToTable("vehicle_categories");

        builder.HasIndex(e => e.Category, "vehicle_categories_category_name_key").IsUnique();

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Category)
            .HasMaxLength(50)
            .HasColumnName("category");
    }
}