using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebAuth.Models;

namespace Web_Auth.Data.Contexts
{
    public partial class SecondDbContext : DbContext
    {
        public SecondDbContext()
        {
        }

        public SecondDbContext(DbContextOptions<SecondDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblDeparment> TblDeparments { get; set; } = null!;
        public virtual DbSet<TblEmployee> TblEmployees { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=NAIRY;Database=API_CF_Demo;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblDeparment>(entity =>
            {
                entity.ToTable("tblDeparment");
            });

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("tblEmployee");

                entity.HasIndex(e => e.DepartmentId, "IX_tblEmployee_DepartmentId");

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TblEmployees)
                    .HasForeignKey(d => d.DepartmentId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
