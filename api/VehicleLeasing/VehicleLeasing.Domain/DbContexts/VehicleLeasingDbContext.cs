using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.DbContexts;

public partial class VehicleLeasingDbContext : DbContext
{
    public VehicleLeasingDbContext()
    {
    }

    public VehicleLeasingDbContext(DbContextOptions<VehicleLeasingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }

    public virtual DbSet<LeasingInterestRate> LeasingInterestRates { get; set; }

    public virtual DbSet<LeasingRequest> LeasingRequests { get; set; }

    public virtual DbSet<LeasingRequestStatus> LeasingRequestStatuses { get; set; }

    public virtual DbSet<LeasingRequestsModificationLog> LeasingRequestsModificationLogs { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleCategory> VehicleCategories { get; set; }

    public virtual DbSet<VehicleFuelType> VehicleFuelTypes { get; set; }

    public virtual DbSet<VehicleStatus> VehicleStatuses { get; set; }

    public virtual DbSet<VehicleTransmission> VehicleTransmissions { get; set; }

    public virtual DbSet<VehiclesModificationLog> VehiclesModificationLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Username=postgres;Password=1234;Host=localhost;Port=5433;Database=vehicle_leasing_db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExchangeRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("exchange_rates_pkey");

            entity.ToTable("exchange_rates");

            entity.HasIndex(e => new { e.CurrencyCode, e.Date }, "uq_currency_date").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .HasColumnName("currency_code");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Rate)
                .HasPrecision(10, 4)
                .HasColumnName("rate");
        });

        modelBuilder.Entity<LeasingInterestRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("leasing_interest_rates_pkey");

            entity.ToTable("leasing_interest_rates");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InterestRate)
                .HasPrecision(5, 4)
                .HasColumnName("interest_rate");
            entity.Property(e => e.MaxAdvancePercent).HasColumnName("max_advance_percent");
            entity.Property(e => e.MaxMonths).HasColumnName("max_months");
            entity.Property(e => e.MinAdvancePercent).HasColumnName("min_advance_percent");
            entity.Property(e => e.MinMonths).HasColumnName("min_months");
        });

        modelBuilder.Entity<LeasingRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("leasing_requests_pkey");

            entity.ToTable("leasing_requests");

            entity.HasIndex(e => e.Date, "leasing_request_date_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.FixedPrice)
                .HasPrecision(15, 2)
                .HasColumnName("fixed_price");
            entity.Property(e => e.LastModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_modified");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Status).WithMany(p => p.LeasingRequests)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_status_id");

            entity.HasOne(d => d.User).WithMany(p => p.LeasingRequests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("leasing_requests_user_id_fkey");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.LeasingRequests)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("leasing_requests_vehicle_id_fkey");
        });

        modelBuilder.Entity<LeasingRequestStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("leasing_request_statuses_pkey");

            entity.ToTable("leasing_request_statuses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<LeasingRequestsModificationLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("leasing_requests_modification_log_pkey");

            entity.ToTable("leasing_requests_modification_log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LeasingRequestId).HasColumnName("leasing_request_id");
            entity.Property(e => e.Operation)
                .HasMaxLength(50)
                .HasColumnName("operation");
            entity.Property(e => e.Time)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time");

            entity.HasOne(d => d.LeasingRequest).WithMany(p => p.LeasingRequestsModificationLogs)
                .HasForeignKey(d => d.LeasingRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("leasing_requests_modification_log_leasing_request_id_fkey");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
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
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_index");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehicles_pkey");

            entity.ToTable("vehicles");

            entity.HasIndex(e => e.Brand, "vehicle_brand_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .HasColumnName("brand");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.EstimatedPrice)
                .HasPrecision(15, 2)
                .HasColumnName("estimated_price");
            entity.Property(e => e.FuelTypeId).HasColumnName("fuel_type_id");
            entity.Property(e => e.LastModified)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_modified");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasColumnName("model");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TransmissionId).HasColumnName("transmission_id");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Category).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehicles_category_id_fkey");

            entity.HasOne(d => d.FuelType).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.FuelTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehicles_fuel_type_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehicles_status_id_fkey");

            entity.HasOne(d => d.Transmission).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.TransmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehicles_transmission_id_fkey");
        });

        modelBuilder.Entity<VehicleCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehicle_categories_pkey");

            entity.ToTable("vehicle_categories");

            entity.HasIndex(e => e.Category, "vehicle_categories_category_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
        });

        modelBuilder.Entity<VehicleFuelType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("fuel_types_pkey");

            entity.ToTable("vehicle_fuel_types");

            entity.HasIndex(e => e.Type, "fuel_types_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('fuel_types_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<VehicleStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehicle_statuses_pkey");

            entity.ToTable("vehicle_statuses");

            entity.HasIndex(e => e.Status, "vehicle_statuses_status_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<VehicleTransmission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transmissions_pkey");

            entity.ToTable("vehicle_transmissions");

            entity.HasIndex(e => e.Transmission, "transmissions_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('transmissions_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Transmission)
                .HasMaxLength(50)
                .HasColumnName("transmission");
        });

        modelBuilder.Entity<VehiclesModificationLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehicles_modification_log_pkey");

            entity.ToTable("vehicles_modification_log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Operation)
                .HasMaxLength(50)
                .HasColumnName("operation");
            entity.Property(e => e.Time)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehiclesModificationLogs)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehicles_modification_log_vehicle_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
