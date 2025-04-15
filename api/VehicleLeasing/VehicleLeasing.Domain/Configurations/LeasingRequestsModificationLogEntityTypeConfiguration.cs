using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class LeasingRequestsModificationLogEntityTypeConfiguration : IEntityTypeConfiguration<LeasingRequestsModificationLog>
{
    public void Configure(EntityTypeBuilder<LeasingRequestsModificationLog> builder)
    {
        builder.HasKey(e => e.Id).HasName("leasing_requests_modification_log_pkey");

        builder.ToTable("leasing_requests_modification_log");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.LeasingRequestId).HasColumnName("leasing_request_id");
        builder.Property(e => e.Operation)
            .HasMaxLength(50)
            .HasColumnName("operation");
        builder.Property(e => e.Time)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("time");

        builder.HasOne(d => d.LeasingRequest).WithMany(p => p.LeasingRequestsModificationLogs)
            .HasForeignKey(d => d.LeasingRequestId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("leasing_requests_modification_log_leasing_request_id_fkey");
    }
}