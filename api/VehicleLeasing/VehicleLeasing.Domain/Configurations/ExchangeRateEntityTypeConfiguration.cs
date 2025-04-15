using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.Configurations;

public class ExchangeRateEntityTypeConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasKey(e => e.Id).HasName("exchange_rates_pkey");

        builder.ToTable("exchange_rates");

        builder.HasIndex(e => new { e.CurrencyCode, e.Date }, "uq_currency_date").IsUnique();

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.CurrencyCode)
            .HasMaxLength(3)
            .HasColumnName("currency_code");
        builder.Property(e => e.Date).HasColumnName("date");
        builder.Property(e => e.Rate)
            .HasPrecision(10, 4)
            .HasColumnName("rate");
    }
}