using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Infrastructure.Persistence
{
    // DbContext configuration
    public class OutOfOfficeDbContext : IdentityDbContext
    {
        // Db sets
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        public DbSet<Project> Projects { get; set; }

        public OutOfOfficeDbContext(DbContextOptions<OutOfOfficeDbContext> options) : base(options)
        {
            
        }

        // Configure relationships and model properties
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(eb =>
            {
                eb.HasOne(e => e.PeoplePartner)
                .WithMany()
                .HasForeignKey(e => e.PeoplePartnerId)
                .OnDelete(DeleteBehavior.Restrict);

                eb.Property(e => e.FullName)
                .HasColumnType("varchar(50)")
                .IsRequired();

                eb.HasIndex(e => e.Email)
                .IsUnique();

                eb.Property(e => e.Email)
                .IsRequired();

                eb.Property(e => e.Subdivision)
                .HasColumnType("varchar(50)")
                .IsRequired();

                eb.Property(e => e.Position)
                .HasColumnType("varchar(50)")
                .IsRequired();

                eb.Property(e => e.Status)
                .IsRequired();

                eb.Property(e => e.OutOfOfficeBalance)
                .IsRequired();
            });

            modelBuilder.Entity<LeaveRequest>(eb =>
            {
                eb.HasOne(l => l.Employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(l => l.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

                eb.Property(l => l.AbsenceReason)
                .HasColumnType("varchar(50)")
                .IsRequired();

                eb.Property(l => l.StartDate)
                .IsRequired();

                eb.Property(l => l.EndDate)
                .IsRequired();

                eb.Property(l => l.Status)
                .HasColumnType("varchar(25)")
                .IsRequired();

                eb.Property(l => l.Comment)
                .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<ApprovalRequest>(eb =>
            {
                eb.HasOne(a => a.Approver)
                .WithOne(e => e.ApprovalRequest)
                .HasForeignKey<ApprovalRequest>(a => a.ApproverId)
                .OnDelete(DeleteBehavior.NoAction);

                eb.HasOne(a => a.LeaveRequest)
                .WithOne(l => l.ApprovalRequest)
                .HasForeignKey<ApprovalRequest>(a => a.LeaveRequestId);

                eb.Property(a => a.Status)
                .HasColumnType("varchar(25)")
                .IsRequired();

                eb.Property(a => a.Comment)
                .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Project>(eb =>
            {
                eb.Property(p => p.ProjectType)
                .HasColumnType("varchar(25)")
                .IsRequired();

                eb.Property(p => p.StartDate)
                .IsRequired();

                eb.Property(p => p.Status)
                .HasColumnType("varchar(25)")
                .IsRequired();

                eb.Property(p => p.Comment)
                .HasColumnType("varchar(100)");
            });
        }
    }
}
