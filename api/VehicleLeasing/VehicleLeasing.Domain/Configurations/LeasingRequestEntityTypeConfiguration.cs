using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class LeasingRequestEntityTypeConfiguration : IEntityTypeConfiguration<LeasingRequest>
{
    public void Configure(EntityTypeBuilder<LeasingRequest> builder)
    {
        builder.HasKey(e => e.Id).HasName("leasing_requests_pkey");

        builder.ToTable("leasing_requests");

        builder.HasIndex(e => e.Date, "leasing_request_date_index");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Date).HasColumnName("date");
        builder.Property(e => e.FixedPrice)
            .HasPrecision(15, 2)
            .HasColumnName("fixed_price");
        builder.Property(e => e.LastModified)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("last_modified");
        builder.Property(e => e.StatusId).HasColumnName("status_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.VehicleId).HasColumnName("vehicle_id");

        builder.HasOne(d => d.Status).WithMany(p => p.LeasingRequests)
            .HasForeignKey(d => d.StatusId)
            .HasConstraintName("fk_status_id");

        builder.HasOne(d => d.User).WithMany(p => p.LeasingRequests)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("leasing_requests_user_id_fkey");

        builder.HasOne(d => d.Vehicle).WithMany(p => p.LeasingRequests)
            .HasForeignKey(d => d.VehicleId)
            .HasConstraintName("leasing_requests_vehicle_id_fkey");
    }
}