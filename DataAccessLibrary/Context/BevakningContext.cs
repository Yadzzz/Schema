using System;
using System.Collections.Generic;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Context;

public partial class BevakningContext : DbContext
{
    public BevakningContext()
    {
    }

    public BevakningContext(DbContextOptions<BevakningContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Availability> Availabilities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Data Source=SQL8005.site4now.net;Initial Catalog=db_a97d46_vast;User Id=db_a97d46_vast_admin;Password=vast123vast");
    //=> optionsBuilder.UseSqlServer("Data Source=LT-KBA-YAD;Initial Catalog=Bevakning;User ID=yad;Password=123;TrustServerCertificate=True;");
    => optionsBuilder.UseSqlServer("Data Source=YAD;Initial Catalog=Bevakning;User ID=yad;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedule");

            entity.Property(e => e.DateEnd).HasColumnType("datetime");
            entity.Property(e => e.DateStart).HasColumnType("datetime");
            entity.Property(e => e.TimeStart).HasMaxLength(100);
            entity.Property(e => e.TimeEnd).HasMaxLength(100);
            entity.Property(e => e.JobType).HasMaxLength(100);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Active).HasColumnType("bit");
            entity.Property(e => e.ForordnandeDate).HasMaxLength(100);
        });

        modelBuilder.Entity<Availability>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Availability");

            entity.Property(e => e.UserId).HasColumnType("int");
            entity.Property(e => e.DateStart).HasColumnType("datetime");
            entity.Property(e => e.DateEnd).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
