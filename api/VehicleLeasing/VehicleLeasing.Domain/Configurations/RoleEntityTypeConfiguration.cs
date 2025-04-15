using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id).HasName("roles_pkey");

        builder.ToTable("roles");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");

        builder.HasMany(d => d.Permissions).WithMany(p => p.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "RolePermission",
                r => r.HasOne<Permission>().WithMany()
                    .HasForeignKey("PermissionId")
                    .HasConstraintName("role_permissions_permission_id_fkey"),
                l => l.HasOne<Role>().WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("role_permissions_role_id_fkey"),
                j =>
                {
                    j.HasKey("RoleId", "PermissionId").HasName("role_permissions_pkey");
                    j.ToTable("role_permissions");
                    j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    j.IndexerProperty<int>("PermissionId").HasColumnName("permission_id");
                });
    }
}