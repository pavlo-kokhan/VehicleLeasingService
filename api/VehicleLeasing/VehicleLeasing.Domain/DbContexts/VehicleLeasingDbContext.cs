using Microsoft.EntityFrameworkCore;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.DataAccess.DbContexts;

public class VehicleLeasingDbContext : DbContext
{
    public VehicleLeasingDbContext() { }

    public VehicleLeasingDbContext(DbContextOptions<VehicleLeasingDbContext> options)
        : base(options) { }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VehicleLeasingDbContext).Assembly);
    }
}
