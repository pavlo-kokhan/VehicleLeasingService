using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id).HasName("users_pkey");

        builder.ToTable("users");

        builder.HasIndex(e => e.Email, "users_email_index");

        builder.HasIndex(e => e.Email, "users_email_key").IsUnique();

        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        builder.Property(e => e.Email).HasColumnName("email");
        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .HasColumnName("name");
        builder.Property(e => e.PasswordHash).HasColumnName("password_hash");
        builder.Property(e => e.RoleId).HasColumnName("role_id");
        builder.Property(e => e.Surname)
            .HasMaxLength(100)
            .HasColumnName("surname");

        builder.HasOne(d => d.Role).WithMany(p => p.Users)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("users_role_id_fkey");
    }
}