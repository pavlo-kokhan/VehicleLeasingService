using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(e => e.Id).HasName("permissions_pkey");

        builder.ToTable("permissions");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .HasColumnName("name");
    }
}