using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkBuses.Models;

public partial class CourseWorkBusesContext : DbContext
{
    public CourseWorkBusesContext()
    {
    }

    public CourseWorkBusesContext(DbContextOptions<CourseWorkBusesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bus> Buses { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Repair> Repairs { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress; Database=CourseWorkBuses; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.BusId).HasName("PK__Buses__6A0F6095209D4F35");

            entity.Property(e => e.BusId).HasColumnName("BusID");
            entity.Property(e => e.BusBrand).HasMaxLength(100);
            entity.Property(e => e.BusLicensePlate).HasMaxLength(20);
            entity.Property(e => e.BusModel).HasMaxLength(100);
            entity.Property(e => e.BusStatus).HasMaxLength(50);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A0412B0FCF3");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientContacts).HasMaxLength(255);
            entity.Property(e => e.ClientFirstName).HasMaxLength(100);
            entity.Property(e => e.ClientLastName).HasMaxLength(100);
            entity.Property(e => e.ClientMiddleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flights__8A9E148E0B420038");

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.ArrivalPoint).HasMaxLength(100);
            entity.Property(e => e.BusId).HasColumnName("BusID");
            entity.Property(e => e.DeparturePoint).HasMaxLength(100);

            entity.HasOne(d => d.Bus).WithMany(p => p.Flights)
                .HasForeignKey(d => d.BusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Flights__BusID__286302EC");
        });

        modelBuilder.Entity<Repair>(entity =>
        {
            entity.HasKey(e => e.RepairId).HasName("PK__Repair__07D0BDCDE74A6B20");

            entity.ToTable("Repair");

            entity.Property(e => e.RepairId).HasColumnName("RepairID");
            entity.Property(e => e.BusId).HasColumnName("BusID");
            entity.Property(e => e.RepairStatus).HasMaxLength(100);

            entity.HasOne(d => d.Bus).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.BusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Repair__BusID__300424B4");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__712CC627FA163EEA");

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.TicketPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TicketStatus).HasMaxLength(50);
            entity.Property(e => e.TicketType).HasMaxLength(50);

            entity.HasOne(d => d.Flight).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__FlightI__2B3F6F97");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("PK__Workers__077C8806A0F09105");

            entity.Property(e => e.WorkerId).HasColumnName("WorkerID");
            entity.Property(e => e.WorkerContacts).HasMaxLength(255);
            entity.Property(e => e.WorkerFirstName).HasMaxLength(100);
            entity.Property(e => e.WorkerLastName).HasMaxLength(100);
            entity.Property(e => e.WorkerMiddleName).HasMaxLength(100);
            entity.Property(e => e.WorkerPosition).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
