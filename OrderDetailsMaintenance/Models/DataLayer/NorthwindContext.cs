using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace OrderDetailsMaintenance.Models.DataLayer;

public partial class NorthwindContext : DbContext
{
    // Opeyemi Obute
    public NorthwindContext()
    {
    }

    // Opeyemi Obute
    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    // Opeyemi Obute
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = ConfigurationManager
                .ConnectionStrings["Northwind"]?.ConnectionString
                ?? throw new ConfigurationErrorsException(
                    "The Northwind connection string is missing from App.config.");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    // Opeyemi Obute
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerId).IsFixedLength();
        });
        OnModelCreatingPartial(modelBuilder);
    }

    // Opeyemi Obute
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
