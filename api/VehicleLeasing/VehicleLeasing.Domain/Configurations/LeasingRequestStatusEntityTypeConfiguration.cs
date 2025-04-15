using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class LeasingRequestStatusEntityTypeConfiguration : IEntityTypeConfiguration<LeasingRequestStatus>
{
    public void Configure(EntityTypeBuilder<LeasingRequestStatus> builder)
    {
        builder.HasKey(e => e.Id).HasName("leasing_request_statuses_pkey");

        builder.ToTable("leasing_request_statuses");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .HasColumnName("status");
    }
}