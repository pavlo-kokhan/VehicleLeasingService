using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class LeasingInterestRateEntityTypeConfiguration : IEntityTypeConfiguration<LeasingInterestRate>
{
    public void Configure(EntityTypeBuilder<LeasingInterestRate> builder)
    {
        builder.HasKey(e => e.Id).HasName("leasing_interest_rates_pkey");

        builder.ToTable("leasing_interest_rates");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.InterestRate)
            .HasPrecision(5, 4)
            .HasColumnName("interest_rate");
        builder.Property(e => e.MaxAdvancePercent).HasColumnName("max_advance_percent");
        builder.Property(e => e.MaxMonths).HasColumnName("max_months");
        builder.Property(e => e.MinAdvancePercent).HasColumnName("min_advance_percent");
        builder.Property(e => e.MinMonths).HasColumnName("min_months");
    }
}